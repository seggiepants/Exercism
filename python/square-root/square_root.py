"""
Calculate the integer square root
"""

def square_root(number):
    """
    Calculate the integer square root for the given number using
    the Newton-Heron iteration swiped from wikipedia.org
    preconditions: 
    * n >= 0
    * guess > 0, defaults to 1 (initial guess)
    number: The number to calculate the square root of.
    """
    guess = 1
    if number < 0 or guess <= 0:
        raise ValueError('Invalid input')

    # isqrt(0) = 0; 
    # isqrt(1) = 1
    if number < 2:
        return number

    prev2 = -1      # x_{i-2}
    prev1 = guess   # x_{i-1}

    while True:
        current = (prev1 + number // prev1) // 2

        # Case 1: converged (steady value)
        if current == prev1:
            return current

        # Case 2: oscillation (2-cycle)
        if current == prev2 and current != prev1:
            # We’re flipping between prev1 and prev2
            # Choose the smaller one (the true integer sqrt)
            return min(prev1, current)

        # Move forward
        prev2 = prev1
        prev1 = current
