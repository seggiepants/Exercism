from functools import reduce

# Data table to map 0-9 in different powers of ten
# from arabic numbers (6) to Roman Numerals (VI)
map_numerals = {
    # 1 to 10 in Roman Numerals
    0: '', 1: 'I', 2: 'II', 3: 'III', 4: 'IV', 
    5: 'V', 6: 'VI', 7: 'VII', 8: 'VIII', 9: 'IX',
    # 10's in Roman Numerals
    10: 'X', 20: 'XX', 30: 'XXX', 40: 'XL', 50: 'L', 
    60: 'LX', 70: 'LXX', 80: 'LXXX', 90: 'XC',
    # 100's in Roman Numerals
    100: 'C', 200: 'CC', 300: 'CCC', 400: 'CD', 500: 'D', 
    600: 'DC', 700: 'DCC', 800: 'DCCC', 900: 'CM',
    # 1,000's in Roman Numerals
    1000: 'M', 2000: 'MM', 3000: 'MMM'
}

def roman(number):
    """
    Return a given number in Roman numeral format.
    Parameters:
    * number: The number to convert to Roman numeral. This must be an
    integer value between 1 and 3,000. 
    Returns:
    * string, the input number expressed as a Roman Numeral
    Errors:
    * Will throw an error for numbers zero or less or greater than 3,000
    """
    # Throw a value error if we are outside of allowed range.
    if number > 3000 or number < 1:
        raise ValueError(f'Number ({number}) is outside the allowed range.')
    
    # Split the digits into a list of digits.
    digits = [int(x) for x in str(number)]
    
    # Reduce glues together all of the intermediate values.
    # Enumerate will give you the index and value in an iterable (string, list, etc.)
    # I am using the 10 ** number format to get a power of ten.
    # With all that in place, I just need to look up a digit in the mapping dictionary.
    return reduce(lambda x, y: x + y, [map_numerals[y * (10 ** (len(digits) - 1 - x))]  for x, y in enumerate(digits)], '')

