# Map a bit flag to an action. Reverse 0b10000 (16) is handled separately.
codes = {
    0b1: 'wink'
    , 0b10: 'double blink'
    , 0b100: 'close your eyes'
    , 0b1000: 'jump'
}

def commands(number):
    """Given a number, generate a matching secret handshake.
    Parameters:
    * number: The number to use to generate the list. Only integers 
    are allowed and only up to the first five bits are considered (0-31).
    Returns:
    * list of actions for the handshake associated with the number.
    """
    # If the number has the correct bit set, add the matching action to
    # the result set. It is important we go in a sorted order.
    results = [codes[key] for key in sorted(codes.keys()) if key & number]
    
    # If the 0b10000 (16) bit is set reverse the list
    if 0b10000 & number:
        results.reverse()
    
    # Return the resulting list
    return results


def secret_code(actions):
    """Given a handshake list, figure out what number was used to 
    generate the given sequence.
    Parameters:
    * actions: A list of actions.
    Returns:
    * A number value that would generate the action list given.
    Exceptions:
    * Will fail if you pass an action that is not in the list of:
    ['wink', 'double blink', 'close your eyes', 'jump']
    """

    # Make a dictionary that maps actions to bits
    # I should probably do this outside of this function. So it is only done once.
    # but it seems redundant so I didn't.
    bits = {}
    for key, value in codes.items():
        bits[value] = key
    
    # For each action add the corresponding bit flag to the list.
    results = [bits[key] for key in actions]

    # If the bits are in descending order it must have been reversed so
    # add 0b10000 (16)
    if len(results) > 1:
        if results[0] > results[1]:
            # must be reversed.
            results.append(0b10000)
    
    # Return the sum of the actions.
    return sum(results)
