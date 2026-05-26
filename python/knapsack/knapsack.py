"""
Find the maximum value of weight can be carried by a knapsack of limited capacity.
"""

def maximum_value(maximum_weight, items):
    """
    Find the set items you can carry that have the maximum value.
    :param maximum_weight: Maximum weight that the knapsack can carry
    :param items: list of objects that have weight and value numeric keys
    :returns: the maximum value of items you can carry in the knapsack from the given items list
    """
    items.sort(key=lambda item: (item['weight'], item['value']), reverse=True)
    return find_maximum_value(maximum_weight, items, {})

def find_maximum_value(maximum_weight, items, memoized):
    """
    Recursive helper function for maximum_value. May also pull from memoized dictionary of 
    previously computed values as well.
    :param maximum_weight: Maximum weight that the knapsack can carry
    :param items: list of objects that have weight and value numeric keys
    :param memoized: dictionary of remaining items to values that have already been computed
    :returns: the maximum value of items you can carry in the knapsack from the given item list.
    """
    scores = []
    for index, item in enumerate(items):
        weight = item['weight']
        value = item['value']

        if weight < maximum_weight:
            next_items = [item for item_index, item in enumerate(items) if item_index != index and item['weight'] <= maximum_weight]
            if len(next_items) == 0:
                scores.append(value)
                continue
            next_weight = maximum_weight - weight
            item_key = ','.join([str(item['weight']) + '|' + str(item['value']) for item in next_items])
            remaining_value = 0
            key = f"{next_weight},[{item_key}]"
            if key in memoized:
                remaining_value = memoized[key]
            else:
                remaining_value = find_maximum_value(next_weight, next_items, memoized)
                memoized[key] = remaining_value
            scores.append(value + remaining_value)
        elif weight == maximum_weight:
            scores.append(value)
        
    if len(scores) == 0:
        return 0
    return max(scores)
