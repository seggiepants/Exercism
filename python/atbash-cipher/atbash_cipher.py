import string

# Helper dictionary that maps a character for encoding/decoding
# since this is such a weak cipher you can use the same dictionary for
# both cases.
encode_lookup = dict(zip(string.ascii_lowercase, string.ascii_lowercase[::-1]))

def encode(plain_text, group_size = 5):
    """encode some plain text using the Atbash Cipher.
    Basically punctuation and whitespace is removed and any alphabetic characters
    are replaced with their version from a reversed copy of the alphabet.
    Non-alphabetic characters that are not punctuation or white space are retained 
    without being modified. 
    The final output is broken into groups of group_size characters with a 
    space in-between. group_size is defaulted to 5 if not passed in.
    Parameters:
    * plain_text - The text to be encoded (will be changed to lower case with 
      punctuation and whitspace removed).
    * group_size - Number of characters in an output grouping.
    Returns:
    * The encoded value for the input string.
    """
    characters = [encode_lookup[ch] if ch in encode_lookup else ch for ch in ''.join(filter(lambda x: x not in string.punctuation + string.whitespace, plain_text.lower()))]
    result = ''
    for i in range(0, len(characters), group_size):
        result = result + ''.join(characters[i:i + group_size]) + ' '
    return result.strip()


def decode(ciphered_text):
    """Decode cipher text encoded with the atbash cipher. Note that any 
    whitespace or punctuation from the original was lost during encoding. Also
    characters that are not alphabetic will pass through unencoded.
    Parameters:
    * ciphered_text - The encoded text to decode.
    Returns:
    * Decoded version of the ciphered_text.
    """
    characters = [encode_lookup[ch] if ch in encode_lookup else ch for ch in ''.join(filter(lambda x: x not in string.punctuation + string.whitespace, ciphered_text.lower()))]
    return ''.join(characters)
