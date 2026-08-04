<?php
// Transpose exercise

declare(strict_types=1);

// Transpose (rotate clockwise) an array of strings
// @param $input: The array to transpose
// @returns: Transposed version of the input.
function transpose(array $input): array
{
    $result = array();

    // Rows is the maximum length of the longest string in the input
    $rows = 0;    
    foreach($input as $row) {
        if (strlen($row) > $rows) {
            $rows = strlen($row);
        }
    }
    
    // If there are no rows return an empty one-line
    if ($rows < 1) {
        $result[] = "";
    } else {
        // Build each row
        for($i = 0; $i < $rows; $i++) {
            $row = "";
            // For each input column get the rowth item.
            for($j = 0; $j < count($input); $j++) {
                // Skip if line is too short.
                if ($i < strlen($input[$j])) {
                    // Space pad to current position
                    while (strlen($row) < $j) {
                        $row .= " ";
                    }
                    $row .= $input[$j][$i];
                }
            }
            $result[] = $row;
        }
    }
    
    return $result;
}
