"""
Find the smallest and largest palindrome products for a range of numbers.
"""

def largest(min_factor, max_factor):
    """Given a range of numbers, find the largest palindromes which
       are products of two numbers within that range.

    :param min_factor: int with a default value of 0
    :param max_factor: int
    :return: tuple of (palindrome, iterable).
             Iterable should contain both factors of the palindrome in an arbitrary order.
    """
    return palindrome_product_helper(min_factor, max_factor, max)


def smallest(min_factor, max_factor):
    """Given a range of numbers, find the smallest palindromes which
    are products of two numbers within that range.

    :param min_factor: int with a default value of 0
    :param max_factor: int
    :return: tuple of (palindrome, iterable).
    Iterable should contain both factors of the palindrome in an arbitrary order.
    """
    return palindrome_product_helper(min_factor, max_factor, min)

def palindrome_product_helper(min_factor, max_factor, compare_fn):
    """Given a range of numbers, find the palindromes matching the result of the function fn
    which are products of two numbers within that range.
    :param min_factor: int with a default value of 0
    :param max_factor: int
    :param compare_fn: function to determine palindrome products to keep
    :return: tuple of (palindrome, iterable).
    Iterable should contain both factors of the palindrome in an arbitrary order.
    """
    if min_factor > max_factor:
        raise ValueError('min must be <= max')

    products = []
    target_value = None

    if compare_fn(1, 2) == 2:
        # Bigger first
        start = max_factor
        stop = min_factor - 1
        step = -1
    else:
        # Smaller first
        start = min_factor
        stop = max_factor + 1
        step = 1

    for outer in range(start, stop, step):
        if step > 0:
            inner_start = outer
            inner_stop = max_factor + 1
        else:
            inner_start = max_factor
            inner_stop = outer - 1
        for inner in range(inner_start, inner_stop, step):
            product = inner * outer 
            if target_value is not None and compare_fn(product, target_value) != product:
                continue
            digits = str(inner * outer)
            if digits == digits[::-1]:
                target_value = product
                products.append((inner, outer))

    if len(products) == 0:
        return None, ()
    return target_value, (pair for pair in products if pair[0] * pair[1] == target_value)
