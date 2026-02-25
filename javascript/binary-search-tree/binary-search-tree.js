//
// This is only a SKELETON file for the 'Binary Search Tree' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class BinarySearchTree {
  constructor(data) {
    this._data = data
    this._left = null
    this._right = null 
  }

  get data() {
    return this._data
  }
  get right() {
    return this._right
  }

  get left() {
    return this._left
  }

  insert(data) {
    if (data <= this._data)
    {
      // left tree
      if (this._left === null)
        this._left = new BinarySearchTree(data)
      else 
        this._left.insert(data)
    }
    else 
    {
      // right tree
      if (this._right === null)
        this._right = new BinarySearchTree(data)
      else 
        this._right.insert(data)
    }
  }

  each(fn) {
    if (this._left)
      this._left.each(fn)
    fn(this._data)
    if (this._right)
      this._right.each(fn)    
  }
}
