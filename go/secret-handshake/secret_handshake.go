// Secret Handshake exercise - teaches accessing bit flags.
package secrethandshake

import "slices"

const wink = 0b00001
const double_blink = 0b00010
const close_eyes = 0b00100
const jump = 0b01000
const reverse = 0b10000

// Give an secret handshake bit code determine what operations to apply and their ordering (forward/reverse)
// @param code: unsigned integer with bit flags (beyond bit 5 is not used)
// @returns: slice of string with secret handshake operations (if any -- empty slice if none)
func Handshake(code uint) []string {
	var ret []string = []string{}
	if code&wink != 0 {
		ret = append(ret, "wink")
	}
	if code&double_blink != 0 {
		ret = append(ret, "double blink")
	}
	if code&close_eyes != 0 {
		ret = append(ret, "close your eyes")
	}

	if code&jump != 0 {
		ret = append(ret, "jump")
	}

	if code&reverse != 0 {
		slices.Reverse(ret)
	}

	return ret
}
