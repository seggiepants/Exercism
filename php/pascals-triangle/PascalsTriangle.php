<?php
// Pascal's Triangle exercise

declare(strict_types=1);

// generate Pascal's Triangle. Each row of the triangle is an array with the numbers in that row.
// will only generate up to the give $rowCount.
// @param $rowCount: How many rows of Pascal's triangle to generate.
// @returns: Array with each element another array with the values for that line of Pascal's triangle.
function pascalsTriangleRows(int $rowCount)
{
    $result = array();
    if ($rowCount < 1) {
        return $result;
    }
    for($i = 0; $i < $rowCount; $i++) {
        if ($i == 0) {
            $result[] = array(1);
        } else {
            $previous = $result[$i - 1];
            $current = array();
            $current[] = $previous[0];
            for($j = 0; $j < count($previous) - 1; $j++) {
                $current[] = $previous[$j] + $previous[$j + 1];
            }
            $current[] = $previous[count($previous) - 1];
            $result[] = $current;
        }
    }
    return $result;
}
