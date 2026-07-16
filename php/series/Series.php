<?php
// Series example. Return all substrings of a given length.

declare(strict_types=1);

// Return an array of all substring of a string of digits of a given length.
// @param $digits: The string to get series from
// @param $series: How long the series should be
// @returns: Array of results.
function slices(string $digits, int $series): array
{
    $ret = array();
    if ($series > strlen($digits)) {
        throw new Exception("Slice length is too large");
    }
    if ($series <= 0) {
        throw new Exception("Slice length is too small");
    }

    for ($i = 0; $i <= strlen($digits) - $series; $i++) {
        $ret[] = substr($digits, $i, $series);
    }
    return $ret;
}
