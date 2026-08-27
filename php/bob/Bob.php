<?php
// Bob exercise simulate a low-verbal teenager

declare(strict_types=1);

class Bob
{
    // Simulate a low-verbal teenager
    // @param $str: The message you speak to the teenager.
    // @returns: The teenager's response one of the following:
    // * "Fine. Be that way!"
    // * "Calm down, I know what I'm doing!"
    // * "Woah, chill out!"
    // * "Sure."
    // * "Whatever."
    public function respondTo(string $str): string
    {
        $str = trim($str);
        $hasAlpha = preg_match("/[a-z]/i", $str);
        $allCaps = strtoupper($str) == $str && $hasAlpha;
        $isEmpty = strlen($str) == 0;
        if ($isEmpty) {
            return "Fine. Be that way!";
        } else if (str_ends_with($str, "?")) {
            if ($allCaps) {
                return "Calm down, I know what I'm doing!";
            } else {    
                return "Sure.";
            }
        } else if ($allCaps) {
            return "Whoa, chill out!";
        }
        return "Whatever.";
    }
}
