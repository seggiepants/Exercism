<?php
// Affine Cipher exercise.

declare(strict_types=1);

const ALPHABET_LEN = 26;
const GROUP_SIZE= 5;

// Encode text using the affine cipher
// @param $text: The message to encode
// @param $num1: The first key
// @param $num2: The second key
// @returns: Encoded version of the text
function encode(string $text, int $num1, int $num2): string
{
    if (!isCoPrime($num1, ALPHABET_LEN)) {
        throw new Exception('num1 and alphabet length must be coprime.');
    }

    $chars = mb_str_split(strtolower($text));

    // E(x) = (ai + b) mod m
    $group_count = 0;
    $result = "";
    foreach($chars as $char) {        
        if (($char >= "a" && $char <= "z") || ($char >= "0" && $char <= "9")) {
            if ($char >= "a" && $char <= "z") {
                $index = ord($char) - ord("a");
                $encrypted = (($num1 * $index) + $num2) % ALPHABET_LEN;
                $result = $result . chr(ord("a") + $encrypted);
            } else {
                $result = $result . $char;
            }
            
            $group_count++;
            if ($group_count >= GROUP_SIZE) {
                $group_count -= GROUP_SIZE;
                $result = $result . " ";
            }
        }
    }
    return trim($result);
}

// Decode text using the affine cipher
// @param $text: The message to decode
// @param $num1: The first key
// @param $num2: The second key
// @returns: The decoded text
function decode(string $text, int $num1, int $num2): string
{    
    // D(y) = (a^-1)(y - b) mod m
    $result = "";
    $chars = mb_str_split(strtolower($text));
    foreach($chars as $char) {        
        if (($char >= "a" && $char <= "z") || ($char >= "0" && $char <= "9")) {
            if ($char >= "a" && $char <= "z") {
                $index = ord($char) - ord("a");
                $value = (mmi($num1, ALPHABET_LEN) * ($index - $num2)) % ALPHABET_LEN;
                while ($value < 0) {
                    $value += ALPHABET_LEN;
                }
                $result = $result . chr(ord("a") + ($value % ALPHABET_LEN));
            } else {
                $result = $result . $char;
            }
        }
    }
    return $result;
    
}

// Check if two numbers are co prime (a, and b have no common factors)
// @param $a: The first number to check
// @param $b: The second number to check
// @returns: true if they are co-prime and false otherwise
function isCoPrime(int $a, int $b) {
  for($i = 2; $i <= $a && $i <= $b; $i++) {
    if ($a % $i == 0 && $b % $i == 0) {
      return false;
    }
  }
  return true;
}

// Find the modular multiplicative inverse of a, and m
// The value is where $a times the value has a remainder after
// dividing by $m of 1.
// @param $a: First Key
// @param $m: Set length
// @returns: Modular Multiplicative inverse of $a and $m.
// @raises: Exception if no value found.
function mmi($a, $m) {
  for($i = 1; $i < $m; $i++) {
    if (($a * $i) % $m == 1) {
      return $i;
    }
  }
  throw new Exception("No MMI for ({$a}, {$m})");
}
