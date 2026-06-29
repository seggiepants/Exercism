<?php
// Acronym: Create an acronym for the given text.

/*
 * By adding type hints and enabling strict type checking, code can become
 * easier to read, self-documenting and reduce the number of potential bugs.
 * By default, type declarations are non-strict, which means they will attempt
 * to change the original type to match the type specified by the
 * type-declaration.
 *
 * In other words, if you pass a string to a function requiring a float,
 * it will attempt to convert the string value to a float.
 *
 * To enable strict mode, a single declare directive must be placed at the top
 * of the file.
 * This means that the strictness of typing is configured on a per-file basis.
 * This directive not only affects the type declarations of parameters, but also
 * a function's return type.
 *
 * For more info review the Concept on strict type checking in the PHP track
 * <link>.
 *
 * To disable strict typing, comment out the directive below.
 */

declare(strict_types=1);

// Return an Acronym (first letter of each word as letters of a larger word) for the given text.
// @param $text: Words to make an acronym from.
// @returns: Acronym for the given text.
function acronym(string $text): string
{
    $words = explode(" ", str_replace("-", " ", $text));
    $result = "";

    for($index = 0; $index < count($words); $index++) {
        if (strlen($words[$index]) > 0) {
            $result = $result . $words[$index][0];
        }
    }

    return strtoupper($result);
}
