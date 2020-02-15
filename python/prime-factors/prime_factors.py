import math

def factors(value):
    """
    Find the prime factors of a given number.
    Parameters: 
    * value: Number to find prime factors for.
    Returns:
    * List of prime factors.
    """
    results = []        # Fill with prime factors.
    remaining = value   # Don't want to mutate the original value.
    candidate = 2
    while remaining > 1:
        # I had a terrible time of this one trying to compute prime numbers
        # to test against. Turns out I didn't have to do that at all.
        # I looked at the other solutions and basically copied them
        # look at previous version for my slow but working approach.
        while remaining % candidate == 0:
            results.append(candidate)
            remaining = remaining / candidate

        candidate += 1
        
    return results