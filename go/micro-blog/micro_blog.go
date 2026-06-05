// Micro Blog exercise.
package microblog

// Truncate a UTF-8 string to five characters (or less)
// @param phrase: The string to truncate
// @returns: New string containing up to the first five characters of the given phrase
func Truncate(phrase string) string {
	const MAX_COUNT int = 5
	runes := []rune(phrase)
	count_runes := min(MAX_COUNT, len(runes))
	return string(runes[:count_runes])
}
