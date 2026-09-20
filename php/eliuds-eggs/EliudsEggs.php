<?php
// Eliuds Eggs Example.

declare(strict_types=1);

// Egg counter class
class EliudsEggs
{
    // Get the bits from a number and count the 1s
    // @param $displayValue: Decimal value of the bits
    // @returns: Number of 1's in the number.
    public function eggCount(int $displayValue): int
    {
        $value = $displayValue;
        $count = 0;
        while ($value != 0) {
            if ($value & 0b00000001 > 0) {
                $count += 1;
            }
            $value = $value >> 1;
        }
        return $count;
    }
}
