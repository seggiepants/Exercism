<?php
// Variable Length Quantity encoding exercise.

declare(strict_types=1);

const bit7_high = 0b10000000; // or it 
const bit7_low = 0b01111111; // and it

// Variable length encoder. Give an array of integers, return a 
// new array of those integers encoded with variable lenghth encoding.
// @param $input: Source array of integers
// @returns: Array of those integers encoded with variable length encoding.
function vlq_encode(array $input): array
{
    $ret = array();

    foreach ($input as $num) {
        $remaining = $num;
        $first = true;
        $temp = array();
        while ($remaining != 0 || $first) {
            $current = $remaining & bit7_low;
            $remaining >>= 7;
            if ($first) {
                array_unshift($temp,  bit7_low & $current);
            } else {
                array_unshift($temp, bit7_high | $current);
            }
            $first = false;
        }
        $ret = array_merge($ret, $temp);
    }
    return $ret;
}

// Decode a variable length quantity array.
// @param $input: Array of input integers.
// @returns: Array of output integers.
// @raises: Error if last byte doesn't end a number, or if we use
// more than four bytes for a value.
function vlq_decode(array $input): array
{
    $ret = array();
    $total = 0;
    if (count($input) > 0) {
        $temp = $input[count($input) - 1] & bit7_high;
        if ($temp != 0) {
            throw new InvalidArgumentException("Incomplete sequence");
        }
    }

    $bytes = 0;
    for($i = 0; $i < count($input); $i++) {
        $num = $input[$i] & bit7_low;
        $bit7 = $input[$i] & bit7_high;
        $total = ($total << 7) | $num;
        $bytes++;
        if ($bytes > 4) {
            throw new OverflowException("Too many bytes");
        }
        if ($bit7 == 0) {
            $ret[] = $total;
            $total = 0;
            $bytes = 0;
        }
    }    
    
    return $ret;
}
