<?php
// Decimal to Roman Numerals exercise

declare(strict_types=1);

// Convert a number from Decimal to Roman numerals. (Only good for integers 1 to 3999.)
// @param $number: The value to convert to Roman numerals
// @returns: The roman numeral equivalent.
function toRoman(int $number): string
{
    if ($number < 1 || $number > 3999) {
        throw new InvalidArgumentException(strval($number) . " is out of range, only 1 to 3999 are accepted.");
    }

    $lookup = array(
        1000 => "M", 
        900 => "CM", 
        500 => "D", 
        400 => "CD", 
        100 => "C", 
        90 => "XC", 
        50 => "L", 
        40 => "XL", 
        10 => "X", 
        9 => "IX", 
        5 => "V", 
        4 => "IV",
        1 => "I");

    $result = "";
    $remaining = $number;
    foreach($lookup as $value => $code) {
        while ($remaining >= $value) {
            $result .= $code;
            $remaining -= $value;
        }
    }
    return $result;
}

