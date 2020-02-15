def convert(number):
    """
    Convert a number to a raindrop sound.
    If the number doesn't have any of the numbers in the dictionary as a factor
    then return the number (as a string). Otherwise concatenate all of the 
    values for matching factors together and return that.
    """
    lookup = {
        3: 'Pling',
        5: 'Plang',
        7: 'Plong'
    }

    ret = ''
    for key, value in lookup.items():
        if number % key == 0:
            ret += value
    
    if ret == '':
        ret = str(number)
    
    return ret
