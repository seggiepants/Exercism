"""
Lookup the values for color bands on a resistor
"""

band_values = {
    'black': 0,
    'brown': 1,
    'red': 2,
    'orange': 3,
    'yellow': 4,
    'green': 5,
    'blue': 6,
    'violet': 7,
    'grey': 8,
    'white': 9,
}

def color_code(color):
    """
    Return the value of a color code.
    color: The color to lookup
    returns: Number - value of color band or ValueError if not in lookup dictionary
    """
    adjusted_color = color.lower().strip()
    if adjusted_color not in band_values:
        raise ValueError(f'Invalid band color: {color}')
    return band_values[adjusted_color]


def colors():
    """
    Return a list of supported band colors, order is not guaranteed to be anything specific.
    """
    return list(band_values.keys())
