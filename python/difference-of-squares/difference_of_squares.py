def square_of_sum(number):
    """Compute the square of the sum of the whole integers from 1 to the 
    supplied number
    Parameters:
    - number: The number to stop on
    Returns:
    - computed sum
    """
    return sum(range(1, number + 1)) ** 2

def sum_of_squares(number):
    """Compute the sum of the squares of the whole integers from 1 to 
    the supplied number.
    Parameters:
    - number: The number to stop on
    Returns:
    - computed sum
    """
    # List comprehension to be fancy and because I need the practice
    return sum(i*i for i in range(1, number + 1))

def difference_of_squares(number):
    """Compute the difference between the square of the sum of 1 to the 
    given number and the sum of the squares from 1 to the given number.
    Just calls the square_of_sum, and sum_of_squares functions to do 
    the actual work.
    Parameters:
    - number: The number to count up to.
    Returns:
    - computed value.
    """
    return square_of_sum(number) - sum_of_squares(number)
