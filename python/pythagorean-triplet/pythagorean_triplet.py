import math
from itertools import combinations

def triplets_with_sum(number):
    """Compute set of pythaogorean triplets that sum up to the given value.
    Parameters:
    * number: The value the triplet should add up to.
    Returns:
    * Set of tuples containing matching pythagorean triplets.
    Warning:
    * Very slow for large numbers.
    """
    return set([x for x in triplets_in_range(1, number) if sum(x) == number])

def triplets_in_range(start, end):
    """Compute possible pythagorean triples between the given start and end
    values. Return them as a list.
    Parameters:
    * start: beginning integer number of the range.
    * end: ending integer number (inclusive) of the range.
    Returns:
    * List of pythagorean triples found.

    NOTES:
    Swiped implementation from: https://medium.com/@lizparody/pythagorean-triplets-in-elixir-python-and-javascript-638f6a60494
    after my implementation was intolerably slow.
    """
    results = []
    for b in range(start, end + 1):
        for a in range(1, b):
            c = math.sqrt( a * a + b * b)
            # Make sure the value for c is an integer
            if c % 1 == 0:
                results.append((a, b, int(c)))
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
    return (sorted_triplet[0] * sorted_triplet[0] + sorted_triplet[1] * sorted_triplet[1]) == (sorted_triplet[2] * sorted_triplet[2])
