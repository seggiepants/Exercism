// Exercise Wordy: Parse a math expression and return the result. No BODMAS/PEMDAS taken into account left to right only.
package wordy

import (
	"regexp"
	"strconv"
	"strings"
)

var reDigit *regexp.Regexp
var reOp *regexp.Regexp

// Parse a somewhat human written mathematical expression and return the result.
// This does not adhere to order of operations this is left to right only.
// @param question: The mathematical expression to parse.
// @returns: int the resulting value, and boolean true if the question was parsed and computed successfully.
func Answer(question string) (int, bool) {
	cutset := " \t\r\n"

	question = strings.TrimPrefix(question, "What is ")
	question = strings.TrimSuffix(question, "?")
	question = strings.Trim(question, cutset)
	question = strings.ReplaceAll(question, "/t", " ")
	question = strings.ReplaceAll(question, "/r", " ")
	question = strings.ReplaceAll(question, "/n", " ")
	question = strings.ReplaceAll(question, " by", "")

	reDigit = regexp.MustCompile(`^-?\d+$`)
	reOp = regexp.MustCompile(`^(plus|minus|multiplied|divided)$`)

	var err error
	var accumulator int64 = 0
	var operand int64 = 0
	operator := ""
	index := 0
	match := ""

	words := strings.Split(question, " ")

	match = reDigit.FindString(words[index])
	if match == "" {
		return 0, false
	}
	accumulator, err = strconv.ParseInt(match, 10, 64)
	if err != nil {
		return 0, false
	}
	index++

	for index < len(words) {
		operator = strings.Trim(reOp.FindString(words[index]), cutset)
		if operator == "" {
			return 0, false
		}
		index++

		if index >= len(words) {
			return 0, false // out of words expecting operand.
		}

		match = reDigit.FindString(words[index])
		if match == "" {
			return 0, false
		}
		operand, err = strconv.ParseInt(match, 10, 64)
		if err != nil {
			return 0, false
		}
		index++

		switch operator {
		case "plus":
			accumulator = accumulator + operand
		case "minus":
			accumulator = accumulator - operand
		case "multiplied":
			accumulator = accumulator * operand
		case "divided":
			accumulator = accumulator / operand
		default:
			// unsupported operator
			return 0, false
		}
	}
	return int(accumulator), true
}
