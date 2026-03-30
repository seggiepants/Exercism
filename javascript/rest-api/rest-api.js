//
// This is only a SKELETON file for the 'Rest API' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class RestAPI {
  constructor(users = []) {
    this.database = users
    // don't want any missing fields.
    this.database.users.map((value) => this.cleanUpUser(value))
  }

  get(url) {    
    if (url.startsWith('/users'))
    {
      let users = url.split('?users=')
      users.shift() // get rid of the /users?users= part
      return this.handleGet(users)
    }
    else
    {
        throw new Error('No handler found.')
    }
  }

  post(url, payload) {
    if (url.startsWith('/add'))
    {
      return this.handleAdd(payload)
    }
    else if (url.startsWith('/iou'))
    {
      return this.handleIOU(payload)
    }
    else 
    {
      throw new Error('No handler found.')
    }
  }

  cleanUpUser(user) {
    if (!('owes' in user))
      user.owes = {}

    if (!('owed_by' in user))
      user.owed_by = {}

    if (!('balance' in user))
      user.balance = 0.0

    return user
  }

  handleAdd(payload) 
  {
    let user = this.cleanUpUser({ name: payload.user })
    if (this.database.users.filter((value) => value.name === user.name) > 0)
      throw new Error(`User: ${user.name} already exists in database.`)
    this.database.users.push(user)
    return user
  }

  handleGet(users)
  {
    if (users.length === 0)
      return this.database
    else
    {      
      return {users: this.database.users.filter((value) => users.includes(value.name))}
    }
  }

  handleIOU(payload)
  {
      let lender = payload.lender
      let lenderObj = this.database.users.filter((value) => value.name === lender)
      
      // Add lender if they don't exist
      if (lenderObj.length === 0)
      {
        lenderObj = this.cleanUpUser({name: lender})
        this.database.users.push(lenderObj)
      }
      else 
        lenderObj = lenderObj[0]  // first element returned

      let borrower = payload.borrower
      let borrowerObj = this.database.users.filter((value) => value.name === borrower)
      // Add Borrower if they don't exist
      if (borrowerObj.length === 0)
      {
        borrowerObj = this.cleanUpUser({name: borrower})
        this.database.users.push(borrowerObj)
      }
      else
        borrowerObj = borrowerObj[0]

      if (!(borrower in lenderObj.owed_by))
        lenderObj.owed_by[borrower] = payload.amount 
      else 
        lenderObj.owed_by[borrower] += payload.amount

      if (!(lender in borrowerObj.owes))
        borrowerObj.owes[lender] = payload.amount 
      else 
        borrowerObj.owes[lender] += payload.amount

      this.recalculateBalance(lenderObj)
      this.recalculateBalance(borrowerObj)
      return {users: [lenderObj, borrowerObj].sort((a, b) => a.name.localeCompare(b.name))}
  }

  recalculateBalance(user)
  {
    let loans = {}
    // Fill loans with owed amounts as negative
    for(let userName of Object.keys(user.owes))
    {
      if (!(userName in loans))
        loans[userName] = -1 * user.owes[userName]
      else
        loans[userName] -= user.owes[userName]
    }

    // Add in owed_by as positive
    for(let userName of Object.keys(user.owed_by))
    {
      if (!(userName in loans))
        loans[userName] = user.owed_by[userName]
      else
        loans[userName] += user.owed_by[userName]
    }

    // Clear them out and repopulate with updated balances
    user.owes = {}
    user.owed_by = {}
    // Balance is just the sum of amounts
    user.balance = Object.values(loans).reduce((acc, value) => acc += value, 0)
    for(let userName of Object.keys(loans))
    {
      let amt = loans[userName]
      if (amt < 0)
        user.owes[userName] = -1 * amt
      else if (amt > 0)
        user.owed_by[userName] = amt 

      // skip 0.00 as those loans are satisfied
    }
  }
}
