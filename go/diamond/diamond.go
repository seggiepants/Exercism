// Diamond exercise.
package diamond

import (
	"errors"
	"strings"
)

// Generate a diamond pattern starting at A going to char then back down to A
// each row should be padded so that the diamond centered and all lines are equal length.
// @param char: The character in range of A-Z to stop at for the center.
// @returns: String with each row separated by a return (\n).
func Gen(char byte) (string, error) {
	if char < 'A' || char > 'Z' {
		return "", errors.New("Character not in range.")
	}
	ret := []string{}
	max := char - 'A'
	exterior := int(max)
	interior := 0
	for i := 0; i <= int(max); i++ {
		var next string = string(rune('A' + i))
		line := ""
		if interior == 0 {
			line = strings.Repeat(" ", exterior) + next + strings.Repeat(" ", exterior)
		} else {
			line = strings.Repeat(" ", exterior) + next + strings.Repeat(" ", interior) + next + strings.Repeat(" ", exterior)
		}
		ret = append(ret, line)
		if interior == 0 {
			interior = 1
		} else {
			interior += 2
		}
		exterior -= 1
	}

	for i := len(ret) - 2; i >= 0; i-- {
		ret = append(ret, ret[i])
	}

	return strings.Join(ret, "\n"), nil
}
