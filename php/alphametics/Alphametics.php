<?php
// Alphametics exercise

declare(strict_types=1);

// Class to solve an Alphametics puzzle.
class Alphametics
{
    // Loop through an array removing a given value if present.
    // @param $value: The needle or value to look for.
    // @param $data: The haystack or array to search through.
    // @returns: The updated array.
    private function array_remove($value, $data) : array {
        $index = array_search($value, $data);
        while ($index !== false) {
            array_splice($data, $index, 1);
            $index = array_search($value, $data);
        }
        return $data;
    }

    // Check if a solution for the puzzle works mathematically.
    // @param $puzzle: The puzzle to check where the letters have been replaced with digits.
    // @returns: True if the right hand side of the equation (last value) is equal to the sum of the remaining values.
    private function compute($puzzle) : bool {
        $nums = array_map(function ($value) {return intval($value); }, explode(" ", $puzzle));
        $rhsAmount = array_pop($nums);
        $lhsAmount = array_reduce($nums, function ($carry, $item) { return $carry + $item; }, 0);
        return $rhsAmount === $lhsAmount;
    }

    // Solve an alphametics puzzle. Complex puzzles can take a long time.
    // @param $puzzle: The puzzle to solve.
    // @returns: null if no solution found or an array mapping character to digit.
    public function solve(string $puzzle): ?array
    {
        $pattern = "/[^A-Z]+/i";
        $tokens = preg_split("/[^A-Z]+/i", strtoupper($puzzle));
        $notZero = array_map(function ($value) { return $value[0]; }, $tokens);
        $uniqueChars = array_unique(mb_str_split(strtoupper($puzzle)));
        $uniqueChars = $this->array_remove(" ", $uniqueChars);
        $uniqueChars = $this->array_remove("+", $uniqueChars);
        $uniqueChars = $this->array_remove("=", $uniqueChars);
        $digitMap = array(null, null, null, null, null, null, null, null, null, null);
  
        return $this->step(implode(" ", $tokens), $digitMap, $notZero, count($uniqueChars), implode("", $uniqueChars), 0);
    }

    // Attempt to solve the puzzle. Chooses a digit for a letter then calls itself recursively to see if a solution can be found.
    // @param $puzzle: The puzzle text to solve (with spaces uniform between values and +, = removed).
    // @param $digitMap: This holds if the digit has been populated in the current solution or not 10 value array of null or unique 0-9.
    // @param $notZero: The first digit of a value cannot be zero so this is the set of letters that start a value.
    // @param $totalUnique: The number of unique characters
    // @param $uniqueChars: String of the unique characters in the puzzle BOB -> BO for example as B repeats.
    // @param $mappedDigits: How many digits have been solved so far.
    // @returns: null if no solution found or an array mapped digits (nulls removed).
    private function step($puzzle, $digitMap, $notZero, $totalUnique, $uniqueChars, $mappedDigits) : ?array {
        // base case check for a valid solution
        if ($mappedDigits == $totalUnique) {
            $valid = $this->compute($puzzle);
            if ($valid == false) { // character => digit
                return null;
            } else {
                $ret = array();
                for($i = 0; $i < count($digitMap); $i++) {
                    if ($digitMap[$i] !== null) {
                        $ret[$digitMap[$i]] = $i;
                    }
                }
                return $ret;
            }
        }

        // find the first unmapped character
        if (strlen($uniqueChars) <= 0) {
            return null;
        }
        
        $candidate = $uniqueChars[0];
        for ($i = count($digitMap) - 1; $i >= 0; $i--) {
            if (($i == 0 && array_search($candidate, $notZero) !== false) || $digitMap[$i] != null) {
                continue;
            }
            $digitMap[$i] = $candidate;
            $ret = $this->step(str_replace($candidate, strval($i), $puzzle), $digitMap, $notZero, $totalUnique, str_replace($candidate, "", $uniqueChars), $mappedDigits + 1);
            if ($ret != null) {
                return $ret;
            } else {
                $digitMap[$i] = null;
            }
        }
        return null;
    }
}
