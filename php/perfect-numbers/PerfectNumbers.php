<?php
// Perfect Numbers exercise. Categorize numbers as perfect, abundant, or deficient based on Aliquot sum.

declare(strict_types=1);

// Categorize numbers as perfect, abundant, or deficient based on Aliquot sum.
// @param $number: The number to classify
// @returns: a string: "perfect", "abundant", or "deficient"
function getClassification(int $number): string
{
    $aliquot_sum = 0;

    if ($number <= 0) {
        throw new InvalidArgumentException("Number must be a positive integer.");
    }

    for ($i = 1; $i < $number; $i++) {
        if ($number % $i == 0) {
            $aliquot_sum += $i;
        }
    }

    if ($aliquot_sum < $number) {
        return "deficient";
    }

    if ($aliquot_sum > $number) {
        return "abundant";
    }

    return "perfect";
}
