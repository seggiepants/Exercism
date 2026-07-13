<?php
// Rail Fence Cipher Exercise.

declare(strict_types=1);

// Encode a plain text message with the rail fence cipher
// @param $plainMessage: The message to encode.
// @param $rails: The number of rails to use.
// @returns: The encoded message.
function encode(string $plainMessage, int $rails): string
{
    $dy = 1;
    $rail = 0;
    $rows = array();
    // Add a blank row for each rail.
    for ($i = 0; $i < $rails; $i++) {
        $rows[] = "";
    }
    foreach(str_split($plainMessage) as $char) {
        $rows[$rail] = $rows[$rail] . $char;
        $rail += $dy;
        if ($dy > 0 && $rail >= count($rows)) {
            $dy = -1;
            $rail = count($rows) - 2;
        } else if ($dy < 0 && $rail < 0) {
            $dy = 1;
            $rail = 1;
        }
    }
    return implode("", $rows);
}

// Decode a cipher message given the text and number of rails
// @param $cipherMessage: The text to decode.
// @param $rails: The number of rails.
// @returns: The decoded text.
function decode(string $cipherMessage, int $rails): string
{
    $lineLengths = array();
    $rows = array();
    $col = array();
    for ($i = 0; $i < $rails; $i++) {
        $lineLengths[$i] = 0;
        $rows[] = "";        
        $col[] = 0;
    }
        
    // Calculate the line lengths.
    $dy = 1;
    $rail = 0;
    foreach(str_split($cipherMessage) as $char) {
        $lineLengths[$rail] = $lineLengths[$rail] + 1;
        $rail += $dy;
        if ($dy > 0 && $rail >= count($rows)) {
            $dy = -1;
            $rail = count($rows) - 2;
        } else if ($dy < 0 && $rail < 0) {
            $dy = 1;
            $rail = 1;
        }
    }
    $pos = 0;
    for ($i = 0; $i < $rails; $i++) {
        $rows[$i] = substr($cipherMessage, $pos, $lineLengths[$i]);
        $pos += $lineLengths[$i];
    }

    $ret = "";
    // Calculate the message.
    $dy = 1;
    $rail = 0;
    for ($i = 0; $i < strlen($cipherMessage); $i++)  {
        $ret = $ret . $rows[$rail][$col[$rail]];
        $col[$rail]++;        
        $rail += $dy;
        if ($dy > 0 && $rail >= count($rows)) {
            $dy = -1;
            $rail = count($rows) - 2;
        } else if ($dy < 0 && $rail < 0) {
            $dy = 1;
            $rail = 1;
        }
    }
    return $ret;
}
