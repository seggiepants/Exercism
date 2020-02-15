from typing import Iterable

def flatten(iterable):
    """Flatten a given iterable.
    Parameters:
    * iterable: The object to flatten.
    Returns:
    * List of objects in the iterable flattend out to a single level.
    """
    result = [] # default starting value
    # If an item in the iterable is iterable itself. Call flatten on it
    # recursively and extend the list. Otherwise just append it to the
    # result (assuming it isn't None).
    for item in iterable:
        if isinstance(item, Iterable) and not isinstance(item, str):
            result.extend(flatten(item))
        elif item != None:
            result.append(item)
        
    return result

