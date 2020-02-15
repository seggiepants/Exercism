# Globals for the bearings
# Change the values as you see fit
NORTH = 0
EAST = 1
SOUTH = 2
WEST = 3

direction_left = {NORTH: WEST, WEST: SOUTH, SOUTH: EAST, EAST: NORTH}
direction_right = {NORTH: EAST, EAST: SOUTH, SOUTH: WEST, WEST: NORTH}

class Robot(object):
    """Robot class loosely based on LOGO style turtle navigation."""

    def __init__(self, bearing=NORTH, x=0, y=0):
        """
        Initialize the robot class with a direction and position.
        Please not that the coordinate grid has NORTH be in the postive 
        Y direction, SOUTH in the negative Y direction, EAST in the positive
        X direction and WEST in the negative X direction.
        Parameters:
        bearing: Should be one of the constants NORTH, EAST, SOUTH, or WEST.
        x: x-coordinate of the robot location.
        y: y-coordinate of the robot location.
        """
        self.__x = x
        self.__y = y
        self.__bearing = bearing

    @property
    def coordinates(self):
        """The current position of the robot on the cartesian grid.
        Returns:
        Tuple of the x, and y coordinate.
        """
        return (self.__x, self.__y)
    
    @property
    def bearing(self):
        """The current direction the robot is facing. Should be one of the
        constants NORTH, EAST, SOUTH, or WEST
        Returns:
        One of the enumerated direction constants
        """
        return self.__bearing

    def advance(self):
        """Move the robot forward one step on the cartesian grid in the 
        direction it is currently facing.
        Exceptions:
        Throws and exception if the current bearing is invalid.
        """
        if self.__bearing == NORTH:
            self.__y += 1
        elif self.__bearing == EAST:
            self.__x += 1
        elif self.__bearing == SOUTH:
            self.__y -= 1
        elif self.__bearing == WEST:
            self.__x -= 1
        else:
            raise Exception("Bearing is set to an invalid bearing.")

    def turn_right(self):
        """Turn the robot right by 90 degrees"""
        self.__bearing = direction_right[self.__bearing]
    
    def turn_left(self):
        """Turn the robot left by 90 degrees"""
        self.__bearing = direction_left[self.__bearing]

    def simulate(self, instructions):
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
        for ch in instructions.strip().upper():
            if ch == 'L':
                self.turn_left()
            elif ch == 'R':
                self.turn_right()
            elif ch == 'A':
                self.advance()
            else:
                raise Exception(f'Invalid command "{ch}" found in instruction list.')
