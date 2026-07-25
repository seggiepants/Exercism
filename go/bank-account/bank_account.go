// Bank Account example. Used to test concurrency with mutexes.
package bankaccount

import "sync"

// Account data
type Account struct {
	balance int64
	opened  bool
	mu      sync.Mutex
}

// Open an account and return a reference to it.
// @param amt: Initial account deposit amount.
// @returns: nil if initial deposit is negative otherwise a reference to the new account.
func Open(amt int64) *Account {
	if amt < 0 {
		return nil
	}
	return &Account{balance: amt, opened: true}
}

// Return the account balance.
// @returns: Balance of the account and success flag.
// @raises: Unsuccessful if acount not opened.
func (a *Account) Balance() (bal int64, ok bool) {
	a.mu.Lock()
	defer a.mu.Unlock()
	if !a.opened {
		return 0, false
	}
	return a.balance, true
}

// Deposit funds into the account. Calls Withdraw if amount is less than or equal to zero
// @param amt: The amount to deposit.
// @returns: The updated balance and a success flag.
// @raises: Unsuccessful if account not opened or any withdrawl problem if amount <= 0.
func (a *Account) Deposit(amt int64) (newBal int64, ok bool) {
	if amt <= 0 {
		return a.Withdraw(amt * -1)
	}
	a.mu.Lock()
	defer a.mu.Unlock()
	if !a.opened {
		return 0, false
	}
	a.balance += amt
	return a.balance, true
}

// Withdraw amount from account balance
// @param amt: The amount to withdraw
// @returns: Remainining balance and success flag
// @raises: Not successful if insufficient funds, negative withdrawl, or unopened account.
func (a *Account) Withdraw(amt int64) (newBal int64, ok bool) {
	a.mu.Lock()
	defer a.mu.Unlock()
	if !a.opened || amt <= 0 {
		return 0, false
	}
	if a.balance < amt {
		return 0, false
	}
	a.balance -= amt
	return a.balance, true
}

// Close the account if opened.
// @returns: Payout of existing amount, and success flag
// @raises: Sucess flag false if not opened
func (a *Account) Close() (pay int64, ok bool) {
	a.mu.Lock()
	defer a.mu.Unlock()
	if !a.opened {
		return 0, false
	}
	a.opened = false
	payOut := a.balance
	a.balance = 0
	return payOut, true
}
