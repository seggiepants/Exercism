<?php
// Mask Credit Card Exercise - String Filtering

declare(strict_types=1);

// Mask the digits in a credit card number except the last four and first digits.
// @param $cc: The credit card number - digits and optionaly a dash (-).
// @returns: filtered version of the input $cc
function maskify(string $cc): string
{
    if (strlen($cc) <= 6) {
        return $cc;
    }
    $str = "1234-5678-9012";
    $pattern = "/\d+/i";
    preg_match_all("/\d+/i", $cc, $matches);
    $digits = implode($matches[0]);
    $stop = strlen($digits) - 4;
    $digitIndex = 0;
    $result = "";
    for ($i = 0; $i < strlen($cc); $i++) {
        $char = $cc[$i];
        if (!is_numeric($char)) {
            $result .= $char;
            continue;
        } 
        if ($digitIndex == 0 || $digitIndex >= $stop) {
            $result .= $char;
        } else {
            $result .= "#";
        }
        $digitIndex++;
    }

    return $result;
}
