<?php

// Exercise - Palindrome Products
declare(strict_types=1);

// Get the smallest palindome in a range and return it and its factors as an array pair.
// @param int $min: The minimum value to start the range.
// @param int $max: The maximum value to start the range.
// @returns array: An array with the first value being the smallest palindrome and the second an array of array pairs for factors that built the palindrome.
// @raises Exception: If the range has no palindromes.
function smallest(int $min, int $max): array
{
    $palindromes = getPalindromes($min, $max);
    if (count($palindromes) == 0) {
        throw new Exception('No palindromes found.');
    }
    $minKey = min(array_keys($palindromes));
    return [$minKey, $palindromes[$minKey]];
}

// Get the largest palindome in a range and return it and its factors as an array pair.
// @param int $min: The minimum value to start the range.
// @param int $max: The maximum value to start the range.
// @returns array: An array with the first value being the largest palindrome and the second an array of array pairs for factors that built the palindrome.
// @raises Exception: If the range has no palindromes.
function largest(int $min, int $max): array
{    
    $palindromes = getPalindromes($min, $max);
    if (count($palindromes) == 0) {
        throw new Exception('No palindromes found.');
    }
    $maxKey = max(array_keys($palindromes));
    return [$maxKey, $palindromes[$maxKey]];
}

// Get an array of all palindromes and their products in a range of $min to $max.
// @param int $min: The minimum value to start the range.
// @param int $max: The maximum value to start the range.
// @returns array: An array with keys that are palindromes and the value is an array of array pairs of factors.
function getPalindromes(int $min, int $max): array 
{
    $result = [];
    for($j = $min; $j <= $max; $j++) {
        for($i = $j; $i <= $max; $i++) {
            $current = $i * $j;
            if (isPalindrome($current)) {
                $factors = [$j, $i];

                // Create empty array if not in keys.
                if (array_key_exists($current, $result) != true) {
                    $result[$current] = [];
                }
                // Only add if the factors are not already present.
                if (array_reduce($result[$current], function ($item) use($factors) {
                    return $item == $factors ? 1 : 0;
                    }, 0) == 0) {
                    $result[$current][] = $factors;
                }
            }
        }
    }
    return $result;
}

// Check to see if a number is a palindrome. 
// Just cast to a string and check the string and reverse of the string are equal.
// @param int $num: The number to check.
// @returns: True if a palindrome, and false otherwise.
function isPalindrome(int $num) : bool 
{
    $text = strval($num);
    return strcmp($text, strrev($text)) == 0;
}
