import random
import string

class Cipher(object):
    """Class to encode/decode text (just letters a-z) using a 
    modified version of the caesar cipher but with a key where
    the code can change for each letter in the encode/decode string.
    """
    def __init__(self, key=None):
        """Constructor, saves or sets up the cipher key
        Parameters:
        * key: What to use as the cipher key. If not specified will
        generate a random key.
        """
        if key == None:
            self.key = self.randomKey()
        else:
            self.key = key

    def encode(self, text):
        """Encode a message based on the key that was given/generated
        when the object was created.
        Parameters:
        * text: The message to encode.
        Returns:
        * Encoded version of the input string (text).
        """
        # Calculate the index into the lower case letters for both the 
        # current character of the string and the current character of the
        # key (can wrap around if text is longer than than the key). Add them
        # together (being careful to wrap around the other size of the alphabet
        # if we go past z). Then turn it back into a letter. Repeat for all 
        # characters in a a string and glue them together and return them.
        return ''.join([chr(ord('a') + ((ord(ch) - ord('a')) + (ord(self.key[i % len(self.key)]) - ord('a'))) % len(string.ascii_lowercase)) for i, ch in enumerate(text.lower())])

    def decode(self, text):
        """Decode a message based on the key that was given/generated
        when the object was created.
        Parameters:
        * text: The message to decode.
        Returns:
        * Decoded version of the input string (text).
        """
        # Calculate the index into the lower case letters for both the 
        # current character of the string and the current character of the
        # key (can wrap around if text is longer than than the key). Subtract
        # the key from the text. Then turn it back into a letter. 
        # Repeat for all characters in a a string and glue them together and 
        # return them.
        return ''.join([chr(ord('a') + ((ord(ch) - ord('a')) - (ord(self.key[i % len(self.key)]) - ord('a'))) % len(string.ascii_lowercase)) for i, ch in enumerate(text.lower())])
    
    def randomKey(self, length=100):
        """generate a key by randomly choosing character from the lower case
        letters (a-z) until you get to the specified length.
        Parameters:
        * length: Length of the desired string, 100 if not specified
        Returns:
        * Random letter string of the length provided.
        """
        # use random.choice to grab a random lowercase letter, fill a list
        # with the desired count of random letters then convert it into a string
        # and return the final value.
        return ''.join([random.choice(string.ascii_lowercase) for i in range(length + 1)])
