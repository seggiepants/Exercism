package isbnverifier

// Validate an isbn number
// @param isbn: The isbn to verify
// @returns: True if the number is valid
func IsValidISBN(isbn string) bool {
	multiplier := 10
	total := 0
	for _, rune := range isbn {
		if rune == '-' {
			continue
		}
		num := 0
		if rune == 'X' && multiplier == 1 {
			num = 10
		} else if rune >= '0' && rune <= '9' {
			num = int(rune - '0')
		} else {
			return false
		}
		total += multiplier * num
		multiplier -= 1
	}
	return multiplier == 0 && total%11 == 0
}
