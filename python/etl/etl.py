def transform(legacy_data):
    """Given a scrabble score table in the old format of a dictionary with
    the key being the score and the value being a list of upper case
    characters which recieve that score. Transform the data into the new 
    format which is keyed on the lower case version of the character with a
    value of the score.
    For example {1: ['A', 'B', 'C']} -> {'A': 1, 'B': 1, 'C': 1}
    Parameters:
    * legacy_data: Dictionary object of data in the old scrabble score format
    Returns:
    * Dictionary object of the legacy data in the new scrabble score format.
    """
    result = {}
    for score, letters in legacy_data.items():
        for letter in letters:
            result[letter.lower()] = score
    return result
