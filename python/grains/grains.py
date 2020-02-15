def square(number):
    """Return the number of grains on the given square of a chessboard
    (see problem description).
    Parameters:
    * number: The square on the chessboard to inspect must be an integer 
    number between 1 and 64
    Returns:
    * The count of grains to be placed on that square.
    Exceptions:
    * Will raise a ValueError if number is not between 1 and 64
    """
    if number < 1 or number > 64:
        raise ValueError(f'Number ({number}) is out of range. 1 - 64 allowed.')
    return 2**(number - 1)


def total(number):
    """Return the total number of grains on the chessboard (see problem description).
    For squares 1 to the given number.
    Parameters:
    * number: The square to stop on. Must be an integer between 1 and 64
    Returns:
    * The total count of grains up to that point on the board
    Exceptions:
    * Will raise a ValueError if number is not between 1 and 64
    """
    if number < 1 or number > 64:
        raise ValueError(f'Number ({number}) is out of range. 1 - 64 allowed.')
    
    return sum([square(i) for i in range(1, number + 1)])
