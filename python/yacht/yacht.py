import collections

"""
This exercise stub and the test suite contain several enumerated constants.

Since Python 2 does not have the enum module, the idiomatic way to write
enumerated constants has traditionally been a NAME assigned to an arbitrary,
but unique value. An integer is traditionally used because it’s memory
efficient.
It is a common practice to export both constants and functions that work with
those constants (ex. the constants in the os, subprocess and re modules).

You can learn more here: https://en.wikipedia.org/wiki/Enumerated_type
"""


# Score categories.
# Change the values as you see fit.
YACHT = 0
ONES = 1
TWOS = 2
THREES = 3
FOURS = 4
FIVES = 5
SIXES = 6
FULL_HOUSE = 7
FOUR_OF_A_KIND = 8
LITTLE_STRAIGHT = 9
BIG_STRAIGHT = 10
CHOICE = 11

def getDigit(category):
    if category == ONES:
        return 1
    elif category == TWOS:
        return 2
    elif category == THREES:
        return 3
    elif category == FOURS:
        return 4
    elif category == FIVES:
        return 5
    elif category == SIXES:
        return 6
    else:
        raise Exception('Invalid category.')
    
def score(dice, category):
    # dice = list of dice face numbers (1-6)
    # category = one of the above "enumerations"

    # Yes for ONES through SIXES I could have done this simpler by abusing 
    # that I know what category's value is. Wanted to be clean so to speak.
    if category in (ONES, TWOS, THREES, FOURS, FIVES, SIXES):
        # Sum up all the digits of the desired value
        digit = getDigit(category)
        return sum(list(filter((lambda x: x == digit), dice)))
    elif category == FULL_HOUSE:
        # Sum of dice if you have 3 of one and 2 of another.
        frequency = collections.Counter(dice)
        three = [key for key, value in frequency.items() if value == 3]
        two = [key for key, value in frequency.items() if value == 2]
        if len(three) >= 1 and len(two) >= 1:
            if (three[0] != two[0]):
                return sum(dice)
            else:
                return 0
        else:
            return 0
    elif category == FOUR_OF_A_KIND:
        # 4 * dice number for four of the same number
        frequency = collections.Counter(dice)
        result = [key * 4 for key, value in frequency.items() if value >= 4]
        if len(result) == 0:
            return 0
        else:
            return result[0]
    elif category == LITTLE_STRAIGHT:
        # 30 points for having 1-5 in the rolls.
        if 1 in dice and 2 in dice and 3 in dice and 4 in dice and 5 in dice:
            return 30
        else:
            return 0
    elif category == BIG_STRAIGHT:
        # 30 points for having 2-6 in the rolls.
        if 2 in dice and 3 in dice and 4 in dice and 5 in dice and 6 in dice:
            return 30
        else:
            return 0
    elif category == CHOICE:
        # Just sum up all the dice amounts.
        return sum(dice)
    elif category == YACHT:
        # five of a kind.
        frequency = collections.Counter(dice)
        result = [key for key, value in frequency.items() if value >= 5]
        if len(result) == 0:
            return 0
        else:
            return 50
    else:
        # Invalid category.
        raise Exception('Invalid category.')
