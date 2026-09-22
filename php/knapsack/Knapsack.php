<?php

// Knapsack exercise

declare(strict_types=1);

// Finds the best value for a set item items of given weight an value that can 
// be carried by a knapsack with a given maximum weight
class Knapsack
{
    // Finds the best value for a set item items of given weight an value that 
    // can be carried by a knapsack with a given maximum weight
    // @param $maximumWeight: The maximum amount of weight the knapsack can 
    // carry (don't worry about volume)
    // @param $items: Array of two element arrays with keys 'weight' and 
    // 'value' that describe possible items that may be carried
    // @returns: The maximum value that the knapsack can carry.
    public function getMaximumValue(int $maximumWeight, array $items): int
    {
        usort($items, function ($a, $b) {
            if ($a['weight'] == $b['weight']) {
                return $b['value'] - $a['value'];
            }
            return $b['weight'] - $a['weight'];
        });
        $memo = array();
        return $this->step($maximumWeight, $items, $memo);
    }

    // Recursive call that finds the best value for a set item items of given 
    // weight an value that can be carried by a knapsack with a given maximum 
    // weight
    // @param $maximumWeight: The maximum amount of weight the knapsack can 
    // carry (don't worry about volume)
    // @param $items: Array of two element arrays with keys 'weight' and 
    // 'value' that describe possible items that may be carried
    // @param $memoized: Saved values for possible combinations of items so we 
    // don't recompute things over and over.
    // @returns: The maximum value that the knapsack can carry.
    public function step(int $maximumWeight, array $items, array & $memoized): int {
        $scores = array();
        
        foreach($items as $i => $item) {
            if ($item['weight'] < $maximumWeight) {
                $candidates = array_filter($items, function ($value, $key) use($i, $maximumWeight) {
                    return $key != $i && $value['weight'] <= $maximumWeight;
                }, ARRAY_FILTER_USE_BOTH);
                $nextWeight = $maximumWeight - $item['weight'];
                $key = $nextWeight . ',[' . array_reduce($candidates, function ($carry, $value) {
                    return $carry . strval($value['weight']) . "|" . strval($value['value']);
                }, "") . "]";
                $value = 0;
                if (array_key_exists($key, $memoized)) {
                    $value = $memoized[$key];
                } else {
                    $value = $this->step($nextWeight, $candidates, $memoized);
                    $memoized[$key] = $value;
                }
                $scores[] = $item['value'] + $value;
            } else if ($item['weight'] == $maximumWeight) {
                $scores[] = $item['value'];
            }
        }
        return array_reduce($scores, function($carry, $value) {
            if ($value > $carry) {
                return $value;
            }
            return $carry;
        }, 0);
    }
}