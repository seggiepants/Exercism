<?php
// Change - Give back change in the smallest number of coins.

declare(strict_types=1);

// Return change in the fewest number of coins.
// @param $coins: available coin denominations.
// @param $amount: The amount of change to return.
// @returns: Array with coin denomintaions
// @raises: InvalidArgumentException if you pass in a negative amount, if the amount 
// is smaller than the smallest available coin, or if there is no way to add up to 
// the desired amount.
function findFewestCoins(array $coins, int $amount): array
{
    // Negative amount is not allowed.
    if ($amount < 0) {
        throw new InvalidArgumentException("Cannot make change for negative value");
    }

    // Special case, 0 change is an empty array.
    if ($amount == 0) {
        return array();
    }

    // Raise error if no coin is small enough.
    $minCoin = min($coins);
    if ($minCoin > $amount) {
        throw new InvalidArgumentException("No coins small enough to make change");
    }

    $changeToCoins = array();         
    // Add single coins to the results.
    for($i = 0; $i < count($coins); $i++) {
        $coin = $coins[$i];        
        $temp = array();
        $temp[] = $coin;
        $changeToCoins[$coin] = $temp;
    }
    
    // Have to do several rounds to find the smallest solution.
    // If you have a result of size n we should do at least n - 1 rounds to make sure 
    // anything smaller makes its way through to the results.
    $round = 0;
    $continue = true; // Continue the loop.
    while ($continue) {
        $round++;

        for ($i = 0; $i < count($coins); $i++) {
            $coin = $coins[$i];

            // Lets keep a copy of the keys since they are mutated during the loop.
            $keys = array_keys($changeToCoins);

            for($j = 0; $j < count($keys); $j++) {
                $key = $keys[$j];
                $value = $changeToCoins[$key];
                if ($key + $coin <= $amount)
                {
                    $copy = [...$value];
                    $copy[] = $coin;
                    $total = array_sum($copy);

                    // Add the candidate if not present or update it if fewer coins.
                    if (!array_key_exists($total, $changeToCoins) || count($changeToCoins[$total]) > count($copy)) {
                        $changeToCoins[$total] = $copy;
                    }
                }
            }
        }

        $continue = TRUE;
        // Stop if we have the target and enough rounds.
        if (array_key_exists($amount, $changeToCoins)) {
            if ($round >= count($changeToCoins[$amount])) {
                $continue = FALSE;
            }
        }
        // Stop if we have enough rounds that we should have met the amount
        // with just the smallest coin.
        if ($round > $amount / $minCoin) {
            $continue = FALSE;
        }
    }

    // Throw exception if we don't have the amount.
    if (!array_key_exists($amount, $changeToCoins)) {
        throw new InvalidArgumentException("No combination can add up to target");
    }

    // Sort the result to please the test cases.
    $result = $changeToCoins[$amount];
    sort($result);
    return $result;
}