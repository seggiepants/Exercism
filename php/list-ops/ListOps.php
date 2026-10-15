<?php

// Exercise: List Operations

declare(strict_types=1);

// Class to hold list operations.
class ListOps
{
    // given two lists, add all items in the second list to the end of the first list
    // @param array $list1: The list to add items from $list2 to.
    // @param array $list2: The list that has items to add to the end of $list1
    // @returns: an array with all of the list items from both lists
    public function append(array $list1, array $list2): array
    {
        foreach($list2 as $item) {
            $list1[] = $item;
        }
        return $list1;
    }

    // given a series of lists, combine all items in all lists into one flattened list
    // @param array $list1: The list to add items from $list2 to.
    // @param array $listn: A set of lists that has items to add to the end of $list1
    // @returns: an array with all of the list items from all lists
    public function concat(array $list1, array ...$listn): array
    {
        foreach($listn as $list) {
            foreach($list as $item) {
                $list1[] = $item;
            }
        }
        return $list1;
    }

    // given a predicate and a list, return the list of all items for which predicate(item) is True);
    // @param callable(mixed $item): bool $predicate The function to call on each item, returns true/false
    // @param array $list: List that has the $predicate function called on each item.
    // @returns array: The items in the list that the predicate evaluates to true 
    public function filter(callable $predicate, array $list): array
    {
        $result = [];
        foreach($list as $item) {
            if ($predicate($item)) {
                $result[] = $item;
            }
        }
        return $result;
    }

    // given a list, return the total number of items within it
    // @param array $list: Return the length of the given list
    // @returns int: The length of $list.
    public function length(array $list): int
    {
        return count($list);
    }

    // given a function and a list, return the list of the results of applying function(item) on all items
    // @param callable(mixed $item): mixed $function: The function to call on each list item.
    // @param array $list: The list to call $function on each element of.
    // @returns array: array of results of calling $function on each element of $list;
    public function map(callable $function, array $list): array
    {
        $result = [];
        foreach($list as $item) {
            $result[] = $function($item);
        }
        return $result;
    }

    // given a function, a list, and initial accumulator, fold (reduce) each item into the accumulator from the left
    // @param callable(mixed $accumulator, mixed $item): mixed $function: Function to call on each element.
    // @param array $list: The list to call the function on left to right.
    // @param mixed $accumulator: Initial value to start folding with.
    // @returns: The accumulated value.
    public function foldl(callable $function, array $list, $accumulator)
    {
        $result = $accumulator;
        foreach($list as $item) {
            $result = $function($result, $item);
        }
        return $result;
    }

    // given a function, a list, and an initial accumulator, fold (reduce) each item into the accumulator from the right
    // @param callable(mixed $accumulator, mixed $item): mixed $function: Function to call on each element.
    // @param array $list: The list to call the function on right to left.
    // @param mixed $accumulator: Initial value to start folding with.
    // @returns: The accumulated value.
    public function foldr(callable $function, array $list, $accumulator)
    {
        $result = $accumulator;
        for($i = count($list) - 1; $i >= 0; $i--) {
            $result = $function($result, $list[$i]);
        }
        return $result;
    }

    // given a list, return a list with all the original items, but in reversed order
    // @param array $list: The list to reverse.
    // @returns: A reversed copy of $list
    public function reverse(array $list): array
    {
        $result = [];
        for($i = count($list) - 1; $i >= 0; $i--) {
            $result[] = $list[$i];
        }
        return $result;
    }
}
