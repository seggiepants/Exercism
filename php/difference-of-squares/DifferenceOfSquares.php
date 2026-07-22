<?php
// Difference of Squares exercise

declare(strict_types=1);

// They said to lookup a good algorithm. I got this one from:
// https://www.geeksforgeeks.org/dsa/difference-between-sum-of-the-squares-of-first-n-natural-numbers-and-square-of-sum/

// Sum up natural numbers 1 to $max and return that squared.
// @param $max: The number to stop on.
// @returns: The square of the sum of the numbers 1-n.
function squareOfSum(int $max): int
{
    // Sum of n naturals numbers
    $sum = ($max * ($max + 1)) / 2;

    return $sum ** 2;  
}

// Sum up the squares of number 1 to $max.
// @param $max: The number to stop on
// @returns: The sum of the numbers 1 to $max squared and then added together.
function sumOfSquares(int $max): int
{
    return ($max * ($max + 1) * (2 * $max + 1)) / 6;
}

// The difference between sum of Squares and square of Sum
// @param $max: The value to stop on
// @returns: sum of squares minus the square of sum of numbers 1 to $max
function difference(int $max): int
{
    return squareOfSum($max) - sumOfSquares($max);
}
