<?php
// Binary Search exercise.

declare(strict_types=1);

// Find a value in a sorted array using Binary Search
// Chose to do it iteratively this time.
// @param $needle: The value to search for
// @param $haystack: The values to search through.
// @returns: Index where the value was found or -1 if not found.
function find(int $needle, array $haystack): int
{
    if (count($haystack) == 0) {
        return -1;
    }

    $start = 0;
    $end = count($haystack) - 1;
    while (true) {
        if ($end - $start <= 0) {
            if ($haystack[$start] == $needle) {
                return $start;
            }
            return -1;
        }

        $middle = $start + intval(($end - $start) / 2);
        $value = $haystack[$middle];
        
        if ($value == $needle) {
            return $middle;
        } 
        if ($value > $needle) {
            $end = $middle - 1;
        }
        if ($value < $needle) {
            $start = $middle + 1;
        }
    }
}