"""
Simulate a single game of bowling and allow checking the final score upon completion
"""

class BowlingGame:
    """
    Class that simulates a single game of bowling
    """
    def __init__(self):
        """
        Initialize a bowling game
        """
        self.frame = 1
        self.pins = 0
        self.ball_rolls = 0
        self.remaining = 10
        self.rolls = []

    def roll(self, pins):
        """
        One roll of the ball in a game
        :param pins: The number of pins knocked down for this throw.
        :raises: ValueError exception if pins are negative, greater than remaining, or if the method is called when the game is completed.
        """
        if pins < 0:
            raise ValueError('Negative roll is invalid')
        
        if pins > self.remaining:
            raise ValueError('Pin count exceeds pins on the lane')

        if self.frame > 10:
            raise ValueError('Cannot roll after game is over')

        self.ball_rolls += 1
        self.pins += pins
        self.remaining -= pins

        if self.frame == 10 and (self.pins == 10 or pins == 10):
            self.remaining = 10 # Reset if we threw a strike/spare since we don't advance the frame yet.

        if self.frame <= 9 and (self.ball_rolls == 2 or self.pins == 10):
            self.next_frame()
        elif self.frame == 10:
            if self.ball_rolls == 3 or (self.ball_rolls == 2 and self.pins < 10):
                self.next_frame()
        self.rolls.append(pins)

    def next_frame(self):
        """
        Setup the next frame
        """
        self.pins = 0
        self.ball_rolls = 0
        self.frame += 1
        self.remaining = 10

    def score(self):
        """
        Calculate the game's score
        :raises: Exception if the game is not finished.
        """
        frame = 1
        ball_rolls = 0
        pins = 0
        points = 0

        if self.frame < 11:
            raise Exception('Score cannot be taken until the end of the game')

        for index, roll in enumerate(self.rolls):
            if frame > 10:
                break
            ball_rolls += 1
            pins += roll

            if ball_rolls >= 2 or pins == 10:
                extra = 0
                # Strike or spare
                if pins == 10 and 1 <= ball_rolls <= 2:
                    # Strike or Spare gets one bonus
                    extra += self.rolls[index + 1] if index + 1 < len(self.rolls) else 0
                    
                if pins == 10 and ball_rolls == 1:
                    # Strike gets a second bonus
                    extra += self.rolls[index + 2] if index + 2 < len(self.rolls) else 0
                points = points + pins + extra
                ball_rolls = 0
                pins = 0
                frame += 1
        return points
