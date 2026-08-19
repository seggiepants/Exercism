package bowling

import "errors"

// Define the Game type here.
type Game struct {
	rolls map[int][]int // records frames and their scores.
	frame int           // current frame
}

// Create a new bowling game object and set it up for a new game.
func NewGame() *Game {
	game := Game{
		rolls: make(map[int][]int),
		frame: 1,
	}
	for i := 1; i <= 10; i++ {
		game.rolls[i] = make([]int, 0)
	}
	return &game
}

// Record a roll for a frame in a game of bowling
// @param pins: How many pins were knocked down (0-10)
// @raises: Error if too many/too few pins, too many pins in a set or too many rolls in a frame. Nil on success
func (g *Game) Roll(pins int) error {
	if pins < 0 || pins > 10 {
		return errors.New("Invalid pin count.")
	}
	frameSum := g.FrameSum(g.frame)
	maxThrows := max(1, len(g.rolls[g.frame]))
	if g.frame < 10 && frameSum < 10 {
		maxThrows = 2
	} else if g.frame == 10 {
		if len(g.rolls[g.frame]) == 1 && g.rolls[g.frame][0] <= 10 {
			maxThrows = 2
		}
		if len(g.rolls[g.frame]) == 2 {
			if g.rolls[g.frame][0] == 10 || g.rolls[g.frame][0]+g.rolls[g.frame][1] == 10 {
				maxThrows = 3 // One extra throw for a spare/turkey
			}
		}
	}

	// Move to next frame if previous is full.
	if len(g.rolls[g.frame]) > maxThrows || g.frame > 10 {
		return errors.New("Too many rolls")
	}
	if len(g.rolls[g.frame]) == maxThrows {
		g.frame++
	}

	if g.frame > 10 {
		return errors.New("Too many rolls")
	}

	g.rolls[g.frame] = append(g.rolls[g.frame], pins)

	total := 0
	for i := 0; i < len(g.rolls[g.frame]); i++ {
		total += g.rolls[g.frame][i]
		if (total > 10 && g.frame < 10) ||
			(total > 30 && g.frame >= 10) ||
			(g.frame == 10 && len(g.rolls[g.frame]) == 3 && g.rolls[g.frame][0] == 10 && g.rolls[g.frame][1] != 10 && i >= 2 && total > 20 && total < 30) {
			return errors.New("Too many pins in a set.")
		}
	}
	return nil
}

// Get the sum of pins hit for a given frame.
// @param frame: The frame to sum (1-10)
// @returns: The number of pins knocked down - doesn't do the lookahead.
func (g *Game) FrameSum(frame int) int {
	total := 0
	for i := 0; i < len(g.rolls[frame]); i++ {
		total += g.rolls[frame][i]
	}
	return total
}

// Calculate how many extra pins to add for the lookahead given by a strike or spare.
// Don't call this for throws on the 10th frame.
// @param frame: Which frame to look at.
// @param throw: The nth Throw of a frame.
// @param count: How many steps of look ahead (1 = spare, 2 = strike)
// @returns: How many extra pins the player gets for that frame & throw.
func (g *Game) PeekAhead(frame int, throw int, count int) int {
	result := 0
	iterations := 0
	pins, ok := g.rolls[frame]
	if !ok {
		return result
	}
	throw++

	for iterations < count {
		if throw >= len(pins) {
			frame++
			throw = 0
		}
		pins, ok = g.rolls[frame]
		if !ok {
			return result
		}
		result += pins[throw]
		throw++
		iterations++
	}

	return result
}

// Calculate the score for a game of bowling
// @returns: Error if we are calculating too early, otherwise the total score.
func (g *Game) Score() (int, error) {
	frameSum := g.FrameSum(g.frame)
	// Too early to quit or spare on 10th frame, or going for the turkey
	if g.frame < 10 ||
		(g.frame == 10 && len(g.rolls[10]) <= 1) ||
		(g.frame == 10 && len(g.rolls[10]) == 2 && g.rolls[10][1] != 0 && frameSum == 10) ||
		(g.frame == 10 && len(g.rolls[10]) == 2 && frameSum == 20) {
		return 0, errors.New("Game is not complete.")
	}
	total := 0
	for key, pins := range g.rolls {
		subtotal := 0
		for i := 0; i < len(pins); i++ {
			subtotal += pins[i]
			if key < 10 && subtotal == 10 && i == 0 {
				subtotal += g.PeekAhead(key, i, 2)
			} else if key < 10 && subtotal == 10 && i == 1 {
				subtotal += g.PeekAhead(key, i, 1)
			}
		}
		total += subtotal
	}
	return total, nil
}
