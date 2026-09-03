<?php
// Isogram exercise

declare(strict_types=1);

// Check if a word is an Isogram (word has no repeating letters)
// @param $word: Word to check if it is an Isogram or not.
// @returns: True if $word is an isogram
function isIsogram(string $word): bool
{
    $letters = array();
    foreach (str_split(strToUpper($word)) as $letter) {
        if ($letter >= "A" && $letter <= "Z") {
            if (array_key_exists($letter, $letters)) {
                $letters[$letter]++;
            } else {
                $letters[$letter] = 1;
            }
        }
    }

    return !array_any($letters, function (string $value, string $key) {
        return $value > 1;
    });
}
