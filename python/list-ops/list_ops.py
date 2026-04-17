"""
list operations
"""

def append(list1, list2):
    """
    Add elements in list2 to the end of list1
    list1: list to append to
    list2: list to append from
    returns: list1 with list2's elements added to it.
    """
    for item in list2:
        list1.append(item)
    return list1

def concat(lists):
    """
    Create a new list with elements from each sub list in lists
    lists: a list of lists to merge.
    returns: the newly created list
    """
    ret = []
    for sublist in lists:
        ret = append(ret, sublist)  
    return ret

def filter(function, list):
    """
    Create a new list which only includes items from list that return true when passed to the given function
    function: Returns True/False for a single argument. Pass it elements from list
    list: A list of arguments to iteratively pass to function
    returns: list of arguments where function(argument) = True
    """
    ret = []
    for item in list:
        if function(item):
            ret.append(item)
    return ret

def length(list):
    """
    Return the length of the given list
    list: list to calculate the length of
    returns: length of list
    """
    # dumb way to do this, I am avoiding the build in method.
    list_len = 0
    for _ in list:
        list_len += 1
    return list_len

def map(function, list):
    """
    Make a new list that returns the value of applying a function to each element in the list
    function: the function to apply to the list items
    list: list of arguments to iteratively pass to the function
    returns: a new list of results of passing the element from the original list to function.
    """
    ret = []
    for item in list:
        ret.append(function(item))  
    return ret 

def foldl(function, list, initial):
    """
    Fold (from left to right). Accumulate the values from function(item) with the given initial value iterated from the left
    function: function to run over the list items (arguments are accumulator then list item)
    list: arguments to iteratively pass into the function.
    initial: the initial value of the accumulator
    returns: The final value of the accumulator after running across the whole list.
    """
    ret = initial
    for item in list:
        ret = function(ret, item)
    return ret

def foldr(function, list, initial):
    """
    Fold (from right to left). Accumulate the values from function(item) with the given initial value iterated from the right
    function: function to run over the list items (arguments are accumulator then list item)
    list: arguments to iteratively pass into the function.
    initial: the initial value of the accumulator
    returns: The final value of the accumulator after running across the whole list.
    """
    ret = initial
    for item in reverse(list):
        ret = function(ret, item)
    return ret

def reverse(list):
    """
    Given a list return a new list which is the same as list but with the items in the reverse order
    list: list of values to reverse
    return: a copy of list with items in reverse order.
    """
    ret = []
    for item in list:
        ret = [item, *ret]
    return ret
