import math

def classify(number):
    """Classify a number based on its aliquot_sum. If equal it is 
    Perfect, greater than is Abundant and less than is Deficient.
    Parameters:
    * number: The value to classify, must be an integer greater than zero.
    Returns:
    * A string 'abundant', 'deficient', or 'perfect' depending on how the sum
    compares to the number.
    """    
    aliquot = aliquot_sum(number)
    if aliquot > number:
        return 'abundant'
    elif aliquot < number:
        return 'deficient'
    else: # aliquot == number
        return 'perfect'

def aliquot_sum(number):
    """Computes the aliquot_sum for a given number.
    Parameters:
    * number: Value to compute the sum for, must be an integer greater than zero.
    Returns:
    * aliquot sum for the given number.
    Exceptions:
    * Raises an exception if zero, a negative number, or non-integer is passed in.
    Note:
    * This can be very slow for large numbers as there are modulus operations that 
    scale with the number entered.
    """
    # Raise a value error for floating point or dissallowed integer values.
    if number < 1 or number % 1 != 0:
        raise ValueError('Number must be an integer greater than zero.')
    
    # only go half way up to save some time as we can't have a factor larger than
    # number > 2
    # List comprehension returns the sum of all numbers greater than one leading 
    # up to the given number that also divide evenly into the given number.
    return sum([i for i in range(1, math.ceil((number + 1)/2)) if number % i == 0])
    