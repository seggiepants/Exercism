<?php
// Sum of Multiples exercise.

declare(strict_types=1);

// Calculate the score of a player passing a level based on level number and vlaue of 
// magical items found.
// @param $number: The "level" the player finished.
// @param $multiples: The value of the magical items found.
// @returns: The sum of the distinct values found. Values are n * value <= level
function sumOfMultiples(int $number, array $multiples): int
{
    $found = array();
    foreach($multiples as $multiple) {
        if ($multiple > 0) {
            $current = $multiple;
            while ($current < $number) {
                $found[] = $current;
                $current += $multiple;
            }
        }
    }
    return array_sum(array_unique($found));
}
