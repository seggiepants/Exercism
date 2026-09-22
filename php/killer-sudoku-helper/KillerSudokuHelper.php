<?php

// Killer Soduku Helper exercise.

declare(strict_types=1);

// Class to Help solve Killer Soduku puzzles
class KillerSudokuHelper
{
    // Check to see if the given array describes the results we want
    // @param $expected: The expected sum of the array.
    // @param $length: The epexted length of the array.
    // @param $results: The array of values to check.
    // @returns: True if the length matches expectations and the sum of results is equal to the expected value.
    private function checkArray(int $expected, int $length, array $results) {
        return $length == count($results) && $expected == array_reduce($results, function($total, $current) {
            return $total + $current;
        });
    }

    // Reduce an array to a distinct key (csv of array values)
    // @param $values: Array of numeric values.
    // @returns: Distinct key for the array (just a csv of the values).
    private function array_key($values) : string {
        return array_reduce($values, function($carry, $item) { return strlen($carry) > 0 ? $carry . "," . strval($item) : strval($item); }, "");
    }

    // Find the combinations of numbers that can solve a given soduku constraint with the given squares, total, and values you may not use.
    // @param $sum: The expected total for the sum of square values.
    // @param $size: The number of squares we must use.
    // @param $exclude: Values that may not be used.
    public function combinations(int $sum, int $size, array $exclude): array
    {
        $keys = [];         // Keep track of solutions so we don't repeat by turning values into a key and saving used ones.
        $solutions = [];    // Solutions found so far.
        for ($i = 1; $i < 10; $i++) {
            if ($size >= 1 && $i <= $sum && !in_array($i, $exclude)) {
                if ($size == 1) {
                    $sub_solutions = [[]];
                } else {
                    $sub_solutions = $this->combinations($sum - $i, $size - 1, [...$exclude, $i]);
                }
                foreach($sub_solutions as $sub_solution) {
                    $solution = [$i, ...$sub_solution];
                    sort($solution);
                    $key = $this->array_key($solution);
                    if ($this->checkArray($sum, $size, $solution) && !in_array($key, $keys)) {                        
                        $keys[] = $key;
                        $solutions[] = $solution;
                    }
                }
            }
        }
        return $solutions;
    }
}
