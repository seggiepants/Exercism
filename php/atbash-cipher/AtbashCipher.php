<?php
//Atbash Cipher implementation includes both encode and decode functions.

declare(strict_types=1);

// Encode a string using the Atbash Cipher
// @param $text: The text to encode.
// @returns: The encoded value.
function encode(string $text): string
{
    $ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    $CHUNK_SIZE = 5;
    $len_Alphabet = strlen($ALPHABET);
    $ord_A = ord("a");
    $ret = "";
    $nth = 0;
    $lower = strtolower($text);

    for($i = 0; $i < strlen($text); $i++) {        
        $char = $lower[$i];
        $write_char = FALSE;
        // Unless it is a letter or digit skip it.
        if ($char >= "0" && $char <= "9") {
            $write_char = TRUE;
        } else if ($char >= "a" && $char <= "z") {
            $char = $ALPHABET[$len_Alphabet - 1 - ord($char) + $ord_A];
            $write_char = TRUE;
        }

        // Write a char, generating a space first if needed.
        if ($write_char == TRUE) {
            if ($i > 0 && $nth >= $CHUNK_SIZE) {
                $nth = 0;
                $ret = $ret . " ";
            }
            $ret = $ret . $char;
            $nth++;
        }
    }
    return $ret;
}

// Decode a string encoded using the Atbash Cipher
// @param $text: The text to decode.
// @returns: The decoded value.
function decode(string $text): string
{
    $ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    $len_Alphabet = strlen($ALPHABET);
    $ord_A = ord("a");
    $ret = "";
    $lower = strtolower($text);

    for($i = 0; $i < strlen($text); $i++) {
        $char = $lower[$i];
        if ($char >= "a" && $char <= "z") {
            $ret = $ret . $ALPHABET[$len_Alphabet - 1 - ord($char) + $ord_A];
        }
        else if ($char != " ") {
            $ret = $ret . $char;
        }

    }
    return $ret;
}
