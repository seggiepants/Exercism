"""
Transpose rows and columns in a \n delimited text
"""

def transpose(text):
    """
    Transpost the given text so that rows and columns are swapped.
    :param text: Rows in the text are delimited by \n This is the value to transpose
    :returns: transposed copy of the input file.
    """
    rows = text.split('\n')
    cols = max(len(row) for row in rows)
    ret = []
    for col_index in range(cols):
        current = ''
        for row_index, row in enumerate(rows):
            if col_index < len(row):
                current = current.ljust(row_index) + row[col_index]
        ret.append(current)

    # Almost had a list comprehension working but it didn't handle internal missing values correctly.
    return '\n'.join(ret)