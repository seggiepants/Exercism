<?php
// Matrix exercise. Unpack a matrix from a string and return it's columns and rows.

declare(strict_types=1);

// 2D Matrix implementation for the exercise.
class Matrix
{
    private array $matrix; // extracted matrix

    // Constructor. Build a matrix from the given string definition.
    // There are newlines (\n) between rows and spaces between columns in a row.
    // @param $matrix: The string to build the 2D matrix from.
    public function __construct(string $matrix)
    {
        $rows = explode("\n", $matrix);
        $this->matrix = array();
        foreach ($rows as $row) {
            preg_match_all("/\d+/", $row, $matches);
            //echo ">> " . var_dump($matches) . PHP_EOL;
            $next = array();
            foreach($matches[0] as $item) {
                $next[] = intval($item);
            }
            $this->matrix[] = $next;
        }
    }

    // Return a row from the class' matrix
    // @param $rowId: 1 based row index.
    // @returns: Array with values from that row in column order.
    // @raises: Error if $rowId is not within the rows of the matrix.
    public function getRow(int $rowId): array
    {
        if ($rowId < 1 && $rowId > count($this->matrix)) {
            throw new InvalidArgumentException("Invalid row id.");
        }
        return $this->matrix[$rowId - 1];
    }

    // Return a column from the class' matrix
    // @param $columnId: 1 based column index.
    // @returns: Array with values from that column in row order.
    // @raises: Error if $columnId is not within the columns in any row.
    public function getColumn(int $columnId): array
    {
        /*
        $results = array();
        foreach($this->matrix as $row) {
            if ($columnId < 1 || $columnId > count($row)) {
                throw new InvalidArgumentException("Invalid column id.");
            }
            $results[] = $row[$columnId - 1];
        }

        return $results;
        */
        if (count($this->matrix) == 0) {
            return ValueError("No matrix data.");
        }
        if ($columnId < 1 || $columnId > count($this->matrix[0])) {
                throw new InvalidArgumentException("Invalid column id.");
        }
        return array_column($this->matrix, $columnId - 1);
    }
}
