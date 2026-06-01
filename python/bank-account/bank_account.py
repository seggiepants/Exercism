"""
Bank Account class implementation
"""
from enum import Enum
import threading

class AccountStatus(Enum):
    """
    Account Status Enumeration
    """
    PENDING = 0
    OPEN = 1
    CLOSED = 2

class BankAccount:
    """
    Bank Account class
    """
    def __init__(self):
        """
        Setup the Bank Account class
        """
        self.state = AccountStatus.PENDING
        self.balance = 0
        self.mutex = threading.Lock()

    def get_balance(self):
        """
        Return the balance of the account
        :raises: ValueError if the account is closed
        """
        if self.state == AccountStatus.CLOSED:
            raise ValueError('account not open')
        return self.balance

    def open(self):
        """
        Open an account
        :raises: Value error if account is open
        """
        if self.state == AccountStatus.OPEN:
            raise ValueError('account already open')
        self.state = AccountStatus.OPEN

    def deposit(self, amount):
        """
        Deposit money into the account
        :param amount: The amount to deposit
        :raises: ValueError if not open or negative amount to deposit
        """
        if self.state != AccountStatus.OPEN:
            raise ValueError('account not open')
        if amount < 0:
            raise ValueError('amount must be greater than 0')
        with self.mutex:
            self.balance += amount

    def withdraw(self, amount):
        """
        Withdraw money from the account
        :param amount: The amount to withdraw
        :raises: ValueError if account not open, amount < 0 or amount > balance
        """
        if self.state == AccountStatus.CLOSED:
            raise ValueError('account not open')
        if amount > self.balance:
            raise ValueError('amount must be less than balance')
        if amount < 0:
            raise ValueError('amount must be greater than 0')
        with self.mutex:
            self.balance -= amount

    def close(self):
        """
        Close the account
        """
        if self.state != AccountStatus.OPEN:
            raise ValueError('account not open')
        self.state = AccountStatus.CLOSED
        with self.mutex:
            self.balance = 0
