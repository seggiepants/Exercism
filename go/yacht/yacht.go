// Yacht exercise. Score a roll of five dice in yacht.
package yacht

import (
	"slices"
)

// Score a roll of five dice in yacht for the selected category.
// @param dice: Slice of five dice faces 1-6.
// @param category: One of "ones", "twos", "threes", "fours", "fives",
// "sixes", "four of a kind", "full house", "big straight",
// "little straight", "choice", or "yacht"
// @returns: Computed score for the rolls and category
func Score(dice []int, category string) int {
	switch category {
	case "ones":
		return Score1to6(dice, 1)
	case "twos":
		return Score1to6(dice, 2)
	case "threes":
		return Score1to6(dice, 3)
	case "fours":
		return Score1to6(dice, 4)
	case "fives":
		return Score1to6(dice, 5)
	case "sixes":
		return Score1to6(dice, 6)
	case "four of a kind":
		return ScoreFourOfAKind(dice)
	case "full house":
		return ScoreFullHouse(dice)
	case "big straight":
		return ScoreStraight(dice, 2)
	case "little straight":
		return ScoreStraight(dice, 1)
	case "choice":
		return SumValues(dice)
	case "yacht":
		return ScoreYacht(dice)
	}
	return 0
}

// Score "ones", "twos", "threes", "fours", "fives", or "sixes"
// for each entry in the rolls array that has the chosen number increment
// the score by that number.
// @param rolls: Slice of dice values (5)
// @param number: The number to count (1-6)
// @returns: Count of $number in $rolls times $number.
func Score1to6(rolls []int, number int) int {
	score := 0
	for _, roll := range rolls {
		if roll == number {
			score += number
		}
	}
	return score
}

// Count the values in an array and return a map int => int of value and how many times it appears.
// @param values: Slice of int to count up occurences of
// @return: map[int]int key is the number in the source slice and value is the number of times it appeared.
func CountValues(values []int) map[int]int {
	results := make(map[int]int, 0)
	for _, value := range values {
		results[value]++
	}
	return results
}

// Sum up a slice of values. Isn't this in the standard library somewhere?
// @param values: slice of int to sum
// @returns: Sum of the values in the slice.
func SumValues(values []int) int {
	sum := 0
	for _, value := range values {
		sum += value
	}
	return sum
}

// Score "four of a kind" if there are 4+ duplicate values in the rolls array
// sum them up and return them (why not use choice);
// @param rolls: Slice of dice values (5)
// @returns: 0 if not four matching things, otherwise that thing times four.
func ScoreFourOfAKind(rolls []int) int {
	kinds := CountValues(rolls)
	for key, value := range kinds {
		if value >= 4 {
			return key * 4
		}
	}
	return 0
}

// Score "full house" if there are 2 of one number and three of another sum them up
// and return it. Again why not just use choice.
// @param rolls: Slice of dice values (5)
// @returns: 0 if not a pair and triple of values, otherwise the sum of all values.
func ScoreFullHouse(rolls []int) int {
	kinds := CountValues(rolls)
	score3 := 0
	score2 := 0
	for key, value := range kinds {
		if value == 3 {
			score3 = key * 3
		} else if value == 2 {
			score2 = key * 2
		}
	}
	if score3 > 0 && score2 > 0 {
		return score3 + score2
	}
	return 0
}

// Score "big straight" 2,3,4,5,6 or "little straight" 1,2,3,4,5
// @param rolls: Slice of dice values (5)
// @param start: Where to start 1 for little straight, 2 for a big straight.
// @returns: 0 if not the desired straight, otherwise 30 points.
func ScoreStraight(rolls []int, start int) int {
	for i := start; i < start+len(rolls); i++ {
		if !slices.Contains(rolls, i) {
			return 0
		}
	}
	return 30
}

// Score "yacht" checks if all cards are the same value
// @param rolls: Slice of dice values (5)
// @returns: 50 for yach, 0 if not.
func ScoreYacht(rolls []int) int {
	score := 50
	for i := 1; i < len(rolls); i++ {
		if rolls[i] != rolls[0] {
			score = 0
			break
		}
	}
	return score
}
