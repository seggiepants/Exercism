<?php
// Scrabble Score - Compute the score for a word in Scrabble

declare(strict_types=1);

// I like to keep my data outside of functions.
$GLOBALS["lookup"] = array(
    "A" => 1, "B" => 3, "C" => 3, "D" => 2, "E" => 1, "F" => 4, "G" => 2, 
    "H" => 4, "I" => 1, "J" => 8, "K" => 5, "L" => 1, "M" => 3, "N" => 1,
    "O" => 1, "P" => 3, "Q" => 10, "R" => 1, "S" => 1, "T" => 1, "U" => 1,
    "V" => 4, "W" => 4, "X" => 8, "Y" => 4, "Z" => 10,
);

// Calculate the score for a word in scrabble.
// @param $word: The word to score.
// @returns: The sum of the values for each letter (A-Z) in the word.
function score(string $word): int
{
    global $lookup;
    $total = 0;
    foreach(str_split(strtoupper($word)) as $char) {
        if (array_key_exists($char, $lookup)) {
            $total += $lookup[$char];
        }
    }
    return $total;
}
