"""
Convert a visual representation of numbers in a list of strings to 
a string of those numbers.
"""

DIGIT_WIDTH = 3
DIGIT_HEIGHT = 4

digits = [
 '    _  _     _  _  _  _  _  _ ', #
 '  | _| _||_||_ |_   ||_||_|| |', # Decimal numbers.
 '  ||_  _|  | _||_|  ||_| _||_|', #
 '                              ' # The fourth line is always blank
]

def convert(input_grid):
    """
    Convert a list of strings to the numbers they represent.
    :param input_grid: list of strings with OCR-able characters
    :returns: String of numbers represented by the input grid.
    """
    if len(input_grid) == 0:
        return ''
    
    # when the rows aren't multiples of 4
    if len(input_grid) % 4 != 0:
        raise ValueError('Number of input lines is not a multiple of four')

    # when the columns aren't multiples of 3
    for row in input_grid:
        if len(row) % 3 != 0 or len(row) == 0:
            raise ValueError('Number of input columns is not a multiple of three')
    ret = ''
    for row_index in range(0,len(input_grid), DIGIT_HEIGHT):
        for col_index in range(0, len(input_grid[row_index]), DIGIT_WIDTH):
            data = [input_grid[index][col_index:(col_index + DIGIT_WIDTH)] for index in range(row_index, row_index + DIGIT_HEIGHT)]
            ret += match_digit(data)
        ret += ','
    return ret[0:len(ret) - 1]

def match_digit(data):
    """Check if a given set of data matches a digit pattern.
    :param data: list of DIGIT_HEIGHT rows each a string of DIGIT_WIDTH characters to check against known digit patterns.
    :returns: '?' if no match found, otherwise the matching digit converted to a single character string.
    """
    for digit in range(10):
        match = (digit + 1) % 10
        for row_index in range(DIGIT_HEIGHT):
            row = digits[row_index][digit * DIGIT_WIDTH: (digit + 1) * DIGIT_WIDTH]
            if data[row_index] != row:
                match = -1
                break
        if match >= 0:
            return str(match)
    return '?'
