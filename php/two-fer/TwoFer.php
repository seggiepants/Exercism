<?php
// Exercise: Two-Fer

declare(strict_types=1);

// Compute a two fer statement. Output depends on if a name is given.
// @returns One for $name, one for me. where $name = "you" if not populated.
function twoFer(string $name = "you"): string
{
    return "One for {$name}, one for me.";
}
