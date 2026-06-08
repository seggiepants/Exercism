// Robot naming exercise.
package robotname

import (
	"errors"
	"math/rand/v2"
)

const MAX_NAMES = 26 * 26 * 10 * 10 * 10 // 676,000

var all_robot_names map[string]bool = make(map[string]bool)

// Define the Robot type here.
type Robot struct {
	name string
}

// Return the name of the robot, generating a new name if none has been asssigned yet.
// @returns: The existing/new robot name.
// @raises: Error if there is no more namespace to allocate robot names (676,000 maximum).
func (r *Robot) Name() (string, error) {
	const ALPHABET_LEN = 26
	const DIGIT_LEN = 10
	if r.name == "" {
		if len(all_robot_names) >= MAX_NAMES {
			return "", errors.New("All robot names have been allocated.")
		}
		name_ok := false
		robot_name := ""
		for !name_ok {
			robot_name = ""
			robot_name += string(rune('A' + rand.IntN(ALPHABET_LEN)))
			robot_name += string(rune('A' + rand.IntN(ALPHABET_LEN)))
			robot_name += string(rune('0' + rand.IntN(DIGIT_LEN)))
			robot_name += string(rune('0' + rand.IntN(DIGIT_LEN)))
			robot_name += string(rune('0' + rand.IntN(DIGIT_LEN)))
			_, name_ok = all_robot_names[robot_name]
			name_ok = !name_ok
		}
		all_robot_names[robot_name] = true
		r.name = robot_name
	}
	return r.name, nil
}

// Reset the robot blanking its name. I think that should allow reuse of the now unused name but the tests say otherwise.
func (r *Robot) Reset() {
	r.name = ""
}
