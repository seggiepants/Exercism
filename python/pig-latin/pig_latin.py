from functools import reduce

consonants = ['b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z']
vowels = ['a', 'e', 'i', 'o', 'u', 'y']

# **Rule 4**: If a word contains a "y" after a consonant cluster or as the second letter in a two letter word it makes a vowel sound (e.g. "rhythm" -> "ythmrhay", "my" -> "ymay").
# otherwise it gets processed incorrectly.
#
# This mandates excluding 'y' from this list.
vowel_sounds = ['a', 'e', 'i', 'o', 'u', 'xr', 'yt']

def translate(text):
    """Translate the given text string to Pig Latin.
    Parameters:
    text: A string with the text to translate. Should be lower case and 
    any words in a phrase separated by whitespace.
    Returns:
    Pig Latin version of the text input.
    Problems:
    * Does not handle upper case words, punctuation, whitespace between
    words is not preserved.
    """
    # List comprehension splits the word on space boundaries and calls
    # translate_word on each section. The resulting list is then glued
    # back into a string with a space between each word.
    return ' '.join([translate_word(word) for word in text.split()])

def translate_word(text):
    """Translate a single word into Pig-Latin
    Parameters:
    * text: The word to translate
    Returns
    * translated version of the input word.    
    """
    # **Rule 1**: If a word begins with a vowel sound, add an "ay" sound to the end of the word. Please note that "xr" and "yt" at the beginning of a word make vowel sounds (e.g. "xray" -> "xrayay", "yttria" -> "yttriaay").
    # reduce call checks if the text starts with anything in vowel_sounds. If anything is true it will
    # return true due to or-ing everything together.
    if reduce(lambda x, y: x or text.lower().startswith(y), vowel_sounds, False):
        result = text + 'ay'
    else:
        # **Rule 2**: If a word begins with a consonant sound, move it to the end of the word and then add an "ay" sound to the end of the word. Consonant sounds can be made up of multiple consonants, a.k.a. a consonant cluster (e.g. "chair" -> "airchay").
        index = 1
        while index < len(text) and (text[index] in consonants or (index == 0 and text[index] == 'y')):
            index += 1
        
        # **Rule 3**: If a word starts with a consonant sound followed by "qu", move it to the end of the word, and then add an "ay" sound to the end of the word (e.g. "square" -> "aresquay").
        if index > 0 and index < len(text) and text[index] == 'u' and text[index - 1] == 'q' :
            index += 1
                
        result = text[index:] + text[0:index] + 'ay'   
    return result
