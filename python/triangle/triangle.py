"""
Collection of functions to test if a triangle is 
* equalateral - all sides the same length
* isoscelese - at least two sides the same length
* scalene - all sides differing length

also tests to see if the lengths given can actually be
a triangle with the is_triangle function.
"""

def equilateral(sides) -> bool:
    """
    Check if the given side lengths form an equilateral triangle
    sides: The lengths of sides of the given triangle
    Returns: True if passes as an equlilateral triangle (all sides the same length)
    """
    if not is_triangle(sides):
        return False
    
    return sides[0] == sides[1] and sides[1] == sides[2]


def isosceles(sides)-> bool:
    """
    Check if the given side lengths form an isosceles triangle
    sides: The lengths of sides of the given triangle
    Returns: True if passes as an isosceles triangle (two or more sides the same length)
    """
    if not is_triangle(sides):
        return False
    
    return sides[0] == sides[1] or sides[0] == sides[2] or sides[1] == sides[2]


def scalene(sides)-> bool:
    """
    Check if the given side lengths form an scalene triangle
    sides: The lengths of sides of the given triangle
    Returns: True if passes as an scalene triangle (no sides same length)
    """
    if not is_triangle(sides):
        return False
    
    return sides[0] != sides[1] and sides[0] != sides[2] and sides[1] != sides[2]

def is_triangle(sides)-> bool:
    """
    Check if the given side lengths form a triangle
    Checks that there are three sides, no side lengths are less than or equal to 
    zero, and that the sum of the two smaller sides is bigger than the largest side.
    sides: The lengths of sides of the potential triangle
    Returns: True if passes as an triangle.
    """
    if len(sides) != 3:
        return False
    
    if [side for side in sides if side <= 0] != []:
        return False
    
    sorted_sides = sorted(sides)
    if sorted_sides[0] + sorted_sides[1] <= sorted_sides[2]:
        return False
    
    return True
    