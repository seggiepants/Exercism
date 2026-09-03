package zebra

// The Dreaded Zebra Puzzle exercise and the final exercise on this track (until they add more/break things).

import (
	"fmt"
	"slices"
)

type Solution struct {
	DrinksWater string
	OwnsZebra   string
}

type Frame struct {
	House int
	Key   string
}

type Row map[string]string

type PuzzleInfo struct {
	data       []Row
	undoStack  []Frame
	categories map[string][]string
}

// To string function for the puzzle. Useful for debugging.
func (info PuzzleInfo) ToString() string {
	var msg string = ""
	for i := 0; i < len(info.data); i++ {
		msg += fmt.Sprintf("%02d: %s, %s, %s, %s, %s\n", i+1, info.data[i]["color"], info.data[i]["nationality"], info.data[i]["drink"], info.data[i]["hobby"], info.data[i]["pet"])
	}
	return msg
}

// Figure out who owns the zebra and who drinks water in the Zebra Puzzle. Solving for constraints.
// @returns: Solution struct with the two bits of information populated.
func SolvePuzzle() Solution {
	var data []Row = []Row{{"color": "", "nationality": "", "drink": "", "pet": "", "hobby": ""},
		{"color": "", "nationality": "", "drink": "", "pet": "", "hobby": ""},
		{"color": "", "nationality": "", "drink": "", "pet": "", "hobby": ""},
		{"color": "", "nationality": "", "drink": "", "pet": "", "hobby": ""},
		{"color": "", "nationality": "", "drink": "", "pet": "", "hobby": ""},
	}

	categories := map[string][]string{
		"color":       {"ivory", "yellow", "blue", "red", "green"},
		"nationality": {"Englishman", "Japanese", "Spainiard", "Norwegian", "Ukranian"},
		"drink":       {"water", "tea", "milk", "coffee", "orange juice"},
		"pet":         {"dog", "zebra", "fox", "horse", "snail"},
		"hobby":       {"dancing", "painting", "reading", "football", "chess"},
	}
	info := PuzzleInfo{
		data:       data,
		undoStack:  make([]Frame, 0),
		categories: categories,
	}

	// populate the givens
	data[0]["nationality"] = "Norwegian" // The Norwegian lives in the first house.
	data[1]["color"] = "blue"            // The Norwegian lives next to the blue house
	data[2]["drink"] = "milk"            // The middle house drinks milk
	if !FillPuzzle(&info) {
		fmt.Print("Fill failed\n")
		fmt.Print(info.ToString())
	}

	return Solution{DrinksWater: WaterDrinker(info.data), OwnsZebra: ZebraOwner(info.data)}
}

// Return absolute value of an integer -- shouldn't this be in the common libraries?
// @param a: The value to return as positive.
// @returns: a or -a if a is negative.
func Abs(a int) int {
	if a < 0 {
		return a * -1
	}
	return a
}

// Populate the puzzle
// @param info: The puzzle solving data.
// @returns: True if filled successfully.
func FillPuzzle(info *PuzzleInfo) bool {
	for house, row := range info.data {
		for category, list := range info.categories {
			if row[category] == "" {
				var used []string = make([]string, 0)
				for i := 0; i < len(info.data); i++ {
					value, ok := info.data[i][category]
					if ok && value != "" {
						used = append(used, value)
					}
				}
				var free []string = make([]string, 0)
				for i := 0; i < len(list); i++ {
					if !slices.Contains(used, list[i]) {
						free = append(free, list[i])
					}
				}
				if len(free) > 0 {
					for _, attempt := range free {
						info.data[house][category] = attempt
						info.undoStack = append(info.undoStack, Frame{house, category})
						stackPointer := len(info.undoStack)
						success := Populate(info)
						if !success {
							Undo(info, stackPointer)
						} else {
							success = FillPuzzle(info)
							if !success {
								Undo(info, stackPointer)
							} else {
								break
							}
						}
					}
				}
			}
		}
		color := info.data[house]["color"]
		nationality := info.data[house]["nationality"]
		drink := info.data[house]["drink"]
		hobby := info.data[house]["hobby"]
		pet := info.data[house]["pet"]

		if color == "" || nationality == "" || drink == "" || hobby == "" || pet == "" {
			return false
		}
		if IsFull(info.data) {
			return true
		}
	}
	return Populate(info) && IsFull(info.data)
}

// Find the house that has a key with the given value
// @param data: The puzzle data.
// @param key: The key/column to look at
// @param value: The expected value.
// @returns: -1 if not found, index of row it was on otherwise.
func Find(data []Row, key string, value string) int {
	for index, row := range data {
		lookup, ok := row[key]
		if ok && value == lookup {
			return index
		}
	}
	return -1
}

// Check if the puzzle if full
// @param data: The puzzle data
// @returns: True if full
func IsFull(data []Row) bool {
	for house := 0; house < len(data); house++ {
		if data[house]["color"] == "" ||
			data[house]["nationality"] == "" ||
			data[house]["drink"] == "" ||
			data[house]["pet"] == "" ||
			data[house]["hobby"] == "" {
			return false
		}
	}
	return true
}

// Run through all the rules (except the givens -- can be filled in without futher data)
// if a rule fails the tests return false, if no errors found return true.
// @param info: Information of Puzzle solving state
// @returns: True if no errors.
func Populate(info *PuzzleInfo) bool {
	// 1 There are five houses
	if len(info.data) != 5 {
		return false
	}

	// 2 Englishman lives in the red house
	if !PopulatePair(info, "nationality", "Englishman", "color", "red") {
		return false
	}

	// 3 Spaniard owns a dog
	if !PopulatePair(info, "nationality", "Spainiard", "pet", "dog") {
		return false
	}

	// 4 Green house drinks coffee
	if !PopulatePair(info, "color", "green", "drink", "coffee") {
		return false
	}

	// 5 Ukranian drinks tea
	if !PopulatePair(info, "nationality", "Ukranian", "drink", "tea") {
		return false
	}

	// 6 - Green house right of Ivory house
	//if !PopulatePairRight(info, "color", "green", "color", "ivory") {
	if !PopulatePairRight(info, "color", "ivory", "color", "green") {
		return false
	}

	// 7 Snail owner goes dancing
	if !PopulatePair(info, "pet", "snail", "hobby", "dancing") {
		return false
	}

	// 8 Yellow house likes painting
	if !PopulatePair(info, "color", "yellow", "hobby", "painting") {
		return false
	}

	// 9 Middle house drinks milk is a given

	// 10 1st house is Norwegian is a given

	// 11 - Reading next to fox
	if !PopulatePairNeighbor(info, "hobby", "reading", "pet", "fox") {
		return false
	}

	// 12 - Painter next to horse
	if !PopulatePairNeighbor(info, "hobby", "painting", "pet", "horse") {
		return false
	}

	// 13 Football drinks orange juice
	if !PopulatePair(info, "hobby", "football", "drink", "orange juice") {
		return false
	}

	// 14 Japanese plays chess
	if !PopulatePair(info, "nationality", "Japanese", "hobby", "chess") {
		return false
	}

	// 15 is also a given Norwegian (1st house) is next to blue house where 2 is only neighbor

	return true
}

// Populate if house[key] === value then house[otherKey] = otherValue
// @param info: Information of Puzzle solving state
// @param key1: First key
// @param value1: First value forms key/value pair with key1
// @param key2: Second key
// @param value2: Second value forms key/value pair with key2
// @returns: flase if a contradition was found. True if you can't prove it is false
func PopulatePair(info *PuzzleInfo, key1 string, value1 string, key2 string, value2 string) bool {
	index := Find(info.data, key1, value1)
	if index >= 0 {
		if info.data[index][key2] == "" {
			info.undoStack = append(info.undoStack, Frame{index, key2})
			info.data[index][key2] = value2
			return true
		} else if info.data[index][key2] != value2 {
			return false
		}
	}

	index = Find(info.data, key2, value2)
	if index >= 0 {
		if info.data[index][key1] == "" {
			info.undoStack = append(info.undoStack, Frame{index, key1})
			info.data[index][key1] = value1
			return true
		} else if info.data[index][key1] != value1 {
			return false
		}
	}
	return true
}

// Populate if house[key] == value then houseToRight[otherKey] = otherValue
// @param info: Current puzzle solving data.
// @param key1: The first key of key/value pair to look for
// @param value1: The first value of key/value pair to look for
// @param key2: The second key of key/value pair to look for
// @param value2: The second value of key/value pair to look for
// @returns true if populated sucessfully.
func PopulatePairRight(info *PuzzleInfo, key1 string, value1 string, key2 string, value2 string) bool {
	index1 := Find(info.data, key1, value1)
	index2 := Find(info.data, key2, value2)

	// if you have both they had better match
	if index1 >= 0 && index2 >= 0 && (index1+1) != index2 {
		return false
	}

	if index1 >= 0 && index1 <= len(info.data)-2 {
		value := info.data[index1+1][key2]
		if value == "" {
			info.undoStack = append(info.undoStack, Frame{index1 + 1, key2})
			info.data[index1+1][key2] = value2
			return true
		} else if value != value2 {
			return false
		}
	}

	if index2 >= 1 && index2 < len(info.data) {
		value := info.data[index2-1][key1]
		if value == "" {
			info.undoStack = append(info.undoStack, Frame{index2 - 1, key1})
			info.data[index2-1][key1] = value1
			return true
		} else if value != value1 {
			return false
		}
	}
	return true
}

// Populate if house[key] === value then houseToLeftOrRight[otherKey] = otherValue
// @param info: Current puzzle solving data.
// @param key1: The first key of key/value pair to look for
// @param value1: The first value of key/value pair to look for
// @param key2: The second key of key/value pair to look for
// @param value2: The second value of key/value pair to look for
// @returns true if populated sucessfully.
func PopulatePairNeighbor(info *PuzzleInfo, key1 string, value1 string, key2 string, value2 string) bool {
	index1 := Find(info.data, key1, value1)
	index2 := Find(info.data, key2, value2)
	// Both populated but more than one spot apart is an error
	if index1 >= 0 && index2 >= 0 {
		if (info.data[index1][key1] == value1 && info.data[index2][key2] == value2) ||
			(info.data[index2][key1] == value1 && info.data[index1][key2] == value2) {
			return true
		}

		if Abs(index2-index1) > 1 {
			return false
		}
	}

	if !PopulatePairNeighbor_Step(info, key1, value1, key2, value2) {
		return false
	}
	if !PopulatePairNeighbor_Step(info, key2, value2, key1, value1) {
		return false
	}
	return true
}

// These searches are lengthy to write so I broke this one in two and
// just call it with both possible combinations (should be redundant, really).
// @param info: Current puzzle solving data.
// @param key1: The first key of key/value pair to look for
// @param value1: The first value of key/value pair to look for
// @param key2: The second key of key/value pair to look for
// @param value2: The second value of key/value pair to look for
// @returns true if populated sucessfully.
func PopulatePairNeighbor_Step(info *PuzzleInfo, key1 string, value1 string, key2 string, value2 string) bool {
	index := Find(info.data, key1, value1)
	if index >= 0 {
		sides := make([]Frame, 0)
		if index-1 >= 0 {
			sides = append(sides, Frame{index - 1, info.data[index-1][key2]})
		}
		if index+1 < len(info.data) {
			sides = append(sides, Frame{index + 1, info.data[index+1][key2]})
		}

		if len(sides) == 1 && sides[0].Key != value2 && sides[0].Key != "" {
			return false
		} else if len(sides) == 1 && sides[0].Key == value2 {
			return true
		} else if len(sides) == 1 && sides[0].Key == "" {
			// only one and it is empty, fill it
			info.undoStack = append(info.undoStack, Frame{sides[0].House, key2})
			info.data[sides[0].House][key2] = value2
			return true
		} else if len(sides) == 2 && sides[0].Key != "" && sides[1].Key != "" && sides[0].Key != value2 && sides[1].Key != value2 {
			// both sides not target
			return false
		} else if len(sides) == 2 && (sides[0].Key == value2 || sides[1].Key == value2) {
			// one side is target
			return true
		} else if len(sides) == 2 && sides[0].Key == "" && sides[1].Key != "" && sides[1].Key != value2 {
			// left empty, right not empty not target
			info.undoStack = append(info.undoStack, Frame{sides[0].House, key2})
			info.data[sides[0].House][key2] = value2
		} else if len(sides) == 2 && sides[0].Key != "" && sides[1].Key == "" && sides[0].Key != value2 {
			// right empty, let not empty not target
			info.undoStack = append(info.undoStack, Frame{sides[1].House, key2})
			info.data[sides[1].House][key2] = value2
		}
	}
	return true
}

// Undo moves in the data up to a pointer in the stack frame
// @param info: Puzzle solving state information
// @param stackPointer: Where to stop in the Undo operation.
func Undo(info *PuzzleInfo, stackPointer int) {
	for len(info.undoStack) > 0 && len(info.undoStack) >= stackPointer {
		var frame Frame = info.undoStack[len(info.undoStack)-1]
		info.undoStack = slices.Delete(info.undoStack, len(info.undoStack)-1, len(info.undoStack))
		//info.undoStack = info.undoStack[:len(info.undoStack)-1]
		info.data[frame.House][frame.Key] = ""
	}
}

// From a filled puzzle find the nationality of the person that drinks water.
// @param data: Slice of rows in the puzzle.
// @returns: Empty string if not found otherwise the nationality of the water drinker
func WaterDrinker(data []Row) string {
	index := Find(data, "drink", "water")
	if index < 0 {
		return ""
	}
	return data[index]["nationality"]
}

// From a filled puzzle find the nationality of the person that has a zebra as a pet.
// @param data: Slice of rows in the puzzle.
// @returns: Empty string if not found otherwise the nationality of the zebra owner
func ZebraOwner(data []Row) string {
	index := Find(data, "pet", "zebra")
	if index < 0 {
		return ""
	}
	return data[index]["nationality"]
}
