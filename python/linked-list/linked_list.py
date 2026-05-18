"""
Doubly Linked list implementation
"""

class Node:
    """
    Node in a doubly-linked list
    """
    def __init__(self, value, succeeding=None, previous=None):
        """
        Initialize a node in the list.
        :param value: The node's value
        :param succeeding: The next node in the list
        :param previous: The previous node in the list
        """
        self.value = value 
        self.next = succeeding
        self.prev = previous


class LinkedList:
    """ 
    Doubly Linked List
    """
    def __init__(self):
        """
        Intialize a new Doubly linked list
        """
        self.head = None
        self.tail = None
        self.pointer = None

    def push(self, value):
        """
        Add a node with a given value to the end of the list
        :param value: Value to encapsulate into a node and add to the end of the list.
        """
        node = Node(value, None, self.tail)
        if self.tail is not None:
            self.tail.next = node
        self.tail = node
        if self.head is None:
            self.head = node
    
    def pop(self):
        """
        Remove the last node from the list and return its value
        :returns: The value from the last element in the list before being removed
        :raises: Index Error if an empty list """
        if self.tail is None:
            raise IndexError("List is empty")
        old_tail = self.tail
        self.tail = old_tail.prev
        if self.tail is None:
            self.head = None
        else:
            self.tail.next = None
        return old_tail.value 
    
    def unshift(self, value):
        """
        Add a new node with a given value to the beginning of the list
        :param value: Value to encapsulate into a node and add to the front of the list
        """
        node = Node(value, self.head, None)
        if self.head is not None:
            self.head.prev = node
        self.head = node
        if self.tail is None:
            self.tail = node
    
    def shift(self):
        """
        Remove and return the value of the node at the beginning of the list
        :returns: Value of the node at the beginning of the list before removal.
        :raises: IndexError if the list is empty
        """
        if self.head is None:
            raise IndexError("List is empty")
        old_head = self.head
        self.head = old_head.next 
        if self.head is None:
            self.tail = None
        else:
            self.head.prev  = None
        return old_head.value
    
    def delete(self, value):
        """
        Delete the first node with the given value from the list.
        :param value: The value of the node to delete
        :raises: ValueError if value is not found in the list.
        """
        pointer = self.head
        while pointer is not None and pointer.value != value:
            pointer = pointer.next
        if pointer is None:
            raise ValueError("Value not found")
        node_prev = pointer.prev
        node_next = pointer.next 
        if node_prev is not None:
            node_prev.next = node_next 
        if node_next is not None:
            node_next.prev = node_prev 
        if self.head == pointer:
            self.head = node_next
        if self.tail == pointer:
            self.tail = node_prev
    
    def __len__(self):
        """
        Return the length of the list
        :returns: length of the list
        """
        if self.head == self.tail:
            if self.head is None:
                return 0
            return 1
        counter = 0        
        pointer = self.head
        while pointer is not None:
            counter += 1
            pointer = pointer.next 
        
        return counter
    
    def __iter__(self):
        """
        Setup the list to be iterated over.
        """
        self.pointer = self.head
        return self
    
    def __next__(self):
        """
        Get current value from the iteration then move to the next item in the list.
        :returns: The current value for the position of the iteration
        :raises: StopIteration when we reach the end of the list
        """
        if self.pointer is None:
            raise StopIteration()
        value = self.pointer.value
        self.pointer = self.pointer.next 
        return value
