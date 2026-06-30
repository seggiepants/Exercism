<?php

declare(strict_types=1);

function detectAnagrams(string $word, array $anagrams): array
{
    $target = prepareWord($word);
    $wordLower = mb_convert_case($word, MB_CASE_LOWER);
    $result = array();
    for ($i = 0; $i < count($anagrams); $i++) {
        $anagramLower = mb_convert_case($anagrams[$i], MB_CASE_LOWER);
        if (strlen($word) == strlen($anagrams[$i]) && $wordLower != $anagramLower && prepareWord($anagrams[$i]) == $target) {
            //echo $i, $word, $anagrams[$i], prepareWord($anagrams[$i]), $target, "\n";
            array_push($result, $anagrams[$i]);
        }
    }
    return $result;
}

function prepareWord(string $word) : string {
    $arr = mb_str_split(mb_convert_case($word, MB_CASE_LOWER));
    sort($arr);
    return implode($arr);
}
