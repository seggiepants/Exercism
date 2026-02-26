//
// This is only a SKELETON file for the 'Simple Linked List' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Element {
  constructor(value) {    
    this._value = value
    this._next = null
  }

  get value() {
    return this._value
  }

  get next() {
    return this._next
  }
}

export class List {
  constructor(value = null) {    
    this._head = null
    if (value !== null)
    {
      if (Array.isArray(value))
      {
        value.map((item) => this.add(new Element(item)))
      }
      else 
      {
        value.add(new Element(value))
      }
    }
  }

  add(nextValue) {
    nextValue._next = this._head 
    this._head = nextValue 
  }

  get length() {
    let count = 0
    let element = this.head
    while (element !== null)
    {
      count++
      element = element.next
    }
    return count
  }

  get head() {
    return this._head
  }

  toArray() {
    let ret = []
    let element = this.head
    while (element !== null)
    {
      ret.push(element.value)
      element = element.next
    }
    return ret 
  }

  reverse() {
    // can't reverse a 0 or 1 length list
    if (this.head === null || this.head.next === null)
      return this

    let element = this.head
    let previous = null
    while(element.next != null)
    {
      let nextElement = element.next 
      element._next = previous 
      previous = element
      element = nextElement
    }
    element._next = previous
    this._head = element
    
    return this
  }
}
