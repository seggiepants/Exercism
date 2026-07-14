<?php

declare(strict_types=1);

// Return a reversed copy of the input.
// @param $text: The string to reverse
// @returns: A copy of $text but in reverse order.
function reverseString(string $text): string
{
    // I am assuming using a built-in function would be frowned upon. strrev(string) for example.
    // This is one way to do it.
    /*
    $ret = "";
    $len = strlen($text);
    for ($i = 0; $i < $len; $i++) {
        $ret .= $text[$len - $i - 1];
    }
    return $ret;
    */

    // Or I could do it by exploding into a list and swapping chars.
    // This one is better because it is multi-byte character aware.
    // looks like php doesn't have var1, var2 = var2, var1 for swapping, too bad.
    /*
    $chars = mb_str_split($text);
    $first = 0;
    $last = count($chars) - 1;
    while ($first < $last) {
        $temp = $chars[$first];
        $chars[$first] = $chars[$last];
        $chars[$last] = $temp;
        $first++;
        $last--;
    }
    return implode($chars);
    */

    // I am going to use the built-in function anyway. 
    return strrev($text);
}
