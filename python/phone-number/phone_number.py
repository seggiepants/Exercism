"""
Validate if a given string is a proper phone number and if so return it in the desired format
"""

import re

class PhoneNumber:
    """
    Phone number class
    """
    def __init__(self, number):
        """
        Check if a number is a proper phone number.
        If valid, parse out the sections and and save it
        to the property .number (just the digits).
        """
        phone_number_regex = r'(\+?1[ -\.]*)?\(?(?P<area_code>[0-9]{3})\)?[ -\.]*(?P<exchange>[0-9]{3})[ -\.]*(?P<subscriber>[0-9]{4})'
        symbols = [symbol for symbol in number if symbol in r'!@#$%^&*;:<>,?/\|']
        if len(symbols) > 0:
            raise ValueError('punctuations not permitted')
        letters = [letter for letter in number if ('a' <= letter  <= 'z') or ('A' <= letter <= 'Z')]
        if len(letters) > 0:
            raise ValueError('letters not permitted')
        digits = [digit for digit in number if '0' <= digit <= '9']
        if len(digits) < 10:
            raise ValueError('must not be fewer than 10 digits')
        if len(digits) > 11:
            raise ValueError('must not be greater than 11 digits')
        if len(digits) == 11 and digits[0] != '1':
            raise ValueError('11 digits must start with 1')

        result = re.search(phone_number_regex, number)
        self.number = ''
        if result is not None:
            self.area_code = result.group('area_code') 
            if self.area_code[0] == '0':
                raise ValueError('area code cannot start with zero')
            if self.area_code[0] == '1':
                raise ValueError('area code cannot start with one')
                
            self.exchange = result.group('exchange') 
            if self.exchange[0] == '0':
                raise ValueError('exchange code cannot start with zero')            
            if self.exchange[0] == '1':
                raise ValueError('exchange code cannot start with one')
            
            self.subscriber = result.group('subscriber')
            self.number = self.area_code + self.exchange + self.subscriber

    def pretty(self):
        """
        Pretty print the class' phone number
        :returns: Pretty version of the class' phone number
        """
        return f'({self.area_code})-{self.exchange}-{self.subscriber}'
