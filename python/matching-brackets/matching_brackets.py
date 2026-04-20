"""
Check if a string has matching brackets
"""

bracket_open = ['(', '[', '{']
bracket_close = [')', ']', '}']
bracket_match = {
    ')': '(',
    ']': '[',
    '}': '{',
}
def is_paired(input_string):
    """
    Check if a string has brackets (parenthesis, braces, curly braces) that are
    matched and nested correctly.
    input_string: The string to check the brackets of
    returns: True if matched and False if not.
    """
    stack = []
    for rune in input_string:
        if rune in bracket_open:
            stack.append(rune)
        
        if rune in bracket_close:
            if len(stack) == 0:
                return False
            bracket_start = stack.pop()
            if bracket_start != bracket_match[rune]:
                return False
        
    return len(stack) == 0
    
