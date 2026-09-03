<?php
// Exercise - Grains double values on a chessboard. Some string addition/pow thrown in to be annoying.

declare(strict_types=1);

// Return the number of grains of rice for a particular square on a chessboard one one square one and 
// double for each subsequent square.
// @returns: Total number of grains expressed as a string
function square(int $number): string
{
    if (!is_int($number)) {
        throw new InvalidArgumentException(strval($number) . " is not a number.");
    }
    if ($number < 1 || $number > 64) {
        throw new InvalidArgumentException(strval($number) . " is not a valid square only integers 1-64 are allowed.");
    }
    return pow2($number);
}

// Find the total number of grains on the entire chessboard if we start with one one square one and 
// double for each subsequent square.
// @returns: Total number of grains expressed as a string
function total(): string
{
    $total = "0";
    for ($i = 1; $i <= 64; $i++) {
        $total = add($total, square($i));
    }
    return $total;
}

// Add two numbers expressed as strings.
// This will be slow.
// @param $value1: First value as a string. Nothing outside of "0" to "9" accepted.
// @param $value2: Second value as a string. Nothing outside of "0" to "9" accepted.
// @returns: Sum of the two values expressed as a string.
function add($value1, $value2) : string {
    $carry = 0;
    $output = "";
    $index1 = strlen($value1) - 1;
    $index2 = strlen($value1) - 1;

    while ($index1 >= 0 || $index2 >= 0) {
        if ($index1 < 0) {
            $digit1 = "0";
        } else {
            $digit1 = $value1[$index1];
            $index1--;
        }
        if ($index2 < 0) {
            $digit2 = "0";
        } else {
            $digit2 = $value2[$index2];
            $index2--;
        }        
        $num = intval($digit1) + intval($digit2) + $carry;
        if ($num < 10) {
            $carry = 0;
        } else {
            $carry = ($num - ($num % 10)) / 10;
            $num = $num % 10;
        }
        $output = strval($num) . $output;
    }
    if ($carry > 0) {
        $output = strval($carry) . $output;
    }
    if (strlen($output) == 0) {
        return "0";
    }
    return $output;
}

// Return 2 to the power of $power expressed as a string.
// This will be slow.
// @param $power: How many times to double.
// @returns: 2^n expressed as a string.
function pow2($power) : string {
    $value = "1";
    for ($i = 1; $i < $power; $i++) {
        $value = add($value, $value);
    }
    return $value;
}