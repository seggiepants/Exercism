import os   # for shelling out to text to speech

# string representation for items in the ones position.
ones = {
    0: 'zero'
    , 1: 'one'
    , 2: 'two'
    , 3: 'three'
    , 4: 'four'
    , 5: 'five'
    , 6: 'six'
    , 7: 'seven'
    , 8: 'eight'
    , 9: 'nine'
}

# string representation for items in the tens position.
tens = {
    0: ''
    , 1: 'ten' # see teens [0]
    , 2: 'twenty'
    , 3: 'thirty'
    , 4: 'forty'
    , 5: 'fifty'
    , 6: 'sixty'
    , 7: 'seventy'
    , 8: 'eighty'
    , 9: 'ninety'
}

# special case string representation for numbers 10-19
teens = {
    0: ''
    , 1: 'eleven'
    , 2: 'twelve'
    , 3: 'thirteen'
    , 4: 'fourteen'
    , 5: 'fifteen'
    , 6: 'sixteen'
    , 7: 'seventeen'
    , 8: 'eighteen'
    , 9: 'nineteen'
}

# Higher order groupings for digits in pairs of three.
groupings = {
    0: ''
    , 1: 'thousand'
    , 2: 'million'
    , 3: 'billion'
}

# text to speech options. Configure for your system before use. May not
# work if you don't have espeak-ng installed.
use_text_to_speech = False
text_to_speech_cmd = r'c:\\\"program files\eSpeak NG"\espeak-ng'

def say(number):
    """
    Return a english text spoken version of the input number.
    Parameters:
    * number - The number to build a string for.
    Returns:
    * US english string version of the given number as a spoken word phrase.
    Note:
    * Only handles integer numbers between 0 and 999,999,999,999
    * Does not handle negative numbers or floating point numbers.
    """
    result = ''
    group_index = 0

    # turn the digits into a list of single digit numbers
    digits = [int(digit) for digit in str(number)]

    # check for too many digits. Raise a Value Error if there is a problem.
    # I added in a check for floating point and negative numbers and note
    # that they aren't supported.
    if len(digits) > (len(groupings) * 3):
        raise ValueError(f'number is larger than allowed value ({number})')
    elif number != round(number):
        raise ValueError('Only integer values are supported.')
    elif number < 0:
        raise ValueError('Only positive numbers are supported.')

    # Only want a zero if we have a single grouping only.
    return_zero = len(digits) <= 3

    # will operate on digits up to three at a time until we run out.
    while len(digits) > 0:
        # break out the ones, tens, and hundreds place. User zero if
        # we run out of digits.
        if len(digits) >= 1:
            one = digits.pop()
        else:
            one = 0
        
        if len(digits) >= 1:
            ten = digits.pop()
        else:
            ten = 0
        
        if len(digits) >= 1:
            hundred = digits.pop()
        else:
            hundred = 0

        # get the phrase for the current three digit group.
        section = say_helper(hundred, ten, one, return_zero)

        # get the current group name.
        group = groupings[group_index]
        if len(group) == 0:
            # if we have not group, just return the section.
            result = section
        elif len(section) > 0:
            if len(group) > 0:
                # add leading space if not blank
                group = ' ' + group
            if len(result) > 0:
                # add trailing space if we have more to add on.
                group = group + ' '
            
            # add current grouping before the result so far.
            result = section + group + result
        
        # switch to the next highest grouping.
        group_index += 1

    # say the phrase by shelling out to the text to speech command
    # if you have it configured. I really should put this in a 
    # json object and pass it in or something.
    if use_text_to_speech:
        os.system(text_to_speech_cmd + ' "' + result + '"')

    return result

def say_helper(hundred, ten, one, return_zero):    
    """
    Return the english phrasing of an up to three digit number.
    Parameters:
    hundred: value 0-9 in the hundreds place.
    ten: value 0-9 in the tens place.
    one: value 0-9 in the ones place.
    Returns:
    string version of the input digits in english spoken phrasing.
    Note:
    hundred, ten, and one must be a single digit number between 0 and 9
    and may not be negative. Decimal points are not supported.
    """
    result = ''

    # how many digits do we really have.    
    if hundred != 0:
        num_digits = 3
    elif ten != 0:
        num_digits = 2
    else:
        num_digits = 1

    # hundreds pace
    if num_digits == 3:
        result += ones[hundred] + ' hundred'
        # only add and if we have more to append.
        if ten != 0 or one != 0:
            result += ' and '
    
    if num_digits >= 2:
        # skip if we have nothing in the tens place.
        if ten != 0:
            # 1 in the tens place is a special case.
            if ten == 1:
                # this part handles 10 if one = 0, and the 
                # teen values if 1 to 9.
                if one == 0:
                    result += tens[ten]
                else:
                    result += teens[one]
            else:
                # add on the tens place twenty to ninet
                result += tens[ten]
                # if we have a digit in the ones place add a dash.
                if one != 0:
                    result += '-'

    # handle the ones place
    if num_digits >= 1:
        # check to see if we should skip this digit.
        skipFlag = False

        # skip the digit if 10 to 19
        if num_digits >= 2 and ten == 1:
            skipFlag = True
        
        # skip the digit if a multiple of ten.
        if one == 0 and num_digits != 1:
            skipFlag = True
        
        # skip the digit if we are just 0 and return_zero flag isn't set.
        if one == 0 and num_digits == 1 and return_zero == False:
            skipFlag = True
        
        if not skipFlag:
            result += ones[one]
    
    return result
