"""
Binary Search Tree implementation
"""

class TreeNode:
    """
    A node in a Binary Search Tree
    """
    def __init__(self, data, left=None, right=None):
        """
        Initialize a Binary Search Tree node.
        If you pass in a left or right node, you must ensure the nodes are properly ordered or 
        you may get undesired behavior
        :param data: The value to use for this node.
        :param left: A TreeNode to use as the left side, normally None in which case left will be created on insert.
        :param right: A TreeNode to use as the right side, normally None in which case right will be created on insert.
        """
        self.data = data
        self.left = left
        self.right = right

    def insert(self, data):
        """
        Insert a value into the binary search tree using current node as root.
        :param data: The data to insert as a new node.
        """
        if data <= self.data:
            if self.left is None:
                self.left = TreeNode(data)
            else:
                self.left.insert(data)
        elif data > self.data:
            if self.right is None:
                self.right = TreeNode(data)
            else:
                self.right.insert(data)
        
    def walk(self):
        """
        Walk the tree returning all of the nodes in order
        :returns: List of nodes in sorted order.
        """
        ret = [] 
        if self.left is not None:
            ret.extend(self.left.walk())
        ret.append(self.data)
        if self.right is not None:
            ret.extend(self.right.walk())
        
        return ret

    def __str__(self):
        """
        Return this TreeNode as a string. This will implicily call the 
        __str__ functions on the left and right nodes too.
        """
        return f'TreeNode(data={self.data}, left={self.left}, right={self.right})'


class BinarySearchTree:
    """
    BinarySearchTree class - holds TreeNode objects as nodes.
    """
    def __init__(self, tree_data):
        """
        Initialize a binary search tree.
        :param tree_data: list of values to sequentially add to the tree.
        :returns: Root of binary search tree."""

        self.root = None
        for data in tree_data:
            if self.root is None:
                self.root = TreeNode(data)
            else:
                self.root.insert(data)

    def data(self):
        """
        Return the data for the root of the tree.
        """
        return self.root

    def sorted_data(self):
        """
        Return a list of data for the root node and all nodes to the left and right.
        Will be an in-order walk resulting in a sorted list for a valid binary search tree
        """
        return self.root.walk()
