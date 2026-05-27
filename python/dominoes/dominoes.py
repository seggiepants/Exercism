"""
Chain a set of tiles into a loop of dominoes with matching number linearly and end to end
"""

def can_chain(dominoes):
    """Check if a set of dominoes can be chained in a circular fashion
    :param dominoes: list of length two tuples where two numbers represent the numbers on the top and bottom of a dominoe
    :returns: None if no pattern found. Otherwise the dominoes arranged where they match linearly and at the ends.
    """
    if len(dominoes) == 0:
        return []
    # Start the chain with the first tile.
    chained = [dominoes[0]]
    result = helper(chained, dominoes[1:])
    if result is not None and len(result) != len(dominoes):  
        # If that didn't work, try again with the tile reversed.
        chained = [tile_reverse(dominoes[0])]
        result = helper(chained, dominoes[1:])

    if result is None or len(result) != len(dominoes):
        return None

    if result[0][0] != result[len(result) - 1][1]:
        return None
    return result

def helper(chained, remaining):
    """
    Recursive helper function used to find a matching chain of dominoes
    :param chained: The list of dominoes chained so far
    :param remaining: The list of dominoes not yet in the chain
    :returns: None if no chain found, otherwise a list of dominoes that can be chained linearly and end to end.
    """
    def result_ok(result):
        return result is not None and len(result) == len(chained) + len(remaining)
    
    def match_first(tile, target, chained, remaining):
        reverse_tile = tile_reverse(tile)
        if current[1] == target:
            result = helper([tile, *chained], remaining)
            if result_ok(result):
                return result
        if reverse_tile[1] == target:
            result = helper([reverse_tile, *chained], remaining)
            if result_ok(result):
                return result
        return None
    
    def match_last(tile, target, chained, remaining):
        reverse_tile = tile_reverse(tile)
        if current[0] == target:
            result = helper([*chained, tile], remaining)
            if result_ok(result):
                return result
        if reverse_tile[0] == target:
            result = helper([*chained, reverse_tile], remaining)
            if result_ok(result):
                return result
        return None
    
    if len(remaining) == 0:
        return chained
    
    first = chained[0][0]
    last = chained[len(chained) - 1][1]

    # last one?
    if len(remaining) == 1:
        last_tile = remaining[0]
        if last_tile[1] == first and last_tile[0] == last:
            # match chain ends when normal
            return [*chained, last_tile]
        if last_tile[0] == first and last_tile[1] == last:
            # match chain ends when reversed
            return [*chained, tile_reverse(last_tile)]
        return None
    
    for index, current in enumerate(remaining):
        remaining_without_current = [tile for tile_index, tile in enumerate(remaining) if tile_index != index]
        
        result = match_first(current, first, chained, remaining_without_current)
        if result_ok(result):
            return result
        result = match_last(current, last, chained, remaining_without_current)
        if result_ok(result):
            return result
        
    return None # No matches for current chain

def tile_reverse(tile):
    """
    Reverse a single dominoe (a, b) => (b, a)
    :param tile: a tuple of two numbers representing a single dominoe
    :returns: a tuple of two numbers representing a dominoe in the reverse order of the original tile (as if flipped around 180 degrees)
    """
    return [tile[1], tile[0]]
