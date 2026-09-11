<?php
// Spiral Matrix Exercise.

declare(strict_types=1);

// Class to hold the draw function to draw a spiral matrix of size $n
class SpiralMatrix
{
    // Draw a spiral matrix of size $n
    // @param $n: The size of the square that holds the spiral matrix.
    // @returns: Array containing the spiral matrix.
    public function draw(int $n): array
    {
        $result = array();
        // Make an empty $n x $n array.
        for ($j = 0; $j < $n; $j++) {
            $row = array();
            for($i = 0; $i < $n; $i++) {
                $row[] = 0;
            }
            $result[] = $row;
        }


        $minX = 0;
        $minY = 0;
        $maxX = $n - 1;
        $maxY = $n - 1;
        $x = 0;
        $y = 0;
        $counter = 1;
        $maxCount = $n * $n;

        while ($counter <= $maxCount) {
            // Left
            while ($x <= $maxX && $counter <= $maxCount) {
                $result[$y][$x] = $counter;
                $counter++;
                $x++;
            }
            $x--;
            $y++;
            $minY++;            

            // Down
            while ($y <= $maxY && $counter <= $maxCount) {
                $result[$y][$x] = $counter;
                $counter++;
                $y++;
            }
            $y--;
            $x--;
            $maxX--;            


            // Right
            while ($x >= $minX && $counter <= $maxCount) {
                $result[$y][$x] = $counter;
                $counter++;
                $x--;
            }
            $x++;
            $y--;
            $maxY--;            

            // Up
            while ($y >= $minY && $counter <= $maxCount) {
                $result[$y][$x] = $counter;
                $counter++;
                $y--;
            }
            $x++;
            $y++;
            $minX++;
        }

        return $result;
    }
}
