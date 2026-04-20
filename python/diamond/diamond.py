"""
Diamond - Generate a diamond pattern for a given letter with 
'A' at the top. Subsequent letters until reaching the given
letter at the middle. Then decreasing back to the 'A' at the bottom point.
"""

def rows(letter):
    """
    Generate a diamond of rows from A to letter and back to A. A to letter should have increasing spaces
    between the letters and letter - 1 back to A should have decreasing spaces to make a diamond pattern.
    The space character should fill non-letter positions in the string. 
    :param letter: The letter and the maximum width of the diamond must be between 'A' and 'Z'
    :returns: List of strings forming the diamond from A -> letter and back to 'A' (don't repeat letter)
    :raises ValueError: If letter is not a value between 'A' and 'Z' inclusive.
    """
    if letter < 'A' or letter > 'Z':
        raise ValueError(f'Letter must be between \'A\' and \'Z\', found: \'{letter}\'.')
    rune = 'A'
    width = (2 * (ord(letter) - ord('A'))) + 1
    results = []
    while rune <= letter:
        distance = ord(rune) - ord('A')
        distance = (2*distance) - 1

        results.append(helper(width, distance, rune))       
        rune = chr(ord(rune) + 1)
    
    rune = chr(ord(rune) - 2)

    while rune >= 'A':
        distance = ord(rune) - ord('A')
        distance = (2*distance) - 1

        results.append(helper(width, distance, rune))        
        rune = chr(ord(rune) - 1)

    return results

def helper(width, distance, rune):
    """
    Compute the row string for a given width, distance between letters and letter.
    :param max_distance: The total width of the row to generate
    :param distance: The number of characters to put between the left and right copies of rune in the string if distance is zero only one rune is written
    :param rune: The rune to print in the string
    :return: Computed string based on the given parameters
    """
    if distance <= 0:
        left = (width - 1) // 2
        right = width - left - 1
        return ' ' * left + rune + ' ' * right
    left = (width - distance - 2) // 2
    right = width - left - distance - 2
    return ' ' * left + rune + ' ' * distance + rune + ' ' * right
