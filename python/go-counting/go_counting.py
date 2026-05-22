"""
Compute territories on the board of a game of Go
using the Board class.
"""

BLACK = 'B'
WHITE = 'W'
NONE = ' '

neighbors = [(0, -1), (-1, 0), (1, 0), (0, 1)]

class Board:
    """
    Count territories of each player in a Go game

    Args:
        board (list[str]): A two-dimensional Go board
    """

    def __init__(self, board):
        """
        Initialize the class by saving the board parameter
        :param board: A list of strings describing the board where individual cells are 
        B, W, or a space.
        """
        self.board = board
        pass

    def territory(self, x, y):
        """
        Find the owner and the territories given a coordinate on the board
        :param x: (int): Column on the board
        :param y: (int): Row on the board
        :Returns: (str, set): A tuple, the first element being the owner
        of that area.  One of "W", "B", " ".  The second being a set of 
        coordinates, representing the owner's territories.
        """
        if x < 0 or y < 0 or y >= len(self.board) or x >= len(self.board[y]):
            raise ValueError('Invalid coordinate')
        results = {
            'stone': [],
            'territory': [],
        }
        self.flood_fill(x, y, results)
        return results['stone'][0] if len(results['stone']) == 1 and len(results['territory']) > 0 else NONE, set(results['territory'])

    def territories(self):
        """
        Find the owners and the territories of the whole board
        :Returns: dict(str, set): A dictionary whose key being the owner,
        i.e. "W", "B", "".  The value being a set of coordinates owned by the owner.
        """
        territory_black = []
        territory_white = []
        territory_none = []
        
        for coord_y, row in enumerate(self.board):
            for coord_x, _ in enumerate(row):
                point = (coord_x, coord_y)
                if point not in territory_black and point not in territory_white and point not in territory_none:
                    result = {
                        'stone': [],
                        'territory': [],
                    }
                    self.flood_fill(coord_x, coord_y, result)
                    if len(result['stone']) == 1 and result['stone'][0] == BLACK:
                        territory_black.extend(result['territory'])
                    elif len(result['stone']) == 1 and result['stone'][0] == WHITE:
                        territory_white.extend(result['territory'])
                    else:
                        territory_none.extend(result['territory'])
        return {
            BLACK: set(territory_black),
            WHITE: set(territory_white),
            NONE: set(territory_none)
        }

    def flood_fill(self, x_coord, y_coord, results):
        """
        Get the cells in a region containing the point(x, y) bordered by stones
        :param x_coord: x-coordinate of the starting point
        :param y_coord: y-coordinate of the starting point
        :param results: Dictionary with two keys 'stone' a list of boundary stones found, 
        an 'territory' a list of x,y tuples with cells found in the region
        """
        cell = self.board[y_coord][x_coord]
        if cell in [BLACK, WHITE]:
            if cell not in results['stone']:
                results['stone'].append(cell)
            return
        if cell != ' ':
            raise ValueError('Unexpected cell value')
        point = (x_coord, y_coord)
        if point in results['territory']:
            return
        results['territory'].append(point)

        for delta in neighbors:
            neighbor_x = x_coord + delta[0]
            neighbor_y = y_coord + delta[1]

            if neighbor_y < 0 or neighbor_y >= len(self.board):
                continue
            if neighbor_x < 0 or neighbor_x >= len(self.board[neighbor_y]):
                continue
            if (neighbor_x, neighbor_y) not in results['territory']:
                self.flood_fill(neighbor_x, neighbor_y, results)
    