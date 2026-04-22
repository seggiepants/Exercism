"""
Character class for a DND game.
Constructor will produce a new character with random ability values.
"""

import math
import random

class Character:
    """
    DND Character class
    """
    def __init__(self):
        """
        Create a new character object with 
        randomly rolled attributes
        """
        self.strength = self.ability()
        self.dexterity = self.ability()
        self.constitution = self.ability()
        self.intelligence = self.ability()
        self.wisdom = self.ability()
        self.charisma = self.ability()
        constitution_modifier = modifier(self.constitution)
        self.hitpoints = constitution_modifier + 10

    @staticmethod
    def ability():
        """
        Generate an ability from four (six-sided) die rolls
        :returns: the sum of the three largest die rolls.
        """
        rolls = [roll(), roll(), roll(), roll()]
        return sum(rolls) - min(rolls)

def modifier(value):
    """
    Calculate the consitution modifier with the given value
    :param value: Character's constitution
    :returns: Constitution modifier (value subtract 10 divide by two and round down)
    """
    return math.floor((value - 10) / 2)

def roll():
    """
    Roll a single die once
    :returns: a random integer between 1 and 6 inclusive.
    """
    return int(random.random() * 6) + 1