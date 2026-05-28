"""
Find the relative distance between two names in a family tree.
"""

class RelativeDistance:
    """
    Class to find the relative distance between two names in a family tree
    """
    def __init__(self, family_tree):
        """
        Initialize the class saving the family tree for later use
        """
        self.family_tree = family_tree

    def one_degree(self, person):
        """
        find parents, children, and siblings of a given name (one degree of separation)
        :param person: Name of the person to find.
        :returns: list of parents, siblings and children of the searched for person.
        """
        ret = []
        tree_keys = self.family_tree.keys()
        # children
        if person in tree_keys:
            ret.extend(self.family_tree[person])

        # Parents & siblings
        for key in tree_keys:
            if person in self.family_tree[key]:
                ret.append(key)
                ret.extend(self.family_tree[key])

        # don't include person in the return set
        return list({entry for entry in ret if entry != person})

    def degree_of_separation(self, person_a, person_b):
        """
        Calculate the degrees of separation between person_a and person_b in a family tree
        :param person_a: Name of the first person to find a connection with.
        :param person_b: Name of the second person to find a connection with.
        :returns: The number of steps between the two people in the family tree.
        :raises: ValueError if person_a, or person_b are not in the family tree or if no connection between them is found.
        """
        # searched is none, new to search is one degree separation
        # if count doesn't change between rounds
        family_a = []
        new_family_a = self.one_degree(person_a)
        if len(new_family_a) == 0:
            raise ValueError('Person A not in family tree.')
        
        family_b = []
        new_family_b = self.one_degree(person_b)
        if len(new_family_b) == 0:
            raise ValueError('Person B not in family tree.')
        
        distance = 0
        
        while True:
            # check if the family groups overlapped in the last round
            distance += 1
            family_a = [*family_a, *new_family_a]
            family_b = [*family_b, *new_family_b]
            if person_b in family_a and person_a in family_b:
                return distance

            # Get the next set of relatives from the new search set
            next_family_a = []
            for person in new_family_a:
                current = self.one_degree(person)
                next_family_a.extend(current)
            
            next_family_b = []
            for person in new_family_b:
                current = self.one_degree(person)
                next_family_b.extend(current)

            # set the new search list. Only include names we haven't searched yet.
            new_family_a = [person for person in next_family_a if person not in family_a]
            new_family_b = [person for person in next_family_b if person not in family_b]

            # if no new names they are not related
            if len(new_family_a) + len(new_family_b) == 0:
                raise ValueError('No connection between person A and person B.')
