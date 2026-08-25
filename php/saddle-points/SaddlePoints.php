<?php
// Saddle Point exercise.

declare(strict_types=1);

// Find the Saddle Points in a 2D matrix. A saddle point is one that is both smallest in its column and largest in its row.
// @param $matrix: The 2D matrix to search on.
// @returns: An array of points. Each point is an array with keys of "row", and "column"
function saddlePoints(array $matrix): array
{
    $results = array();
    $height = count($matrix);
    $width = 0;
    if ($height > 0) {
        $width = count($matrix[0]);
    }

    for($j = 0; $j < $height; $j++) {
        for($i = 0; $i < $width; $i++) {
            if (isSaddlePoint($matrix, $i, $j, $width, $height)) {
                $results[] = array(
                    "row" => $j + 1, 
                    "column" => $i + 1,
                );                
            }
        }
    }

    return $results;
}

// Check to see if a given point is a saddle point in the matrix
// @param $matrix: The matrix to lookup data from
// @param $x: x-coordinate of point to look at
// @param $y: y-coordinate of point to look at
// @param $width: How wide is the 2D matrix
// @param $height: How tall is the 2D matrix
// @returns: True if the given point on the matrix is a saddle point.
function isSaddlePoint(array $matrix, int $x, int $y, int $width, int $height) : bool {
    if ($x < 0 || $x >= $width || $y < 0 || $y >= $height) {
        return false;
    }

    $rowMax = max($matrix[$y]);
    if ($matrix[$y][$x] != $rowMax) {
        return false;
    }
    $colMin = $matrix[0][$x];
    for ($j = 1; $j < $height; $j++) {
        if ($matrix[$j][$x] < $colMin) {
            $colMin = $matrix[$j][$x];
        }
    }
    return $rowMax == $matrix[$y][$x] && $rowMax == $colMin;
}
