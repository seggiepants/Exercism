<?php
// Micro-Blog Exercise. Trim to 5 code points

declare(strict_types=1);

// MicroBlog class holds the truncate function.
class MicroBlog
{
    // Truncate a multi-byte unicode string to five characters.
    // @param $text: The text to split.
    // @returns: String of up to five runes/code points.
    public function truncate(string $text): string
    {
        return mb_substr($text, 0, 5);
    }
}
