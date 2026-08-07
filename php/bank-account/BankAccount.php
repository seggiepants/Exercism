<?php
// Bank Account exercise. Doesn't look like PHP has native mutex so that aspect is removed.

declare(strict_types=1);

// Simulates a Bank Account that you can open, close, deposit and withdraw from.
class BankAccount
{
    private string $err_msg = "account not open";
    private int $balance;
    private bool $closed = true;

    // Open the account
    public function open()
    {
        if ($this->closed == false) {
            throw new Exception("account already open");
        }
        $this->closed = false;
        $this->balance = 0;
    }

    // Close the account
    public function close()
    {
        if ($this->closed) {
            throw new Exception($this->err_msg);
        }
        $this->balance = 0;
        $this->closed = true;

    }

    // Retrieve the current account balance
    // @returns: The account balance (int)
    // @throws: Exception if account is not open
    public function balance(): int
    {
        if ($this->closed) {
            throw new Exception($this->err_msg);
        }
        return $this->balance;
    }

    // Deposit money into the account
    // @param $amt: Amount to deposit (int)
    // @throws: Exception if account is not open, or amount <= 0
    public function deposit(int $amt)
    {
        if ($this->closed) {
            throw new Exception($this->err_msg);
        }
        if ($amt <= 0) {
            throw new InvalidArgumentException("amount must be greater than 0");
        }
        $this->balance += $amt;
    }

    // Withdraw money from the account
    // @param $amt: Amount to withdraw (int)
    // @throws: Exception if account is not open, or amount <= 0 or amount > balance
    public function withdraw(int $amt)
    {
        if ($this->closed) {
            throw new Exception($this->err_msg);
        }
        if ($amt <= 0) {
            throw new InvalidArgumentException("amount must be greater than 0");
        }
        if ($amt > $this->balance) {
            throw new InvalidArgumentException("amount must be less than balance");
        }        
        $this->balance -= $amt;
    }
}
