"""
Calculate the values for the color bands on a resistor. Return the value as a string.
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

tolerance = {
    'grey': '±0.05%',
    'gray': '±0.05%',
    'violet': '±0.1%',
    'blue': '±0.25%',
    'green': '±0.5%',
    'brown': '±1%',
    'red': '±2%',
    'gold': '±5%',
    'silver': '±10%',
}

def adjust_color(color):
    return color.lower().strip()

def decimal_strip(value):
    return value.rstrip('0').rstrip('.') if '.' in value else value

def resistor_label(colors):
    """
    Return the numeric value for a one, four, or five color band resistor.
    For one band resistors only the first value is returned in ohms
    For a four band a two band value is returned with the third band as a multiplier and the final band as tolerance
    For a five band a three band value is returned with the fourth band as a multiplier and the final band as tolerance
    colors: a list of color names
    """
    if not len(colors) in [1, 4, 5]:
        raise ValueError(f'Color list must be of length 1, 4, or 5. Found length {len(colors)}')
    ret = 0
    
    # default for 1 band
    index_multiplier = 1
    index_tolerance = -1

    if len(colors) == 4:
        index_multiplier = 2
        index_tolerance = 3

    if len(colors) == 5:
        index_multiplier = 3
        index_tolerance = 4
    
    for index in range(index_multiplier):
        if len(colors) >= (index + 1):
            ret = ret * 10 + color_bands[adjust_color(colors[index])]

    if index_multiplier > 1:
        multiplier = color_bands[adjust_color(colors[index_multiplier])]
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
    
    text_tolerance = ''
    if index_tolerance > 1:
        text_tolerance = ' ' + tolerance[adjust_color(colors[index_tolerance])]

    return f'{decimal_strip(str(ret))} {scale}ohms{text_tolerance}'
