"""
Binary search through a list for a given value
"""

def find(search_list, value):
    """
    Search through a sorted list to find a given value
    search_list: sorted list to search in
    value: the value to search for
    """
    start_index = 0
    end_index = len(search_list) - 1

    while True:
        if end_index < start_index or start_index > end_index:
            raise ValueError('value not in array')
    
        middle = ((end_index - start_index) // 2) + start_index
        middle_value = search_list[middle]

        if value == middle_value:
            return middle
    
        if middle_value > value:
            end_index = middle - 1
    
        if middle_value < value:
            start_index = middle + 1    
    
    
