// Variable Length Quantity - Encode a uint32 in a variable number of bytes and decode it too.
package variablelengthquantity

import "errors"

const bit8 byte = 0b10000000
const mask7bits byte = 0b01111111

// Encode a set of integers as variable length integers that
// only take as many bytes as required (1 to 4 since uint32 is max)
// @param intput: slice of uint32 with the values to encode.
// @returns: slice of byte with the encoded values
func EncodeVarint(input []uint32) []byte {
	values := make([]byte, 0)
	current := make([]uint32, 0)

	for _, number := range input {
		// Break it up into 7 bit sections.
		if number == 0 {
			current = append(current, 0)
		} else {
			for number != 0 {
				sevenBits := number & uint32(mask7bits)
				number = number >> 7
				current = append(current, sevenBits)

			}
		}

		// Process in reverse order
		for i := len(current) - 1; i >= 0; i-- {
			value := current[i]
			if i > 0 {
				value = uint32(bit8) | value
			}
			values = append(values, byte(value)) // all but last get bit 8 set.

		}
		current = current[:0]
	}
	return values

}

// Decode a slice of variable length encoded uint32 values
// @param input: slice of byte with the encoded data.
// @returns: slice of uint32 with the decoded values if things went well.
// @raises: Error if the decoding did not stop in a good state
func DecodeVarint(input []byte) ([]uint32, error) {
	values := make([]uint32, 0)
	flag := false
	var value uint32 = 0
	var number byte = 0

	for _, number = range input {
		flag = (number & bit8) == 0              // Stop decoding current number when bit 8 is set.
		value = value << 7                       // Move current number over to make room.
		value = value | uint32(number&mask7bits) // add on the next 7 bits.

		if flag {
			// Reset for next number.
			values = append(values, value)
			value = 0
		}
	}

	if !flag {
		// If we didn't end with the flag set there should have been more data.
		return nil, errors.New("More data expected.")
	}

	return values, nil
}
