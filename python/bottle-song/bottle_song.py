"""
Recite the bottle song a variant of 100 bottles of beer on the wall
"""

numbers = ["no", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten"]

def recite(start, take=1):
    """
    Recite the lyrics to the bottle song a variant of 100 bottles of beer on the wall.
    :param start: which verse to start at
    :param take: how many verses to return
    :returns: An array of strings where each string is a line of a verse with an empty string between verses.
    """
    ret = []
    first = True
    for verse_number in range(start, start - take, -1):
        if not first:
            ret.append("")
        ret.extend(verse(verse_number))
        first = False
    return ret

def verse(number):
    """
    Calculate the text of a single verse of the bottle song
    :param number: The verse to compute
    :returns: text of the verse as a list"""
    bottles = "bottles"
    if number == 1:
        bottles = "bottle"
    bottles_next = "bottles"
    if number - 1 == 1:
        bottles_next = "bottle"

    return (f"{numbers[number].title()} green {bottles} hanging on the wall,\n" + \
        f"{numbers[number].title()} green {bottles} hanging on the wall,\n" + \
        "And if one green bottle should accidentally fall,\n" + \
        f"There'll be {numbers[number - 1]} green {bottles_next} hanging on the wall.").split("\n")