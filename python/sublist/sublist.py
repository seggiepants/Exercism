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

# Possible sublist categories.
# Change the values as you see fit.
SUBLIST = 0
SUPERLIST = 1
EQUAL = 2
UNEQUAL = 3

def sublist(list_one, list_two):
    """Check if one list is a subset, superset, equal to or not equal
    to the other.
    Parameters:
    * list_one: First list to check 
    * list_two: List to compare against list_one
    Returns:
    Enumerated constant of EQUAL if they have the same values, 
    UNEQUAL if neither is a subset of the other, SUBSET if list_one
    can be found in list_two, and SUPERSET if list_two can be found
    in list_one.
    """
    if list_one == list_two:
        return EQUAL
    else:
        if is_sublist(list_one, list_two):
            return SUBLIST
        elif is_sublist(list_two, list_one):
            return SUPERLIST
        else:
            return UNEQUAL

def is_sublist(list_one, list_two):
    """Helper function to test if list_one is a sub set of list_two
    Parameters:
    * list_one: List to see if it is contained within list_two
    * list_two: candidate list that may contain list_one within.
    Returns:
    - True if list_one can be found within list two
    - False if list_one is larger than list two or cannot be found in list two.
    """
    # Exit early if list one is bigger than list two, can't be a 
    # sublist by definition
    if len(list_one) > len(list_two):
        return False
    
    # step through the list looking for a match.
    for i in range(len(list_two) - len(list_one) + 1):
            if list_one == list_two[i:i + len(list_one)]:
                return True
    
    # If you got this far they don't match so return false.
    return False
