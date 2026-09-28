<?php

// OCR Numbers exercise.

declare(strict_types=1);

// Recognize numbers in the given input (array of strings).
// @param $input: Array of strings (one per line).
// @returns: String of digits found in the input. Unknown characters returned as ?
function recognize(array $input): string
{
    $digits = array(
        0 => [" _ ",
            "| |",
            "|_|",
            "   "],
        1 => ["   ",
            "  |",
            "  |",
            "   "],
        2 => [" _ ",
            " _|",
            "|_ ",
            "   "],
        3 => [" _ ",
            " _|",
            " _|",
            "   "],
        4 => ["   ",
            "|_|",
            "  |",
            "   "],
        5 => [" _ ",
            "|_ ",
            " _|",
            "   "],
        6 => [" _ ",
            "|_ ",
            "|_|",
            "   "],
        7 => [" _ ",
            "  |",
            "  |",
            "   "],
        8 => [" _ ",
            "|_|",
            "|_|",
            "   "],
        9 => [" _ ",
            "|_|",
            " _|",
            "   "],
    );

    $WORD_WIDTH = 3;
    $WORD_HEIGHT = 4;

    if (count($input) > 0 && count($input) % $WORD_HEIGHT != 0) {
        throw new InvalidArgumentException("Input row count must be a multiple of four.");
    }
    array_map(function($row) use($WORD_WIDTH) {
        if (strlen($row) > 0 && strlen($row) % $WORD_WIDTH != 0) {
            throw new InvalidArgumentException("Input row length must be a multiple of three.");
        }
    }, $input);

    $ret = "";
    for($y = 0; $y < count($input); $y += $WORD_HEIGHT) {
        $row = $input[$y];
        if (strlen($ret) > 0) {
            // commas between rows.
            $ret .= ",";
        }
        for ($x = 0; $x < strlen($row); $x+=$WORD_WIDTH) {
            $found = false;
            foreach($digits as $index => $digit) {
                $found = true;
                for($j = 0; $j < $WORD_HEIGHT; $j++) {
                    if (strcmp(substr($input[$y + $j], $x, $WORD_WIDTH), $digit[$j]) != 0) {
                        $found = false;
                        break;
                    }
                }
                if ($found) {
                    $ret .= strval($index);
                    break;
                }
            }
            if ($found == false){
                $ret .= "?";
            }
        }
    }


    return $ret;
}
