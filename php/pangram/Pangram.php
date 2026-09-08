<?php
// Pangram exercise. See if a given string has all of the letters a-z of the english alphabet.

declare(strict_types=1);

// Check to see if the given $string uses all of the letters of the english alphabet
// @param $string: The text to check if it is a pangram or not.
// @returns: True if a pangram
function isPangram(string $string): bool
{
    $letters = array_count_values(str_split(strtolower($string)));
    for ($i = ord("a"); $i <= ord("z"); $i++) {
        $letter = chr($i);
        if (!array_key_exists($letter, $letters)) {
            return false;
        }
    }
    return true;

}
