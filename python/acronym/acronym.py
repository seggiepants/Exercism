def abbreviate(words):
    wordList = words.replace("_", "").replace("-", " ").upper().split()
    acronym = ""
    for word in wordList:
        if len(word) > 0:
            acronym = acronym + word[0]
    return acronym
