"""
Zipper Implementation
"""

class Zipper:
    """
    Zipper Class
    """
    def __init__(self, tree = None, move_stack = None):
        """
        Setup a new zipper object with optional tree and move_stack
        :param tree: default tree to start with (None is not passed in)
        :param move_stack: default move_stack to start with (None is default and converted to empty list if not passed in)
        """
        self.tree = tree
        if move_stack is None:
            self.move_stack = []
        else:
            self.move_stack = move_stack
        self.focus = self.find_focus()

    @staticmethod
    def from_tree(tree):
        """
        Create a new zipper with the given tree.
        :param tree: starting tree
        :returns: Zipper object with tree and focus on the root
        """
        return Zipper(tree)

    def value(self):
        """
        Return the value at the focused node in the tree
        :returns: Value at focus or None if there is no focus
        """
        if self.focus is None:
            return None
        return self.focus['value']

    def set_value(self, value):
        """
        Set the value at the focused node.
        :param value: New value for the focused node
        :returns: A new zipper with the current tree and focus
        """
        if self.focus is not None:
            self.focus['value'] = value
        return Zipper(self.tree, self.move_stack)

    def left(self):
        """
        Move focus to the left node of the focus if available
        :returns: A new zipper with the current tree and new focus
        """
        if self.focus is None or self.focus['left'] is None:
            return None
        return Zipper(self.tree, [*self.move_stack, 'L'])

    def set_left(self, sub_tree):
        """
        Change the left node at the focus location to the new sub_tree
        :param sub_tree: New value to use as the left branch of the focused node
        :returns: A zipper with the current tree and focus
        """
        if self.focus is not None:
            self.focus['left'] = sub_tree
        return Zipper(self.tree, self.move_stack)

    def right(self):
        """
        Move focus to the right node of the focus if available
        :returns: A new zipper with the current tree and new focus
        """
        if self.focus is None or self.focus['right'] is None:
            return None
        return Zipper(self.tree, [*self.move_stack, 'R'])

    def set_right(self, sub_tree):
        """
        Change the right node at the focus location to the new sub_tree
        :param sub_tree: New value to use as the right branch of the focused node
        :returns: A zipper with the current tree and focus
        """
        if self.focus is not None:
            self.focus['right'] = sub_tree
        return Zipper(self.tree, self.move_stack)

    def up(self):
        """
        Undo the last move which will take us up one node in the tree.        
        :returns: None is no previous move, otherwise a new zipper with the current tree and new focus
        """
        if self.move_stack is None or len(self.move_stack) == 0:
            return None
        
        moves = [*self.move_stack]
        moves.pop()

        return Zipper(self.tree, moves)

    def to_tree(self):
        """
        Return the tree 
        :returns: The zipper's tree
        """
        return self.tree
    
    def find_focus(self):
        """
        Find the current focus location in the tree by iterating through the 
        left and right moves.
        :returns: The current focus node.
        """
        current = self.tree
        if self.tree is None:
            return None
        
        if self.move_stack is not None and len(self.move_stack) > 0:
            for move in self.move_stack:
                if move == 'L' and current is not None and current['left'] is not None:
                    current = current['left']
                if move == 'R' and current is not None and current['right'] is not None:
                    current = current['right']
        
        return current
        