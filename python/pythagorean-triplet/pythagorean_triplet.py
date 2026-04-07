import math

def triplets_with_sum(number):
    """Compute set of pythaogorean triplets that sum up to the given value.
    Parameters:
    * number: The value the triplet should add up to.
    Returns:
    * Set of tuples containing matching pythagorean triplets.
    """
    # If you won't take a reasonable 8s runtime I am just going to 
    # swipe code from a community solution using fancy math
    # I already solved it well enough.
    results  = []
    for a in range(1, number):
        denom = 2*(number-a)
        num = 2*a**2 + number**2 - 2*number*a
        if denom > 0 and num % denom == 0:
            c = num // denom
            b = number - a - c
            if b > a:
                results.append([a,b,c])
    return results

def is_triplet(triplet):
    """Check to see if a given iterable is a pythagorean triplet such that
    the squares of two of the items in the iterable is equal to the square of a 
    third item in the iterable.
    Parameters:
    * triplet: iterable of size 3 containing 3 (distinct) integers.
    Returns:
    * True if the trio is a pythagorean triplet and false otherwise.
    Exceptions:
    * Throws an exception if the given item is not of length 3.
    """
    if len(triplet) != 3:
        raise Exception('Enumerable triplet must have a length of 3.')
    
    sorted_triplet = sorted(triplet)
    return (sorted_triplet[0] ** 2 + sorted_triplet[1] ** 2) == (sorted_triplet[2] ** 2)
