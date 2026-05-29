"""
Alphametics Puzzle
"""
import re

def solve(puzzle):
    """
    Solve an Alphametics Puzzle if possible.
    :param puzzle: the puzzle to solve should be in format of (WORD +)* WORD = WORD with spaces between words and +/=
    :returns: None if no solution found or a Dictionary of Letter to value mappings.
    """
    tokens = re.findall(r'[A-Z]+', puzzle, )
    unique_chars = set(puzzle)
    for char in [' ', '+', '=']:
        if char in unique_chars:
            unique_chars.remove(char)
    not_zero = [token[0] for token in tokens]
    digit_map = [None, None, None, None, None, None, None, None, None, None]

    ret = step(' '.join(tokens), digit_map, not_zero, len(unique_chars), ''.join(unique_chars), 0)
    if type(ret) != dict:
        return None
    return ret

def compute(puzzle):
  """
  Check if a given puzzle solution is mathematically consistent (right hand value = sum of all other values)
  :param puzzle: The solved puzzle, just a space separated string of numbers
  :returns: True is the solution is valid.
  """
  puzzle.split(' ')
  nums = [int(value) for value in puzzle.split(' ')]  
  rhsAmount = nums.pop()
  return rhsAmount == sum(nums)

def step(puzzle, digit_map, not_zero, total_unique, unique_chars, mapped_digits):
    """
    Recursive call for the solver. Solve one step of the solution
    :param puzzle: The puzzle to solve (+ and = have been removed)
    :param digit_map: List of digits that have been mapped if the value at index == None it is available to be mapped still.
    :param not_zero: Leading characters in words may not be zero, this is a list of them
    :param total_unique: count of unique characters
    :param unique_chars: string with the available unique charaters (Hello => Helo)
    :mapped_digits: The number of digits that have been mapped
    :returns: Dictionary of character to digit mappings that solve the puzzle or None if no solution found.
    """
    # base case check for a valid solution
    if mapped_digits == total_unique:
        valid = compute(puzzle)

        if not valid:
            return False
        else:
            ret = {}

            for index, value in enumerate(digit_map):
                if value != None:
                    ret[value] = index
            return ret
    # find the first unmapped character
    if len(unique_chars) <= 0:
        return False

    candidate = unique_chars[0]
    for index in range(len(digit_map) - 1, -1, -1):
        if index == 0 and candidate in not_zero or digit_map[index] is not None:
            continue
        digit_map[index] = candidate
        ret = step(puzzle.replace(candidate, str(index)), digit_map, not_zero, total_unique, unique_chars.replace(candidate, ''), mapped_digits + 1)
        if type(ret) == dict:
            return ret
        else:
            digit_map[index] = None

    return False
