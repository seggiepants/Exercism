package luhn

// Test is a given string represents a number that would pass as a luhn number.
// :param id: The digits to check. only 0-9 and spaces are allowed.
// :returns: True if the digits can pass as a Luhn number. False if invalid
// characters, insuffucient digits or a number that does not pass the lunh function.
func Valid(id string) bool {
	count_digits := 0
	total := 0
	double := true
	for i := len(id) - 1; i >= 0; i-- {
		rune := id[i]
		if rune < '0' || rune > '9' {
			if rune != ' ' {
				return false
			}
			continue
		}
		double = !double
		count_digits++
		num := int(rune - '0')
		if double {
			num *= 2
			if num > 9 {
				num -= 9
			}
		}
		total += num
	}
	return count_digits > 1 && total%10 == 0
}
