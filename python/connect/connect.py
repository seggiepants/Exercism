"""
Connect Game. Determine if the board the object is initalized (represents a hex grid
so jagged left edge indenting along a diagonal and rows with spaces between values and 
offset between rows making a diamond pattern)
Example: 
. O . X .
 . X X O .
  O O O X .
   . X O X O
    X O O O X
"""

class ConnectGame:
    """
    Class to determine if there is a winner on a Connect Game board.
    """
    def __init__(self, board):
        """
        Initialize the ConnectGame state.
        :param board: The current state of the board a multi-line string
        """
        self.board = [' ' * index + row.strip() for index, row in enumerate(board.split('\n'))]

    def get_winner(self):
        """
        Determine if X or O has won the game for the object's game board state.
        :returns: X if won by X, O if won by O, and an empty string if no winner (yet).
        """
        candidate_o = [index for index, value in enumerate(self.board[0]) if value == 'O']
        for candidate in candidate_o:
            if self.trace(candidate, 0, 'O', []):
                return 'O'
            
        candidate_x = [index for index, row in enumerate(self.board) if row[index] == 'X']
        for candidate in candidate_x:
            if self.trace(candidate, candidate, 'X', []):
                return 'X'

        return ''
    
    def trace(self, coord_x, coord_y, player, visited):
        """
        Trace out the possible paths available for a given player, starting position
        and a list of previously visited nodes so we don't go in circles.
        :param coord_x: x-coordinate of location on the board.
        :param coord_y: y-coordinate of location on the board.
        :param player: X or O X wins if touches right hand side, O if it touches the bottom
        :param visited: List of visited locations so I don't go around in loops infinitely.
        :returns: True if a path was found (false otherwise)."""
        if (coord_x, coord_y) in visited:
            return False
        
        visited.append((coord_x, coord_y))

        if coord_y < 0 or coord_y >= len(self.board): 
            return False # Out of range on Y axis
        
        if coord_x < 0 or coord_x >= len(self.board[coord_y]):
            return False # Out of range on X axis
        
        if self.board[coord_y][coord_x] != player:
            return False # We are not on an matching square
        
        if player == 'O' and coord_y == len(self.board) - 1:
            return True # made it to the bottom
        
        if player == 'X' and coord_x == len(self.board[coord_y]) - 1:
            return True # made it to the right side

        # return true if any of the six hex positions finds the desired goal (recursively)    
        return self.trace(coord_x - 1, coord_y + 1, player, visited) or \
            self.trace(coord_x + 1, coord_y + 1, player, visited) or \
            self.trace(coord_x - 1, coord_y - 1, player, visited) or \
            self.trace(coord_x + 1, coord_y - 1, player, visited) or \
            self.trace(coord_x + 2, coord_y, player, visited) or \
            self.trace(coord_x - 2, coord_y, player, visited)
        
    