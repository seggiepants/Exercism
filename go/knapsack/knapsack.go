package knapsack

// Knapsack exercise

import (
	"fmt"
	"slices"
	"strconv"
)

type Item struct {
	Weight, Value int
}

// Knapsack takes in a maximum carrying capacity and a slice of items
// and returns the maximum value that can be carried by the knapsack
// given that the knapsack can only carry a maximum weight given by maximumWeight
// @param maximumWeight: The maximum item weigth that the Knapsack can hold
// @param items: Slice it items (weight, value) to try to add to the knapsack.
// @returns: The best value that the knapsack can hold.
func Knapsack(maximumWeight int, items []Item) int {
	slices.SortFunc(items, func(a Item, b Item) int {
		if a.Weight == b.Weight {
			return b.Value - a.Value
		}
		return b.Weight - a.Weight
	})
	memoized := make(map[string]int, 0)
	return Helper(maximumWeight, items, memoized)
}

// Recursive Helper function for the Knapsack function.
// @param maximumWeight: The maximum item weigth that the Knapsack can hold
// @param items: Slice it items (weight, value) to try to add to the knapsack.
// @param memoized: Stored sub-solutions that have already been computed.
// @returns: The best value that the knapsack can hold. Zero if no solutions
func Helper(maximumWeight int, items []Item, memoized map[string]int) int {
	scores := make([]int, 0)
	for i, item := range items {
		if item.Weight < maximumWeight {
			var nextItems []Item = make([]Item, 0)
			for index, value := range items {
				if index != i && value.Weight <= maximumWeight {
					nextItems = append(nextItems, value)
				}
			}
			nextWeight := maximumWeight - item.Weight
			var value int
			key := strconv.Itoa(nextWeight) + ",["
			for _, nextItem := range nextItems {
				key += fmt.Sprintf("%d|%d, ", nextItem.Weight, nextItem.Value)
			}
			key += "]"

			value, ok := memoized[key]
			if !ok {
				value = Helper(nextWeight, nextItems, memoized)
				memoized[key] = value
			}
			scores = append(scores, item.Value+value)
		} else if item.Weight == maximumWeight {
			scores = append(scores, item.Value)
		}
	}
	if len(scores) == 0 {
		return 0
	}
	return slices.Max(scores)
}
