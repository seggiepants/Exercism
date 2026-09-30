<?php

// Run Length Encoding Exercise.
// Looks like I could have done this more simply with regular expressions as seen in the community solutions.
// Match a repeated character: /(.)\1+/
// Match a number followed by a character \D is non-digit: /(\d+)(\D)/
// Then use preg_replace_callback and str_repeat.
// Well I am keeping my version, it works, it is mine.

declare(strict_types=1);

// Encode a string with run-length encoding.
// @param string $input: The text to encode.
// @return string: The encoded text.
function encode(string $input): string
{
    $counter = 0;
    $previous = "";
    $result = "";
    foreach(str_split($input) as $char) {
        if (strcmp($previous, $char) == 0) {
            $counter++;
        } else {
            if ($counter > 0 && strcmp($previous, "") != 0) {
                if ($counter == 1) {
                    $result .= $previous;
                } else {
                    $result = $result . strval($counter) . $previous;
                }
            }
            $counter = 1;
            $previous = $char;
        }
    }
    // Handle the last character.
    if ($counter > 0 && strcmp($previous, "") != 0) {
        if ($counter == 1) {
            $result .= $previous;
        } else {
            $result = $result . strval($counter) . $previous;
        }
    }
    return $result;
}

// Decode a string encoded with run-length encoding.
// @param string $input: The text to dencode.
// @return string: The decded text.
function decode(string $input): string
{
    $counter = 0;
    $result = "";
    foreach(str_split($input) as $char) {
        if($char >= "0" && $char <= "9") {
            $counter = ($counter * 10) + (ord($char) - ord("0"));
        } else {
            if ($counter > 0) {
                $result .= str_repeat($char, $counter);
                $counter = 0;
            } else {
                $result .= $char;
            }
        }
    }
    return $result;
}
