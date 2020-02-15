def recite(start, take=1):
    """
    Recite versus to the song 99 Bottles of beer on the wall. You 
    can recide a single verse or a subset of verses.
    Parameters:
    * start: Verse to start on, must be an integer between 0 and 99.
    * take: Number of verses to return. Again an integer and should not take
    you past than the last verse. If you do not specify a value, it will
    default to 1.
    Returns:
    * A list of versus to the song. There are two lines per verse and if you return
    more than one verse a blank line between verses.
    Exceptions:
    * A ValueError will be raised if the start is outside of 0-99 or if 
    start and take goes past the last verse.
    """
    # Throw a value error if outside the desired range.
    if start >= 100 or start < 0 or (start - take + 1) < 0:
        raise ValueError('Verse outside of range.')

    # Dictionaries to hold word exceptions. I will use the dictionary.get
    # function with the verse number, and pass the regular usage if we are
    # not on an override.
    bottles = {1: 'bottle'}
    reference = {1: 'it'}
    remaining = {0: 'No more'}

    lines = []  # value to return
    for i in range(start, start - take, -1): # -1 at the end counts backwards.
        # If we are not on the starting verse add the extra line between verses.
        if i != start:
            lines.append('')
        
        # Convenience variables to reduce a bit of redundancy and improve readability.
        count = remaining.get(i, str(i))
        bottle = bottles.get(i, 'bottles')
        count_next = remaining.get(i - 1, str(i - 1))
        bottle_next = bottles.get(i - 1, 'bottles')
        bottle_ref = reference.get(i, 'one')

        # line 1        
        lines.append(f'{count} {bottle} of beer on the wall, {count.lower()} {bottle} of beer.')
        if i == 0:
            # Zero is a special case. The default is too long to want to try my dictionary trick
            # again so I am just going to return the special line with an if/else statement.
            lines.append('Go to the store and buy some more, 99 bottles of beer on the wall.')
        else:
            lines.append(f'Take {bottle_ref} down and pass it around, {count_next.lower()} {bottle_next} of beer on the wall.')
        
    # return the final result.
    return lines
