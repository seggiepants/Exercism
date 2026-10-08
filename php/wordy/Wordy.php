<?php

// Exercise: Wordy
declare(strict_types=1);

// Calculate an expression in strict left to right expressed somewhat verbally.
// The string starts with "What is ", and ends with "?", numbers are expressed normally,
// but operators are plus => +, minus => -, multiplied by => *, and divided by => /
// @param string $input: The input string to parse and calculate from.
// @returns int: The evaluated value of the expression in $input.
// @raises InvalidArgumentException: When encounting invalid or unknown syntax/operators.
function calculate(string $input): int
{
    $reBegin = '/^What is (-?\d+)/';
    $reContinue = '/^ (plus|minus|multiplied by|divided by) (-?\d+)/';
    $lhs = 0; 
    $rhs = 0;
    $op = "plus";
    $offset = 0;

    if (preg_match($reBegin, $input, $matches) == null) {
        throw new InvalidArgumentException("Malformed input. Expected What is followed by a number.");
    }
    $offset += strlen($matches[0]);
    $lhs = intval($matches[1]);
    while ($offset < strlen($input) && $input[$offset] != "?") {

        if (preg_match($reContinue, substr($input, $offset), $matches) == null) {
            throw new InvalidArgumentException("Malformed input. Expected operator followed by a number.");
        }
        $offset += strlen($matches[0]);
        $op = $matches[1];
        $rhs = intval($matches[2]);
        switch($op) {
            case "plus":
                $lhs = $lhs + $rhs;
                break;
            case "minus":
                $lhs = $lhs - $rhs;
                break;
            case "multiplied by":
                $lhs = $lhs * $rhs;
                break;
            case "divided by":
                $lhs = $lhs / $rhs;
                break;
            default:
                throw new InvalidArgumentException("Malformed input. Unsupported operation: " . $op . ".");
        }
    }
    return $lhs;
}
