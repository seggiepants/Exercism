"""Zebra Puzzle - Fill in data from given constraints. 
This is a Constraint Satisfaction Problem.
"""

# I made this global so I only have to compute the puzzle once.
data = [{ 'color': '', 'nationality': '', 'drink': '', 'pet': '', 'hobby': ''}
    , { 'color': '', 'nationality': '', 'drink': '', 'pet': '', 'hobby': ''}
    , { 'color': '', 'nationality': '', 'drink': '', 'pet': '', 'hobby': ''}
    , { 'color': '', 'nationality': '', 'drink': '', 'pet': '', 'hobby': ''}
    , { 'color': '', 'nationality': '', 'drink': '', 'pet': '', 'hobby': ''}
]

colors = ['ivory', 'yellow', 'blue', 'red', 'green']
nationalities = ['Englishman', 'Japanese', 'Spainiard', 'Norwegian', 'Ukranian']
drinks = ['water', 'tea', 'milk', 'coffee', 'orange juice']
pets = ['dog', 'zebra', 'fox', 'horse', 'snail']
hobbies = ['dancing', 'painting', 'reading', 'football', 'chess']

def init():
    """
    Compute the values for the zebra puzzle
    So that the methods can query against the data.
    """
    # populate the givens
    data[0]['nationality'] = 'Norwegian' # The Norwegian lives in the first house.
    data[1]['color'] = 'blue' # The Norwegian lives next to the blue house
    data[2]['drink'] = 'milk' # The middle house drinks milk
    if not is_full():
        if not fill_puzzle([]):
            print('Fill failed')
    
def fill_puzzle(undo_stack):
    """
    Populate the puzzle
    undo_stack - Moves taken in LIFO order.
    """
    for house in range(len(data)):
        pairs = {'color': colors, 'nationality': nationalities, 'drink': drinks, 'pet': pets, 'hobby': hobbies}
        for key, values in pairs.items():
            if data[house][key] == '':
                used = [item[key] for item in data if item[key] != '']
                free = [value for value in values if value not in used]
                if len(free) > 0:
                    for attempt in free:
                        data[house][key] = attempt
                        undo_stack.append([house, key])
                        stack_pointer = len(undo_stack)
                        success = populate(undo_stack)
                        if not success:
                            undo(undo_stack, stack_pointer)
                        else:
                            success = fill_puzzle(undo_stack)
                            if not success:
                                undo(undo_stack, stack_pointer)
                            else:
                                break
        if data[house]['color'] == '' or \
        data[house]['nationality'] == '' or \
        data[house]['drink'] == '' or \
        data[house]['hobby'] == '' or \
        data[house]['pet'] == '':
            return False
      
        if is_full():
            return True
    
    return populate(undo_stack) and is_full()

def find(key, value):
    """
    Find the house that has a key with the given value
    key - the property to search
    value - the desired value of that property
    Returns -1 if not found otherwise zero based index of house with match
    """
    ret = [index for index, row in enumerate(data) if row[key] == value]
    if len(ret) == 0:
        return -1
    return ret[0]

def is_full():
    """
    Is the board filled in
    Returns True/False
    """
    return not any(data[house]['color'] == '' or \
                    data[house]['nationality'] == '' or \
                    data[house]['drink'] == '' or \
                    data[house]['pet'] == '' or \
                    data[house]['hobby'] == '' for house in range(len(data)))

def populate(undo_stack):
    """
    Run through all the rules (except the givens -- can be filled in without futher data)
    if a rule fails the tests return false, if no errors found return true.
    undo_stack - keep track of moves taken in case we need to undo a wrong series of guesses.
    """
    # 1 There are five houses
    if len(data) != 5:
        return False    
    
    # 2 Englishman lives in the red house
    if not populate_pair(undo_stack, 'nationality', 'Englishman', 'color', 'red'):
        return False
    
    # 3 Spaniard owns a dog
    if not populate_pair(undo_stack, 'nationality', 'Spainiard', 'pet', 'dog'):
        return False
    
    # 4 Green house drinks coffee
    if not populate_pair(undo_stack, 'color', 'green', 'drink', 'coffee'):
        return False
    
    # 5 Ukranian drinks tea
    if not populate_pair(undo_stack, 'nationality', 'Ukranian', 'drink', 'tea'):
        return False
    
    # 6 - Green house right of Ivory house
    if not populate_pair_right(undo_stack, 'color', 'ivory', 'color', 'green'):
        return False
    
    # 7 Snail owner goes dancing
    if not populate_pair(undo_stack, 'pet', 'snail', 'hobby', 'dancing'):
        return False
    
    # 8 Yellow house likes painting
    if not populate_pair(undo_stack, 'color', 'yellow', 'hobby', 'painting'):
        return False
    
    # 9 Middle house drinks milk is a given
    # 10 1st house is Norwegian is a given
    # 11 - Reading next to fox
    if not populate_pair_neighbor(undo_stack, 'hobby', 'reading', 'pet', 'fox'):
        return False
    
    # 12 - Painter next to horse
    if not populate_pair_neighbor(undo_stack, 'hobby', 'painting', 'pet', 'horse'):
        return False
    
    # 13 Football drinks orange juice
    if not populate_pair(undo_stack, 'hobby', 'football', 'drink', 'orange juice'):
        return False
    
    # 14 Japanese plays chess
    if not populate_pair(undo_stack, 'nationality', 'Japanese', 'hobby', 'chess'):
        return False
    
    # 15 is also a given Norwegian (1st house) is next to blue house where 2 is only neighbor 
    
    return True 

def populate_pair(undo_stack, key1, value1, key2, value2):
    """ 
    Populate if house[key] === value then house[otherKey] = otherValue
    undo_stack - moves taken in case we need to undo
    key1, value1 the first key, value pair
    key2, value2 the second key, value pair
    """
    index = find(key1, value1)
    if index >= 0:
        if data[index][key2] == '':
            undo_stack.append([index, key2])
            data[index][key2] = value2
            return True
        if data[index][key2] != value2:
            return False
    
    index = find(key2, value2)
    if index >= 0:
        if data[index][key1] == '':
            undo_stack.append([index, key1])
            data[index][key1] = value1
            return True
        if data[index][key1] != value1:
            return False
    return True

def populate_pair_right(undo_stack, key1, value1, key2, value2):
    """
    Populate if house[key] === value then houseToRight[otherKey] = otherValue
    undo_stack - moves taken in case we need to undo
    key1, value1 - first key, value pair to search/populate for
    key2, value2 - second key, value pair to search/populate for
    """
    index1 = find(key1, value1)
    index2 = find(key2, value2)
    # if you have both they had better match
    if index1 >= 0 and index2 >=0 and (index1 + 1) != index2:
        return False

    if 0 <= index1 <= len(data) - 2:
        if data[index1 + 1][key2] == '':
            undo_stack.append([index1 + 1, key2])
            data[index1 + 1][key2] = value2 
            return True 
        if data[index1 + 1][key2] != value2:
            return False 

    if 1 <= index2 < len(data):
        if data[index2 - 1][key1] == '':
            undo_stack.push([index2 - 1, key1])
            data[index2 - 1][key1] = value1
            return True 
        if data[index2 - 1][key1] != value1:
            return False 
    return True

def populate_pair_neighbor(undo_stack, key1, value1, key2, value2):
    """
    Populate if house[key] === value then houseToLeftOrRight[otherKey] = otherValue
    undo_stack - moves taken in case we need to undo
    key1, value1 - first key, value pair to search/populate for
    key2, value2 - second key, value pair to search/populate for
    """
    index1 = find(key1, value1)
    index2 = find(key2, value2)
    # Both populated but more than one spot apart is an error
    if index1 >= 0 and index2 >= 0:
        if (data[index1][key1] == value1 and data[index2][key2] == value2) or \
            (data[index2][key1] == value1 and data[index1][key2] == value2):
            return True 

        if abs(index2 - index1) > 1:
            return False

    if not populate_pair_neighbor_step(undo_stack, key1, value1, key2, value2):
        return False 
    if not populate_pair_neighbor_step(undo_stack, key2, value2, key1, value1):
        return False 
    return True

def populate_pair_neighbor_step(undo_stack, key1, value1, key2, value2):
    """
    These searches are lengthy to write so I broke this one in two and
    just call it with both possible combinations (should be redundant, really).
    undo_stack - moves taken in case we need to undo
    key1, value1 - first key, value pair to search/populate for
    key2, value2 - second key, value pair to search/populate for
    """
    index = find(key1, value1)
    if index >= 0:
        sides = []
        if index - 1 >= 0:
            sides.append([index -1, data[index - 1][key2]])
        if index + 1 < len(data):
            sides.append([index + 1, data[index + 1][key2]])

        if len(sides) == 1 and sides[0][1] != value2 and sides[0][1] != '':
            return False
        
        if len(sides) == 1 and sides[0][1] == value2:
            return True
        
        if len(sides) == 1 and sides[0][1] == '':
            # only one and it is empty, fill it
            undo_stack.append([sides[0][0], key2])
            data[sides[0][0]][key2] = value2
            return True
        
        if len(sides) == 2 and sides[0][1] != '' and sides[1][1] != '' and sides[0][1] != value2 and sides[1][1] != value2:
            # both sides not target
            return False
        
        if len(sides) == 2 and (sides[0][1] == value2 or sides[1][1] == value2):
            # one side is target
            return True
        
        if len(sides) == 2 and sides[0][1] == '' and sides[1][1] != '' and sides[1][1] != value2:
            # left empty, right not empty not target
            undo_stack.append([sides[0][0], key2])
            data[sides[0][0]][key2] = value2
        elif len(sides) == 2 and sides[0][1] != '' and sides[1][1] == '' and sides[0][1] != value2:
            # right empty, let not empty not target
            undo_stack.append([sides[1][0], key2])
            data[sides[1][0]][key2] = value2
    return True

def undo(undo_stack, stack_pointer):
    """
    Undo moves down to a given stack pointer
    undo_stack - the moves taken so far.
    stack_pointer - place on the stack to undo moves to.
    """
    while len(undo_stack) >= stack_pointer:
        [house, key] = undo_stack.pop(len(undo_stack) - 1)
        data[house][key] = ''

def drinks_water():
    """
    Find the nationality of the house that drinks water
    """
    init()
    return data[find('drink', 'water')]['nationality']

def owns_zebra():
    """
    Find the nationality of the house that has a pet zebra
    """
    init()
    return data[find('pet', 'zebra')]['nationality']
