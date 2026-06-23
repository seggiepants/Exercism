// exercise: eliuds eggs - count binary digits.
package eliudseggs

// Count the bits in a number
// @param displayValue binary representation of the eggs in their locations converted to decimal
// @returns: count of 1's in the binary.
func EggCount(displayValue int) int {
	var count int = 0
	var currentValue int = displayValue

	for currentValue != 0 {
		if currentValue&0x01 == 1 {
			count++
		}
		currentValue = currentValue >> 1
	}
	return count
}
