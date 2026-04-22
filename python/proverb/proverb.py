"""
Recide the 'for the want of a nail the kingdom was lost' proverb 
but with the given arguments and optional qualifier for the last line.
"""

def proverb(*args, qualifier=None):
    """
    Display the all for the want of a nail proverb with the given arguments.
    :param args: variable list of arguments to use in the proverb
    :param qualifier: will be added to the describe the first item at the end of the proverb text.
    :returns: The lines of the proverb in a list. Empty list if no arguments are given.
    """
    if qualifier is not None:
        qualifier += ' '
    else:
        qualifier = ''
        
    ret = []
    first = True
    for item in args:
        if not first:
            ret.append(f'For want of a {previous} the {item} was lost.')
        previous = item
        first = False


    if len(args) > 0:
        ret.append(f'And all for the want of a {qualifier}{args[0]}.')

    return ret
