"""
Calculate the number of eggs for an decimal value
"""

def egg_count(display_value):
    """
    Count the eggs in a display_value where each egg is a positive binary digit from the number
    :param display_value: decimal value to count the bit 1 values from
    :returns: The number of bit 1 values in the given display_value which is also the number of eggs.
    """
    num_eggs = 0
    current_value = display_value
    while current_value != 0:
        bit = current_value % 2
        current_value = (current_value - bit) // 2
        num_eggs += bit
    return num_eggs
