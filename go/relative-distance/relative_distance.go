// Relative Distance exercise - find how closely related two people are in a family tree.
package relativedistance

import "slices"

type Node struct {
	name     string
	parents  map[string]*Node
	children map[string]*Node
}

// Calculate how many people are separating two people in a family tree.
// @param familyTree: map[string][]string or name to slice of child names.
// @param personA: The first person to find a link in the family tree for.
// @param personB: The second person to find a link in the family tree for.
// @returns: How many degrees of separation between personA and personB in the given family tree.
// @raises: Return value set to false if no path was found between personA and personB.
func DegreeOfSeparation(familyTree map[string][]string, personA, personB string) (int, bool) {
	nodes := make(map[string]Node, 0)
	for name, children := range familyTree {
		FillChildren(nodes, nil, name, children)
	}
	_, ok := nodes[personA]
	if !ok {
		return 0, false
	}
	_, ok = nodes[personB]
	if !ok {
		return 0, false
	}
	for _, node := range nodes {
		if len(node.parents) == 0 {
			// start from a root.
			pathA, ok := FindPath(&node, personA)
			if !ok {
				continue
			}
			pathB, ok := FindPath(&node, personB)
			if !ok {
				continue
			}
			total := len(pathA) + len(pathB)
			duplicates := 0
			for i := 0; i < min(len(pathA), len(pathB)); i++ {
				if pathA[i] == pathB[i] {
					if pathA[i] == personA || pathA[i] == personB || pathB[i] == personA || pathB[i] == personB {
						duplicates++
					} else {
						duplicates += 2
					}
				} else {
					break
				}
			}
			return total - duplicates - 1, true
		}

	}
	return 0, false
}

// Fill out the parent child relationships between nodes in the dataset.
// @param nodes: map of names to their node.
// @param parent: What to populate for the node's parent, nil if no parent info
// @param name: The name of the child to populate
// @param children: The children of the node with the name equal to name parameter
// will call this function on all children of current node.
// @returns: Address of the populated node.
func FillChildren(nodes map[string]Node, parent *Node, name string, children []string) *Node {
	nodeParent, ok := nodes[name]
	if !ok {
		nodeParent = Node{name: name, parents: make(map[string]*Node, 0), children: make(map[string]*Node, 0)}
		nodes[name] = nodeParent
	}

	if parent != nil {
		if _, ok = nodeParent.parents[parent.name]; !ok {
			nodeParent.parents[parent.name] = parent
		}
	}
	if len(children) > 0 {
		for _, child := range children {
			nodeChild := FillChildren(nodes, &nodeParent, child, []string{})
			nodeParent.children[child] = nodeChild
		}
	}

	return &nodeParent
}

// Find a path from the root to the a node with the desired name
// @param root: Where to start looking for the node with the desired name
// @param name: The name to look for.
// @returns: Slice of string with steps in the path to the name, and a
// boolean flag saying if anything was found.
// @raises: ok = false if no path found.
func FindPath(root *Node, name string) ([]string, bool) {
	if root.name == name {
		return []string{name}, true
	}

	for _, childNode := range root.children {
		path, ok := FindPath(childNode, name)
		if ok {
			return slices.Concat([]string{root.name}, path), true
		}
	}
	return []string{}, false
}
