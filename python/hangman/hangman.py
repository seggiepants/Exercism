"""
A class to allow the user to play Hangman
I don't really understand what Functional Reactive Programming is
So this may or may not fit. I tried. The tests assuming state updates
I thought I shouldn't be doing didn't help.
"""

# Game status categories
# Change the values as you see fit
STATUS_WIN = 'win'
STATUS_LOSE = 'lose'
STATUS_ONGOING = 'ongoing'


class Hangman:
    """
    Class to keep track of the state of a game of Hangman.
    Make a new object for every new word
    """
    def __init__(self, word):
        """
        Initialize the Hangman class with defaults.
        """
        self.remaining_guesses = 9
        self.word = word
        self.guesses = []        

    def guess(self, char):
        """
        Guess a letter in the word.
        :param char: The letter to guess
        """
        if self.get_status() != STATUS_ONGOING:
            raise ValueError('The game has already ended.')
        if char in self.guesses or char not in self.word:
            self.remaining_guesses -= 1
        self.guesses.append(char)

    def get_masked_word(self):
        """
        Get the word with any un-guessed character masked with a _ (underscore)
        :returns: The target word but with any unguessed letters replaced with an underscore (_).
        """
        return ''.join([char if char in self.guesses else '_' for char in self.word])

    def get_status(self):
        """
        Get the current game status
        :returns: Game Status, STATUS_WIN if we guessed the word, STATUS_LOSE 
        if there are no remaining guesses and STATUS_ONGOING otherwise.
        """
        if self.word == self.get_masked_word():
            return STATUS_WIN
        if self.remaining_guesses < 0:
            return STATUS_LOSE
        return STATUS_ONGOING
