package linkedlist

import (
	"errors"
)

// Define List and Node types here.
type Node struct {
	Value any
	next  *Node
	prev  *Node
}

type List struct {
	head *Node
	tail *Node
}

// Note: The tests expect Node type to include an exported field with name Value to pass.

// Create a new list struct
// @param elements: Values to prepopulate the list with (variadic)
// @returns: pointer to a List struct that contains the new list prepopulated with given elements.
func NewList(elements ...any) *List {
	result := &List{head: nil, tail: nil}

	for _, elem := range elements {
		result.Push(elem)
	}

	return result
}

// Create a new Node struct
// @param value: The node's value
// @param next: The next node after this new one.
// @param prev: The previous node before this new one.
// @returns: pointer to a Node struct that contains the new node.
func NewNode(value any, next, prev *Node) *Node {
	return &Node{Value: value, next: next, prev: prev}
}

// Return the next node in the list after the given node n
// @param n: The current node
// @returns: nil if no next node otherwise the node after n
func (n *Node) Next() *Node {
	if n == nil {
		return nil
	}
	return n.next
}

// Return the previous node in the list before the given node n
// @param n: The current node
// @returns: nil if no previous node otherwise the node before n
func (n *Node) Prev() *Node {
	if n == nil {
		return nil
	}
	return n.prev
}

// Add a new value to the beginning of the list
// @param v: The value to encapsulate in a node and add to the beginning of the list.
func (l *List) Unshift(v any) {
	if l.head == l.tail && l.head == nil {
		l.head = NewNode(v, nil, nil)
		l.tail = l.head
		return
	}
	newNode := NewNode(v, l.head, nil)
	l.head.prev = newNode
	l.head = newNode
}

// Add a new value to the end of the list
// @param v: The value to encapsulate in a node and add to the end of the list.
func (l *List) Push(v any) {
	if l.head == l.tail && l.head == nil {
		l.head = NewNode(v, nil, nil)
		l.tail = l.head
		return
	}
	newNode := NewNode(v, nil, l.tail)
	l.tail.next = newNode
	l.tail = newNode
}

// Remove an element from the beginning of the list and return it.
// @returns: value of element at the beginning of the list
// @raises: error if no node at head of list.
func (l *List) Shift() (any, error) {
	if l.head == nil {
		return nil, errors.New("No value to shift")
	}
	head := l.head
	l.head = l.head.next
	if l.head == nil {
		l.tail = nil
	} else {
		l.head.prev = nil
	}
	return head.Value, nil
}

// Remove an element from the end of the list and return it.
// @returns: value of element at the beginning of the list
// @raises: error if no node at head of list.
func (l *List) Pop() (any, error) {
	if l.tail == nil {
		return nil, errors.New("No value to shift")
	}
	tail := l.tail
	l.tail = l.tail.prev
	if l.tail == nil {
		l.head = nil
	} else {
		l.tail.next = nil
	}
	return tail.Value, nil
}

// Reverse a list in place.
func (l *List) Reverse() {
	oldHead := l.head
	oldTail := l.tail
	current := l.head
	// go through list swapping next and previous
	for current != nil {
		next := current.next
		current.next = current.prev
		current.prev = next
		current = next
	}
	// swap beginning and end
	l.tail = oldHead
	l.head = oldTail
}

// Return the first node in the list
// @returns: first node in the list
func (l *List) First() *Node {
	return l.head
}

// Return the last node in the list
// @returns: last node in the list
func (l *List) Last() *Node {
	return l.tail
}

// Return the count of nodes in the list
// @returns: count of nodes in the list
func (l *List) Count() int {
	ret := 0
	current := l.head
	for current != nil {
		ret++
		current = current.next
	}
	return ret
}

// Delete removes the first node in a list with a given value.
// Returns true if a node was removed.
func (l *List) Delete(v any) bool {
	current := l.head
	if current == nil {
		return false // empty list
	}

	// search for value
	for current != nil && current.Value != v {
		current = current.next
	}

	if current == nil {
		return false // not found
	}

	// save next/prev to bypass the current value
	prev := current.prev
	next := current.next
	// fix next
	if next != nil {
		next.prev = prev
	}
	// fix previous
	if prev != nil {
		prev.next = next
	}
	// fix head
	if l.head == current {
		l.head = next
	}
	// fix tail
	if l.tail == current {
		l.tail = prev
	}
	return true
}
