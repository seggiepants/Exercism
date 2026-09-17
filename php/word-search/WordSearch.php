<?php
// Word Search Exercise

declare(strict_types=1);
require_once 'Result.php';

// Diagonal line data class.
class Diagonal 
{
    public private(set) int $column;    // Column the diagonal was taken from
    public private(set) int $row;       // Row the diagonal was taken from.
    public private(set) int $dx;        // Does the diagonal go to the left or right.
    public private(set) string $data;   // The extracted diagonal slice.

    // Initialize the diagonal line data.
    // @param $column: The column the diagonal started from
    // @param $row: The row the diagonal started from
    // @param $dx: Did the diagonal move left or right.
    // @param $data: Extracted slice of the puzzle.
    public function __construct($column, $row, $dx, $data) {
        $this->column = $column;
        $this->row = $row;
        $this->dx = $dx;
        $this->data = $data;
    }

}

// Search for a word in a puzzle grid. An array of strings. It is assumed that all lines are equal length with no empty rows/columns
class WordSearch
{
    // This exercises uses additional files for data classes.
    // In the online editor these are available as additional
    // tabs next to this file's tab.

    private array $puzzle; // Holds the data.

    // Initialize the Word Search Class
    // @param $grid: The grid to search on.
    public function __construct(array $grid)
    {
        $this->puzzle = $grid;
    }

    // Search for a word on the puzzle grid left-right, right-left, top-bottom, bottom-top, and the diagonal directions.
    // @returns: Null if not found, and a Result object if found.
    public function search(string $word): ?Result
    {
        $wordRev = strrev($word);
        // Horizontal
        foreach($this->puzzle as $rowIndex => $row) {
            // Check left to right;
            $pos = strpos($row, $word);
            if ($pos !== false) {
                return new Result(
                    start: new Location(column: $pos + 1, row: $rowIndex + 1),
                    end: new Location(column: $pos + strlen($word), row: $rowIndex + 1),
                );
            }

            // Check right to left;
            $pos = strpos($row, $wordRev);
            if ($pos !== false) {
                return new Result(
                    start: new Location(column: $pos + strlen($word), row: $rowIndex + 1),
                    end: new Location(column: $pos + 1, row: $rowIndex + 1),
                );
            }
        }

        // Vertical
        for($col = 0; $col < strlen($this->puzzle[0]); $col++) {
            // Grab a column
            $column = implode("", array_map(function ($value) use($col) { 
                return $value[$col];
            }, $this->puzzle));
            
            // Check top to bottom
            $pos = strpos($column, $word);
            if ($pos !== false) {
                return new Result(
                    start: new Location(column: $col + 1, row: $pos + 1),
                    end: new Location(column: $col + 1, row: $pos + strlen($word)),
                );
            }

            // Check bottom to top;
            $pos = strpos($column, $wordRev);
            if ($pos !== false) {
                return new Result(
                    start: new Location(column: $col + 1, row: $pos + strlen($word)),
                    end: new Location(column: $col + 1, row: $pos + 1),
                );
            }
        }

        // Calculate all of the diagonals. Top row left to right, Left edge left to right, 
        // Top row right to left and right edge right to left.
        $diagonals = array();
        // Top Left to Bottom Right
        for ($col = 0; $col < strlen($this->puzzle[0]); $col++) {
            $diagonals[] = new Diagonal($col, 0, 1, $this->GetDiagonal($col, 0, 1));
        }
        for ($row = 1; $row < count($this->puzzle); $row++) {
            $diagonals[] = new Diagonal(0, $row, 1, $this->GetDiagonal(0, $row, 1));
        }

        // Top Right to Bottom Left
        for ($col = 0; $col < strlen($this->puzzle[0]); $col++) {
            $diagonals[] = new Diagonal($col, 0, -1, $this->GetDiagonal($col, 0, -1));
        }
        for ($row = 1; $row < count($this->puzzle); $row++) {
            $diagonals[] = new Diagonal(strlen($this->puzzle[0]) - 1, $row, -1, $this->GetDiagonal(strlen($this->puzzle[0]) - 1, $row, -1));
        }

        foreach($diagonals as $diagonal) {
            // Check left to right on data;
            $pos = strpos($diagonal->data, $word);
            if ($pos !== false) {
                $startCol = $diagonal->column + ($diagonal->dx * $pos);
                $startRow = $diagonal->row + $pos;
                $endCol = $diagonal->column + ($diagonal->dx * ($pos + strlen($word) - 1));
                $endRow = $diagonal->row + $pos + strlen($word) - 1;
                return new Result(
                    start: new Location(column: $startCol + 1, row: $startRow + 1),
                    end: new Location(column: $endCol + 1, row: $endRow + 1),
                );
            }

            // Check right to left on data;
            $pos = strpos($diagonal->data, $wordRev);
            if ($pos !== false) {
                $startCol = $diagonal->column + ($diagonal->dx * ($pos + strlen($word) - 1));
                $startRow = $diagonal->row + $pos + strlen($word) - 1;
                $endCol = $diagonal->column + ($diagonal->dx * $pos);
                $endRow = $diagonal->row + $pos;

                return new Result(
                    start: new Location(column: $startCol + 1, row: $startRow + 1),
                    end: new Location(column: $endCol + 1, row: $endRow + 1),
                );
            }
        }

        // Not found.
        return null;
    }

    // Get a diagonal line from the puzzle. Stops when you attemp to go past the left, right, top or bottom edge
    // @param $colStart: The column of the puzzle to start at 
    // @param $rowStart: The row of the puzzle to start at
    // @param $dx: Does the diagonal go to the left (1) or right (-1). Only 1, and -1 are valid values
    // @returns: Diagonal slice of the puzzle.
    function GetDiagonal($colStart, $rowStart, $dx) : string {
        $result = "";
        $row = $rowStart;
        $col = $colStart;
        
        while ($col >= 0 && $col < strlen($this->puzzle[0]) && $row >= 0 && $row < count($this->puzzle)) {
            $result .= $this->puzzle[$row][$col];
            $col += $dx;
            $row += 1;
        }
        return $result;
    }
}
