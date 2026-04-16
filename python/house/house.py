"""
Return sections of the nusery rhyme 'This is the house that Jack built.
"""

noun_verb = [
    {'noun': 'house that Jack built.', 'verb': '', 'extra': ''},
    {'noun': 'malt', 'verb': 'lay', 'extra': ''},
    {'noun': 'rat', 'verb': 'ate', 'extra': ''},
    {'noun': 'cat', 'verb': 'killed', 'extra': ''},
    {'noun': 'dog', 'verb': 'worried', 'extra': ''},
    {'noun': 'cow', 'verb': 'tossed', 'extra': ' with the crumpled horn'},
    {'noun': 'maiden', 'verb': 'milked', 'extra': ' all forlorn'},
    {'noun': 'man', 'verb': 'kissed', 'extra': ' all tattered and torn'},
    {'noun': 'priest', 'verb': 'married', 'extra': ' all shaven and shorn'},
    {'noun': 'rooster', 'verb': 'woke', 'extra': ' that crowed in the morn'},
    {'noun': 'farmer', 'verb': 'kept', 'extra': ' sowing his corn'},
    {'noun': 'horse', 'verb': 'belonged to', 'extra': ' and the hound and the horn'},
]

def recite(start_verse, end_verse):
    """Recite the nusery rhyme from start_verse to end_verse. Each line
    starts at the given verse and runs to the first
    - start_verse the verse to start with
    - end_verse if start_verse != end_verse all verses between start and end verse will be recited
    returns: A list of strings with each being one of the verses recited
    """
    ret = []
    for verse_set in range(start_verse - 1, end_verse):
        first = True
        lines = []
        for verse in range(verse_set, -1, -1):
            if first:
                first = False
                lines.append(f'This is the {noun_verb[verse]['noun']}{noun_verb[verse]['extra']}')
            elif verse == 0:
                lines.append(f'that {noun_verb[verse + 1]['verb']} in the house that Jack built.')
            else:
                lines.append(f'that {noun_verb[verse + 1]['verb']} the {noun_verb[verse]['noun']}{noun_verb[verse]['extra']}')
        ret.append(' '.join(lines))
    return ret
