// Allergies example. Really just testing bit flags.
package allergies

// Known allergens. Not the numbers are powers of two so these should be
// bit flags.
var allergens map[string]int = map[string]int{
	"eggs":         1,
	"peanuts":      2,
	"shellfish":    4,
	"strawberries": 8,
	"tomatoes":     16,
	"chocolate":    32,
	"pollen":       64,
	"cats":         128,
}

// Return all of the allergens given by an allergies bitflag
// expresed as an unsigned integer.
// @param allergies: Known allergens someone is allergic to encoded
// as bit flags in an unsigned integer.
// @returns: Slice of strings of allergens the allergies bitflag includes.
func Allergies(allergies uint) []string {
	var ret []string = []string{}

	for key := range allergens {
		if AllergicTo(allergies, key) {
			ret = append(ret, key)
		}
	}

	return ret
}

// Test if an allergies bitflag value is allergic to the given allergen.
// @param allergies: set of bitflags saved as an unsigned integer
// @param allergen: string name of an allergen.
// @returns: True if allergies bitflag contains the bit for the specificed allergen
// also returns false if allergen is not found.
func AllergicTo(allergies uint, allergen string) bool {
	value, ok := allergens[allergen]
	if !ok {
		return false
	}
	return (allergies & uint(value)) != 0
}
