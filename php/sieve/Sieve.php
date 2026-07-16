<?php
// Sieve of Erastosthenes exercise.

declare(strict_types=1);

// Compute primes up to $number using the Sieve of Erastosthenes
// @param $number: Number to stop at.
// @returns: Prime numbers between 2 and $number.
function sieve(int $number): array
{
    $nums = array_fill(2, $number - 1, 0);
    for ($i = 2; $i <= $number; $i++) {
        if ($nums[$i] == 0) {
            for($j = 2 * $i; $j <= $number; $j+= $i) {
                $nums[$j] = 1;
            }
        }
    }
    return array_keys(array_intersect($nums, [0]));
}
