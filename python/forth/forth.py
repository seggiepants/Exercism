"""
Simple interpreter for a subset of Forth
"""
import re

class StackUnderflowError(Exception):
    """
    Custom Stack Underflow Error exception
    """
    pass


def evaluate(input_data):
    """Evaluate the given program
    - input_data: List of strings containing a forth program
    Returns: The stack (as a list)
    """
    if len(input_data) == 0:
        return [] # do nothing on empty program.

    stack = []
    symbols = [
        {'name': '+', 'value': add},
        {'name': '-', 'value': subtract},
        {'name': '*', 'value': multiply},
        {'name': '/', 'value': divide},
        {'name': 'dup', 'value': dup},
        {'name': 'drop', 'value': drop},
        {'name': 'swap', 'value': swap},
        {'name': 'over', 'value': over},
    ]
    for program in input_data:
        index = 0
        done = False 
        while not done:
            token = get_token(program, index)
            match token['type']:
                case 'Name':
                    index = token['end']
                    evaluate_token(stack, symbols, token)
                case 'Number':
                    index = token['end']
                    evaluate_token(stack, symbols, token)
                case 'BeginDefinition':
                    index = token['end']
                    index = parse_definition(program, symbols, index)
                case 'EndDefinition':
                    raise Exception('Unexpected end of definition')
                case 'EOF':
                    done = True
            if index > len(program):
                done = True
    return stack

def check_stack(stack, required_operands = 2):
    """ 
    Is the stack in the desired state for this function call. 
    - stack: current contents of the stack
    - required_operands: number of operands expected on the stack (default is 2)
    Returns True if sufficient operants, throws a StackUnderflowError if not enough found.
    """
    if len(stack) == 0:
        raise StackUnderflowError('Insufficient number of items in stack')
    
    if len(stack) < required_operands:
        raise StackUnderflowError('Insufficient number of items in stack')

    return True 

def evaluate_token(stack, symbols, token, lookback = None):
    """
    Evaluate a name or number token. (Just names and number so you can't nest a definition
    inside of a definition). EOF is handled in the main evaluate function.
    stack - current stack 
    symbols - symbol table 
    token - current token a dictionary of type, name, value, start and end
    lookback - where to start looking in the symbol table for a name
    """
    function = None
    match token['type']:
        case 'Number':
            stack.append(token['value'])
        case 'Name':
            function = find_name(symbols, token['value'], lookback)
            if function is None:
                raise ValueError('undefined operation')
            function(stack)


def evaluate_tokens(stack, symbols, tokens, lookback = None):
    """
    Evaluate a list of tokens
    stack - current stack 
    symbols - symbol table 
    tokens - a list of tokens to evaluate
    lookback - where to start looking in the symbol table for a name

    """
    for token in tokens:
        evaluate_token(stack, symbols, token, lookback)

def find_name(symbols, name, lookback = None):
    """ 
    find a name in the symbol table, starting at lookback/end of list 
    and iterating backwards to find it. Return null if not found.
    symbols - symbol table 
    name - name of the definition we are looking for
    lookback - where to start looking in the symbol table for a name

    """
    target = name.lower()
    begin = lookback                # len(symbols) - 1
    if begin is None:
        begin = len(symbols) - 1

    for index in range(begin, -1, -1):
        if target == symbols[index]['name']:
            return symbols[index]['value']
    return None


def get_token(text, start):
    """
    Get the next token from the given input text
    text - The string we are parsing
    start - offset into the string to parse from
    """
    re_ws = r'^\s+'                                     # white space
    re_num = r'^-?\d+'                                  # numbers (integer only)
    re_name = r'^([a-zA-Z][a-zA-Z0-9_-]*|\+|-|\*|\/)'   # names
    index = start 
    while index < len(text):
        current = text[index:]
        ret = re.search(re_ws, current)
        if ret is not None:
            index += ret.span()[1]
            continue

        ret = re.search(re_num, current)
        if ret is not None:
            num = int(ret.group())
            end_index = index + ret.span()[1]
            return { 
                'type': 'Number',
                'value': num,
                'start': index,
                'end': end_index,
            }    

        if text[index] == ':':
            return {
                'type': 'BeginDefinition',
                'value': ':',
                'start': index,
                'end': index + 1,
            }

        if text[index] == ';':
            return {
                'type': 'EndDefinition',
                'value': ';',
                'start': index,
                'end': index + 1,
            }

        ret = re.search(re_name, current)
        if ret is not None:
            end_index = index + ret.span()[1]
            return { 
                'type': 'Name',
                'value': ret.group(),
                'start': index,
                'end': end_index,
            }    

        raise Exception(f'Unrecognized token at {index}')

    return {
        'type': 'EOF',
        'value': '',
        'start': index,
        'end': index
    }

def parse_definition(program, symbols, index):
    """
    Parse a symbol definition
    program - the string we are parsing the program definition from
    index - offset into the program data
    """
    tokens = []
    position = index

    # expect name
    token = get_token(program, position)
    if token['type'] != 'Name':
        raise ValueError('illegal operation')
    name = token['value']
    position = token['end']

    while token['type'] != 'EndDefinition':    
        token = get_token(program, position)
        if token['type'] == 'EOF':
            raise Exception('Unexpected end of file')
        position = token['end']
        if token['type'] != 'EndDefinition':
            tokens.append(token)

    if token['type'] == 'EndDefinition':
        # lookback is to only look at definitions from when the word was defined.
        # you can get some nasty infinite loops otherwise.
        lookback = len(symbols) - 1

        # save the word
        # the function to call it encapsulates the symbol table, tokens, and lookback and just calls evaluateTokens on them.
        symbols.append({
            'name': name.lower(), 
            'value': lambda stack: evaluate_tokens(stack, symbols, tokens, lookback)
        })
    return position

# --------------------
# Built-in functions
# --------------------
def add(stack):
    """
    Add two numbers place the result onto the stack
    stack: we pull the numbers operate from off the stack
    """
    check_stack(stack)

    second = stack.pop()
    first = stack.pop()
    stack.append(first + second)


def subtract(stack):
    """
    Subtrack one number from another and place the result onto the stack
    stack: we pull the numbers operate from off the stack
    """
    check_stack(stack)

    second = stack.pop()
    first = stack.pop()
    stack.append(first - second)


def multiply(stack):
    """
    Multiply two numbers place the result onto the stack
    stack: we pull the numbers operate from off the stack
    """    
    check_stack(stack)

    second = stack.pop()
    first = stack.pop()
    stack.append(first * second)


def divide(stack):
    """
    Integer divide one number by another and place the result onto the stack
    stack: we pull the numbers operate from off the stack
    """
    check_stack(stack)

    second = stack.pop()
    first = stack.pop()
    if second == 0:
        raise ZeroDivisionError('divide by zero')
    stack.append(first // second)    # Change // to / for non-integer division


def dup(stack):
    """
    Duplicate the top item on the stack
    stack: The stack we operate on
    """    
    check_stack(stack, 1)
    first = stack.pop()

    stack.append(first)
    stack.append(first)

def drop(stack):
    """
    Drop the top item from the stack
    stack: The stack we operate on
    """    
    check_stack(stack, 1)
    stack.pop()

def swap(stack):
    """
    Swap the top two items on the stack
    stack: The stack we operate on
    """    
    check_stack(stack)
    second = stack.pop()
    first = stack.pop()

    stack.append(second)
    stack.append(first)


def over(stack):
    """
    Duplicate the second item in the stack to the top of the stack.
    stack: The stack we operate on
    """    
    check_stack(stack)
    second = stack.pop()
    first = stack.pop()

    stack.append(first)
    stack.append(second)
    stack.append(first)