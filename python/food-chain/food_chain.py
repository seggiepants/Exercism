"""
Recite the nursery rhyme: I know an old lady who swallowed a fly.
"""

ANIMAL_ORDER = {"fly", "spider", "bird", "cat", "dog", "goat", "cow", "horse"}

SPIDER_EXTRA = " that wriggled and jiggled and tickled inside her"
LAST_LINE = "I don\'t know why she swallowed the fly. Perhaps she\'ll die."

ANIMALS = [
    {
        "name": "fly",
        "comment": LAST_LINE,
    },
    {
        "name": "spider",
        "comment": "It wriggled and jiggled and tickled inside her.",
    },
    {
        "name": "bird",
        "comment": "How absurd to swallow a bird!",
    },
    {
        "name": "cat",
        "comment": "Imagine that, to swallow a cat!",
    },
    {
        "name": "dog",
        "comment": "What a hog, to swallow a dog!",
    },
    {
        "name": "goat",
        "comment": "Just opened her throat and swallowed a goat!",
    },
    {
        "name": "cow",
        "comment": "I don't know how she swallowed a cow!",
    },
    {
        "name": "horse",
        "comment": "She's dead, of course!",
    },
]

def recite(start_verse, end_verse):
    """
    Recite one or more verses of the nusery rhyme - I know an old lady who swalled a fly
    :param start_verse: one based index of the verse to start with (1-8)
    :param end_verse: one based index of the verse to stop on (1-8) should be >= start_verse
    :returns: list of strings with the lines of the nursery rhyme with empty lines between verses
    """
    ret = []
    for index in range(start_verse, end_verse + 1):
        if index > start_verse:
            ret.append("")
        ret.extend(verse(index - 1))
    return ret
    

def verse(index):
    """
    Recite a single verse of the nusery rhyme - I know an old lady who swallowed a fly
    running through the preceding animal chain.
    :param index: zero based index of the verse to recite.
    :returns: list of string with the lines for that verse.
    """
    ret = []
    current = ANIMALS[index]["name"]
    ret.append(f"I know an old lady who swallowed a {current}.")
    ret.append(ANIMALS[index]["comment"])

    if index > 0 and index != len(ANIMALS) - 1:
        for animal_idx in range(index - 1, -1, -1):
            previous = current 
            current = ANIMALS[animal_idx]["name"]
            extra = SPIDER_EXTRA if current == "spider" else ""            
            ret.append(f"She swallowed the {previous} to catch the {current}{extra}.")
        ret.append(LAST_LINE)

    return ret
