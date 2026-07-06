<?php
// Crypto Square Exercise.

declare(strict_types=1);

// Encode the given input string using the crypto square algorithm.
// @param $plaintext: The text to encode.
// @returns: The encoded text with a space between each grouping.
function crypto_square(string $plaintext): string
{
    $sanitized = strtolower(join("", preg_split("/[^A-Za-z0-9]+/", $plaintext)));
    $width = intval(ceil(sqrt(strlen($sanitized))));
    $height = 0;
    while ($width * $height < strlen($sanitized)) {
        $height++;
    }
    while (strlen($sanitized) < $width * $height) {
        $sanitized = $sanitized . " ";
    }
    $rows = array();
    for ($j = 0; $j < $width; $j++) {
        $rows[] = "";
        for ($i = $j; $i < strlen($sanitized); $i += $width) {
            $rows[$j] = $rows[$j] . $sanitized[$i];
        }
    }
    return implode(" ", $rows);    
}
