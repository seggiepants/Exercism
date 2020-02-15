def reverse(text):
    # I really could just use the built in reverse function
    # if we convert the text to a list. Something like:
    # temp = list(text)
    # temp.reverse()
    # return ''.join(temp)
    #
    # Looking at the solutions of others, I also could have just done
    # return text[::-1]
    # I always seem to have a blind spot for the indexers
    #
    # But that seems to defeat the point of the exercise, so lets do it
    # manually. We have a left and right side and meet in the middle.
    left = ''
    right = ''
    left_idx = 0
    right_idx = len(text) - 1
    while left_idx <= right_idx:
        if left_idx == right_idx:
            # we have a middle to add in
            # only put it on one side or the other. I chose the left side.
            left += text[left_idx]
        else:
            # Operate normally, trick is right needs to be appended in reverse.
            right = text[left_idx] + right
            left += text[right_idx]
        left_idx += 1
        right_idx -= 1
    return left + right
