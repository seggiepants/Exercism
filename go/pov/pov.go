package pov

import "slices"

type Tree struct {
	// Add the needed fields here
	label    string
	children []*Tree
}

// New creates and returns a new Tree with the given root value and children.
func New(value string, children ...*Tree) *Tree {
	return &Tree{label: value, children: children}
}

// Value returns the value at the root of a tree.
func (tr *Tree) Value() string {
	return tr.label
}

// Children returns a slice containing the children of a tree.
// There is no need to sort the elements in the result slice,
// they can be in any order.
func (tr *Tree) Children() []*Tree {
	return tr.children
}

// String describes a tree in a compact S-expression format.
// This helps to make test outputs more readable.
// Feel free to adapt this method as you see fit.
func (tr *Tree) String() string {
	if tr == nil {
		return "nil"
	}
	result := tr.Value()
	if len(tr.Children()) == 0 {
		return result
	}
	for _, ch := range tr.Children() {
		result += " " + ch.String()
	}
	return "(" + result + ")"
}

// POV problem-specific functions

// Find a node in the tree
// @param root: Root of the tree to search from
// @param value: The label of the node we are searching for
// @param source: The path build so far.
// @returns: Path from root to node with the given value and boolean indicating success/failure
func find(root *Tree, value string, source []*Tree) (dest []*Tree, ok bool) {
	if root.label == value {
		return append(source, root), true
	}

	for _, child := range root.children {
		newPath, ok := find(child, value, source)
		if ok {
			return append([]*Tree{root}, newPath...), true
		}
	}
	return nil, false
}

// FromPov returns the pov from the node specified in the argument.
// @param from: The node that should be at root of the reoriented tree.
// @returns: The root of the reoriented tree.
func (tr *Tree) FromPov(from string) *Tree {
	if tr.label == from {
		return tr
	}
	path, ok := find(tr, from, []*Tree{})
	if !ok {
		return nil
	}
	root := path[0]
	path = slices.Delete(path, 0, 1)
	for len(path) > 0 {
		next_node := path[0]
		path = slices.Delete(path, 0, 1)
		for index, child := range root.children {
			if child.label == next_node.label {
				root.children = slices.Delete(root.children, index, index+1)
				break
			}
		}
		next_node.children = append(next_node.children, root)
		root = next_node
	}
	return root
}

// PathTo returns the shortest path between two nodes in the tree.
// @param from: Label of node to start with
// @param to: Label of node to end on.
// @returns: Slice of strings one element for each step from 'from' to 'to'.
func (tr *Tree) PathTo(from, to string) []string {
	if from == to {
		return []string{from}
	}

	root_from, ok := find(tr, from, []*Tree{})
	if !ok {
		return nil
	}

	root_to, ok := find(tr, to, []*Tree{})
	if !ok {
		return nil
	}

	from_path := make([]string, len(root_from))
	for index, node := range root_from {
		from_path[index] = node.Value()
	}

	to_path := make([]string, len(root_to))
	for index, node := range root_to {
		to_path[index] = node.Value()
	}

	current := ""
	switched := false
	index := len(from_path) - 1
	ret := []string{}

	for !switched || index < len(to_path) {
		if switched {
			ret = append(ret, to_path[index])
			index += 1
		} else {
			current = from_path[index]
			contains_index := slices.Index(to_path, current)
			if contains_index > -1 {
				index = contains_index + 1
				switched = true
			} else {
				index -= 1
			}

			// Turn around at root.
			if index < 0 {
				switched = true
				index = 1
			}

			// Return the current item.
			ret = append(ret, current)
		}
	}
	return ret
}
