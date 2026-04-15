"""
Calculate the values for the first two colors on a resistor.
"""

color_bands = {
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

def adjust_color(color):
    return color.lower().strip()

def value(colors):
    """
    Return the numeric value for the first two colors in the given colors list
    If there are less than two colors only the available colors will be counted. With zero for an empty list
    colors: a list of color names
    """
    ret = 0
    for index in range(2):
        if len(colors) >= (index + 1):
            ret = ret * 10 + color_bands[adjust_color(colors[index])]    
    return ret
