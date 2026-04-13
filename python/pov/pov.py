"""
Code to reorient a tree based on a given node. 
"""

from json import dumps


class Tree:
    """
    N-Ary tree class implementation with just string (label) as node values.
    """
    def __init__(self, label, children=None):
        """
        Class initializer
        """
        self.label = label
        self.children = children if children is not None else []

    def __dict__(self):
        """
        Return class as a dictionary
        """
        return {self.label: [child.__dict__() for child in sorted(self.children)]}

    def __str__(self, indent=None):
        """
        Return class as a string
        """
        return dumps(self.__dict__(), indent=indent)

    def __lt__(self, other):
        """
        Compare with another tree and check if one is less than the other based on label string compare
        """
        return self.label < other.label

    def __eq__(self, other):
        """
        Compare with another tree and check if they are equal (if the dictionaries are equal)
        """
        return self.__dict__() == other.__dict__()
    
    def __hash__(self) -> int:
        """
        Compute the hash code for this object
        """
        return hash(self.label, self.children)
    
    def find(self, root, value, path):
        """
        Find a value in the tree starting with a given root
        root = Label of node to start searching at.
        value = label we are looking for.
        path = list filled with path from root to value
        """
        if root.label == value:
            path.append(root)
            return True
        
        for child in root.children:
            ret = self.find(child, value, path)
            if ret:
                path.insert(0, root)
                return True
        return False

    def from_pov(self, from_node):
        """
        Rebuild the tree with the given node as root.
        from_node = The node that should be at root of the reoriented tree.
        """
        if self.label == from_node:
            return self
        
        path = []
        if not self.find(self, from_node, path):
            raise ValueError('Tree could not be reoriented')
        root = path[0]
        path.pop(0)
        while len(path) > 0:
            next_node = path.pop(0)

            root.children = [child for child in root.children if child.label != next_node.label]
            next_node.children.append(root)
            root = next_node
        return root

    def path_to(self, from_node, to_node):
        """
        Return the path from one node to the other. It goes from from_node up the tree
        potentially to the root and back down (if necessary) to to_node.
        - from_node = Label of node to start with
        - to_node = Label of node to end on.
        """
        if from_node == to_node:
            return [from_node]
        
        root_from = []
        if not self.find(self, from_node, root_from):
            raise ValueError('Tree could not be reoriented')

        root_to = []
        if not self.find(self, to_node, root_to):
            raise ValueError('No path found')
        
        from_path = [node.label for node in root_from]
        to_path = [node.label for node in root_to]
        
        current = ''
        switched = False
        index = len(from_path) - 1
        ret = []
        
        while not switched or index < len(to_path):
            if switched:
                current = to_path[index]
                index += 1
                ret.append(current)
            else:
                current = from_path[index]
                next_index = -1
                if current in to_path:
                    next_index = to_path.index(current)
                
                if next_index >= 0:
                    switched = True
                    index = next_index + 1
                else:
                    index -= 1


                # Turn around at root.
                if index < 0:
                    switched = True
                    index = 1           

                # Return the current item.
                ret.append(current)

        return ret
