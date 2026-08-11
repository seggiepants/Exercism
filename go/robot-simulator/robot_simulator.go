// Robot simulator exercise - a lot harder than on other language tracks.
package robot

import (
	"fmt"
	"slices"
)

// See defs.go for other definitions

// Step 1
// Define N, E, S, W here.
const (
	N Dir = iota
	E
	S
	W
	DIR_COUNT
)

var DeltaX = map[Dir]int{
	N: 0, S: 0, E: 1, W: -1,
}

var DeltaY = map[Dir]int{
	N: 1, S: -1, E: 0, W: 0,
}

// Rotate the robot clockwise
func Right() {
	Step1Robot.Dir = (Step1Robot.Dir + 1) % DIR_COUNT
}

// Rotate the robot counter clockwise
func Left() {
	Step1Robot.Dir = (Step1Robot.Dir + DIR_COUNT - 1) % DIR_COUNT
}

// Advance the step 1 robot in current direction
func Advance() {
	Step1Robot.X += DeltaX[Step1Robot.Dir]
	Step1Robot.Y += DeltaY[Step1Robot.Dir]
}

func (d Dir) String() string {
	switch d {
	case N:
		return "North"
	case S:
		return "South"
	case E:
		return "East"
	case W:
		return "West"
	}
	return "Unknown"
}

// Step 2
// Define Action type here.
type Action rune

// Start up a robot that recieves command and then sends them to the room.
// @param command: Channel to receive actions on
// @param action: Channel to send actions to (the room)
func StartRobot(command chan Command, action chan Action) {
	for true {
		cmd, ok := <-command
		if !ok {
			break
		}
		action <- Action(cmd)
	}
	action <- Action('X')
}

// Process robot actions happening within a room (rectangle)
// @param extent: Area the actions should remain within the bounds of.
// @param robot: Step2Robot object.
// @param action: channel that actions are received on.
// @param report: Send the robot status back to this channel upon completion.
func Room(extent Rect, robot Step2Robot, action chan Action, report chan Step2Robot) {
	var running bool = true
	for running {
		step, ok := <-action
		if !ok {
			break
		}
		switch step {
		case 'L':
			robot.Dir = (robot.Dir + DIR_COUNT - 1) % DIR_COUNT
		case 'R':
			robot.Dir = (robot.Dir + 1) % DIR_COUNT
		case 'A':
			var dx RU = RU(DeltaX[robot.Dir])
			var dy RU = RU(DeltaY[robot.Dir])
			if robot.Easting+dx <= extent.Max.Easting && robot.Easting+dx >= extent.Min.Easting {
				robot.Easting += dx
			}
			if robot.Northing+dy <= extent.Max.Northing && robot.Northing+dy >= extent.Min.Northing {
				robot.Northing += dy
			}
		case 'X':
			running = false
		}
	}
	report <- robot

}

// Check to see if a position is within the given rect
// @param extent: Rectangle to check against
// @param pos: Position to check if it is in the rectangle or not.
// @returns: bool, true if the pos is within the bounds of the rectangle.
func InRect(extent Rect, pos Pos) bool {
	if pos.Easting > extent.Max.Easting || pos.Easting < extent.Min.Easting ||
		pos.Northing > extent.Max.Northing || pos.Northing < extent.Min.Northing {
		return false
	}
	return true
}

// Step 3
// Define Action3 type here.
type Action3 struct {
	Name    string
	Command rune
}

// Start up a robot with the given action script.
// @param name: Robot name
// @param script: One character per action A, R, and L are the only accepted actions
// @param action: Send the actions one by one to this channel.
// @param log: Log errors here
func StartRobot3(name, script string, action chan Action3, log chan string) {
	if len(name) == 0 {
		log <- "Attempt to start a robot with no name"
	}

	for _, ch := range script {
		if ch != 'R' && ch != 'L' && ch != 'A' {
			log <- fmt.Sprintf("Invalid command %c\n", ch)
			break
		}
		action <- Action3{Name: name, Command: ch}
	}
	action <- Action3{Name: name, Command: 'X'}
	//close(action)
}

// Processed channel actions from the robots making sure things stay consistent and in bounds.
// @param extent: The area robots are allowed to stay in.
// @param robots: Slice of Step3Robot robots
// @param action: Channel to listen to for robot action3 actions
// @param rep: Report the state of the robots after processing
// @param log: Log errors to this channel
func Room3(extent Rect, robots []Step3Robot, action chan Action3, rep chan []Step3Robot, log chan string) {
	robotNames := make(map[string]int, 0)
	robotPos := make(map[Pos]int, 0)
	for _, robot := range robots {
		robotNames[robot.Name]++
		robotPos[robot.Pos]++
	}
	for name, count := range robotNames {
		if count > 1 {
			log <- fmt.Sprintf("There are %d robots with the name \"%s\".", count, name)
		}
	}
	for pos, count := range robotPos {
		if count > 1 {
			log <- fmt.Sprintf("There are %d robots with the position [%d, %d].", count, int(pos.Easting), int(pos.Northing))
		}
		if !InRect(extent, pos) {
			log <- fmt.Sprintf("There are %d robot(s) out of bounds at position [%d, %d].", count, int(pos.Easting), int(pos.Northing))
		}
	}

	remaining := len(robots)
	for remaining > 0 {
		step, ok := <-action
		if !ok {
			break
		}
		index := slices.IndexFunc(robots, func(r Step3Robot) bool { return r.Name == step.Name })
		if index < 0 {
			log <- fmt.Sprintf("Unknown robot: \"%s\"", step.Name)
			break
		} else {
			robot := &robots[index]
			switch step.Command {
			case 'L':
				robot.Dir = (robot.Dir + DIR_COUNT - 1) % DIR_COUNT
			case 'R':
				robot.Dir = (robot.Dir + 1) % DIR_COUNT
			case 'A':
				var dx RU = RU(DeltaX[robot.Dir])
				var dy RU = RU(DeltaY[robot.Dir])
				var nextX = robot.Easting + dx
				var nextY = robot.Northing + dy
				var err bool = false
				for _, robotCompare := range robots {
					if robotCompare.Name != robot.Name {
						if robotCompare.Pos.Easting == nextX && robotCompare.Pos.Northing == nextY {
							log <- fmt.Sprintf("Command would put robots %s and %s at the same location [%d, %d] -- FAILED.", robot.Name, robotCompare.Name, nextX, nextY)
							err = true
							break
						}
					}
				}
				if !err {
					if nextX <= extent.Max.Easting && nextX >= extent.Min.Easting && nextY <= extent.Max.Northing && nextY >= extent.Min.Northing {
						robot.Easting = nextX
						robot.Northing = nextY
					} else {
						log <- fmt.Sprintf("Robot \"%s\" bumped into the wall.", robot.Name)
					}
				}
			case 'X':
				remaining--
			}
		}

	}

	rep <- robots
}
