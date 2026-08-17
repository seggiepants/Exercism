<?php
// NthPrime exercise


declare(strict_types=1);

// Return the nth prime number > 1
// @param $number: Which prime to return.
// @returns: The $number-th prime number
function prime(int $number)
{
    if ($number <= 0) {
        throw new Exception("Number must be positive and greater than zero.");
    }
    $primes = array(2);
    $next = 2;
    while (count($primes) < $number) {
        $found = false;
        foreach($primes as $divisor) {
            if ($next % $divisor == 0) {
                $found = true;
                break;
            }
        }
        if (!$found) {
            $primes[] = $next;
        }
        $next++;
    }    
    return $primes[$number - 1];
}
