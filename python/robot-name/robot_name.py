import random   # for randomly generating a robot name.
import string   # for the handy uppercase letters and digits strings

# Some constants in case we want to modify the robot naming conventions later.
NUM_CHARS = 2
NUM_DIGITS = 3

class Robot(object):
    """Robot object. The robot has a name, but not much else. All names
    are globably unique and non-reusable. They are set by the factory.
    """

    # Want this to be shared by all robot class instances.
    robot_names = set()

    def __init__(self):
        """Initialize the robot and give it a new factory name."""
        self._name = self.random_name()
    
    @property
    def name(self):
        """The name of the robot"""
        return self._name

    def reset(self):
        """Reset the robot to factory defaults and give it a new name."""
        self._name = self.random_name()

    def random_name(self):
        """Generate a new globably unique robot name. This function may
        never return if the random number generator runs out of names it 
        can create or if we use up all robot names. Will get slower over
        time.
        """
        name = ''
        while len(name) == 0:
            # Generate a random name based on the give format.
            # loop until you get one that isn't already in use.
            # When you find a new one add it to the global list.
            name = random.choices(string.ascii_uppercase, k= NUM_CHARS) 
            name += random.choices(string.digits, k=NUM_DIGITS)
            name = ''.join(name)

            if name in self.robot_names:
                # Duplicate name, try again.
                name = ''
            else:
                # Save the new name so we don't get a duplicate later.
                self.robot_names.add(name)
        
        return name

