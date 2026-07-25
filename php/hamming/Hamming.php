<?php
// Hamming Exercise - Check the number of base differences between two strands of dna.

declare(strict_types=1);

// Compare two strands of DNA and return the number of differences between the two.
// @param $strandA: The first dna strand to check.
// @param $strandB: The second dna strand to check.
// @returns: The number of places where $strandA is not the same as $strandB.
// @raises: InvalidArgumentException if the strand lengths are not equal.
function distance(string $strandA, string $strandB): int
{
    if (strlen($strandA) != strlen($strandB)) {
        throw new InvalidArgumentException("strands must be of equal length");
    }
    $ret = 0;
    for ($i = 0; $i < strlen($strandA); $i++) {
        if ($strandA[$i] != $strandB[$i]) {
            $ret++;
        }
    }
    return $ret;
}
