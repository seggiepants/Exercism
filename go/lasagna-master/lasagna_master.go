package lasagnamaster

// TODO: define the 'PreparationTime()' function
func PreparationTime(layers []string, timePerLayer int) int {
	if timePerLayer == 0 {
		timePerLayer = 2
	}

	return len(layers) * timePerLayer

}

// TODO: define the 'Quantities()' function
func Quantities(layers []string) (int, float64) {
	noodleLayers := 0
	sauceLayers := 0
	for index := 0; index < len(layers); index++ {
		if layers[index] == "noodles" {
			noodleLayers++
		}
		if layers[index] == "sauce" {
			sauceLayers++
		}
	}
	return 50 * noodleLayers, 0.2 * float64(sauceLayers)
}

// TODO: define the 'AddSecretIngredient()' function
func AddSecretIngredient(friendIngredients, ingredients []string) {
	ingredients[len(ingredients)-1] = friendIngredients[len(friendIngredients)-1]
}

// TODO: define the 'ScaleRecipe()' function
func ScaleRecipe(quantities []float64, numServings int) []float64 {
	var scaled []float64
	scaleFactor := float64(numServings) / 2
	for index := 0; index < len(quantities); index++ {
		scaled = append(scaled, quantities[index]*scaleFactor)
	}
	return scaled
}

// Your first steps could be to read through the tasks, and create
// these functions with their correct parameter lists and return types.
// The function body only needs to contain `panic("")`.
//
// This will make the tests compile, but they will fail.
// You can then implement the function logic one by one and see
// an increasing number of tests passing as you implement more
// functionality.
