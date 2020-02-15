def primes(limit):
    """Find prime numbers between 2 and the given limit using the
    sieve of Erasthones. You basically make a list of all numbers
    between 2 and the limit. Then start at the first prime and mark all
    the multiples of it remaining in the list as non-prime. Then move
    on to the next non-marked item in the list and mark their multiples
    as non-prime and so on and so on until you run out of items in the list.
    Finally return just the items in the list that are still marked as prime.
    """
    if limit < 2:
        # return an empty list if you started before 2.
        return []
    elif limit != round(limit):
        # raise an error if you didn't give me an integer.
        raise ValueError('Limit must be an integer (whole number).')
    else:
        # iteratively mark everything that is non-prime with a zero.
        result = list(range(2, limit + 1))
        for i, step in enumerate(result):
            if step > 0:
                for n in range(i + step, len(result), step):
                    result[n] = 0
        return [value for value in result if value > 0]

