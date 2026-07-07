<?php
// ETL exercise.

declare(strict_types=1);

// Transform the given input array. Swap key = array[value] for lowercase(value) = key
// @param $input: The array to transform.
// @returns: Array of the results of transforming the input.
function transform(array $input): array
{
    $ret = array();
    foreach($input as $key => $values) {
        $points = intval($key);
        foreach($values as $letter) {
            $ret[strtolower($letter)] = $points;
        }
    }
    return $ret;
}
