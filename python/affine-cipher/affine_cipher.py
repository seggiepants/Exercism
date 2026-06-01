"""
Affine Cipher Implementation
Why yes I did make the bulk of the implementation into a large unreadable list-comprehension just because I could.
"""


ALPHABET = 'abcdefghijklmnopqrstuvwxyz'
ALPHABET_LEN = len(ALPHABET)
ALLOWED_CHARS = ALPHABET + '0123456789'

def encode(plain_text, a, b):
    """
    Encode a plain text value with keys a and b
    :param plain_text: The text to encrypt
    :param a: The first encoding key. Must be co-prime to size of encoding alphabet (26 for a-z)
    :param b: The second encoding key. Neither a or b may be a multiple of each other (3 and 6 would be bad because 6=3*2, but 5, 6 is ok)
    :returns: Encrypted text broken into batches of length 5 with spaces inbetween.
    :raises: ValueError if no Modular Multiplicative Inverse is found.
    """# E(x) = (ai + b) mod m
    mmi_value = mmi(a, ALPHABET_LEN)
    return group(''.join([ALPHABET[(a * ALPHABET.find(char) + b) % ALPHABET_LEN] 
                          if char in ALPHABET else char for char in 
                          [char for char in plain_text.lower() if char in ALLOWED_CHARS]]))

def decode(ciphered_text, a, b):
    """
    Decode a ciphered text that was generated with keys a and b
    :param ciphered_text: The encrypted text
    :param a: The first encoding key. Must be co-prime to size of encoding alphabet (26 for a-z)
    :param b: The second encoding key. Neither a or b may be a multiple of each other (3 and 6 would be bad because 6=3*2, but 5, 6 is ok)
    :returns: Decrypted text.
    :raises: ValueError if no Modular Multiplicative Inverse is found.
    """
    # D(y) = (a^-1)(y - b) mod m
    mmi_value = mmi(a, ALPHABET_LEN)
    return ''.join([ALPHABET[(mmi_value*(ALPHABET.find(char) - b)) % ALPHABET_LEN] 
                    if char in ALPHABET else char for char in 
                    [char for char in ciphered_text.lower() if char in ALLOWED_CHARS]])

def group(text, size = 5):
    """Return a string broken up into chunks of size with spaces inbetween
    :param text: the string to break into chunks
    :param size: the desired size of the chunks (default is 5)
    :returns: copy of the input string with a space inserted after every size characters
    """
    return ' '.join([(text[i:i+size]) for i in range(0, len(text), size)])

def mmi(a, m):
    """
    Find the Modular Multiplicative Inverse of A and M
    :param a: One of the keys
    :param m: The size of the key alphabet
    :returns: Value X such that a*x % m == 1 
    :raises: Value error if no match found.
    """
    for i in range(1, m + 1):
        if (a * i) % m == 1:
            return i
    raise ValueError('a and m must be coprime.')
