<?php
// Matching Brackets Exercise.

declare(strict_types=1);


// Check if the given text has matching brackets. The count and ordering should match.
// Only checks (, [, and {
// @param $input: The text to check.
// @returns: True if the braces match and false otherwise.
function brackets_match(string $input): bool
{
    $start = "[({";
    $finish = "])}";
    $nesting = array();
    if (strlen($input) > 0) {
        foreach(str_split($input) as $char) {
            if (str_contains($start, $char)) {
                $nesting[] = $char;
            } else if (str_contains($finish, $char)) {
                if (count($nesting) == 0) {
                    return false;
                }                
                $match = end($nesting);
                if (strpos($start, $match) == strpos($finish, $char)) {
                    array_pop($nesting);
                } else {
                    return false;
                }
            }
        }
    }
    return count($nesting) == 0;
}
