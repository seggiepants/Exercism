<?php
// Leap year exercise.

declare(strict_types=1);

// Check if a given year is a leap year.
// @param $year: The year to test.
// @returns: True if a leap year.
function isLeap(int $year): bool
{
    if ($year % 400 == 0) {
        return true;
    }
    if ($year % 100 == 0) {
        return false;
    }
    return $year % 4 == 0;
}
