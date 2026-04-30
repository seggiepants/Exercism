"""
(Singly) Linked List implementation with Node and EmpyListExeption classes
"""

class EmptyListException(Exception):
    """
    Exception for an empty linked list
    """
    pass


class Node:
    """
    Node object. The link in the linked list
    """
    def __init__(self, value):
        """
        Construct a new linked list node.
        :param value: The value for this node.
        """
        self.node_value = value
        self.node_next = None

    def value(self):
        """
        Return the value for this node.
        """
        return self.node_value

    def next(self):
        """
        Return a reference to the next item in the linked list.
        """
        return self.node_next


class LinkedList:
    """
    Linked list of Node objects
    """
    def __init__(self, values=None):
        """
        Create a new linked list with optional starting values
        :param values: list of values to add to the list
        """
        self.top = None
        self.index = None
        if values is not None:
            for value in values:
                self.push(value)

    def __iter__(self):
        """
        Setup the linked list for iteration
        :returns: object to iterate with
        """
        self.index = self.top
        return self
    
    def __next__(self):
        """
        Get the current item in the list and move to the next
        :returns: value at the current position in the list
        :raises: StopIteration if we get to the end of the list
        """
        if self.index is None:
            raise StopIteration
        
        current = self.index.value()
        self.index = self.index.next()
        return current

    def __len__(self):
        """
        Get the length of this linked list
        :returns: length of the linked list.
        """
        ret = 0
        current = self.top
        while current is not None:
            ret += 1
            current = current.next()
        return ret

    def head(self):
        """
        Return the node at the head of the list
        :returns: The node at the head of the list
        :raises: EmptyListException if list is empty.
        """
        if self.top is None:
            raise EmptyListException('The list is empty.')
        return self.top

    def push(self, value):
        """
        Push a new value to the head of the list.
        :param value: The value to add (as a node) to the head of the list.
        """
        new_node = Node(value)
        if self.top is not None:
            new_node.node_next = self.top
        
        self.top = new_node


    def pop(self):
        """
        Pop a value from the linked list at the head of the list.
        :returns: Value at the head of the list removing that node.
        :raises: EmptyListException if the list is empty.
        """
        if self.top is None:
            raise EmptyListException('The list is empty.')
        
        current_value = self.top.value()
        self.top = self.top.next()
        return current_value

    def reversed(self):
        """
        Reverse this linked list. Return the values as a list in reverse order
        :returns: A list of the linked list node values in reverse order.
        """
        current = self.top
        ret = []
        while current is not None:
            ret.insert(0, current.value())
            current = current.next()

        return ret
