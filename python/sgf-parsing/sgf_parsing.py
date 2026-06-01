"""
SgfTree object and parser
"""

class SgfTree:
    """
    SgfTree Node Class
    """
    def __init__(self, properties=None, children=None):
        """
        Create a new SgfTree Node
        :param properties: properties to populate
        :param children: child nodes to populate
        """
        self.properties = properties or {}
        self.children = children or []

    def __eq__(self, other):
        """
        Check if two SgfTree nodes are equal self and other
        :param other: Node to compare against self
        :returns: True if equivalent
        """
        if not isinstance(other, SgfTree):
            return False
        for key, value in self.properties.items():
            if key not in other.properties:
                return False
            if other.properties[key] != value:
                return False
        for key in other.properties.keys():
            if key not in self.properties:
                return False
        if len(self.children) != len(other.children):
            return False
        for child, other_child in zip(self.children, other.children):
            if child != other_child:
                return False
        return True

    def __ne__(self, other):
        """
        Check if two SgfTree nodes are not equivalent (self and other)
        :returns: True if not equivalent, False if equivalent
        """
        return not self == other
    
    def __hash__(self):
        return hash(self.properties, self.children)


def parse(input_string):
    """
    Parse a Sgf tree input string.
    :param input_string: The text to parse
    :returns: Root of parsed SgfTree
    :raises: Value error if input cannot be parsed.
    """
    text = input_string.strip()
    if len(text) == 0:
        raise ValueError('tree missing')

    properties = {}
    children = []
    char_index = parse_node(properties, children, text, 0)
    if char_index == -1:
        raise ValueError('Malformed input.')
    return SgfTree(properties, children)


def parse_node(properties, children, input_string, index):
    """
    Parse a node of the Sgf input
    :param properties: dictionary to add properties to.
    :param children: list of children to add to (parse_property will do this)
    :param input_string: text to parse
    :param index: Where we are at in the text.
    :returns: index to continue parsing from
    :raises: Value error for parsing errors.
    """
    char_index = index
    
    # Expect (
    if input_string[char_index] != '(':
        raise ValueError('tree missing')
    
    char_index += 1

    # Expect properties
    if input_string[char_index] != ';':
        raise ValueError('tree with no nodes')
    
    char_index += 1
    next_index = parse_property(properties, children, input_string, char_index)
    while next_index != -1:
        char_index = next_index
        next_index = parse_property(properties, children, input_string, char_index)

    # Expect )
    if input_string[char_index] != ')':
        raise ValueError('properties without delimiter')
    char_index += 1
    return char_index

def parse_property(properties, children, input_string, index):
    """
    Parse a node property
    :param properties: Dictionary of properties to add to
    :param children: List of child nodes to fill out.
    :param input_string: text to parse
    :param index: where in the text to parse from
    :returns: index to continue parsing from
    :raises: ValueError if parse problems encountered
    """
    char_index = index
    char = get_next_char(input_string, char_index, True, False)
    name = ''
    while char[1] >= 0:
        name += char[0]
        char_index = char[1]
        char = get_next_char(input_string, char_index, False, False)
        
    if char_index >= len(input_string):
        return -1 # Not found.

    if name != name.upper():
        raise ValueError('property must be in uppercase')

    # Read Properties
    property_list = []
    while char_index < len(input_string) and input_string[char_index] == '[':
        # Expect [
        if char_index >= len(input_string) or input_string[char_index] != '[':
            return -1
        char_index += 1

        value_start = char_index
        char = get_next_char(input_string, char_index, True, True)
        value = ''
        while char[1] >= 0:
            value += char[0]
            char_index = char[1]
            char = get_next_char(input_string, char_index, False, True)

        if char_index == value_start: # No value
            return -1

        # Expect ]
        if char_index >= len(input_string) or input_string[char_index] != ']':
            return -1
        char_index += 1
        property_list.append(value)


    if len(property_list) < 1:
        return -1

    if char_index < len(input_string) and input_string[char_index] == ';':
        char_index += 1
        child_properties = {}
        grand_children = []
        next_index = parse_property(child_properties, grand_children, input_string, char_index)
        if next_index != -1:
            children.append(SgfTree(child_properties, grand_children))
            char_index = next_index
    elif char_index < len(input_string) and input_string[char_index] == '(':
        while char_index < len(input_string) and input_string[char_index] == '(':
            child_properties = {}
            grand_children = []
            next_index = parse_node(child_properties, grand_children, input_string, char_index)
            if next_index != -1:
                children.append(SgfTree(child_properties, grand_children))
                char_index = next_index
    if name in properties.keys():
        raise ValueError(f'Key \"{name}\" exists multiple times.')
    properties[name] = property_list

    return char_index

def get_next_char(input_string, index, first_char, allow_square_bracket_start):
    """
    Get the next character (or two) from the input string with some substitutions
    :param input_string: The text to read from
    :param index: The read location in the text
    :param first_char: is this the first character we are reading from?
    :param allow_square_bracket_start: do we allow a plain [ in the input?
    :returns: Tuple with the character read/subtituted followed by the index to read from next.
    """
    if index >= len(input_string):
        return ('', -1)
    char = input_string[index]
    two_chars = input_string[index:index+2]
    if 'a' <= char <= 'z' or 'A' <= char <= 'Z' or '0' <= char <= '9' or char in ' ;=\n':
        return (char, index + 1)
    if not first_char and char in '()':
        return (char, index + 1)
    if not first_char and char == '[' and allow_square_bracket_start:
        return (char, index + 1)
    if char == '\t':
        return (' ', index + 1)
    if two_chars in {r'\[', r'\]', r'\\', '\\\t', '\\\n', '\\t', '\\n'}:
        if two_chars == '\\\t':
            return (' ', index + 2)
        if two_chars == '\\\n':
            return ('', index + 2)
        if two_chars in {'\\t', '\\n'}:
            return two_chars[1], index + 2
        return (two_chars[1], index + 2)
    return ('', -1)