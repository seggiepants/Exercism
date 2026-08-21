// Alphametics puzzle exercise. Find a matching math equation when you replace characters with digits.
package alphametics

import (
	"errors"
	"regexp"
	"slices"
	"strconv"
	"strings"
)

// Solve an alphametics puzzle. A-Z characters in a string are repaced with digits. If done properly the match works when converted from character to digit.
// @param puzzle: The puzzle to solve.
// @returns map[string]int where the key is the character in the puzzle and the int is the digit it should be mapped to.
// @raises: error is raised if no solution was found.
func Solve(puzzle string) (map[string]int, error) {
	r := regexp.MustCompile("[A-Z]+")
	var tokens []string = make([]string, 0)
	var notZero []string = make([]string, 0) // 1st character of a word can't be zero unless only character

	for _, match := range r.FindAllStringSubmatch(puzzle, -1) {
		token := match[0]
		tokens = append(tokens, token)
		temp := string(token[0])
		if len(token) > 1 && !slices.Contains(notZero, temp) {
			notZero = append(notZero, temp)
		}
	}

	var uniqueChars []string = make([]string, 0)
	for _, char := range puzzle {
		if char >= 'A' && char <= 'Z' {
			test := string(char)
			if !slices.Contains(uniqueChars, test) {
				uniqueChars = append(uniqueChars, test)
			}
		}
	}
	var digitMap [10]string = [10]string{"", "", "", "", "", "", "", "", "", ""}
	reducedPuzzle := strings.Join(tokens, " ")
	ret, ok := Step(reducedPuzzle, digitMap, notZero, len(uniqueChars), strings.Join(uniqueChars, ""), 0)
	if !ok {
		return map[string]int{}, errors.New("No solution found.")
	}
	return ret, nil
}

// Perform one step of determining the solution.
// @param puzzle: Simplified puzzle input (just a space separated sequence of numbers)
// @param digitMap: Array of 10 strings. "" = no mapping otherwise digitMap[i] = character as string says that digit at that index maps to that character
// @param notZero: Slice of strings. Digits that can't start a number because 01 = 1 and the zero would have been removed.
// @param totalUnique: The number of unique characters in the puzzle
// @param mappedDigits: map[string]int This map has a single character string with a character as the key and the digit it maps to as the value
// @returns: map[string]int that maps a character (as string) to a digit, and a ok flag
// @raises: ok flag is false if we couldn't find a solution.
func Step(puzzle string, digitMap [10]string, notZero []string, totalUnique int, uniqueChars string, mappedDigits int) (map[string]int, bool) {
	var ret map[string]int = make(map[string]int, 0)

	// base case check for a valid solution
	if mappedDigits == totalUnique {
		valid := Compute(puzzle)
		if !valid { // character => digit
			return ret, false
		} else {
			ret = make(map[string]int, 0)
			for i, digit := range digitMap {
				if digit != "" {
					ret[digit] = i
				}
			}
			return ret, true
		}
	}

	// find the first unmapped character
	if len(uniqueChars) <= 0 {
		return ret, false
	}

	candidate := string(uniqueChars[0])
	for i := len(digitMap) - 1; i >= 0; i-- {
		if (i == 0 && slices.Contains(notZero, candidate)) || digitMap[i] != "" {
			continue
		}
		digitMap[i] = candidate
		ret, ok := Step(strings.ReplaceAll(puzzle, candidate, strconv.Itoa(i)), digitMap, notZero, totalUnique, strings.ReplaceAll(uniqueChars, candidate, ""), mappedDigits+1)
		if ok {
			return ret, true
		} else {
			digitMap[i] = ""
		}
	}
	return ret, false
}

// After the characters have been replaced with digits make sure the sums of parts add up to the total
// @param puzzle: The puzzle with characters replaced by digits (space separated string of numbers)
// @returns: True if all but the last part of the puzzle add up to the last part of the puzzle
func Compute(puzzle string) bool {
	var total int = 0
	var rhsAmount int = 0
	for _, chunk := range strings.Split(puzzle, " ") {
		num, err := strconv.Atoi(chunk)
		if err == nil {
			total += rhsAmount
			rhsAmount = num
		} else {
			return false
		}
	}
	return rhsAmount == total
}
