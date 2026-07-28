<?php
// Line-up exercise address a customer by name and number with number having a nd, rd, th, or st suffix

declare(strict_types=1);

// Address a customer by their name and number where the number has the proper suffix.
// @param $name: The name of the customer
// @param $number: The customer's position in line (integer >= 1)
// @returns: A custom greeting for the customer.
function format(string $name, int $number): string
{
    $last_digit = $number % 10;
    $last_two_digits = $number % 100;

    $suffix = match ($number % 100) {
        11, 12, 13 => "th", 
        default => match($number % 10) {
            1 => "st", 
            2 => "nd", 
            3 => "rd", 
            default => "th",
        }
    };
    return "{$name}, you are the {$number}{$suffix} customer we serve today. Thank you!";
}
