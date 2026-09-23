<?php

// Largest Series Product Exercise

declare(strict_types=1);

// Largest Series Product Class
class Series
{
    private string $input;
    // Constructor - Get the class ready be saving the input.
    // @param $input: The input to find the largetProduct in.
    public function __construct(string $input)
    {
        $this->input = $input;
    }

    // Find the largest product of the digits in a span of size $span digits in the input.
    // @param $span: How many characters for each block to check.
    // @returns: The largest product of digits from a block in the input, or 1 if span is 0.
    // @raises: InvalidArgumentException if $span is greater than the input, 
    // $span is less than zero, or non-digits found in the input.
    public function largestProduct(int $span): int
    {
        if ($span > strlen($this->input)) { 
            throw new InvalidArgumentException("Span is too long.");
        }
        if ($span < 0) {
            throw new InvalidArgumentException("Span may not be negative.");
        } else if ($span == 0) {
            return 1;
        }

        if (preg_match("/[^0-9]+/i", $this->input)) {
            throw new InvalidArgumentException("Only digits are allowed in input.");
        }
        $max = 0;
        $input_len = strlen($this->input) - $span + 1;
        for ($i = 0; $i < $input_len; $i++) {
            $subString = substr($this->input, $i, $span);
            if (strlen($subString) > 0) {
                $candidate = array_reduce(str_split($subString), function ($carry, $value) {
                    return $carry * intval($value);
                }, 1);
                if ($candidate > $max) {
                    $max = $candidate;
                }
            }            
        }
        return $max;
    }
}
