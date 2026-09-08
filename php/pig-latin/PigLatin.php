<?php
// Pig Latin Exercise

declare(strict_types=1);

// Convert a series of word to pig latin.
// @param $text: Word or phrase to convert to pig-latin
// @returns: Word or phrase converted to pig-latin and lower case (because lazy).
function translate(string $text): string
{
    $pattern = '/\\w+/';
    $match_count = preg_match_all($pattern, strtolower($text), $matches);
    if ($match_count == 0 || $match_count == false) {
        return "";
    }
    $result = "";
    foreach($matches[0] as $word) {        
        $next = PigLatin($word);
        //echo $word . " => " . $next .PHP_EOL;

        if (strlen($result) > 0) {
            if (strlen($next) > 0) {
                $result .= " " . $next;
            }
        } else {
            $result = $next;
        }
    }

    return $result;
}

// Convert one word to pig latin
// @param $word: The word to convert to pig latin
// @returns: Word converted to pig-latin or the original word if no rules applied.
function PigLatin(string $word) : string {
    $vowels = array("a", "e", "i", "o", "u");
    $consonants = array("b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "v", "w", "x", "y", "z");
    if (strlen($word) < 1) {
        return "";
    }

    $first = $word[0];
    $remainder = substr($word, 1);
    $posQu = strpos($word, "qu");
    $posVowel = strcspn($word, implode($vowels));
    $posY = strpos($word, "y");

    if (str_contains($word, "y") && $posY < $posVowel && !str_starts_with($word, "yt")) {
        //echo PHP_EOL . substr($word, $posY) . "|" . substr($word, 0, $posY) . "|ay (y rule)" . PHP_EOL;
        if ($posY < 1) {
            $posY = 1;
        }
        return substr($word, $posY) . substr($word, 0, $posY) . "ay";
    } else if (str_contains($word, "qu") && $posQu < $posVowel) {
        //echo PHP_EOL . substr($word, $posQu + 2) . "|" . substr($word, 0, $posQu + 2) . "|ay" . PHP_EOL;
        return substr($word, $posQu + 2) . substr($word, 0, $posQu + 2) . "ay";
    } else if (in_array($first, $vowels) || str_starts_with($word, "xr") || str_starts_with($word, "yt")) {
        //echo PHP_EOL . "Starts with vowel " . PHP_EOL;
        return $word . "ay";
    } else if (in_array($first, $consonants)) {       
        //echo PHP_EOL . "Default" . PHP_EOL; 
        return substr($word, $posVowel) . substr($word, 0, $posVowel) . "ay";
    }
    return $word;
}
