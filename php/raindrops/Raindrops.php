<?php
// Raindrops exercise

declare(strict_types=1);

// Return a raindrop sound for a given number.
// @param $number: The number to convert to a raindrop sound
// @returns: The text of the raindrop sound.
function raindrops(int $number): string
{
    $result = "";

    if ($number % 3 == 0) {
        $result .= "Pling";        
    }
    if ($number % 5 == 0) {
        $result .= "Plang";
    }
    if ($number % 7 == 0) {
        $result .= "Plong";
    }
    return strlen($result) == 0 ? strval($number) : $result;
}
