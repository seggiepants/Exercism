package dominoes

import "slices"

// Define the Domino type here.
type Domino [2]int

// Return the first number on the domino
func (d Domino) First() int {
	return d[0]
}

// Return the second number on the domino
func (d Domino) Last() int {
	return d[1]
}

// Return a copy of the Domino but reversed
func (d Domino) Reverse() Domino {
	var result Domino
	result[0] = d[1]
	result[1] = d[0]
	return result
}

// Chain the dominoes end to end matching numbers and in a loop matching last
// number of last to first number of first domino
// @param input: Slice of Domino to work with.
// @returns: Slice of domino in chained order.
// @raises: ok set to false if no chain found.
func MakeChain(input []Domino) ([]Domino, bool) {
	if len(input) == 0 {
		return []Domino{}, true
	}

	// Start the chain with the first tile.
	chained := []Domino{input[0]}
	result, ok := Dominoes(chained, input[1:])
	if ok && len(result) != len(input) {
		// If that didn't work, try again with the tile reversed.
		chained = []Domino{input[0].Reverse()}
		result, ok = Dominoes(chained, input[1:])
	}

	if !ok || len(result) != len(input) {
		return []Domino{}, false
	}

	if result[0].First() != result[len(result)-1].Last() {
		return []Domino{}, false
	}
	return result, true
}

// Chain dominoes recursively
// @param chained: The dominos that have been chained together so far.
// @param remaining: The dominos left to add to the chain.
// @returns: Slice of dominos in chain order.
// @raises: ok = false if no valid circular chain found.
func Dominoes(chained []Domino, remaining []Domino) ([]Domino, bool) {
	if len(remaining) == 0 {
		return chained, true
	}

	first := chained[0].First()
	last := chained[len(chained)-1].Last()

	// last one?
	if len(remaining) == 1 {
		lastTile := remaining[0]
		if (lastTile.Last() == first) && (lastTile.First() == last) {
			// match chain ends when normal
			//return {chained..., lastTile}, true
			return append(chained, lastTile), true
		} else if lastTile.First() == first && lastTile.Last() == last {
			// match chain ends when reversed
			return append(chained, lastTile.Reverse()), true
		} else {
			return chained, false
		}
	}

	for index := 0; index < len(remaining); index++ {
		current := remaining[index]
		currentReverse := current.Reverse()

		if current.Last() == first {
			// match first tile
			result, ok := Dominoes(append([]Domino{current}, chained...), slices.Delete(slices.Clone(remaining), index, index+1))
			if ok && len(result) == len(chained)+len(remaining) {
				return result, true
			}
		} else if currentReverse.Last() == first {
			// match first tile reversed
			result, ok := Dominoes(append([]Domino{currentReverse}, chained...), slices.Delete(slices.Clone(remaining), index, index+1))
			if ok && len(result) == len(chained)+len(remaining) {
				return result, true
			}
		} else if current.First() == last {
			// match last tile
			result, ok := Dominoes(append(chained, current), slices.Delete(slices.Clone(remaining), index, index+1))
			if ok && len(result) == len(chained)+len(remaining) {
				return result, true
			}
		} else if currentReverse.First() == last {
			// match last tile
			result, ok := Dominoes(append(chained, currentReverse), slices.Delete(slices.Clone(remaining), index, index+1))
			if ok && len(result) == len(chained)+len(remaining) {
				return result, true
			}
		}
	}
	return []Domino{}, false // No matches for current chain
}
