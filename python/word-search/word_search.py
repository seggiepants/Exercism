"""
Word Search Implementation
"""

class Point:
    """
    Point class
    """
    def __init__(self, x, y):
        """
        Setup the point object.
        :param x: The x-coordinate for the point object.
        :param y: The y-coordinate for the point object.
        """
        self.x = x
        self.y = y

    def __eq__(self, other):
        """Check if this an another point object are equal
        :param other: The other point object to compare against.
        :returns: True/False if equivalent
        """
        return self.x == other.x and self.y == other.y
    
    def __hash__(self):
        """
        Paired with the __eq__ function this does a hash of this object.
        Only really added so exercism doesn't cry
        :returns: hash of this object
        """
        return hash((self.x, self.y))
    
    def __str__(self):
        """
        Return a string representation of the object (for debugging)
        :returns: This object as a string
        """
        return f'Point({self.x},{self.y})'


class WordSearch:
    """
    Word Search class
    """
    def __init__(self, puzzle):
        """
        Initialize the object saving a reference to the puzzle data
        """
        self.puzzle = puzzle

    def search(self, word):
        """
        Search for a word in either forward or reverse on the puzzle grid.
        :param word: The word to search for
        :returns: None if not found otherwise a tuple of two point objects the first being the start location and the second the end."""
        
        word_reverse = list(word)
        word_reverse.reverse()
        word_reverse = ''.join(word_reverse)
        
        ret = self.probe(word)
        if ret[0]:
            return (ret[1], ret[2])
        
        ret = self.probe(word_reverse)
        if ret[0]:
            return (ret[1], ret[2])
        
        return None
    
    def probe(self, word):
        """
        Search for a word on the puzzle in many different directions crawing in one of 8 directions
        :param word: the word to search for
        :returns: tuple of length 3 if success True followed by the beginning and end points. If failed Falsed followed by two zero points
        """
        rows = len(self.puzzle)
        cols = len(self.puzzle[0])

        start_x = -1
        start_y = -1
        stop_x = -1
        stop_y = -1
        chars_found = 0
        delta = [(0, 1), (0, -1), (1, 0), (-1, 0), (1, 1), (1, -1), (-1, 1), (-1, -1)]

        for row in range(rows):
            for col in range(cols):
                if self.puzzle[row][col] == word[0]:
                    for dx, dy in delta:
                        chars_found = 1
                        start_x = col
                        start_y = row
                        stop_x = col + dx
                        stop_y = row + dy 
                        index = 1
                        
                        while 0 <= stop_x < cols and 0 <= stop_y < rows and index < len(word) and word[index] == self.puzzle[stop_y][stop_x]:
                            chars_found += 1
                            index += 1
                            stop_x += dx 
                            stop_y += dy
                        
                        if chars_found == len(word):
                            return (True, Point(start_x, start_y), Point(stop_x - dx, stop_y - dy))
        return (False, Point(0, 0), Point(0, 0))
