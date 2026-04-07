"""
Simulate a robot that can be given commands to move around a cartesian grid.
It has a position and direction. It can turn left, or right, and advance.
"""

# Globals for the directions
# Change the values as you see fit
NORTH = 0
EAST = 1
SOUTH = 2
WEST = 3

direction_left = {NORTH: WEST, WEST: SOUTH, SOUTH: EAST, EAST: NORTH}
direction_right = {NORTH: EAST, EAST: SOUTH, SOUTH: WEST, WEST: NORTH}

class Robot:
    """Robot class loosely based on LOGO style turtle navigation."""

    def __init__(self, direction=NORTH, x_coordinate=0, y_coordinate=0):
        """
        Initialize the robot class with a direction and position.
        Please not that the coordinate grid has NORTH be in the postive 
        Y direction, SOUTH in the negative Y direction, EAST in the positive
        X direction and WEST in the negative X direction.
        Parameters:
        direction: Should be one of the constants NORTH, EAST, SOUTH, or WEST.
        x: x-coordinate of the robot location.
        y: y-coordinate of the robot location.
        """
        self.__x = x_coordinate
        self.__y = y_coordinate
        self.__direction = direction

    @property
    def coordinates(self):
        """The current position of the robot on the cartesian grid.
        Returns:
        Tuple of the x, and y coordinate.
        """
        return (self.__x, self.__y)
    
    @property
    def direction(self):
        """The current direction the robot is facing. Should be one of the
        constants NORTH, EAST, SOUTH, or WEST
        Returns:
        One of the enumerated direction constants
        """
        return self.__direction

    def advance(self):
        """Move the robot forward one step on the cartesian grid in the 
        direction it is currently facing.
        Exceptions:
        Throws and exception if the current direction is invalid.
        """
        if self.__direction == NORTH:
            self.__y += 1
        elif self.__direction == EAST:
            self.__x += 1
        elif self.__direction == SOUTH:
            self.__y -= 1
        elif self.__direction == WEST:
            self.__x -= 1
        else:
            raise Exception('Direction is set to an invalid direction.')

    def turn_right(self):
        """Turn the robot right by 90 degrees"""
        self.__direction = direction_right[self.__direction]
    
    def turn_left(self):
        """Turn the robot left by 90 degrees"""
        self.__direction = direction_left[self.__direction]

    def move(self, instructions):
        """Move the robot according to the supplied string of commands.
        Each command is one character in the string. Allowed characters are:
        A - Advance
        L - Turn Left 90 degrees.
        R - Turn Right 90 degrees.
        Parameters:
        instructions - command string. Case insensitive, leading and trailing
        whitespace will be ignored.
        Exceptions:
        Throws and error if it finds an invalid command.
        """
        for instruction in instructions.strip().upper():
            if instruction == 'L':
                self.turn_left()
            elif instruction == 'R':
                self.turn_right()
            elif instruction == 'A':
                self.advance()
            else:
                raise Exception(f'Invalid command "{instruction}" found in instruction list.')
