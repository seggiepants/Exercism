def find_anagrams(word, candidates):
    '''Return a list of words from the given candidates that are anagrams of the given word. 
    An anagram is a word that can be made from the letters in the given word. You have to use
    all of the letters and only those letters. A word is not an anagram of itself, and
    the test should be case insensitive.
    Parameters:
    word: String to find matching anagrams of
    candidates: List of strings, the words to compare to see if they are anagrams.
    '''
    # What I am going to do is lower case the word, turn it into a list and sort it. 
    # Then for each candidate word, do the same thing. If the words match, then it is
    # an anagram, or would be as long as we manage the case and make sure it isn't just
    # the word matching itself.
    #
    # I was about to do some letter frequency type of thing, but this is far simpler.
    # Should be faster too as we should spend more time in python's library functions instead
    # of looping through interpreted code.
    angram = sorted(word.lower()))
    return  [candidate for candidate in candidates if angram == sorted(candidate.lower()) and candidate.lower() != word.lower()]

