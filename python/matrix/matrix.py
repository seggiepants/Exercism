class Matrix(object):
    # constructor
    # parse matrix_string and populate the rows[] list with a sub list for each row.
    # numbers are delimited by spaces, rows by the carriage return \n
    def __init__(self, matrix_string):
        self.rows = []
        for row in matrix_string.splitlines():
            currentRow = []
            for item in row.split(' '):
                currentRow.append(self.parseNum(item))
            self.rows.append(currentRow)
    
    # Return the supplied row.
    def row(self, index):
        try:
            return self.rows[index - 1]
        except:
            raise Exception('Row at index: ' + str(index) + ' not found.')

    # Return the supplied column
    def column(self, index):
        try:
            return list(map(lambda row: row[index - 1], self.rows))
        except:
            raise Exception('Error accessing column at index: ' + str(index) + '.')

    # Parse a number as integer as possible, float if that is better. Throw an error
    # if it can't be interpreted as float or integer.
    def parseNum(self, num):
        try:
            return int(num)
        except ValueError:
            return float(num)
        except:
            raise Exception('Expected number, but found: ' + num + '.')
    
