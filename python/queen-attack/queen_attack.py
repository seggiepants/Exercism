"""
Check to see if two queens can attack one another
"""

class Queen:
    """
    Queen on a chessboard
    """
    def __init__(self, row, column):
        """
        Create a new queeen object saving its location on the board
        into the .row and .column properties
        """
        self.check_position(row, column)
        self.row = row
        self.column = column

    def can_attack(self, another_queen):
        """
        Can this queen and another_queen attack one another
        :param: another_queen. The queen to compare our queen's position against
        :returns: True is they can attack each other false otherwise
        :raises: ValueError for an two queens on the same square
        """
        if self.row == another_queen.row and self.column == another_queen.column:
            raise ValueError('Invalid queen position: both queens in the same square')
        
        if self.row == another_queen.row:
            return True
        
        if self.column == another_queen.column:
            return True
        
        delta_x = abs(self.row - another_queen.row)
        delta_y = abs(self.column - another_queen.column)

        if delta_x > 0 and delta_x == delta_y:
            return True
        
        return False

    @staticmethod    
    def check_position(row, column):
        """
        Run some sanity checks on the position to see if it a valid spot on a 8x8 chessboard (zero indexed)
        :param row: Which row the position is in
        :param column: Which column the position is at
        :raises: ValueError if outside the board
        """
        if row < 0:
            raise ValueError('row not positive')
        if row >= 8:
            raise ValueError('row not on board')
        
        if column < 0:
            raise ValueError('column not positive')
        
        if column >= 8:
            raise ValueError('column not on board')
