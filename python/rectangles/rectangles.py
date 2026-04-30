"""
For a given diagram calculate the number of rectangles it contains
"""

HORIZONTAL = '-+'
VERTICAL = '|+'
CORNER = '+'

def rectangles(strings):
    """
    Count the number of rectangles in the given diagram.
    :param strings: List of strings, should all be the same length
    :returns: count of rectangles found
    """
    # turn strings into a list of list of chars for easier indexing
    ret = 0
    board = [list(row) for row in strings]
    for row_index, row in enumerate(board):
        for col_index, rune in enumerate(row):
            if rune in CORNER:
                ret += trace_rectangle(col_index, row_index, board)
    return ret

def trace_rectangle(col_index, row_index, board):
    """
    See if you can trace out a rectangle from this location. Try to 
    keep going and branch out when we encounter a corner character.
    :param col_index: column index to start at
    :param row_index: row index to start at
    :returns: number of rectangles traced out starting at this position.
    """
    col_count = len(board[0])

    ret = 0

    # trace left
    current_x = col_index + 1
    current_y = row_index
    while current_x < col_count and board[current_y][current_x] in HORIZONTAL:
        # if corner try to turn there.
        if board[current_y][current_x] == CORNER:
            ret += trace_down(col_index, row_index, current_x, current_y, board)
        current_x += 1
    return ret 

def trace_down(left, top, col_index, row_index, board):
    """
    Search downard for a corner to branch on or non-vertial to stop at.
    :param left: column where the rectangle started
    :param top: row where the rectangle started
    :param col_index: column index to start at - top right corner
    :param row_index: row index to start at - top right corner
    :returns: number of rectangles traced out.
    """
    
    row_count = len(board)

    ret = 0
    # trace down
    current_x = col_index
    current_y = row_index + 1
    while current_y < row_count and board[current_y][current_x] in VERTICAL:
        # if corner try to turn there.
        if board[current_y][current_x] == CORNER:
            ret += trace_right(left, top, current_x, current_y, board)
        current_y += 1
    return ret 

def trace_right(left, top, right, bottom, board):
    """
    Search right to see if we have a corner at the expected spot and horizontal characters inbetween
    No rectangles if we encounter an unexpected character
    :param left: column where the rectangle started
    :param top: row where the rectangle started
    :param right: column where the right side of the rectangle should be
    :param bottom: row where the bottom of the rectangle should be
    :returns: number of rectangles traced out.
    """

    current_x = right
    current_y = bottom

    # make sure the corner is where it should be
    if board[bottom][left] != CORNER:
        return 0
    
    while current_x >= left and board[current_y][current_x] in HORIZONTAL:
        current_x -= 1
    
    # did we stop early
    if current_x != left - 1:
        return 0
    
    return trace_up(left, top, bottom, board)

def trace_up(left, top, bottom, board):
    """
    Search up to see if we have a corner at the expected spot and vertical characters inbetween
    No rectangles if we encounter an unexpected character
    :param left: column where the rectangle started
    :param top: row where the rectangle started
    :param bottom: row where the bottom of the rectangle should be
    :returns: number of rectangles traced out.
    """

    current_x = left
    current_y = bottom

    # make sure the corner is where it should be
    if board[top][left] != CORNER:
        return 0
    
    while current_y >= top and board[current_y][current_x] in VERTICAL:
        current_y -= 1
    
    # did we stop early
    if current_y != top - 1:
        return 0
    
    # one rectangle found    
    return 1
