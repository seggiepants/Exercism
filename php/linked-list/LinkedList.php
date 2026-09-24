<?php

// Linked List exercise

declare(strict_types=1);

// Class to hold a single node in the list.
class Node {
    public ?Node $next;
    public ?Node $prev;
    public int $value;

    // Create a node and give it a value.
    public function __construct($value) {
        $this->value = $value;
        $this->next = null;
        $this->prev = null;
    }
}

// Class to represent the doubly linked list.
class LinkedList
{
    private ?Node $head;
    private ?Node $tail;

    // Build a new linked list with no nodes.
    public function __construct()
    {
        $this->head = null;
        $this->tail = null;
    }

    // Add a value to the end of the list.
    // @param $value: The value to add at the end of the list.
    public function push($value) {
        if ($this->tail == null) {
            $this->head = new Node($value);
            $this->tail = $this->head;
        } else {
            $next = new Node($value);
            $next->prev = $this->tail;
            $this->tail->next = $next;
            $this->tail = $next;
        }
    }

    // Remove the value at the end of the list.
    // @returns: Value at the end of the list or null if an empty list.
    public function pop(): ?int {
        if ($this->tail == null) {
            return null;
        }
        $value = $this->tail->value;
        $this->tail = $this->tail->prev;
        if ($this->tail != null) {
            $this->tail->next = null;
        } else {
            $this->head = null;
        }
        return $value;
    }

    // Add a value at the front of the list
    // @param $value: The value to add.
    public function unshift($value) {
        if ($this->head == null) {
            $this->head = new Node($value);
            $this->tail = $this->head;
        } else {
            $first = new Node($value);
            $first->next = $this->head;
            $this->head->prev = $first;
            $this->head = $first;
        }
    }

    // Remove the value at the font of the list
    // @returns: Value at the front of the list, or null if an empty list.
    public function shift() : ?int {
        if ($this->head == null) {
            return null;
        }
        $value = $this->head->value;
        $this->head = $this->head->next;
        if ($this->head != null) {
            $this->head->prev = null;
        } else {
            $this->tail = null;
        }
        return $value;
    }

    // Search for a value in the list and delete the value from the list if found.
    // @param $value: The numeric value to search for.
    public function delete($value) {
        $node = $this->head;
        while ($node != null && $node->value != $value) {
            $node = $node->next;
        }
        if ($node != null && $node->value == $value) {
            if ($node->next != null) {
                $node->next->prev = $node->prev;
            }
            if ($node->prev != null) {
                $node->prev->next = $node->next;
            }
            if ($this->head == $node) {
                $this->head = $node->next;
            }
            if ($this->tail == $node) {
                $this->tail = $node->prev;
            }
            unset($node);
        }
    }

    // Return the number of items in the list.
    // @returns: Count of items in the list
    public function count() : int {
        $result = 0;
        $node = $this->head;
        while ($node != null) {
            $result++;
            $node = $node->next;
        }
        return $result;
    }

    // Return a text representation of the list. (for debugging)
    // @returns: A string that represents the list [/] are the null pointers 
    // before the start and after the end of the list and node values are 
    // encased in square brackets ([]). Ex: [/][1][2][3][/]
    public function toString() : string {
        $result = "[\]";
        $node = $this->head;
        while ($node != null) {
            $result .= "[" . strval($node->value) . "]";
            $node = $node->next;
        }
        $result .= "[\]";
        return $result;
    }
}
