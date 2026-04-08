def slices(series, length):
    """Return each run of length characters within the given string series
    as a list. Duplicate values are allowed.
    Parameters:
    series: string to return subsections of.
    length: integer, length of desired sub-sections
    Returns:
    List of subsections in the string.
    """
    result = []
    if len(series) == 0:
        raise ValueError('series cannot be empty')
    
    if length == 0:
        raise ValueError('slice length cannot be zero')
    
    if length < 0:
        raise ValueError('slice length cannot be negative')
    
    if len(series) < length:
        raise ValueError('slice length cannot be greater than series length')
    
    for i in range(len(series) - length + 1):
        result.append(series[i:i + length])
    
    return result
