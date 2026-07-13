<?php
// Resistor color exercise.

declare(strict_types=1);

// Globals are all messed up. I think the test harness including 
// this file is doing the damage.
// Instead of declaring a variable for a global set it in the $GLOBALS super-global instead.
// In this case I can change it to a const. Why don't const need a $ prefix? Got the const thing from
// community solutions.

const resistorColors = [
    "black" => 0,
    "brown" => 1,
    "red" => 2,
    "orange" => 3,
    "yellow" => 4,
    "green" => 5,
    "blue" => 6,
    "violet" => 7,
    "grey" => 8,
    "white" => 9,
];

// Return all of the color bands supported.
// @returns: Array with the color band names supported (lower-case).
function getAllColors(): array 
{  
    return array_keys(resistorColors);
}

// Return the value for a given color band.
// @param $color: The color band (lower-case expected)
// @returns: The value for the color band, or -1 if not found.
function colorCode(string $color): int
{
    if (array_key_exists($color, resistorColors)) {
        return resistorColors[$color];
    }
    return -1;
}
