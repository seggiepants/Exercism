"""
Calculate the possible combinations of numbers of a given length that add up to a given total
that could be solutions for Killer Soduku positions
"""

# looks like I should have been using itertools.combinations.

def combinations(target, size, exclude):
    """
    Find all number combinations of a given length that add up to a target value
    excluding numbers that are already taken.
    :param target: The target amount to reach
    :param size: The number of digits that must used to add up to the target
    :param exclude: A list of numbers that can not be used (taken)
    :returns: A list containing lists of number (length=size) that add up to target empty list if no matches
    """
    final = []
    ret = ['dummy']
    disallowed = [*exclude]
    
    while len(ret) > 0:
        ret = combination_helper(target, size, [], disallowed)
        if len(ret) > 0:
            disallowed = [*disallowed, *ret]
            final.append(ret)
    return final

def combination_helper(target, size, current, exclude):
    """
    Find one combination of numbers of length size that add up to target, for 1-9 not 
    including number in the exclude list. 
    :param target: The number to add up to
    :param size: The number of digits that must be consumed
    :param current: The digits already under consideration
    :param exclude: Digits that cannot be used
    :returns: A list of the digits of length size that add up to target or an empty list if no match found.
    """
    current_sum = sum(current)
    current_len = len(current)
    
    if current_len > size:
        return []
    
    if current_sum > target:
        return []
    
    if current_sum == target:
        if current_len == size:
            return current
        return []
    
    possibilities = [counter + 1 for counter in range(9) if counter + 1 not in current and counter + 1 not in exclude]
    if len(possibilities) == 0:
        return []
    
    for possibility in possibilities:
        ret = combination_helper(target, size, [*current, possibility], exclude)
        if len(ret) == size and sum(ret) == target:
            return ret
    
    return []
    