"""
Calculate the values for the first three colors on a resistor. Return the value as a string.
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

def label(colors):
    """
    Return the numeric value for the first three colors in the given colors list
    If there are less than three colors only the available colors will be counted. With zero for an empty list
    The third color is a power of ten multiplier. Value will be returned as a string like 33 ohms however for
    large ohm values the ohm label may be replaced with kiloohms, megaohms or gigaohms with the value reduced
    accordingly.
    colors: a list of color names
    """
    ret = 0
    for index in range(2):
        if len(colors) >= (index + 1):
            ret = ret * 10 + color_bands[adjust_color(colors[index])]
    
    if len(colors) >= 3:
        multiplier = color_bands[adjust_color(colors[2])]
        ret = ret * (10**multiplier)

    scale = ''
    if 1_000 <= ret < 1_000_000:
        ret /= 1_000
        scale = 'kilo'
    elif 1_000_000 <= ret < 1_000_000_000:
        ret /= 1_000_000
        scale = 'mega'
    elif ret >= 1_000_000_000:
        ret /= 1_000_000_000
        scale = 'giga'
    return f'{int(ret)} {scale}ohms' 
