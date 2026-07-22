<?php
// Word Count exercise.

declare(strict_types=1);

// Return a hash map (array) of the words in the string to the number of occurences.
// @param $words: The string to pattern match on.
// @returns: Array with word as key (in lower case) and number of ocurrences as the value.
function wordCount(string $words): array
{
    $pattern = "/[a-z0-9]+(\'[a-z]+)?/im";
    preg_match_all($pattern, strtolower($words), $result);
    return array_count_values($result[0]);
}
