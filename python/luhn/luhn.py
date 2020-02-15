import functools
import re

class Luhn(object):
    def __init__(self, card_num):
        # save card number, but remove any spaces
        self.card_num = card_num.replace(" ", "")

    def valid(self):        
        # reject if not all digits
        if re.search("[^0-9]", self.card_num) != None:
            return False
        
        # reject if card number is too short
        if len(self.card_num) <= 1:
            return False

        # change the card number from a string of digit characters to a list of numbers
        digits = list(map((lambda x: int(x)), list(self.card_num)))
        
        # reverse it so we don't need special cases for odd/even lengths
        digits.reverse()

        # sum up the even digits doubling them, but subtracting 9 if it goes over 9
        score = sum(map((lambda x: (x * 2) - 9 if x * 2 > 9 else x * 2), digits[1::2]))
        
        # add in the sum of odd digits
        score = score + sum(digits[::2]) 

        # return true if score is evenly divisble by 10
        return (score % 10) == 0
