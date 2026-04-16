"""
Greet customers in a line up
"""

def line_up(name, number):
    """
    Compute the desired greeting for a customer with a given name and number
    name: The name of the customer
    number: The ordering of the customer for the given day
    returns: Formatted string greeting the customer with a number and its proper suffix
    """
    tens = number % 100
    ones = number % 10
    suffix = 'th'
    if ones == 1 and tens != 11:
        suffix = 'st'
    if ones == 2 and tens != 12:
        suffix = 'nd'
    if ones == 3 and tens != 13:
        suffix = 'rd'
    
    return f'{name}, you are the {number}{suffix} customer we serve today. Thank you!'