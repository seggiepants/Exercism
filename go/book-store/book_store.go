package bookstore

import (
	"math"
	"slices"
	"strconv"
	"strings"
)

const BOOK_PRICE int = 800

var cache map[string]float64 = make(map[string]float64)
var DISCOUNT map[int]float64 = map[int]float64{1: 0.0, 2: 0.05, 3: 0.10, 4: 0.20, 5: 0.25}
var PRICES map[int]float64 = map[int]float64{
	1: 8.00,
	2: 15.20,
	3: 21.60,
	4: 25.60,
	5: 30.0,
}

/**
 * Note: we expect the total in cents (1$ = 100 cents).
 */

// Get the total of the books in the given items array with the best possible discount.
// @param $items: Array with keys 1-5 (1-5 being books in the series) with the number of books
// purchased for each entry in the series.
// @returns: Two digit implied decimal value of the books purchased with best discount.
func Cost(books []int) int {
	var bookMap map[int]int = map[int]int{
		1: 0, 2: 0, 3: 0, 4: 0, 5: 0,
	}

	for _, book := range books {
		bookMap[book]++
	}
	permutations := GetPermutations()
	return int(math.Round(helper(permutations, bookMap) * 100.0))
}

// Calculate all of the different combinations of discounts.
// @returns: array filled with arrays. Where each sub-array is a potential combination of books in the series.
func GetPermutations() [][]int {
	var permutations = make([][]int, 0)
	// all 5
	permutations = append(permutations, []int{1, 2, 3, 4, 5})

	// include 4 -- skip 1
	for i := 0; i < 5; i++ {
		baseline := []int{1, 2, 3, 4, 5}
		baseline = slices.Delete(baseline, i, i+1)
		permutations = append(permutations, baseline)
	}

	// Length 3
	for k := 1; k <= 3; k++ {
		for j := k + 1; j <= 4; j++ {
			for i := j + 1; i <= 5; i++ {
				permutations = append(permutations, []int{k, j, i})
			}
		}
	}

	// Length 2
	for j := 1; j <= 5; j++ {
		for i := j + 1; i <= 5; i++ {
			permutations = append(permutations, []int{j, i})
		}
	}

	// single
	for i := 1; i <= 5; i++ {
		permutations = append(permutations, []int{i})
	}

	return permutations
}

// Recursive helper function to find the best book discount.
// @param permutations: All possible book permutations of books purchased in the series. Combinations of 1-5 no duplicates.
// @param books: Map with key (index) = value (count of books for that entry in the series being purchased)
// @returns floating point value of best price found. Returned as float as to no lose precision until the last moment.
func helper(permutations [][]int, books map[int]int) float64 {

	totalBooks := 0
	var parts []string = make([]string, 0)
	for i := 1; i <= 5; i++ {
		value, ok := books[i]
		if ok {
			totalBooks += value
			parts = append(parts, strconv.Itoa(i)+"="+strconv.Itoa(value))
		}
	}
	if totalBooks == 0 {
		return 0
	}

	var cacheKey string = strings.Join(parts, "|")
	price, ok := cache[cacheKey]
	if ok {
		return price
	}

	var scores []float64 = make([]float64, 0)

	for _, permutation := range permutations {
		var copy map[int]int = make(map[int]int, len(books))
		for index, value := range books {
			copy[index] = value
		}

		// Does the current permutation match the books on hand?
		var missing bool = false
		for _, value := range permutation {
			if copy[value] == 0 {
				missing = true
				break
			}
		}
		if !missing {
			for _, value := range permutation {
				copy[value] -= 1
			}
			// Add the score for the permutation and best score for remaining books.
			scores = append(scores, PRICES[len(permutation)]+helper(permutations, copy))
		}
	}
	// Save the mimimum price in cache.
	cache[cacheKey] = slices.Min(scores)

	return cache[cacheKey]
}
