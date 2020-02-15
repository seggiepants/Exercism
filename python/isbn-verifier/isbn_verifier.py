import re

def is_valid(isbn):
    # Return false if we have an X followed by another X
    if re.search(r'X(?=.*?X)', isbn.upper()) != None:
        return False
    
    letters = re.sub(r'[ -]', '', isbn)
    digits = re.sub(r'[^\dX]', '', isbn.upper())
    
    # Check to see if we have non-digits in the string (outside of a trailing X)
    # Compare length of letters which only removed space and dash with the number
    # of digits.
    # If we don't have 10 characters for digits that is a fail state too.
    if len(letters) != len(digits) or len(digits) != 10:
        return False
    
    # There could still be a single X in the middle of the string
    # if so return false if we have an X not at the end of the string.
    x_position = digits.find('X')
    if x_position != -1 and x_position != len(digits) - 1:
        return False
    
    # Check to see if the digits add up properly.
    result = 0
    multiplier = 10
    for i in range(len(digits)):
        if digits[i] == 'X':
            value = 10
        else:
            value = int(digits[i])
        result += multiplier * value
        multiplier -= 1  
    return result % 11 == 0
