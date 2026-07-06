<?php
// Diamond Exercise.

declare(strict_types=1);

// Draw a diamond pattern with an "A" at the top and bottom.
// Growing below with each character up to the given letter, then
// shrinking back down to "A" at the bottom.
// Example for "D":
// "   A   "
// "  B B  "
// " C   C "
// "D     D"
// " C   C "
// "  B B  "
// "   A   "
// @param $letter: The letter at the middle of the diamond shape.
// @returns: Array of strings forming the diamon pattern.
function diamond(string $letter): array
{
    $ret = array();
    $max = Ord($letter) - Ord("A");
    $exterior = $max;
    $interior = 0;
    for ($i = 0; $i <= $max; $i++) {
        $char = chr(ord("A") + $i);
        if ($interior == 0) {
            $ret[] = str_repeat(" ", $exterior) . $char . str_repeat(" ", $exterior);
        } else {
            $ret[] = str_repeat(" ", $exterior) . $char . str_repeat(" ", $interior) . $char . str_repeat(" ", $exterior);
        }
        if ($interior == 0) {
            $interior = 1;
        } else {
            $interior += 2;
        }
        $exterior -= 1;
    }

    if ($max > 0) {
        $exterior = 1;
        $interior = $interior - 4;
        for ($i = $max - 1; $i >= 0; $i--) {
            $char = chr(ord("A") + $i);
            if ($interior <= 0) {
                $ret[] = str_repeat(" ", $exterior) . $char . str_repeat(" ", $exterior);
            } else {
                $ret[] = str_repeat(" ", $exterior) . $char . str_repeat(" ", $interior) . $char . str_repeat(" ", $exterior);
            }
            if ($interior == 1) {
                $interior = 0;
            } else {
                $interior -= 2;
            }
            $exterior += 1;
        }        
    }
    return $ret;
}
