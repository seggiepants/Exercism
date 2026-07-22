// Simple Linked list exercise.
package simplelinkedlist

import (
	"errors"
	"slices"
)

// Define the List and Element types here.
type List struct {
	head *Element
}

type Element struct {
	data int
	next *Element
}

// Create a brand new list and add the given elements to it.
// @param elements: zero or more elements to add to the new list.
// @returns: Reference to the new list.
func New(elements []int) *List {
	var ret List = List{head: nil}

	for _, elem := range elements {
		ret.Push(elem)
	}
	return &ret
}

// Return the number of items in the list (linear cost)
// @returns: The number of elements in the list.
func (l *List) Size() int {
	var count int = 0
	for elem := l.head; elem != nil; elem = elem.next {
		count++
	}
	return count
}

// Push a new item to the head of the list.
// @param element: The value to push
func (l *List) Push(element int) {
	node := Element{data: element, next: l.head}
	l.head = &node
}

// Return the value at the head of the list and remove it.
// @returns: Item at the head of the list.
// @raises: Error if the list is empty
func (l *List) Pop() (int, error) {
	if l.head == nil {
		return 0, errors.New("list is empty")
	}
	top := l.head
	l.head = top.next
	return top.data, nil
}

// Return the value at the head of the list.
// @returns: Item at head of list or error for empty list
// @raises: Error is empty list.
func (l *List) Peek() (int, error) {
	if l.head == nil {
		return 0, errors.New("list is empty")
	}

	return l.head.data, nil
}

// Returns the items in the list as a slice of int
// @returns: Slice of integer for the items in the list.
func (l *List) Array() []int {
	var ret []int = make([]int, l.Size())
	var index = 0
	for current := l.head; current != nil; current = current.next {
		ret[index] = current.data
		index++
	}
	slices.Reverse(ret)
	return ret
}

// Return a reversed copy of this list.
// @returns: New list of same items but in reverse order
func (l *List) Reverse() *List {
	var nums []int = l.Array()
	slices.Reverse(nums)
	return New(nums)
}
