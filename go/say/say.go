// Say exercise - Change a number to a spoken word string version of the number (example 13 -> "thirteen")
package say

var zeroToNineteen []string = []string{
	"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten",
	"eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"}

var tens []string = []string{"", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"}

// Given a number return the number as it would be spoken.
// @param n: The number to convert to a spoken word string
// @returns: Spoken word string version of the number, and success flag
// @raises: Success flag is false if the number is not a positive integer > 0
func Say(n int64) (string, bool) {
	if n < 0 || n > 999_999_999_999 {
		return "Number must be between 0 and 999,999,999,999.", false
	}

	if n == 0 {
		return zeroToNineteen[n], true
	}

	triples := map[string]int64{
		"billion":  1_000_000_000,
		"million":  1_000_000,
		"thousand": 1_000,
	}

	var ret string = ""
	for name, value := range triples {
		larger := value * 1000
		if n%larger >= value {
			threeDigit := ((n - (n % value)) / value) % 1_000

			if threeDigit > 0 {
				if len(ret) > 0 {
					ret += " "
				}
				ret += zeroToNineHundredNinteyNine(threeDigit) + " " + name
			}
		}
	}

	if n%1_000 > 0 {
		if len(ret) > 0 {
			ret += " "
		}
		ret += zeroToNineHundredNinteyNine(n % 1_000)
	}
	return ret, true
}

// For a number between 0 and 999 calculate the spoken word equivalent
// @parma n: The number to convert
// @returns: The string version of the number.
func zeroToNineHundredNinteyNine(n int64) string {
	var ret string = ""
	if n%1_000 >= 100 {
		var threeDigit int64 = n % 1000
		var digit int64 = (threeDigit - (threeDigit % 100)) / 100

		if len(ret) > 0 {
			ret += " "
		}
		ret += zeroToNineteen[digit] + " hundred"
	}
	if n%100 > 0 {
		var twoDigit string = zeroToNinteyNine(n % 100)
		if len(twoDigit) > 0 {
			if len(ret) > 0 {
				ret += " "
			}
		}
		ret += twoDigit
	}
	return ret
}

// For a number between 0 and 99 calculate the spoken word equivalent
// @parma n: The number to convert
// @returns: The string version of the number.
func zeroToNinteyNine(n int64) string {
	var ret string = ""
	if n%100 > 0 {
		var twoDigit int64 = n % 100
		if twoDigit < 20 {
			ret += zeroToNineteen[twoDigit]
		} else {
			ret += tens[(twoDigit-(twoDigit%10))/10]
			if twoDigit%10 != 0 {
				ret += "-" + zeroToNineteen[twoDigit%10]
			}
		}
	}
	return ret
}
