<?php
// Book store exercise. Why is this marked easy instead of hard.
// Yes I did just base this on my javascript version.

declare(strict_types=1);

$GLOBALS['cache'] = array();

const BOOK_PRICE = 800;
const DISCOUNT = array( 1 => 0, 2 => 0.05, 3 => 0.10, 4 => 0.20, 5 => 0.25);
const PRICES = array(
    1 => BOOK_PRICE, 
    2 => (2 * BOOK_PRICE) * (1 - DISCOUNT[2]),
    3 => (3 * BOOK_PRICE) * (1 - DISCOUNT[3]),
    4 => (4 * BOOK_PRICE) * (1 - DISCOUNT[4]),
    5 => (5 * BOOK_PRICE) * (1 - DISCOUNT[5]),
);

/**
 * Note: we expect the total in cents (1$ = 100 cents).
 */

// Get the total of the books in the given items array with the best possible discount.
// @param $items: Array with keys 1-5 (1-5 being books in the series) with the number of books 
// purchased for each entry in the series.
// @returns: Two digit implied decimal value of the books purchased with best discount.
function total(array $items): int
{
    $books = array(
        1 => 0, 2 => 0, 3 => 0, 4 => 0, 5 => 0
    );
    foreach($items as $item) {
        $books[$item]++;
    }
    $permutations = getPermutations();
    $result =  intval(helper($permutations, $books));    

    return $result;
}

// Calculate all of the different combinations of discounts.
// @returns: array filled with arrays. Where each sub-array is a potential combination of books in the series.
function getPermutations(): array
{
  $permutations = array();
  // all 5
  $permutations[] = array(1, 2, 3, 4, 5);
  
  // include 4 -- skip 1
  for($i = 0; $i < 5; $i++){
    $baseline = array(1, 2, 3, 4, 5);
    array_splice($baseline, $i, 1);
    $permutations[] = $baseline;
  }

  // Length 3
  for($k = 1; $k <= 3; $k++) {
    for($j = $k + 1; $j <= 4; $j++) {
      for($i = $j + 1; $i <= 5; $i++){
        $permutations[] = array($k, $j, $i);
      }
    }
  }

  // Length 2
  for($j = 1; $j <= 5; $j++) {
    for($i = $j + 1; $i <= 5; $i++) {
      $permutations[] = array($j, $i);
    }
  }

  // single 
  for($i = 1; $i <= 5; $i++) {
    $permutations[] = array($i);
  }

  return $permutations;
}

// Recursive helper function to find the best book discount.
// @param $permutations: All possible book permutations of books purchased in the series. Combinations of 1-5 no duplicates.
// @param $books: Associative array with key (index) = value (count of books for that entry in the series being purchased)
// @returns floating point value of best price found. Returned as float as to no lose precision until the last moment.
function helper($permutations, $books): float {    
    global $cache;

    $sum = 0;
    $parts = array();
    foreach($books as $key => $value) {
        $sum += $value;
        $parts[] = $key . "=" . strval($value);
    }
    if ($sum == 0) {
        return 0;
    }

    $cache_key = implode("|", $parts);
    if (array_key_exists($cache_key, $cache)) {
        return $cache[$cache_key];
    }

    $scores = array();

    foreach ($permutations as $permutation) {
        $copy = array();
        foreach($books as $key => $value) {
            $copy[$key] = $value;
        }

        // Does the current permutation match the books on hand?
        $missing = false;
        foreach($permutation as $value) {
            if ($copy[$value] == 0) {
                $missing = true;
                break;
            }
        }
        if (!$missing) {
            foreach($permutation as $value) {
                $copy[$value] -= 1;
            }   
            // Add the score for the permutation and best score for remaining books.
            $scores[] = PRICES[count($permutation)] + helper($permutations, $copy);
        }
    }
    // Save the mimimum price in cache.
    $cache[$cache_key] = min($scores);

    return $cache[$cache_key];
}
