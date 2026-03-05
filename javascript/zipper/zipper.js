//
// This is only a SKELETON file for the 'Zipper' exercise. It's been provided as a
// convenience to get you started writing code faster.
//
// trees are {value, left, right} where left and right may be null

export class Zipper {
  constructor(tree, moveStack = null) {
    this.tree = tree
    this.moveStack = moveStack === null ? [] : moveStack
    this.focus = moveStack === null || moveStack.length === 0 ? tree : this.findFocus()
  }

  static fromTree(tree, moveStack = null) {
    return new Zipper(tree, moveStack)
  }

  static TreeToString(node)
  {
    let left = node.left === null ? 'null' : this.TreeToString(node.left)
    let right = node.right === null ? 'null' : this.TreeToString(node.right)
    return `(value: ${node.value}, left: ${left}, right: ${right})`
  }

  findFocus()
  {
    let current = this.tree
    if (this.moveStack != null)
    {
      for(let move of [...this.moveStack])
      {
        if (move === 'L' && current !== null && current.left !== null)
          current = current.left
        else if (move === 'R' && current !== null && current.right !== null)
          current = current.right
      }
    }
    return current
  }

  toTree() {
    return this.tree
  }

  value() {
    return this.focus.value
  }

  left() {
    if (this.focus === null || this.focus.left === null)
      return null
    return new Zipper(this.tree, [...this.moveStack, 'L'])
  }

  right() {
    if (this.focus === null || this.focus.right === null)
      return null
    return new Zipper(this.tree, [...this.moveStack, 'R'])
  }

  up() {
    if (this.moveStack === null || this.moveStack.length === 0)
      return null
    let newStack = [...this.moveStack]
    newStack.pop()
    return new Zipper(this.tree, newStack)
  }

  setValue(value) {
    let newZipper = Zipper.fromTree(Zipper.TreeCopy(this.tree), [... this.moveStack])
    if (newZipper.focus !== null)
    {
      newZipper.focus.value = value
    }
    return newZipper
  }

  setLeft(value) {
    let newZipper = Zipper.fromTree(Zipper.TreeCopy(this.tree), [... this.moveStack])
    if (newZipper.focus !== null)
    {
      newZipper.focus.left = value
    }
    return newZipper
  }

  setRight(value) {
    let newZipper = Zipper.fromTree(Zipper.TreeCopy(this.tree), [... this.moveStack])
    if (newZipper.focus !== null)
    {
      newZipper.focus.right = value
    }
    return newZipper
  }

  static TreeCopy(tree)
  {
    let left = tree.left === null ? null : this.TreeCopy(tree.left)
    let right = tree.right === null ? null : this.TreeCopy(tree.right)
    return {value: tree.value, left: left, right: right}
  }
}
