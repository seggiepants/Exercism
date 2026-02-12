//
// This is only a SKELETON file for the 'Bank Account' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class BankAccount {
  constructor() {
    this.state = 'pending'
    this._balance = 0
  }

  open(newBalance = 0.00) {
    if (this.state === 'open')
      throw new ValueError()

    this.state = 'open'
    this._balance = newBalance
  }

  close() {
    if (this.state !== 'open')
      throw new ValueError()
    this.state = 'closed'
  }

  deposit(amount) {    
    if (this.state !== 'open' || amount <= 0)
      throw new ValueError()
    this._balance += amount
  }
 
  withdraw(amount) {
    if (this.state === 'closed')
      throw new ValueError()
    if (amount > this.balance || amount <= 0) 
      throw new ValueError()
    else
      this._balance -= amount
  }

  get balance() {
    if (this.state === 'closed')
      throw new ValueError()
    return this._balance
  }
}

export class ValueError extends Error {
  constructor() {
    super('Bank account error');
  }
}
