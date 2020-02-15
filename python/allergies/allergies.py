class Allergies(object):
    """Object use to store and query a score for a list allergencs or 
    particular to test to see if a particular allergen is a problem.
    """
    def __init__(self, score):
        """Object used to classify a set of allegens that a user 
        is allergic to. Allergens are stored as a bitmask so that one
        numnber can afford you several allergen flags. Allergens not in 
        the database will not be considered.
        """
        self.score = score
        self.allergens = {
            'eggs': 1,
            'peanuts': 2,
            'shellfish': 4,
            'strawberries': 8, 
            'tomatoes': 16, 
            'chocolate': 32, 
            'pollen': 64, 
            'cats': 128
        }

    def allergic_to(self, item):
        """Test if the object is flagged as allergic to a given item.
        Parameters:
        item: (String) name of the allergen. This is case sensitive and must 
              be an exact string match.
        Returns:
            true if the allergen is found and the object is scored as allergic.
            false if the allergen is found and the object is not scored as allergic.
            and exception if the given allergen is not found in the database
            (may be due to a misspelling or extra whitespace).
        """
        if item in self.allergens.keys():
            bit_flag = self.allergens[item]
            return (bit_flag & self.score) != 0
        else:
            raise Exception(f'Allergen "{item}", not found in database.')

    @property
    def lst(self):
        """Return a list of allergens in the database that the given 
        object is marked as allergic to.
        Returns:
        - List of strings of allergens object is allergic to.
        """
        return [key for key in self.allergens.keys() if self.allergic_to(key)]
        