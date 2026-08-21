<?php
// Resistor Color Trio Exercise

declare(strict_types=1);

$GLOBALS['lookup'] = array(
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
);

class ResistorColorTrio
{
    // Parse resistor colors. The first two are the first two digit of ohms. The last is the power of
    // ten multiplier. Also add another multiplication by ten for extra unused colors.
    // Normalize the amount with a metric ohms prefix for values >= 1,000 (kilo, mega, giga)
    // @returns: value string with the resistor reading.
    public function label($colors): string
    {
        global $lookup;
        if (count($colors) < 3) {
            return "0 ohms";
        }
        $value = ($lookup[$colors[0]] * 10) + $lookup[$colors[1]];
        $value = $value * pow(10, $lookup[$colors[count($colors) - 1]]);
        if (count($colors) > 3) {
            $value *= pow(10, count($colors) - 3);
        }
        $unit = "ohms";
        if ($value >= 1000000000) {
            $unit = "gigaohms";
            $value /= 1000000000;
        } else if ($value >= 1000000) {
            $unit = "megaohms";
            $value /= 1000000;
        } else if ($value >= 1000) {
            $unit = "kiloohms";
            $value /= 1000;
        }

        return strval($value) . " " . $unit;
    }
}
