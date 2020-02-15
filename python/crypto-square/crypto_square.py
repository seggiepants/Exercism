import math
import string

def cipher_text(plain_text):
    """Implementation of the crypto-square cipher. Attempts to make a 
    (mostly) square grid out of characters in a string, and transposes
    them before returning the output.
    Parameters:
    * plain_text: The text to encode
    Returns:
    * encoded version of the input parameter plain_text
    """
    text = sanitize_text(plain_text)
    
    # Exit early if we have an empty string, as we
    # don't want a divide by zero error.
    if len(text) == 0:
        return ''
    
    # math.ceil to round up always. It is better to have
    # too many cells instead of too few.
    cols = math.ceil(math.sqrt(len(text)))
    rows = math.ceil(len(text) / cols)

    # add spaces to bring the string to cols*rows characters
    text += ' ' * ((cols * rows) - len(text))

    # For each column make a string. Each string is a character from
    # the string skipping column characters for each step.
    return ' '.join(text[i::cols] for i in range(cols))

def sanitize_text(plain_text):
    """Clean up the given text by removing spaces and punctuation
    Parameters:
    * plain_text: The string to sanitize
    Retuns:
    * Copy of the input plain_text but with spaces and punctuation as 
    defined by the string library removed.
    """
    trantab = str.maketrans('', '', string.punctuation + string.whitespace)
    return plain_text.lower().strip().translate(trantab)
