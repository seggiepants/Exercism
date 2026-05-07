"""
Find change for given coin denominations. Try to get the fewest number
of coins returned possible. Throw an error for degenerate cases or when
change cannot be made.
"""

ERR_TARGET_NOT_FOUND = 'can\'t make target with given coins'
ERR_NEGATIVE_NOT_ALLOWED = 'target can\'t be negative'

MEMOIZED = {}

def find_fewest_coins(coins, target):
    """
    Return a target amount of change using the coins available in coins.
    Should return the solution using the least number of coins.
    :param coins: list of coin denominations in the change drawer
    :param target: how much change to return
    :returns: list of coins totaling the change, empty list if not change to return.
    :raises: ValueError if target amount < 0 or if can't find a way to make the target change.
    """
    MEMOIZED.clear()
    if target == 0:
        return []
    if target < 0:
        raise ValueError(ERR_NEGATIVE_NOT_ALLOWED)
    
            
    coins.sort(reverse=True)
    ret = find_change(coins, [], target, target)
    if sum(ret) != target:
        raise ValueError(ERR_TARGET_NOT_FOUND)
    ret.sort()
    return ret
    
def find_change(available, coins, target, max_iterations):
    """
    Return a target amount of change using the coins available in coins.
    :param available: Coins denominations in the change drawer
    :param coins: coins we have taken so far.
    :param target: coin value we wish to get to.
    :param max_iterations: cancel early if we are looking for a solution when we already have a better one.
    :returns: shortest list of coins adding up to target or empty list if not found.
    """
    total_so_far = sum(coins)
    total_remaining = target - total_so_far
    if total_remaining == 0:
        return coins    # found the target
    if total_remaining < 0:
        return []       # went over the target
    if len(coins) > max_iterations:
        return []       # didn't find anything shorter.
    
    best = []
    for coin in (coin for coin in available if coin <= total_remaining):
        new_coins = [*coins, coin]
        new_sum = sum(new_coins)
        if new_sum < target:
            target_amount = target - new_sum
            if target_amount in MEMOIZED:
                new_coins = [*new_coins, *MEMOIZED[target_amount]]
                new_sum = sum(new_coins)
            else:
                max_iterations = target_amount
                if len(best) > 0:
                    max_iterations = len(best) - len(new_coins)
                additional_coins = find_change(available, [], target_amount, max_iterations)
                if new_sum + sum(additional_coins) == target:
                    if not target_amount in MEMOIZED:
                        MEMOIZED[target_amount] = additional_coins
                    new_coins = [*new_coins, *additional_coins]
                    new_sum = sum(new_coins)
        if new_sum == target and (len(best) == 0 or len(best) > len(new_coins)):
            best = new_coins
    return best
