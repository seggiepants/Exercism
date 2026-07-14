<?php
// Resistor Color Duo exercise

declare(strict_types=1);

const colors = [
    "black" => 0,
    "brown" => 1,
    "red" => 2,
    "orange" => 3,
    "yellow" => 4,
    "green" => 5,
    "blue" => 6,
    "violet" => 7,
    "grey" => 8,
    "white" => 9
];

// Resistor Color Duo class. Has a function to retrieve resistor color band 
// value for up to two bands.
class ResistorColorDuo
{
    // Retrieve value of given color bands of a resitor for up to two bands.
    // @param $colors: Array of resistor colors. Names should be lower-case and 
    // match the values in the color lookup table
    // @param $colors: Array of colors to compete with.
    // @returns: The computed value of the first two bands. 10 * $colors[0] + $colors[1]
    public function getColorsValue(array $colors): int
    {
        $ret = 0;
        foreach ($colors as $index => $value) {
            if ($index >= 2) {
                break;
            }
            $ret = ($ret * 10) + colors[$value];
        }
        return $ret;
    }
}
