<?php
// Nucleotide Count exercise.

declare(strict_types=1);

// Return an array that maps nucleotides to the count of their appearance in the input
// Available nucleotides are a, c, t, and g
// @param $input: The input to count nucleotides from.
// @returns: Array where the nucleotides are a, c, t, and g, and the values are the number of times
// they appear in the input.
// @raises: An error is throw if there is a non-nucleotide character.
function nucleotideCount(string $input): array
{
    $result = array("a" => 0, "c" => 0, "t" => 0, "g" => 0);

    if (strlen($input) > 0) {
        foreach(str_split(strtolower($input), 1) as $char) {
            if (array_key_exists($char, $result)) {
                $result[$char]++;
            } else {
                throw new Exception("non-nucleotide found.");
            }
        }
    }
    return $result;
}
