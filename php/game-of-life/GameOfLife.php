<?php
// Game of Life exercise

declare(strict_types=1);

// Simulate the Game of Life.
class GameOfLife
{
    /**
     * In PHP 8.4 and newer you can use Asymmetric Property Visibility to enhance data encapsulation
     * @see https://www.php.net/manual/en/language.oop5.visibility.php#language.oop5.visibility-members-aviz
     */
    public private(set) array $matrix;

    // Setup the GameOfLife class instance
    public function __construct(array $matrix)
    {
        $this->matrix = $matrix;
    }

    // Calculate the next step of the simulation and update the matrix with the result.
    public function tick(): void
    {
        $next = array();
        // Make a new next array, same size as current but all zeros.
        for ($j = 0; $j < count($this->matrix); $j++) {
            $row = array();
            for ($i = 0; $i < count($this->matrix[$j]); $i++) {
                $current = $this->matrix[$j][$i];
                $neighbors = $this->getNeighborCount($i, $j);

                if ($current == 1 && ($neighbors == 2 || $neighbors == 3)) {
                    $row[] = 1;
                } else if ($current == 0 && $neighbors == 3) {
                    $row[] = 1;
                } else {
                    $row[] = 0;
                }
            }
            $next[] = $row;
        }
        $this->matrix = $next;
        
    }

    // Get the number of neighbors for a cell in the matrix.
    // a cell is not its own neighbor.
    // @param $x: x-coordinate to look for neighbors at
    // @param $x: y-coordinate to look for neighbors at
    // @returns: Count of neighbors (cell==1) found.
    private function getNeighborCount($x, $y): int {
        $count = 0;
        $maxY = count($this->matrix) -1;
        for ($j = max(0, $y - 1); $j <= min($maxY, $y + 1); $j++) {
            $maxX = count($this->matrix[$j]) - 1;
            for ($i = max(0, $x - 1); $i <= min($maxX, $x + 1); $i++) {
                if ($x != $i || $y != $j) {
                    $count += $this->matrix[$j][$i];
                }
            }
        }
        return $count;
    }
}
