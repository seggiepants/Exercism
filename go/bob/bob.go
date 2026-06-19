// Simulate the responses from a teenager named Bob
package bob

import "strings"

const alphabet string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"

// Return likely responses from a unresponsive teenager named Bob
// @param remark: The sentence spoken to Bob
// @resunts: Bob's response.
func Hey(remark string) string {
	trimmed := strings.Trim(remark, " \t\r\n")
	isAllCaps := remark == strings.ToUpper(remark) && strings.ContainsAny(remark, alphabet)
	endsWithQuestionMark := strings.HasSuffix(trimmed, "?")
	isSilence := len(trimmed) == 0
	if isSilence {
		return "Fine. Be that way!"
	}
	if endsWithQuestionMark && isAllCaps {
		return "Calm down, I know what I'm doing!"
	}
	if isAllCaps {
		return "Whoa, chill out!"
	}
	if endsWithQuestionMark {
		return "Sure."
	}

	return "Whatever."
}
