<?php
// Collatz Conjecture exercise.

declare(strict_types=1);

// Calculate the number of step from the given number to 1 using
// the algorithm for the Collatz Conjecture.
// @param $number: The number to start with
// @returns: The number of steps to reach one.
function steps(int $number): int
{
    $steps = 0;
    if ($number <= 0) {
        throw new InvalidArgumentException("Only positive numbers are allowed");
    }
    while ($number != 1) {
        if ($number % 2 == 0) {
            $number = $number / 2;
        } else {
            $number = (3 * $number) + 1;
        }
        $steps++;
    }

    return $steps;
}
