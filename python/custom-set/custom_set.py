"""
Custom Set Class Implementation
"""

class CustomSet:
    """
    Custom Set class
    I will resist the temptation to just route this through a python set.
    A list will be used instead.
    :param elements: Elements to initialize the custom set with
    """
    def __init__(self, elements=None):
        self.storage = []
        if elements is None:
            elements = []
        for element in elements:
            self.add(element)

    def isempty(self):
        """
        Check if the set is empty
        :returns: True if an empty set
        """
        return len(self.storage) == 0

    def __contains__(self, element):
        """
        Check if the custom set contains a specific element
        :param element: What to search for in the set.
        :returns: True if the element is found.
        """
        return element in self.storage

    def issubset(self, other):
        """
        Check if this set is a sub set of the other custom set
        :param other: The set to check is a equal to/superset of this set.
        :returns: True if this set is a subset of the other set.
        """
        missing = [element for element in self.storage if element not in other.storage]
        return len(missing) == 0

    def isdisjoint(self, other):
        """
        Check if this set is disjoint with another set (they have no overlapping items)
        :param other: The set to check against
        :returns: True if both sets are non-empty and contain unique items.
        """
        if self.isempty() or other.isempty():
            return True
        
        shared = [element for element in self.storage if element in other.storage]
        return len(shared) == 0

    def __eq__(self, other):
        """
        Check if two sets are equal
        :param other: The other custom set object to compare against
        :returns: True if the sets are the same.
        """
        # False if they are different lengths.
        if len(self.storage) != len(other.storage):
            return False
        
        # If any element in this set is not in the other they are not equal.
        unequal = [element for element in self.storage if element not in other.storage]        
            
        # Same length, same elements
        return len(unequal) == 0

    def __hash__(self) -> int:
        """
        Return a hash of this custom set object.
        Only added this because exercism complains if you have eq but not hash
        :returns: hash of this custom set object.
        """
        return hash(self.storage)
        

    def add(self, element):
        """
        Add an element into the custom set if it is not already present.
        :param element: The item to add to the custom set
        """
        if not element in self.storage:
            self.storage.append(element)

    def intersection(self, other):
        """
        Return a new set that contains elements common to this set and the other set
        :param other: Custom set to compare against
        :returns: A new custom set object containing the shared elements
        """
        return CustomSet([element for element in self.storage if element in other.storage])

    def __sub__(self, other):
        """
        Return a new set that contains elements in self that are not in the other set.
        :param other: Custom set to compare against
        :returns: A new custom set object containing the elements unique to this set.
        """
        return CustomSet([element for element in self.storage if element not in other.storage])

    def __add__(self, other):
        """
        Return a new set that cointains the unique elements between this set and the other set
        :param other: The Custom set to make a new set with
        :returns: A new custom set with unique elements from both sets
        """
        ret = [*self.storage]
        ret.extend([element for element in other.storage if element not in self.storage])
        return CustomSet(ret)
