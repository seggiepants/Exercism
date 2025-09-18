public class BankAccount
{
    Mutex mutex = new Mutex(); // Provides atomic access.
    bool isOpen = false;    // Is the account open.
    decimal balance;        // The balance of the account.

    /// <summary>
    /// Open an account.
    /// </summary>
    /// <exception cref="InvalidOperationException">Fails if the account is already open.</exception>
    public void Open()
    {
        if (isOpen)
            throw new InvalidOperationException("Account is already open.");
        isOpen = true;
        balance = 0;
    }

    /// <summary>
    /// Close this account.
    /// </summary>
    /// <exception cref="InvalidOperationException">Fails is the account is not open.</exception>
    public void Close()
    {
        if (!isOpen)
            throw new InvalidOperationException("Cannot close an Account that is not open.");
        isOpen = false;
    }

    /// <summary>
    /// Return the account balance.
    /// </summary>
    public decimal Balance
    {
        get
        {
            if (isOpen)
            {
                try
                {
                    mutex.WaitOne();
                    return balance;
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
                throw new InvalidOperationException("Account is not open.");
        }
    }

    /// <summary>
    /// Add money to the account.
    /// </summary>
    /// <param name="change">The amount to add.</param>
    /// <exception cref="InvalidOperationException">Fails if amount is less than or equal to zero.</exception>
    public void Deposit(decimal change)
    {
        if (change <= 0)
            throw new InvalidOperationException("Change must be a positive non-zero amount.");

        if (isOpen)
        {
            try
            {
                mutex.WaitOne();
                balance += change;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        else
            throw new InvalidOperationException("Account is not open.");

    }

    /// <summary>
    /// Remove money from the account.
    /// </summary>
    /// <param name="change">The amount to remove.</param>
    /// <exception cref="InvalidOperationException">Fails if the change is less than or equal to zero. Also will fail if the amount to withdraw exceeds the balance.</exception>
    public void Withdraw(decimal change)
    {
        if (isOpen)
        {
            if (balance - change < 0)
                throw new InvalidOperationException("Cannot withdraw in excess of balance.");
            else if (change <= 0)
                throw new InvalidOperationException("Change must be a positive non-zero amount.");
            try
            {
                mutex.WaitOne();
                balance -= change;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
        else
            throw new InvalidOperationException("Account is not open.");
    }
}
