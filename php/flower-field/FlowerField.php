<?php

// Flower Field Exercise - Minesweeper for people that need a "safe space".

declare(strict_types=1);

// Minesweeper for those with fragile constitutions.
class FlowerField
{
    public private(set) array $garden;

    // Setup the class so the given garden may be annotated.
    // We assume a rectangular garden not a ragged array.
    // @param array $garden: The garden array (an array of strings where each string is a row in the grid) to annotate.
    public function __construct(array $garden)
    {
        $this->garden = $garden;
    }

    // Return an annotated version of the class' garden layout.
    // @returns: New array with the same dimensions of the class' garden but with 
    // any spots not marked as '*' replaced with the computed number of neighbors that 
    // are set to '*'. If zero then the value is copied through instead of populating '0'.
    public function annotate(): array
    {
        $next = [];
        foreach($this->garden as $y => $row) {
            $nextRow = "";
            for($x = 0; $x < strlen($row); $x++) {
                if (substr($row, $x, 1) != "*") {
                    $count = $this->neighborCount($x, $y);
                    if ($count > 0) {
                        $nextRow .= strval($count);
                    } else {
                        $nextRow .= substr($this->garden[$y], $x, 1);
                    }
                } else {
                    $nextRow .= substr($this->garden[$y], $x, 1);
                }
            }
            $next[] = $nextRow;
        }
        return $next;
    }

    // Return the number of cells neighboring a point on the grid that are marked with '*'.
    // It is assumed that (x, y) form a valid point on the grid (zero based).
    // @param int $x: X-coordinate of point in the grid.
    // @param int $y: Y-coordinate of point in the gird.
    // @returns int: Count of neighboring cells marked with '*'.
    private function neighborCount(int $x, int $y) : int
    {
        $delta = [[-1, -1], [0, -1], [1, -1], [-1, 0], [1, 0], [-1, 1], [0, 1], [1, 1]];
        $count = 0;
        foreach($delta as $pair) {
            $i = $x + $pair[0];
            $j = $y + $pair[1];

            if ($j >= 0 && $j < count($this->garden) && $i >= 0 && $i < strlen($this->garden[$j]) && substr($this->garden[$j], $i, 1) == "*") {
                $count++;
            }                
        }
        return $count;
    }
}
