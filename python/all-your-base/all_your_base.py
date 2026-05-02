"""
Convert a list of digits from one base to another
"""

def rebase(input_base, digits, output_base):
    """
    Convert a list of digits from one base to another.
    :param input_base: The base of the input digits 2 = binary, 10 = decimal, etc.
    :param digits: List of digits forming the number in the input base. Must be 0 >= digit <= input_base
    :param output_base: The base of the output digits to return.
    :returns: List of digits in the desired base that equals the digits in the input base.
    :raises: ValueError if you give an base < 2 or if any of the given digits are not >= 0 and < input_base
    """
    if input_base < 2:
        raise ValueError('input base must be >= 2')
    
    if output_base < 2:
        raise ValueError('output base must be >= 2')
    
    has_bad_digits = len([digit for digit in digits if digit < 0 or digit >= input_base]) > 0
    if has_bad_digits:
        raise ValueError('all digits must satisfy 0 <= d < input base')
    
    total = 0

    # digits from input base
    for digit in digits:
        total = total * input_base
        total += digit

    if total == 0:
        return [0]
        
    result = []
    remaining = total

    while remaining > 0:
        remainder = remaining % output_base
        result.append(remainder)
        remaining = remaining - remainder
        remaining /= output_base
    
    result.reverse()
    return result
