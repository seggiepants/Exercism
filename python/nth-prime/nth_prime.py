def prime(number):
    """Return the nth prime where number is the nth index.
    The first four primes are 2, 3, 5, 7. So if you asked for nth prime
    where number = 4 you would get 7.
    Parameters:
    * number: The nth prime number to return.
    Returns:
    * Prime number for the given index.
    """
    # check for sanity
    if number <= 0 or number % 1 != 0:
        raise ValueError('number must be a postive integer >= 1.')
    
    primes = [] # holds prime numbers found so far.
    current = 1 # start counting at 1 (well 2 actually)
    while len(primes) < number:
        current += 1
        if is_next_prime(current, primes):
            # if we found a prime add it to the list.
            primes.append(current)
    
    # Once we have enough return the last one.
    return primes[-1]

def is_next_prime(number, primes):
    """Check to see if a given number is the next prime number available.
    You must run this in order or you may have gaps.
    Parameters:
    * number: The value to check to see if it is prime or not.
    * primes: List of prime numbers found while iterating up to number.
    Returns:
    * False if this is a multiple of any prime in the list, True otherwise.
    """
    # sanity check
    if number <= 1:
        return False

    # if we aren't divisible by any of the primes found so far, then we
    # must be a prime number.
    if any(number % prime == 0 for prime in primes):
        return False
        
    # Not a multiple of any prime so it must be a prime.
    return True
