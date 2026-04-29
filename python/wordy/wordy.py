"""
Calculate the answer for conversational match questions
"""
import re

OPERATORS = {
    'plus': '+',
    'minus': '-',
    'multiplied': '*',
    'divided': '/',
    'cubed': '**',
}

INVALID_OPERATORS = ['**']
SYNTAX_ERROR = 'syntax error'
UNKNOWN_OPERATION = 'unknown operation'

def answer(question):
    """
    Compute the result of the given math question
    :param question: The expression to evaluate
    :returns: The computed value (Number)
    """
    is_num = r'[+|-]?[0-9]+\.?[0-9]*'
    is_whitespace = r'[^\w-]+'

    # Chop into words
    words = re.split(is_whitespace, question)

    # Match words and parse into tokens
    tokens = []
    for word in words:
        match = re.match(is_num, word)
        if match:
            tokens.append(int(word))
        elif word in OPERATORS:
            tokens.append(OPERATORS[word])
    
    # Evaluate the tokens    
    ret = 0
    operation = '+'
    for token in tokens:
        if isinstance(token, int):
            if operation == '':
                raise ValueError(SYNTAX_ERROR)
            ret = compute(operation, ret, token)
            operation = ''
        elif token in OPERATORS.values():
            if operation != '':
                raise ValueError(SYNTAX_ERROR)
            operation = token
    
    # Check for incomplete operation
    if operation != '':
        if operation in INVALID_OPERATORS:
            raise ValueError(UNKNOWN_OPERATION)
        raise ValueError(SYNTAX_ERROR)
        
    return ret

def compute(operation, accumulator, value):
    """
    Compute a binary math operation (+,-,*,/) for two numbers.
    :param operation: operation to perform (+, -, *, /)
    :param accumulator: left side of the binary operation
    :param value: right side of the binary operation
    :returns: computed value of accumulator operation value (1 + 2 would be 3 for example)
    :raises: Value Error if an unsupported operation
    """
    if operation == '+':
        return accumulator + value
    if operation == '-':
        return accumulator - value
    if operation == '*':
        return accumulator * value
    if operation == '/':
        return accumulator / value
    raise ValueError(UNKNOWN_OPERATION)
