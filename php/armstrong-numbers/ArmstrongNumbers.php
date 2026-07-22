<?php
// Armstrong number exercise. A number is an armstrong number if the sum of digits raised to the power of 
// the number of digits in the number is the same as the number. 153 => 1^3 + 5^3 + 3^3 = 1 + 125 + 27 = 153 = Armstrong.
declare(strict_types=1);

// Check if a number is an Armstrong number (all digits raised to the power of the number of 
// digits and summed is the same as the number).
// @param $number: The number to check.
// @returns: True if Armstrong Number and False if it isn't.
function isArmstrongNumber(int $number): bool
{
    $digits = array();
    $current = $number;
    while ($current != 0) {
        $digit = $current % 10;
        $current = ($current - $digit) / 10;
        $digits[] = $digit;
    }
    $power = count($digits);
    return $number == array_sum(array_map(fn ($digit) => pow($digit, $power), $digits));
}
