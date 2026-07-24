<?php
// Flatten array exercise

declare(strict_types=1);

// Flatten an array so any nested arrays have values included inline in the result.
// @param $input: The array to flatten.
// @returns: Array with the flattened value of the array and values from any sub-arrays.
function flatten(array $input): array
{
    $ret = array();

    foreach ($input as $value) {
        if (is_array($value)) {
            $ret = array_merge($ret, flatten($value));
        } else {
            if (!is_null($value)) {
                $ret[] = $value;
            }
        }
    }

    return $ret;
}
