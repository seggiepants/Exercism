<?php
// Robot Simulator Exercise

declare(strict_types=1);

// Robot Simulation class. Robot has a position on a grid and a facing cardinal direction.
class RobotSimulator
{
    private ?int $dir;  // Direction index
    private int $x;     // x-coordinate
    private int $y;     // y-coordinate

    // Turns a direction into it's text label. Keys match the self::$delta array and are ordered so they turn clockwise.
    private static $directions = array(
        0 => "north", 1 => "east", 2 => "south", 3 => "west",
    );

    // Delta X, Y for moving in a cardinal direction. Keys match the keys in the self::$directions array.
    private static $delta = array(
        0 => array(0, 1), 
        1 => array(1, 0), 
        2 => array(0, -1), 
        3 => array(-1, 0), 
    );

    // Construct a robot and place them in the given direction and at the given position.
    // @param int[] $position: Where the robot should be located on an x, y grid.
    // @param string $direction: The direction the robot is facing "north", "south", "east", or "west".
    public function __construct(array $position, string $direction)
    {
        $this->dir = array_find_key(self::$directions, function(string $value) use ($direction)  {
            return strcmp($direction, $value) == 0;
        });
        
        $this->x = $position[0];
        $this->y = $position[1];

        if ($this->dir === NULL) {
            throw new InvalidArgumentException("Invalid direction, only north, south, east, or west are accepted.");
            $this->dir = "";
        }
    }

    // Follow a list of instructions for a robot.
    // @param string $instructions: The instructions for the robot to follow only A, R, and L are allowed they are for _A_dvance, _R_ight 90 degrees, and _L_eft 90 degrees.
    public function instructions(string $instructions): void
    {
        foreach(str_split($instructions) as $instruction) {
            switch($instruction) {
                case "R":
                    $this->dir = ($this->dir + 1) % 4;
                    break;
                case "L":
                    $this->dir = ($this->dir + 3) % 4;
                    break;
                case "A":
                    if ($this->dir !== NULL) {
                        $this->x += self::$delta[$this->dir][0];
                        $this->y += self::$delta[$this->dir][1];
                    }
                    break;
                default:
                    throw new InvalidArgumentException("Invalid command: \"" . $instruction . "\".");
            }
        }
    }

    // Get the position of the robot.
    // @return int[]: The x and y coordinate of the robot on the grid as a array of size two.
    public function getPosition(): array
    {
        return [$this->x, $this->y];
    }

    // Get the direction of the robot.
    // @return string: Return the direction the robot is facing "north", 
    // "south", "east", or "west". Null if returned if the current 
    // direction is invalid.
    public function getDirection(): string
    {
        if ($this->dir === NULL) {
            return "";
        }
        return self::$directions[$this->dir];
    }
}
