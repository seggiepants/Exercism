//
// This is only a SKELETON file for the 'Linked List' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

class Node {
  constructor(value)
  {
    this.value = value
    this.next = null
    this.prev = null
  }
}

export class LinkedList {
  constructor()
  {
    this.first = null 
    this.last = null
  }
  push(value) {
    let current = new Node(value)
    if (this.first == null)
      this.first = current

    if (this.last == null)
      this.last = current
    else
    {
      this.last.next = current
      current.prev = this.last
      this.last = current
    }

  }

  pop() {
    let current = this.last
    if (current == null)
      return null
    this.last = current.prev
    if (this.first === current)
      this.first = null
    return current.value
  }

  shift() {
    let current = this.first
    if (current == null)
      return null
    this.first = current.next    
    return current.value
  }

  unshift(value) {
    let current = new Node(value)
    if (this.first == null)
      this.first = current

    if (this.last == null)
      this.last = current
    else
    {
      this.first.prev = current
      current.next = this.first
      if (this.last === current)
        this.last = null
      this.first = current
    }
  }

  find(value) {
    let current = this.first
    while (current != null && current.value !== value)
    {
      current = current.next
    }

    return current
  }

  delete(value) {
    let current = this.find(value)
    if (current == null)
      return

    if (this.first === current)
      this.first = current.next

    if (this.last === current)
      this.last = current.prev

    if (current.next != null)
    {
      current.next.prev = current.prev
    }

    if (current.prev != null)
    {
      current.prev.next = current.next
    }

  }

  count() {
    let count = 0
    let current = this.first
    while (current != null)
    {
      count++
      current = current.next
    }
    return count
  }  
}
