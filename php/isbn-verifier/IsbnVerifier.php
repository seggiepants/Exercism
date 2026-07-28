<?php
// ISBN Verifier exercise.

declare(strict_types=1);

// Digit to value. X gets both cases as 10, 0-9 are 0-9 as integers
$GLOBALS["digits"] = array(
    "0" => 0, "1" => 1, "2" => 2, "3" => 3, 
    "4" => 4, "5" => 5, "6" => 6, 
    "7" => 7, "8" => 8, "9" => 9, 
    "X" => 10, "x" => 10);

// Class to validate if a number is an ISBN
class IsbnVerifier
{

    // Check if an ISBN number is valid.
    // @param $isbn: The text to validate is an ISBN number
    // @returns: True if a valid ISBN number, false if not or regex failes to find one.
    public function isValid(string $isbn): bool
    {
        global $digits;
        $pattern = "/^\s*(?<isbn>\d-?\d{3}-?\d{5}-?[\dX])\s*$/i";
        preg_match($pattern, $isbn, $components);
        if (!array_key_exists("isbn", $components)) {
            return false;
        }
        $total = 0;
        $multiplier = 10;
        foreach (mb_str_split(str_replace("-", "", $components["isbn"])) as $letter) {
            $total += $multiplier * $digits[$letter];
            $multiplier--;
            if ($multiplier <= 0) {
                break;
            }
        }
        return $total % 11 == 0;
    }
}
