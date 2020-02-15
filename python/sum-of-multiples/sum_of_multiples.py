def sum_of_multiples(limit, multiples):
    """Return the sum of integer numbers from 1 to limit - 1 that 
    are a multiple of any number in the list multiples. Zeroes are
    filtered out, and an empty list for multiples will just return zero.
    Parameters:
    * limit - Stop checking when you hit this number. Should be an integer
    greater or equal to 1.
    * multiples - A list of integer numbers to check if things are multiples against.
    Returns:
    * Sum of the numbers that are a multiple of any number in the multiples list and
    are between 1 and limit - 1 
    """
    # Filter out any divide by zero errors.
    multiples = [x for x in multiples if x != 0]

    # Innermost map returns true/false if the item n is a multiple of x from multiples
    # We reduce that to a single true false with the any call.
    # We run that over everything in the range of 1 to limit - 1 returning only the numbers 
    # that match the inner check.
    # Finally we sum up the results and return the value.
    return sum([n for n in range(1, limit) if any(map(lambda x: n % x == 0, multiples))])

