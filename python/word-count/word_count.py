import re

def count_words(sentence):
    """
    count_words
    returns a dictonary of each word in the given sentence where the 
    keys are the words and the values are the number of times it is repeated
    in a sentence. The comparison is case-insensitive. Results are returned
    in lower case.
    Word delimiters include spaces, commas, underscores, carriage return, line feed, and tabs.
    Two consecutive delimiters do not return an empty word in the sentence.
    """
    ret = {}
    for word in re.split(' |,|_|\r|\n|\t', sentence):
        filteredWord = filter_word(word.strip('\'').strip('"').lower())
        if len(filteredWord) > 0:
            if filteredWord in ret:
                ret[filteredWord] += 1
            else:
                ret[filteredWord] = 1
    return ret

def filter_word(word):
    """
    filter_word
    Removes junk punctuation from a word (or string).
    """
    filterChars = '!@#$%^&*():?<>[],.'
    ret = ''
    for char in word:
        if char not in filterChars:
            ret += char
    return ret
