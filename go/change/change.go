// Make best change from given coins exercise
package change

import (
	"errors"
	"fmt"
	"slices"
	"strconv"
)

// Calculate the change for the given coin types.
// @param coins: slice of int with coin denominations
// @param target: The exact change to make
// @returns: slice of int with the coins needed to make the change
// @raised: Error if cannot make exact change, or the desired total is negative
func Change(coins []int, target int) ([]int, error) {
	if target == 0 {
		return []int{}, nil
	} else if target < 0 {
		return []int{}, errors.New("Negative totals are not allowed.")
	}

	slices.Sort(coins)
	result := Helper(coins, make([]int, 0), target+1, target, map[string][]int{})
	slices.Sort(result)

	if len(result) == 0 {
		return []int{}, fmt.Errorf("The total %d cannot be represented in the given currency.", target)
	}
	return result, nil
}

// Without the memoization complex requests take too much time.

// Helper/Recursive call function to find the best change.
// @param coinArray: Available denominations of coins.
// @param coins: Coins chosen so far.
// @smallestSoFar: Smallest number of coins used to get to the total so far.
// @target: The target amount to get to.
// @memoized: Lookup table of values already found (so we don't compute the same thing over and over)
func Helper(coinArray []int, coins []int, smallestSoFar int, target int, memoized map[string][]int) []int {
	var subTotal int = SumSlice(coins)
	if subTotal == target {
		return coins
	}

	next := make([]int, 0)
	for _, coin := range coinArray {
		if coin+subTotal <= target {
			var smaller []int = make([]int, 0)
			var key string = strconv.Itoa(target - subTotal - coin)
			lookup, ok := memoized[key]
			if coin+subTotal == target {
				smaller = []int{coin}
			} else if ok {
				smaller = []int{coin}
				smaller = append(smaller, lookup...)
			} else {
				temp := Helper(coinArray, []int{}, target-subTotal-coin+1, target-subTotal-coin, memoized)
				memoized[key] = temp
				smaller = []int{coin}
				smaller = append(smaller, temp...)
			}
			current := make([]int, 0)
			current = append(current, coins...)
			current = append(current, smaller...)
			if len(current) > 0 && SumSlice(current) == target && len(current) < smallestSoFar && (len(next) == 0 || len(next) > len(current)) {
				next = make([]int, 0)
				next = append(next, current...)
				smallestSoFar = len(next)
			}
		}
	}
	return next
}

// Sum a slice of int and return the total.
// @param data: Slice of int to sum up.
// @returns: The total of the values in the slice.
func SumSlice(data []int) int {
	var result int = 0
	for _, value := range data {
		result += value
	}
	return result
}
