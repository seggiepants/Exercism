//
// This is only a SKELETON file for the 'Sublist' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class List {
  constructor(data = []) {
    this.data = [ ...data ]
  }

  compare(other) {
    // Same length and empty is equal, Same length same data is equal
    if (this.data.length === other.data.length)
    { 
      if (this.data.length === 0)
        return 'EQUAL'
      if (this.data.filter((value, index) => value !== other.data[index]).length === 0)
        return 'EQUAL'
    }

    if (this.data.length === 0 && other.data.length > 0)
      // Empty is sublist of non-empty
      return 'SUBLIST'
    else if (this.data.length > 0 && other.data.length === 0)
      // Non-empty is a superlist of empty
      return 'SUPERLIST'
    else if (this.data.length === other.data.length)
      // Already said they are not equal so if they are the same
      // length they can't be super/sublist either
      return 'UNEQUAL' 
    else if (isSubList(this, other))
      return 'SUBLIST'
    else if (isSubList(other, this))
      return 'SUPERLIST'
    // Nothing left but unequal
    return 'UNEQUAL'
  }
}

// Check if a is a SubList of b
function isSubList(a, b)
{
  let i = -1
  // Find first match to first item in a
  i = b.data.findIndex((value, index) => value === a.data[0] && index > i)
  while (i > -1)
  {
    if (i + a.data.length > b.data.length)
      return false // not enough space
    // Count up the items that don't match between a and b.slice(i)
    let rejectCount = a.data.filter((value, index) => value !== b.data[i + index]).length
    // if no rejects it is a sublist
    if (rejectCount === 0)
      return true
    // Otherwise find the next index that matches the start of a
    i = b.data.findIndex((value, index) => value === a.data[0] && index > i)
  }
  // no match found not a sublist
  return false
}
