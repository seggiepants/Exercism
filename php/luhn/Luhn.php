<?php

// Luhn exercise - Check if a number is a valid luhn number.

declare(strict_types=1);

// Is the given number a Luhn number?
// @param $number: The number to check
// @returns: True when a valid Luhn number.
function isValid(string $number): bool
{
    $filtered = str_replace(" ", "", $number);
    $filtered = mb_str_split($filtered);
    if (count($filtered) <= 1) {
        return false;
    }

    $even = true;
    $sum = 0;
    for ($i = count($filtered) - 1; $i >= 0; $i--) {
        $digit = $filtered[$i];
        if ($digit < "0" || $digit > "9") {
            return false;
        }
        $even = !$even;
        $num = intval($digit);
        if ($even) {
            $num *= 2;
            if ($num > 9) {
                $num -= 9;
            }
        }
        $sum += $num;

    }

    return $sum % 10 == 0;
}
