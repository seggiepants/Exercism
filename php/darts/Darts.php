<?php
// Darts exercise

declare(strict_types=1);

// Calculate the score for a dart thrown at a point. Assume dart board center is at (0, 0)
// Out of bounds: > 10 units | 0 points
// Outer: 10 units | 1 point
// Middle: 5 units | 5 points
// Inner: 1 unit   | 10 points
// @param $xAxis: Where it hit on the x axis
// @param $yAxis: Where it hit on the y axis
// @returns: int 0, 1, 5, or 10
function score(float $xAxis, float $yAxis): int
{
    $distance = sqrt($xAxis ** 2.0 + $yAxis ** 2.0);
    return match(true) {
        $distance <= 1.0 => 10,
        $distance <= 5.0 => 5,
        $distance <= 10.0 => 1,
        default => 0,
    };
}
