import math

def score(x, y):
    """
    Score a throw in a game of darts based on the distance from the center (0, 0)
    x: x-coordinate of the throw destination
    y: y-coordinate of the throw destination
    """
    distance = math.sqrt(x * x + y * y)
    if distance <= 1.0:
        return 10
    
    if distance <= 5.0:
        return 5
    
    if distance <= 10.0:
        return 1
    
    return 0
