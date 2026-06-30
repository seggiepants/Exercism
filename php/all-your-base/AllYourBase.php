<?php
// All You Base: Exercise where you manually change numbers between 
// number bases (Binary, Decimal, Hex, etc.)

declare(strict_types=1);

/// Change a number given by an array of digits from a beginning base 
// to a target number base.
// @param $fromBase: The number base the digits are from.
// @param $digits: Array of digits to convert
// @param $toBase: The number to return should be in this base.
// @returns: Array of digits for the original number in the new base.
// @raises: InvalidArgumentException if there is a bad input base or a digit 
// that doesn't exist in the original base.
function rebase(int $fromBase, array $digits, int $toBase): array
{
    $number = 0;
    if ($fromBase <= 1) {
        throw new InvalidArgumentException("input base must be >= 2");
    } 
    if ($toBase <= 1) {
        throw new InvalidArgumentException("output base must be >= 2");
    }
    // Go from fromBase to a number.
    for ($i = 0; $i < count($digits); $i++) {
        if ($digits[$i] >= $fromBase || $digits[$i] < 0) {
            throw new InvalidArgumentException("all digits must satisfy 0 <= d < input base");
        }
        $number *= $fromBase;        
        $number += $digits[$i];
    }
    $result = array();
    if ($number == 0) {
        array_push($result, 0);
    } else {
        // Go from a number to array of digits in the target base.
        while ($number != 0) {
            $digit = $number % $toBase;
            $number = ($number - $digit) / $toBase;
            array_unshift($result, $digit);
        }
    }
    return $result;
}
