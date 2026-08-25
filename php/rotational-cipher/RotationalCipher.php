<?php
// Rotational (Caeser) Cipher exercise.
// Note to self, I should have used array_map

declare(strict_types=1);

const ALPHABET_SIZE = 26;

class RotationalCipher
{
    // Rotate a single character by a shift for a given start of the alphabet
    // @param $char: The character to rotate
    // @param $minChar: The character at the start of the alphabet ("a", or "A")
    // @param $shift: How many places to rotate the character.
    // @returns: The rotated character
    private function rotateChar(string $char, string $minChar, int $shift): string
    {
        $index = ord($char) - ord($minChar);
        $index += $shift;
        $index = $index % ALPHABET_SIZE;
        return chr($index + ord($minChar));
    }
    
    // Rotate a text string by a set number of places.
    // Characters that are not A-Z or a-z will not be rotated and will propogate to the
    // output as-is.
    // @param $text: The text to rotate
    // @param $shift: The number of places to rotate each character.
    // @returns: Copy of $text but with the alphabetic characters rotated by the shift amount.
    public function rotate(string $text, int $shift): string
    {

        $result = "";
        foreach(str_split($text, 1) as $char) {
            if ($char >= "a" && $char <= "z") {
                $result .= $this->rotateChar($char, "a", $shift);
            } else if ($char >= "A" && $char <= "Z") {
                $result .= $this->rotateChar($char, "A", $shift);
            } else {
                $result .= $char;
            }
        }
        return $result;    
    }
}
