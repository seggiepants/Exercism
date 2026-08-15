// Zipper exercise
package zipper

import "errors"

type Node struct {
	value int   // Value for the node
	left  *Node // Left sub tree
	right *Node // Right sub tree
}

type Zipper struct {
	// Add fields here.
	current *Node  // Where we are in the tree
	root    *Node  // The root of the tree
	path    string // Path to take from the root of the tree to the current node
}

// Create a brand new zipper from the given node (node assumed to be root and current node)
// @returns: New zipper struct initialized with the given root node
func NewZipper(tree *Node) Zipper {
	return Zipper{current: tree, root: tree, path: ""}
}

// Return the value at the currently selected node in the tree
// @returns: value at current node, or 0 if there is no currently selected node.
func (z Zipper) Value() int {
	if z.current == nil {
		return 0
	}
	return z.current.value
}

// Return the tree from the zipper
// @returns: The root of the tree.
func (z Zipper) ToTree() *Node {
	return z.root
}

// Move focus to the node on the left of the current. Adds an L to the path.
// @returns: New zipper with focus set to the left subtree
// @raises: Error if there is no left subtree to switch to.
func (z Zipper) Left() (Zipper, error) {
	if z.current == nil || z.current.left == nil {
		return NewZipper(nil), errors.New("No left node to change to.")
	}

	return Zipper{current: z.current.left, root: z.root, path: z.path + "L"}, nil
}

// Move focus to the node on the right of the current. Adds an R to the path.
// @returns: New zipper with focus set to the right subtree
// @raises: Error if there is no right subtree to switch to.
func (z Zipper) Right() (Zipper, error) {
	if z.current == nil || z.current.right == nil {
		return NewZipper(nil), errors.New("No right node to change to.")
	}
	return Zipper{current: z.current.right, root: z.root, path: z.path + "R"}, nil
}

// Go up a step in the tree and set the current node to that. Chops the last entry off from the path too
// @returns: New zipper with position set to parent of the current node.
// @raises: Error if there is no parent node.
func (z Zipper) Up() (Zipper, error) {
	current, err := z.GetParent()
	if err != nil {
		return Zipper{}, err
	}
	path := z.path
	if len(path) > 0 {
		path = z.path[:len(z.path)-1]
	}
	return Zipper{current: current, root: z.root, path: path}, nil
}

// Copy the node tree in the zipper -- used in Copy()
// @param root: The root of the tree to copy from (used in recursive calls)
// @returns: reference to the copy of root of the tree with sub-trees
func (z Zipper) CopyTree(root *Node) *Node {
	if root == nil {
		return nil
	}
	return &Node{value: root.value, left: z.CopyTree(root.left), right: z.CopyTree(root.right)}
}

// Make an independent copy of this zipper so that edits are not reflected in this copy
// @returns: New zipper that is a copy of this one.
func (z Zipper) Copy() Zipper {
	root := z.CopyTree(z.root)
	current := root
	// find current.
	for _, char := range z.path {
		if char == 'L' && current.left != nil {
			current = current.left
		}
		if char == 'R' && current.right != nil {
			current = current.right
		}
	}
	return Zipper{root: root, current: current, path: z.path}

}

// Find the parent of the current node (used for up)
// walks the path except the last node from root
// @returns: Parent of the current node.
// @raises: Error if already at root
func (z Zipper) GetParent() (*Node, error) {
	if len(z.path) <= 0 {
		return nil, errors.New("Cannot move up")
	}
	path := z.path[:len(z.path)-1]
	current := z.root
	for _, char := range path {
		if char == 'L' && current.left != nil {
			current = current.left
		}
		if char == 'R' && current.right != nil {
			current = current.right
		}
	}
	return current, nil
}

// Overwrite the value at the current node.
// @param v: new value for the current node
// @returns: New zipper with the updated tree.
func (z Zipper) SetValue(v int) Zipper {
	next := z.Copy()
	next.current.value = v
	return next
}

// Overwrite the Left branch of the current node.
// @param v: Node to use as the new left branch
// @returns: New zipper with the updated tree.
func (z Zipper) SetLeft(v *Node) Zipper {
	next := z.Copy()
	next.current.left = v
	return next
}

// Overwrite the Right branch of the current node.
// @param v: Node to use as the new right branch
// @returns: New zipper with the updated tree.
func (z Zipper) SetRight(v *Node) Zipper {
	next := z.Copy()
	next.current.right = v
	return next
}
