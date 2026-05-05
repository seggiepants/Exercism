"""
Generate a grid of size x size and fill it with a spiral matrix starting at the top left and 
filling numbers clockwise toward the center.
"""

DIR_RIGHT = 0
DIR_DOWN = 1
DIR_LEFT = 2
DIR_UP = 3

delta = [(1, 0), (0, 1), (-1, 0), (0, -1)]

def spiral_matrix(size):
    """
    Generate a spiral matrix of size x size starting at the top left and spiraling clockwise
    :param size: The size of the matrix to return
    :returns: a size x size list of lists filled with a spiral matrix
    """
    if size == 0:
        return []
    
    ret = [list(range(size)) for index in range(size)]
    top = 0
    bottom = size - 1
    left = 0
    right = size - 1
    coord_x = 0
    coord_y = top
    delta_dir = DIR_RIGHT
    for counter in range(size * size):
        ret[coord_y][coord_x] = counter + 1

        coord_x += delta[delta_dir][0]
        coord_y += delta[delta_dir][1]

        if coord_x > right:
            # Turn right moving down
            top += 1
            coord_x = right
            coord_y = top
            delta_dir = DIR_DOWN
        elif coord_y > bottom:
            # Turn right moving left
            right -= 1
            coord_x = right
            coord_y = bottom
            delta_dir = DIR_LEFT
        elif coord_x < left:
            # Turn right moving up
            bottom -= 1
            coord_x = left
            coord_y = bottom 
            delta_dir = DIR_UP
        elif coord_y < top:
            left += 1
            coord_x = left
            coord_y = top
            delta_dir = DIR_RIGHT

    return ret
