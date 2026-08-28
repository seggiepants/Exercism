<?php
// Green Bottle Song exercise

declare(strict_types=1);

// Generate the lyrics to the Green Bottle Song.
class BottleSong
{
    private static array $numberToName = array(
        0 => "no", 
        1 => "one", 
        2 => "two", 
        3 => "three", 
        4 => "four", 
        5 => "five", 
        6 => "six", 
        7 => "seven", 
        8 => "eight", 
        9 => "nine", 
        10 => "ten", 
    ); // Private static array is much better than a global.

    // Recite a single verse of the green bottles song.
    // @param $number: The verse number (should be an integer 1 to 10)
    // @returns: String with the text for that verse.
    public function verse(int $number): string
    {
        $template = "%s green %s hanging on the wall,\n".
            "%s green %s hanging on the wall,\n".
            "And if one green bottle should accidentally fall,\n".
            "There'll be %s green %s hanging on the wall.";
        $ten = ucwords(self::$numberToName[$number]);
        $nine = self::$numberToName[$number - 1];
        $bottleTen = $number != 1 ? "bottles" : "bottle";
        $bottleNine = $number - 1 != 1 ? "bottles" : "bottle";
        return sprintf($template, $ten, $bottleTen, $ten, $bottleTen, $nine, $bottleNine);
    }

    // Recite a group of verses from the green bottles song.
    // @param $start: The verse to start on (should be integer 1-10)
    // @param $size: The number of verses to recite (positive integer, $start - $size should be >= 0)
    // @returns: The group of verses separated by two newline characters.
    public function verses(int $start, int $size): string
    {
        $results = array();
        for ($index = $start; $index > $start - $size; $index--) {
            $results[] = $this->verse($index);
        }
        return join("\n\n", $results);
    }

    // Recite the entire green bottles song.
    // @returns: The entire green bottles song with verses separated by two newline characters.
    public function lyrics(): string
    {
        return $this->verses(10, 10);
    }
}
