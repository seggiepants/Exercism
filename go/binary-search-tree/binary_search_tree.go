// Binary Search Tree exercise.
package binarysearchtree

// Struct for Binary Search Tree
type BinarySearchTree struct {
	left  *BinarySearchTree
	data  int
	right *BinarySearchTree
}

// NewBst creates and returns a new BinarySearchTree.
// @param i: data for the Binary Search Tree node created.
// @returns: New Binary Search Tree node with i as data and left & right = nil
func NewBst(i int) *BinarySearchTree {
	return &BinarySearchTree{left: nil, right: nil, data: i}
}

// Insert inserts an int into the BinarySearchTree.
// I am not sure why we need to allow multiple copies of the same value in the tree.
// Seems wrong to me.
// @param i: data to insert into the tree.
func (bst *BinarySearchTree) Insert(i int) {
	if bst.data >= i {
		if bst.left == nil {
			bst.left = NewBst(i)
		} else {
			bst.left.Insert(i)
		}
	} else if bst.data < i {
		if bst.right == nil {
			bst.right = NewBst(i)
		} else {
			bst.right.Insert(i)
		}
	}
}

// SortedData returns the ordered contents of BinarySearchTree as an []int.
// The values are in increasing order starting with the lowest int value.
// A BinarySearchTree that has the numbers [1,3,7,5] added will return the
// []int [1,3,5,7].
// @returns: The data from the tree node and it's children in sorted order
// as a slice of int.
func (bst *BinarySearchTree) SortedData() []int {
	var result []int = []int{}
	if bst.left != nil {
		result = append(result, bst.left.SortedData()...)
	}
	result = append(result, bst.data)
	if bst.right != nil {
		result = append(result, bst.right.SortedData()...)
	}
	return result
}
