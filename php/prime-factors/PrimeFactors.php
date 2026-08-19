<?php
// Prime Factors exercise. Find the set of prime numbers that are the product of a number.


declare(strict_types=1);

// Find the prime factors of a given number.
// These are the prime numbers that are multiplied to produce the numbers. A prime factor may appear
// multiple times 2 * 2 = 4 for example.
// @param $number: The number to find factors for.
// @returns: Array of prime factors. May be an empty array for a number < 2 or when no factors are found.
function factors(int $number): array
{
    if ($number <= 1) {
        return array();
    }
    $factors = array();
    $done = false;
    while (!$done) {
        for ($value = 2; $value <= $number; $value++) {
            if ($number % $value == 0) {
                $factors[] = $value;
                $number = $number / $value;
                break;
            }
        }
        $done = $number <= 1;
    }
    return $factors;
}
