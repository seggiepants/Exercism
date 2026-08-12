// Satellite exercise - Rebuild a binary tree from inorder and preorder traversals
package satellite

import (
	"errors"
	"slices"
)

type Node struct {
	Value string
	Left  *Node
	Right *Node
}

// Rebuild a Binary Tree from inorder and preorder traversals.
// Tree is rebuilt recursively.
// @param preorder: slice of strings that preorder iterate over the tree (self, left, right)
// @param inorder: slice of strings that inorder iterate over the tree (left, self, right)
// @returns: Reconstructed tree
// @raises: Error is non-nil if the inorder and preorder slices are not the same length.
func TreeFromTraversals(preorder, inorder []string) (*Node, error) {
	indexPre := 0

	if len(preorder)+len(inorder) == 0 {
		return nil, nil
	}
	if len(preorder) != len(inorder) {
		return nil, errors.New("traversals must have the same length")
	}
	pre := make(map[string]int, 0)
	in := make(map[string]int, 0)
	for i := 0; i < len(preorder); i++ { // both slices already checked to be the same length.
		pre[preorder[i]] = 1
		in[inorder[i]] = 1
	}

	// if pre or in is shorter than inorder or preorder we have one or more duplicate items
	if len(pre) != len(preorder) || len(in) != len(inorder) {
		return nil, errors.New("traversals must contain unique items")
	}

	// First item in pre-order is the root.
	root := Node{Value: preorder[indexPre], Left: nil, Right: nil}
	indexPre = slices.Index(inorder, root.Value)
	if indexPre >= 0 {
		var left *Node = nil
		var right *Node = nil
		var err error = nil
		if indexPre > 0 {
			left, err = TreeFromTraversals(preorder[1:indexPre+1], inorder[:indexPre])
			if err != nil {
				return nil, err
			}
		}
		root.Left = left
		right, err = TreeFromTraversals(preorder[indexPre+1:], inorder[indexPre+1:])
		if err != nil {
			return nil, err
		}
		root.Right = right
	} else {
		return nil, errors.New("traversals must have the same elements")
	}
	return &root, nil
}
