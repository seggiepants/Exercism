<?php
// Food Chain exercise - Recite the "I know an old lady that swallowed a fly" nursery rhyme.

declare(strict_types=1);

// Recite I know an old lady who swallowed a fly.
class FoodChain
{

    private static $animal = array(
        "fly", 
        "spider", 
        "bird", 
        "cat", 
        "dog", 
        "goat", 
        "cow",
        "horse"
    );
    private static $description = array(
        "", 
        "It wriggled and jiggled and tickled inside her.",
        "How absurd to swallow a bird!",
        "Imagine that, to swallow a cat!",
        "What a hog, to swallow a dog!", 
        "Just opened her throat and swallowed a goat!",
        "I don't know how she swallowed a cow!",
        "She's dead, of course!"
    );

    // Recite a single verse of the nursery rhyme "I know an old lady who swallowed a fly."
    // @param $verseNumber: The verse to recite (1 - 8)
    // @returns: array of strings one for each line.
    public function verse(int $verseNumber): array
    {
        $lines = array();
        $lines[] = "I know an old lady who swallowed a " . self::$animal[$verseNumber - 1] . ".";
        if ($verseNumber > 1) {
            $lines[] = self::$description[$verseNumber - 1];
        }
        
        if (($verseNumber > 1) && ($verseNumber < 8)) {
            for ($i = $verseNumber; $i >= 2; $i--) {
                $predator = self::$animal[$i - 1];
                $prey = self::$animal[$i - 2];
                if ($prey == "spider") {
                    $prey = "spider that wriggled and jiggled and tickled inside her";
                }
                $lines[] = "She swallowed the " . $predator . " to catch the " . $prey . ".";
            }
        }
        
        if ($verseNumber != 8) {
            $lines[] = "I don't know why she swallowed the fly. Perhaps she'll die.";
        }
        return $lines;
    }

    // Recite a range of verses from the nursery rhyme "I know an old lady who swallowed a fly."
    // @param $start: The starting verse to recite (1 - 8)
    // @param $end: The ending verse to recite (1 - 8 and greater than $start)
    // @returns: array of strings one for each line. There is also a blank line between verses.
    public function verses(int $start, int $end): array
    {
        $lines = array();
        for ($i = $start; $i <= $end; $i++) {
            if (count($lines) > 0) {
                $lines[] = "";
            }
            $lines = array_merge($lines, $this->verse($i));
        }
        return $lines;
    }

    // Recite the nursery rhyme "I know an old lady who swallowed a fly."
    // @returns: array of strings one for each line. There is also a blank line between verses.
    public function song(): array
    {
        return $this->verses(1, 8);
    }
}
