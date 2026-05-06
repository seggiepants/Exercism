"""
Convert a sequence of numbers to a variable length seven bit encoding
"""

SEVEN_BITS = 0b1111111
BIT_EIGHT = 0b10000000

def encode(numbers):
    """
    Convert a sequence of number to a variable length seven bit encoding with the 8th bit indicating a continuation flag.
    :param numbers: a list of numbers to encode
    :returns: list of numbers (can be expressed as bytes) that encodes the numbers in the numbers list.
    """
    ret = []
    for num in numbers:
        if num <= SEVEN_BITS:
            ret.append(num)
        else:
            value = num
            index = len(ret)
            first = True
            while value != 0:
                next_seven = value & SEVEN_BITS
                value = value >> 7
                if first:
                    ret.insert(index, next_seven)
                    first = False
                else:
                    ret.insert(index, BIT_EIGHT | next_seven)

    return ret


def decode(bytes_):
    """
    Decode a list of bytes back into numbers from a 7 bit variable length encoding that has the eight bit as a continuation flag.
    :param bytes_: A list of numbers (0-255) that will be converted from variable length to their original values
    :returns: a list of decoded numbers
    :raises: ValueError if the sequence is incomplete (ends with the continuation flag set)
    """
    ret = []
    current = 0
    high_bit = 0
    for byte in bytes_:
        high_bit = byte & BIT_EIGHT
        current = (current << 7) + (byte & SEVEN_BITS)
        if high_bit == 0:
            ret.append(current)
            current = 0
    if high_bit != 0:
        raise ValueError('incomplete sequence')
    return ret
