package twobucket

import (
	"errors"
	"slices"
)

// Find an optimal way to get a desired goal value with two buckets of a given size
// @param sizeBucketOne: The size of the first bucket
// @param sizeBucketTwo: The size of the second bucket
// @param goalAmount: The amount we are trying to measure
// @param startBucket: Start with this bucket filled ('one', or 'two')
// @returns: The goal bucket, number of moves, and the other bucket.
// @raises: error if the goal is nonsensicle or a path to it could not be found.
func Solve(sizeBucketOne, sizeBucketTwo, goalAmount int, startBucket string) (string, int, int, error) {
	if goalAmount > max(sizeBucketOne, sizeBucketTwo) {
		return "n/a", -1, -1, errors.New("Invalid goal - larger than maximum possible.")
	}
	if sizeBucketOne <= 0 || sizeBucketTwo <= 0 {
		return "n/a", -1, -1, errors.New("Invalid bucket size.")
	}
	if goalAmount <= 0 {
		return "n/a", -1, -1, errors.New("Invalid goal amount")
	}
	var bucketOneAmt int = 0
	var bucketTwoAmt int = 0
	switch startBucket {
	case "one":
		bucketOneAmt = sizeBucketOne
	case "two":
		bucketTwoAmt = sizeBucketTwo
	default:
		return "n/a", -1, -1, errors.New("Invalid start bucket name")
	}
	moveStack := make([][2]int, 0)
	moveStack = append(moveStack, [2]int{bucketOneAmt, bucketTwoAmt})

	result, err := step(moveStack, bucketOneAmt, bucketTwoAmt, startBucket, sizeBucketOne, sizeBucketTwo, goalAmount)
	if err != nil {
		return "n/a", -1, -1, err
	}
	return result.bucket, result.moves, result.other, nil
}

type Result struct {
	bucket string
	moves  int
	other  int
}

// Solve the two bucket problem recursively. Try every possible move then reevaluate from that state
// until you hit the goal. You can have multiple ways to the goal so return one with the shortest sequence
// @param moveStack: Slice of Bucket amount pairs. No use reevaluating a previous state
// @param bucketOneAmt: How many liters bucket one currently contains
// @param bucketTwoAmt: How many liters bucket two currently contains
// @param originalBucket: What bucket did we start on for the illegal move checks
// @param bucketOneMax: How many liters can bucket one hold
// @param bucketTwoMax: How many liters can bucket two hold
// @param goal: The number of liters we want to get to in one of the given buckets.
// @returns: Result struct containing the bucket that ended up with the goal amount, number of moves,
// and how much was in the remaining bucket returned as an object expected by the test.
// On error moves will be -1
// and goal_bucket will be n/a also other bucket will be 0.
func step(moveStack [][2]int, bucketOneAmt int, bucketTwoAmt int, originalBucket string, bucketOneMax int, bucketTwoMax int, goal int) (Result, error) {
	// Base state, return on goal
	if bucketOneAmt == goal {
		return Result{moves: len(moveStack), bucket: "one", other: bucketTwoAmt}, nil
	}

	if bucketTwoAmt == goal {
		return Result{moves: len(moveStack), bucket: "two", other: bucketOneAmt}, nil
	}

	// Three possible actions:
	// - fill one or two
	// - pour one into two or two into one until other full or current empty
	// - empty one or two
	// Move is invalid if it leaves original_bucket = empty and other_bucket = filled
	results := make([]Result, 0)

	// Fill 1
	illegalFill := originalBucket == "two" && bucketTwoAmt == 0
	var wasVisited bool = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == bucketOneMax && pair[1] == bucketTwoAmt
	}) != -1
	if !(illegalFill || wasVisited || bucketOneAmt == bucketOneMax) {
		result, err := step(append(moveStack, [2]int{bucketOneMax, bucketTwoAmt}),
			bucketOneMax,
			bucketTwoAmt,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Fill 2
	illegalFill = originalBucket == "one" && bucketOneAmt == 0
	wasVisited = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == bucketOneAmt && pair[1] == bucketTwoMax
	}) != -1

	if !(illegalFill || wasVisited || bucketTwoAmt == bucketTwoMax) {
		result, err := step(append(moveStack, [2]int{bucketOneAmt, bucketTwoMax}),
			bucketOneAmt,
			bucketTwoMax,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Pour 1 to 2
	amountToPour := min(bucketOneAmt, bucketTwoMax-bucketTwoAmt)
	illegalPour := originalBucket == "one" && bucketOneAmt-amountToPour == 0 && bucketTwoAmt+amountToPour == bucketTwoMax
	wasVisited = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == bucketOneAmt-amountToPour && pair[1] == bucketTwoAmt+amountToPour
	}) != -1
	if !(illegalPour || wasVisited || amountToPour <= 0) {
		result, err := step(append(moveStack, [2]int{bucketOneAmt - amountToPour, bucketTwoAmt + amountToPour}),
			bucketOneAmt-amountToPour,
			bucketTwoAmt+amountToPour,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Pour 2 to 1
	amountToPour = min(bucketTwoAmt, bucketOneMax-bucketOneAmt)
	illegalPour = originalBucket == "two" && bucketTwoAmt-amountToPour == 0 && bucketOneAmt+amountToPour == bucketOneMax
	wasVisited = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == bucketOneAmt+amountToPour && pair[1] == bucketTwoAmt-amountToPour
	}) != -1

	if !(illegalPour || wasVisited || amountToPour <= 0) {
		result, err := step(append(moveStack, [2]int{bucketOneAmt + amountToPour, bucketTwoAmt - amountToPour}),
			bucketOneAmt+amountToPour,
			bucketTwoAmt-amountToPour,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Empty 1
	illegalEmpty := (originalBucket == "one" && bucketTwoAmt == bucketTwoMax)
	wasVisited = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == 0 && pair[1] == bucketTwoAmt
	}) != -1

	if !(illegalEmpty || wasVisited || bucketOneAmt == 0) {
		result, err := step(append(moveStack, [2]int{0, bucketTwoAmt}),
			0,
			bucketTwoAmt,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Empty 2
	illegalEmpty = originalBucket == "two" && bucketOneAmt == bucketOneMax
	wasVisited = slices.IndexFunc(moveStack, func(pair [2]int) bool {
		return pair[0] == bucketOneAmt && pair[1] == 0
	}) != -1

	if !(illegalEmpty || wasVisited || bucketTwoAmt == 0) {
		result, err := step(append(moveStack, [2]int{bucketOneAmt, 0}),
			bucketOneAmt,
			0,
			originalBucket,
			bucketOneMax,
			bucketTwoMax,
			goal)
		if err == nil && result.moves >= 0 {
			results = append(results, result)
		}
	}

	// Failure if we ran out of legal moves.
	if len(results) == 0 {
		return Result{
			bucket: "n/a",
			moves:  -1,
			other:  0,
		}, errors.New("No more legal moves")
	}

	// Return the result with the shortest path. Don't care
	// how a tie is sorted as we only really want the length.
	slices.SortFunc(results, func(a Result, b Result) int {
		return a.moves - b.moves
	})
	return results[0], nil
}
