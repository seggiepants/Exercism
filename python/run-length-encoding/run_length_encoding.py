
def decode(string):
    """
Decodes an character string (no digits in the output) that is run
length encoded. Before any run is a decimal integer count of how many
times a character should repeat, followed by the character to repeat.
Characters not to be run length encoded are written out without a counter.
Parameters:
- string: value to decode.

Returns:
- Decoded version of the input string.
    """

    ret = ''
    digit = 0
    for ch in string:
        if ch.isdigit():
            # start saving the digit.
            # if you get multiple places we want to multiply by 10 each time
            # to move it over a decimal place.
            digit = (digit * 10) + int(ch)
        else:
            # If we have a digit write it out that many times. Otherwise
            # there was nothing to run length encode so just write the character.
            if digit != 0:
                ret += ch * digit
                digit = 0
            else:
                ret += ch
    
    return ret


def encode(string):
    """
Encodes an character string (no digits in the input) with run length
encoding. Any repeating characters will be written out as a count of 
repetitions followed by the character to repeat. Character that do not
repeat are written out as-is.
Parameters:
- string: value to encode.

Returns:
- Encoded version of the input string.
    """

    ret = ''
    previous = ''
    duplicate_count = 1
    # compare to previous character. If the same save up duplicate count
    # if different write out the count and character, then reset for the
    # next run.
    for ch in string:
        if ch == previous and previous != '':
            duplicate_count += 1
        else:
            if duplicate_count > 1:
                ret += str(duplicate_count) + previous
            else:
                ret += previous
            duplicate_count = 1
        previous = ch
    
    # finish up the last character.
    if duplicate_count > 1:
        ret += str(duplicate_count) + previous
    else:
        ret += previous
        
    return ret
