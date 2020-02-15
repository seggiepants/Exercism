from functools import reduce

def largest_product(series, size):
    """
    Computes the maximum value for the product of digits of size size in the
    given parameter series.
    Parameters:
    * series: string of digits to search for a largest product.
    * size: length of the sub section of digits to compute the product of.
    Returns:
    * Maximum product of digits of length size within series.
    Exceptions:
    * Throws an error for a non-empty series with non-digits (0-9)
    * Throws an error is size is not a positive number.
    """
    if size < 0 or len(series) < size:
        raise ValueError(f'Invalid size: "{size}".')
    elif len(series) > 0 and not series.isdigit():
        raise ValueError(f'"{series}" contains one or more characters that are not a digit."')
    else:
        # Notes:
        # * Reduce is used to compute the product of digits in the sub string, 
        # by multiplying them together priming the pump with a 1.
        # * List comprehension runs the reduce over every sub section of the series of size size.
        # * Max returns the largest number computed.
        return max([reduce(lambda a, b: a * int(b), series[x:x+size], 1) for x in range(len(series) - size + 1)])
