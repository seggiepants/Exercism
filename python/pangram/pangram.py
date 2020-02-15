def is_pangram(sentence):
    # Create a dictionary of letters with zero as inital counts
    letter_counts = {}
    for i in range(ord('a'), ord('z') + 1):
        letter_counts[chr(i)] = 0
    
    # Increment counter for every letter in the sentence that is in the
    # letter_counts dictionary.
    for ch in sentence.lower():
        if ch in letter_counts:
            letter_counts[ch] += 1
    
    # Use the filter function to return entries with a count of zero
    # Then turn that into a list and check if the length is > 0 to get
    # our return value. Not sure if shipping this out to python library 
    # code is faster than an early exit if found, but for 26 entries, it 
    # probably should be.
    return len(list(filter(lambda x: x == 0, letter_counts.values()))) == 0

