from enum import Enum   # To make a direction enumeration
import re               # To help clean up the message to encode.

class Direction(Enum):
    """
    Enumeration of UP and DOWN directions because I didn't want to
    have module level constants anymore. Also includes a very handy 
    zig-zag method to bounce between the directions.
    """
    UP = 0
    DOWN = 1

    @staticmethod
    def zig_zag(row, direction, max_row):
        """
        Zig-Zag up and down swapping direction when you hit the top or 
        bottom of the rows.
        Parameters:
        * row: The row you are currently (postive integer >= 0 and > max_row).
        * direction: The direction you are currently moving.
        * max_row: The maximum or total number of rows.
        Returns:
        * row: Updated value for which row you are on.
        * direction: The direction you should currently be moving.
        """
        if direction == Direction.DOWN:
            row += 1
            if row == max_row - 1:
                direction = Direction.UP
        else: # direction == Direction.UP:
            row -= 1
            if row == 0:
                direction = Direction.DOWN
        return row, direction

    
def encode(message, rails):
    """Encode a message using the rail-fence cipher for the given
    number or rails.
    Parameters:
    * message: The message to encode. Will be converted to upper case
    and have any non A-Z and 0-9 characters will be removed.
    * rails: The number of rows or rails to encode the message with.
    Returns:
    * Rail-Fence encoded version of the input message.
    """
    lines = [[] for i in range(rails)]
    dir = Direction.DOWN
    rail = 0
    message_clean = re.sub(r'[^A-Z0-9]', '', message.upper())    
    for ch in message_clean:
        lines[rail].append(ch)
        rail, dir = Direction.zig_zag(rail, dir, rails)
    
    # inner join joins interior lists into string, and the outer join
    # glues those together into the result.
    return ''.join([''.join(line) for line in lines])


def decode(encoded_message, rails):
    """Decode a message encoded with the rail-fence cipher for the 
    given number of rails (rows).
    Parameters:
    * encoded_message: string to decode.
    * rails: number of rows/rails to use when decoding
    Returns:
    * Decrypted version of the encoded message
    """
    # Figure out the length of each rail. Easiest to just
    # simulate encoding so we can figure out how long each row/rail
    # should be
    line_len = [0 for i in range(rails)]
    dir = Direction.DOWN
    rail = 0
    for _ in encoded_message:
        line_len[rail] += 1
        rail, dir = Direction.zig_zag(rail, dir, rails)
    
    # Fill in each rail according to length. Keeping a running index 
    # into the message just split up the string by length of each rail.
    index = 0
    lines = []
    for length in line_len:
        lines.append([ch for ch in encoded_message[index:index + length]])
        index += length
    
    # Finally Zig-Zag back and forth reconstructing the message
    result = ''
    dir = Direction.DOWN
    rail = 0
    for _ in encoded_message:
        result += lines[rail].pop(0)
        rail, dir = Direction.zig_zag(rail, dir, rails)
    
    return result
