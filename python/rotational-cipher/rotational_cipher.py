def rotate(text, key):
    """
    Encrypt a given text value by replacing each character with another 
    character that is key spaces away in the alphabet. If you go past Z
    you should wrap around to A again. This is case sensitive so we will handle
    A-Z, and a-z separately. Characters that are not in the alphabet will be
    passed through to the output unchanged.

    For example ABC rotate 4 is EFG. 

    parameters:
    text: string, the value to encrypt
    key: number, is the offset
    """
    cipher = ''         # return value
    capA = ord('A')     # code for capital A (only want to compute once)
    capZ = ord('Z')     # code for captial Z
    lowerA = ord('a')   # code for lower case A
    lowerZ = ord('z')   # code for lower case Z

    for ch in text:
        code = ord(ch)
        if code >= capA and code <= capZ:
            code = code + key
            while code > capZ:
                code -= (capZ - capA + 1)
            cipher += chr(code)            
        elif code >= lowerA and code <= lowerZ:
            code = code + key
            while code > lowerZ:
                code -= (lowerZ - lowerA + 1)
            cipher += chr(code)
        else:
            cipher += ch
    
    return cipher


