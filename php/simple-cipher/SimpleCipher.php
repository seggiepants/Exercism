<?php

// Exercise: Simple Cipher

declare(strict_types=1);

// Simple Cipher exercise class.
class SimpleCipher
{
    const int KEY_LEN = 100;
    const string ALPHABET = "abcdefghijklmnopqrstuvwxyz";
    public string $key;

    // Setup the Simple Cipher class. Uses the given key if not null otherwise a random key is generated.
    // @param string $key = null: If null generate a random string of letters for the key otherwise use the given value for the key.
    public function __construct(string $key = null)
    {
        if ($key !== null) {
            $this->key = $key;
        } else {
            $this->key = "";
            for($i = 0; $i < self::KEY_LEN; $i++) {
                $this->key .= substr(self::ALPHABET, rand(0, strlen(self::ALPHABET) - 1), 1);
            }
        }
    }

    // Encrypt the cipher text using the key
    // @param string $plainText: The text that should be Simple Cipher encoded with the class instance's key.
    // @returns string: The encoded/cipher text.
    public function encode(string $plainText): string
    {
        $lenAlpha = strlen(self::ALPHABET);
        $lenKey = strlen($this->key);
        $result = "";
        foreach(str_split(strToLower($plainText)) as $i => $char) {
            $index = strpos(self::ALPHABET, $char);
            if ($index === false) {
                $result .= $char;
            } else {
                $keyChar = substr($this->key, $i % $lenKey, 1);
                $keyIndex = strpos(self::ALPHABET, $keyChar);
                if ($keyIndex === false) {
                    $result .= $char;
                } else {
                    $result .= substr(self::ALPHABET, ($index + $keyIndex) % $lenAlpha, 1);
                }
            }
        }
        return $result;
    }

    // Decrypt the cipher text using the key
    // @param string $cipherText: The Simple Cipher encoded text that was encoded with the class instance's key.
    // @returns string: The decoded original text.
    public function decode(string $cipherText): string
    {
        $lenAlpha = strlen(self::ALPHABET);
        $lenKey = strlen($this->key);

        $result = "";
        foreach(str_split(strToLower($cipherText)) as $i => $char) {
            $index = strpos(self::ALPHABET, $char);
            if ($index === false) {
                $result .= $char;
            } else {
                $keyChar = substr($this->key, $i % $lenKey, 1);
                $keyIndex = strpos(self::ALPHABET, $keyChar);
                if ($keyIndex === false) {
                    $result .= $char;
                } else {
                    $result .= substr(self::ALPHABET, ($index - $keyIndex + $lenAlpha) % $lenAlpha, 1);
                }
            }
        }
        return $result;
    }
}
