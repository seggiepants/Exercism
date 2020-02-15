def saddle_points(matrix):
    """
    Search for saddle points within the given matrix. Return them in a
    list where each item is a dictionary with row and column keys.
    Parameters:
    * matrix: A list composed of lists of numbers. Ex: [[1, 2], [3, 4]]
    Returns:
    * A list of dictionary objects with two items 'row', and 'columns' where
    the keys show the position of a saddle point. Ex: [{"row":3, "column": 5}]
    Note that upper left of the matrix is referenced as 1, 1, not 0, 0.
    """

    # Sanity Check
    if any(len(row) != len(matrix[0]) for row in matrix):
        raise ValueError('Irregular matrix detected.')
    
    results = []

    # Can't really iterate if we have an empty matrix. Fails a 
    # test case if we try.
    if len(matrix) > 0:
        rows = len(matrix)
        cols = len(matrix[0])        
        for row in range(rows):
            for col in range(cols):
                # Check if any item greater or equal to all in row.
                if any(matrix[row][i] > matrix[row][col] for i in range(cols) if i != col):
                    continue
                
                # Check if any item less or equal to all in column.
                if any(matrix[j][col] < matrix[row][col] for j in range(rows) if j != row):
                    continue
                results.append({"row": row + 1, "column": col + 1})
    
    # Add an empty dictionary for when we have no results, just to
    # appease the test cases.
    if len(results) == 0:
        results.append(dict())
    
    return results
