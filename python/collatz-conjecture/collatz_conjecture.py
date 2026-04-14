"""
Calculate how many steps it takes for a positive integer number greater than zero to reach one 
following the steps i the collatz conjecture.
"""

def steps(number):
    """
    Calculate how many steps it takes for a positive integer number greater than zero to reach one 
    following the steps i the collatz conjecture.
    number: The number to start with.
    returns: The number of steps take to get to zero.
    """
    if number <= 0:
        raise ValueError('Only positive integers are allowed')
    num_steps = 0
    while number != 1:
        if number % 2 == 0:
            number /= 2
        else:
            number = number * 3 + 1
        num_steps += 1
    return num_steps
