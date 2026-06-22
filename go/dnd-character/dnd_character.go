package dndcharacter

import (
	"math"
	"math/rand"
)

type Character struct {
	Strength     int
	Dexterity    int
	Constitution int
	Intelligence int
	Wisdom       int
	Charisma     int
	Hitpoints    int
}

// Modifier calculates the constitution modifier for hit points
// @param score: Constitution value
// @returns: Constitution modifier. Hitpoints will be this + 10.
func Modifier(score int) int {
	return int(math.Floor(float64(score-10) / 2.0))
}

// Ability uses randomness to generate the score for an ability
// Roll four die and return the sum minus the minimum value.
// @returns: Ability score a number between 3 and 18.
func Ability() int {
	const DIE_COUNT = 6
	const NUMBER_COUNT = 4
	min := 0
	total := 0
	for i := 0; i < NUMBER_COUNT; i++ {
		value := rand.Intn(DIE_COUNT) + 1
		if i == 0 || i < min {
			min = value
		}
		total += value
	}
	total -= min
	return total
}

// Creates a new DND Character with random scores for abilities and hp calculated
// @returns: A new DND character struct filled with stat abilities.
func GenerateCharacter() Character {
	char := Character{
		Strength:     Ability(),
		Dexterity:    Ability(),
		Constitution: Ability(),
		Intelligence: Ability(),
		Wisdom:       Ability(),
		Charisma:     Ability(),
	}
	char.Hitpoints = Modifier(char.Constitution) + 10
	return char
}
