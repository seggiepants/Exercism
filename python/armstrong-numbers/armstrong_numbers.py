def is_armstrong_number(number):
    """
    Check to see if the given number is an Armstrong Number. 
    An armstrong number is a number that the sum of its digits 
    each raised to the power of the number of digits in the number
    is equal to the number.
    For example
    * 9 is an Armstrong number, because 9 = 9^1 = 9
    * 10 is *not* an Armstrong number, because 10 != 1^2 + 0^2 = 1
    * 153 is an Armstrong number, because: 153 = 1^3 + 5^3 + 3^3 = 1 + 125 + 27 = 153

    Parameters;
    number: Should be a postive integer.
    """
    
    if int(number) != number:
        # I don't think a floating point number can be an armstrong number
        # so I am going to return false
        return False

    if number < 0:
        # I don't know what to do with the minus sign. So I am
        # calling it false, couldn't see anything on wikipedia 
        # about negative numbers so I an executive decision and 
        # saying it isn't allowed.
        return False

    if number == 0:
        # I don't think my code handles this case otherwise.
        # I consider 0^1 = 0 which is Armstrong in my book.
        return True
    
    # used to store the digits, I decided I didn't want to do any
    # string manipulation on a number. Using modulus math instead.
    digits = []

    # use a different variable so we can compare against the original value
    num = number 
    while num != 0:
        digit = num % 10
        digits.append(digit)
        num = (num - digit) // 10 # Looks like // does integer division.
    
    num_digits = len(digits)
    running_total = 0
    for digit in digits:
        running_total += digit ** num_digits
    
    return running_total == number
