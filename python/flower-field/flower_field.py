"""
Annotate a flower field with the number of neighboring flowers where
there are one or more neighboring flowers.
A Balwderized and Lame version of Minesweeper.
"""

BLANK = ' '
FLOWER = '*'
ERR_INVALID = 'The board is invalid with current input.'

def annotate(garden):
    """
    Return a copy of the given garden with empty spaces annotated with the number of flowers in the 
    surrounding space (cardinal directions and diagonals up to one space away). If there are no surrounding
    flowers leave the space blank.
    :param garden: List of strings showing a map of known flowers ' ' = Empty '*' = Flower
    :returns: List of strings with blank squares filled with numbers then one or more flowers are neighboring
    """
    lengths = [len(row) for row in garden]
    if len(lengths) > 0 and min(lengths) != max(lengths):
        raise ValueError(ERR_INVALID)    

    invalid_positions = [value for value in ''.join(garden) if value not in [BLANK, FLOWER]]
    if len(invalid_positions) > 0:
        raise ValueError(ERR_INVALID)
    
    copy = [list(row) for row in garden]
    for row in garden:
        col_length = len(row)
        for col_index, value in enumerate(row):
            if value not in [BLANK, FLOWER]:
                raise ValueError(ERR_INVALID)
    
    row_length = len(garden)
    for row_index, row in enumerate(garden):        
        col_length = len(row)
        for col_index, value in enumerate(row):

            if value == BLANK:
                flower_count = get_neighbor_count(garden, row_index, col_index, row_length, col_length)
                if flower_count > 0:
                    copy[row_index][col_index] = str(flower_count)
    return [''.join(row) for row in copy]

def get_neighbor_count(garden, row_index, col_index, row_length, col_length):
    """
    Count the number of neighboring flowers for a given cell.
    :param garden: list of strings representing the garden ' ' = Blank, '*' = Flower
    :param row_index: y-coordinate to search nearby
    :param col_index: x-coordinate to search nearby
    :param row_length: number of rows in the grid
    :param col_length: number of columns in the grid
    :returns: count of neighboring flowers between -1, 0, or 1 steps away on the x 
    and/or y axis not counting the given cell.
    """
    flower_count = 0
    for y_coord in range(row_index - 1, row_index + 2):
        if y_coord < 0 or y_coord >= row_length:
            continue
        for x_coord in range(col_index - 1, col_index + 2):
            if x_coord < 0 or x_coord >= col_length:
                continue

            if not (row_index == y_coord and col_index == x_coord) and garden[y_coord][x_coord] == FLOWER:
                flower_count += 1
    return flower_count