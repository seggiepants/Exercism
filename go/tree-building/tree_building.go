// Tree Building exercise. I thought we were supposed to be refactoring.
package treebuilding

import (
	"errors"
	"slices"
)

// I thought we were supposed to be refactoring.

type Record struct {
	ID     int
	Parent int
	// feel free to add fields as you see fit
}

type Node struct {
	ID       int
	Children []*Node
	// feel free to add fields as you see fit
}

// Build a tree of nodes from a slice of records.
// @param records: slice of record.
// @returns: Root node of the reconstituted tree or an error.
// @raises: Error for node IDs not in order or duplicated, bad parent ID, multiple root nodes
// or lookup for node, node's parent, or root node not found.
func Build(records []Record) (*Node, error) {
	nodes := make(map[int]*Node, 0)
	// Make a node for each record.
	for _, rec := range records {
		if rec.Parent > rec.ID {
			return nil, errors.New("Parent ID must be less than or equal to ID")
		}
		if rec.ID >= len(records) {
			return nil, errors.New("ID is too large")
		}
		nodes[rec.ID] = &Node{ID: rec.ID, Children: []*Node{}}
	}

	if len(nodes) < len(records) {
		return nil, errors.New("Duplicate ID found.")
	}

	// Build the tree
	var root *Node = nil
	var ok bool
	for _, rec := range records {
		if rec.ID == rec.Parent {
			if root != nil {
				return nil, errors.New("Multiple root nodes found.")
			}
			root, ok = nodes[rec.ID]
			if !ok {
				return nil, errors.New("Expected node not found - root.")
			}
		} else {
			node, ok := nodes[rec.ID]
			if !ok {
				return nil, errors.New("Expected node not found - child.")
			}
			parent, ok := nodes[rec.Parent]
			if !ok {
				return nil, errors.New("Expected node not found - parent.")
			}
			parent.Children = append(parent.Children, node)
			slices.SortFunc(parent.Children, func(a, b *Node) int { return a.ID - b.ID })
		}
	}
	if root == nil {
		return nil, errors.New("No root node found.")
	}
	return root, nil
}
