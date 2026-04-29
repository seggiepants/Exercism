"""
Pascal's Triangle (recursively)
"""

def rows(row_count):
    """
    Calculate Pascal's Triangle
    Why am I not allowed to do this iteratively?
    :param row_count: The row number to compute to.
    :returns: list of numbers for pascal's triangle from 1 to row_count empty list if row_count == 0
    :raises: ValueError if a negative row count is given.
    """
    results = []
    pascals_triangle(row_count, results)
    return results
    
def pascals_triangle(row_count, results):
    """
    Recursively Compute Pascal's Triangle
    :param row_count: current row_number stop at 1
    :param results: data computed so far.
    :raises: ValueError if a negative row count is given.
    """
    if row_count < 0:
        raise ValueError("number of rows is negative")
    
    if row_count == 0:
        return 
    
    if row_count == 1:
        results.append([1])
        return     
    
    pascals_triangle(row_count - 1, results)
    current = [0, *results[-1], 0]
    results.append([current_item + next_item for current_item, next_item in zip(current[0:-1], current[1:])])
