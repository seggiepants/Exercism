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
    if length <= 0:
        raise ValueError(f'Invalid length supplied. Should be an integer greater than zero, instead recieved {length}')
    elif len(series) < length:
        raise ValueError('Cannot get a subsection of that length.')
    else:
        for i in range(len(series) - length + 1):
            result.append(series[i:i + length])
        
    return result
