"""
Game of Life
For a given 2D matrix return the next state in the game of life simulation.
"""

def tick(matrix):
    """Run the game of life simulation one step returning a matrix with next next output state.
    :param matrix: Current state of the game of life simulation
    :returns: Next state of the game of life simulation as a 2D array of 0, 1 values 1 - alive 0 - dead.
    """
    next_matrix = []
    if len(matrix) > 0:
        next_matrix = new_matrix(len(matrix), max(len(row) for row in matrix), 0)
    for row_index, row in enumerate(matrix):
        for column_index, col in enumerate(row):
            neighbor_count = count_neighbors(matrix, row_index, column_index)
            next_matrix[row_index][column_index] = int((col != 0 and neighbor_count == 2) or neighbor_count == 3)
    return next_matrix

def new_matrix(rows, cols, fill):
    """
    Create a new 2D matrix of size rows x cols where all the
    cells are set to fill.
    :param rows: How many rows the new matrix should have.
    :param cols: How many columns the new matrix should have.
    :param fill: What to fill the matrix with.
    :returns: A new 2D matrix filled with default value
    """
    return [[fill for column in range(cols)] for row in range(rows)]

def get_matrix(matrix, row, column, default):
    """
    Get a cell from a 2D matrix. Returning default if 
    the row/column is out of bounds.
    :param matrix: The matrix to get a value from
    :param row: The row index to retrieve from
    :param column: The column index to retrieve from
    :param default: Value to return if out of bounds
    :returns: Matrix[row, column] or default if out of bounds
    """
    if row < 0 or column < 0 or row >= len(matrix) or column >= len(matrix[row]):
        return default
    return matrix[row][column]

def count_neighbors(matrix, row, column):
    """
    Get the number of living neighbors for a position in the matrix
    :param matrix: The matrix to search (2-D list)
    :param row: row index to search from
    :param column: column index to search from
    :returns: The number of living neighbors of the given cell (itself not included)
    """
    return sum(get_matrix(matrix, row_index, column_index, 0) for row_index in range(row - 1, row + 2) for column_index in range(column - 1, column + 2) if row_index != row or column_index != column)
