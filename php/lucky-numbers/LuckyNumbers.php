<?php
// Exercise: Lucky Numbers - Type conversion in PHP

class LuckyNumbers
{
    // Sum up the digits given by two arrays of digits
    // @param $digitsOfNumber1: Array of digits for the first number.
    // @param $digitsOfNumber2: Array of digits for the second number.
    // @returns: The sum of the first and second array cast to numbers.
    public function sumUp(array $digitsOfNumber1, array $digitsOfNumber2): int
    {
        return (int)implode("", $digitsOfNumber1) + (int)implode("", $digitsOfNumber2);
    }

    // Check if a number is a palindrome (reads the same forward and backward).
    // @param $number: The number to check
    // @returns: TRUE if palindrome otherwise FALSE
    public function isPalindrome(int $number): bool
    {
        $text = (string)$number;
        $first = 0;
        $last = strlen($text) - 1;
        while ($first < $last) {
            if ($text[$first] != $text[$last]) {
                return FALSE;
            }
            $first++;
            $last--;
        }
        return TRUE;
    }

    // Validate an input string for correctness
    // @param $input: The string to validate.
    // @returns: "Required field if empty, "Must be a whole number larger than
    // 0" if zero, negative or not an integer. Blank "" if no problems.
    public function validate(string $input): string
    {
        if (strlen($input) == 0) {
            return "Required field";
        }

        $int_value = (int) $input;
        $float_value = (float) $input;
        if ($int_value <= 0 || (float)$int_value != $float_value) {
            return "Must be a whole number larger than 0";
        }

        return "";
    }
}
