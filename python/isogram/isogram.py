def is_isogram(string):
    """
    Isogram
    An isogram is a word or phase that doesn't repeat a letter.
    We are only checking in a case-insensitive manner and are allowing
    both space (' ') and dash (-) to repeat.
    """
    characterSet = set()
    exclusionSet = set([' ', '-'])

    for char in string.upper():
        if char not in exclusionSet:
            if char in characterSet:
                return False
            else:
                characterSet.add(char)

    return True
