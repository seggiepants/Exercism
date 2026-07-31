<?php
// Sublist Exercise see if two arrays A, and B are equal A is a sublist of B, 
// B is a sublist of A or unequal

declare(strict_types=1);

// Sublist Exercise see if two arrays A, and B are equal A is a sublist of B, 
// B is a sublist of A or unequal
class Sublist
{
    // Compare two arrays and return a sting indicating if one is a sublist of the other.
    // @param $listOne: first list to compare.
    // @param $listTwo: second list to compare.
    // @returns: "EQUAL", "SUBLIST", "SUPERLIST", or "UNEQUAL"
    public function compare(array $listOne, array $listTwo): string
    {
        // Can't be equal if different lengths.
        $sameLength = count($listOne) == count($listTwo); 
        // one contains two
        $oneSuperlistTwo = $this->IsSublist($listTwo, $listOne);
        if ($sameLength && $oneSuperlistTwo) {
            return "EQUAL";
        }
        if ($oneSuperlistTwo) {
            return "SUPERLIST";
        }

        if ($this->IsSublist($listOne, $listTwo)) {
            return "SUBLIST";
        }
        return "UNEQUAL";
    }

    // Check to see if one list is a sub list of the other.
    // @param $listOne: The array that may be a sublist of $listTwo
    // @param $listTwo: The array that may be a superlist of $listOne
    // @returns: True/False
    function IsSublist(array $listOne, array $listTwo): bool {
        // Empty is a sublist of any list.
        if (count($listOne) == 0) {
            return true; 
        }

        $indexFirst = array_keys($listTwo, $listOne[0]);
        foreach($indexFirst as $index) {
            if ($index + count($listOne) > count($listTwo)) {
                continue;
            }
            $success = true;
            for($i = 0; $i < count($listOne); $i++) {
                if ($listOne[$i] != $listTwo[$i + $index]) {
                    $success = false;
                    break;
                }
            }
            if ($success) {
                return true; // made it through
            }
        }
        return false; // no match found
    }
}
