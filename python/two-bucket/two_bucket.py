"""
Two-Bucket measuring problem implementation.
"""

def measure(bucket_one, bucket_two, goal, start_bucket):
    """
    Find an optimal way to get a desired goal value with two buckets of a given size
    :param bucket_one: The size of the first bucket
    :param bucket_two: The size of the second bucket
    :param goal: The amount we are trying to measure
    :param start_bucket: Start with this bucket filled ('one', or 'two')
    :returns: Tuple of number of moves, the goal bucket, and the other bucket.
    :raises: Value error if the goal is nonsensicle or a path to it could not be found.
    """
    if goal > max(bucket_one, bucket_two):
        raise ValueError('Invalid goal - larger than maximum possible.')
    bucket_one_amt = bucket_one if start_bucket == 'one' else 0
    bucket_two_amt = bucket_two if start_bucket == 'two' else 0
    result = step([[bucket_one_amt, bucket_two_amt]], bucket_one_amt, bucket_two_amt, start_bucket, bucket_one, bucket_two, goal)
    if result['goal_bucket'] == 'n/a':
        raise ValueError('Invalid goal - no possible path.')
    return (result['moves'], result['goal_bucket'], result['other_bucket'])

def step(move_stack, bucked_one_amt, bucked_two_amt, original_bucket, bucked_one_max, bucked_two_max, goal):
    """
    Solve the two bucket problem recursively. Try every possible move then reevaluate from that state
    until you hit the goal. You can have multiple ways to the goal so return one with the shortest sequence
    :param move_stack: Array of Bucket amount pairs. No use reevaluating a previous state
    :param bucked_one_amt: How many liters bucket one currently contains
    :param bucked_two_amt: How many liters bucket two currently contains
    :param original_bucket: What bucket did we start on for the illegal move checks
    :param bucked_one_max: How many liters can bucket one hold
    :param bucked_two_max: How many liters can bucket two hold
    :param goal: The number of liters we want to get to in one of the given buckets.
    :returns: The number of moves, bucket that ended up with the goal amount, and how much
    was in the remaining bucket returned as an object expected by the test. On error moves will be -1
    and goal_bucket will be n/a also other bucket will be 0.
    """
    # Base state, return on goal
    if bucked_one_amt == goal:
        return {
            'moves': len(move_stack),
            'goal_bucket': 'one',
            'other_bucket': bucked_two_amt,
        }
    
    if bucked_two_amt == goal:
        return {
            'moves': len(move_stack),
            'goal_bucket': 'two',
            'other_bucket': bucked_one_amt,
      }

    # Three possible actions:
    # - fill one or two
    # - pour one into two or two into one until other full or current empty
    # - empty one or two
    # Move is invalid if it leaves original_bucket = empty and other_bucket = filled
    results = []

    # Fill 1
    illegal_fill = original_bucket == 'two' and bucked_two_amt == 0
    was_visited = any(pair[0] == bucked_one_max and pair[1] == bucked_two_amt for pair in move_stack)
    if not (illegal_fill or was_visited or bucked_one_amt == bucked_one_max):
        result = step([*move_stack, [bucked_one_max, bucked_two_amt]],
            bucked_one_max,
            bucked_two_amt,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Fill 2
    illegal_fill = original_bucket == 'one' and bucked_one_amt == 0
    was_visited = any(pair[0] == bucked_one_amt and pair[1] == bucked_two_max for pair in move_stack)
    if not (illegal_fill or was_visited or bucked_two_amt == bucked_two_max):
        result = step([*move_stack, [bucked_one_amt, bucked_two_max]],
            bucked_one_amt,
            bucked_two_max,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Pour 1 to 2
    amount_to_pour = min(bucked_one_amt, bucked_two_max - bucked_two_amt)
    illegal_pour = original_bucket == 'one' and bucked_one_amt - amount_to_pour == 0 and bucked_two_amt + amount_to_pour == bucked_two_max
    was_visited = any(pair[0] == bucked_one_amt - amount_to_pour and pair[1] == bucked_two_amt + amount_to_pour for pair in move_stack)
    if not (illegal_pour or was_visited or amount_to_pour <= 0):
        result = step([*move_stack, [bucked_one_amt - amount_to_pour, bucked_two_amt + amount_to_pour]],
            bucked_one_amt - amount_to_pour,
            bucked_two_amt + amount_to_pour,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Pour 2 to 1
    amount_to_pour = min(bucked_two_amt, bucked_one_max - bucked_one_amt)
    illegal_pour = original_bucket == 'two' and bucked_two_amt - amount_to_pour == 0 and bucked_one_amt + amount_to_pour == bucked_one_max
    was_visited = any(pair[0] == bucked_one_amt + amount_to_pour and pair[1] == bucked_two_amt - amount_to_pour for pair in move_stack)
    if not (illegal_pour or was_visited or amount_to_pour <= 0):
        result = step([*move_stack, [bucked_one_amt + amount_to_pour, bucked_two_amt - amount_to_pour]],
            bucked_one_amt + amount_to_pour,
            bucked_two_amt - amount_to_pour,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Empty 1
    illegal_empty = (original_bucket == 'one' and bucked_two_amt == bucked_two_max)
    was_visited = any(pair[0] == 0 and pair[1] == bucked_two_amt for pair in move_stack)
    if not (illegal_empty or was_visited or bucked_one_amt == 0):
        result = step([*move_stack, [0, bucked_two_amt]],
            0,
            bucked_two_amt,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Empty 2
    illegal_empty = original_bucket == 'two' and bucked_one_amt == bucked_one_max
    was_visited = any(pair[0] == bucked_one_amt and pair[1] == 0 for pair in move_stack)
    if not (illegal_empty or was_visited or bucked_two_amt == 0):
        result = step([*move_stack, [bucked_one_amt, 0]],
            bucked_one_amt,
            0,
            original_bucket,
            bucked_one_max,
            bucked_two_max,
            goal)
        if result['moves'] >= 0:
            results.append(result)

    # Failure if we ran out of legal moves.
    if len(results) == 0:
        return {
            'moves': -1, 
            'goal_bucket': 'n/a', 
            'other_bucket': 0, 
        }

    # Return the result with the shortest path. Don't care
    # how a tie is sorted as we only really want the length.
    results.sort(key=lambda obj: obj['moves'], reverse=False)
    return results[0]