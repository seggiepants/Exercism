<?php
// Square Root - Compute the Integer Square Root of a Number

declare(strict_types=1);

const MAX_GUESSES = 100;

// Integer Square Root using Heron's method
// https://en.wikipedia.org/wiki/Square_root_algorithms
// @param $number: Number to return the integer square root of.
// @returns: Integer square root of $number.
function squareRoot(int $number): int
{

    if ($number < 0) {
        throw new InvalidArgumentException("Only positive integers are accepted.");
    }
    if ($number == 0) {
        return 0;
    }

    $guess = max(1, intdiv($number, 2));
    $next_guess = 0;
    for($i = 0; $i < MAX_GUESSES; $i++) {
        $next_guess = intdiv($guess + intdiv($number, $guess), 2);
        if ($next_guess ** 2 == $number || abs($next_guess - $guess) <= 1)
            break;
        $guess = $next_guess;
    }
    return $next_guess;
}
