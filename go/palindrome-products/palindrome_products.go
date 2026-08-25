// Palindrom Products exercise
package palindromeproducts

import (
	"errors"
	"strconv"
)

// Define Product type here.
type Product struct {
	Product        int
	Factorizations [][2]int
}

// Create a new Product struct with default values
// @returns: Product struct initialized with defaults (Product 0, empty Factorizations)
func NewProduct() Product {
	return Product{Product: 0, Factorizations: [][2]int{}}
}

// Find the palindrome products between fmin and fmax, return the smallest and largest
// @param fmin: The start of the number range
// @param fmax: The end of the number range (inclusize)
// @returns: First Product is minimum Palindrome product, second is the Maximum. Will return Empty/New Products if none found.
// @raises: Error if fmin > fmax
func Products(fmin, fmax int) (Product, Product, error) {

	if fmin > fmax {
		return NewProduct(), NewProduct(), errors.New("min must be <= max")
	}
	candidates := make(map[int][][2]int, 0)
	for j := fmin; j <= fmax; j++ {
		for i := j; i <= fmax; i++ {
			product := i * j
			if IsPalindrome(product) {
				factors, ok := candidates[product]
				if !ok {
					factors = make([][2]int, 0)
				}
				found := false
				for _, factor := range factors {
					if factor[0] == j && factor[i] == j {
						found = true
						break
					}
				}
				if !found {
					factors = append(factors, [2]int{j, i})
				}
				candidates[product] = factors
			}
		}
	}
	if len(candidates) == 0 {
		return NewProduct(), NewProduct(), nil
	}

	first := true
	var min Product = NewProduct()
	var max Product = NewProduct()
	for key, value := range candidates {
		if first || key < min.Product {
			min.Product = key
			min.Factorizations = value
		}

		if first || key > max.Product {
			max.Product = key
			max.Factorizations = value
		}

		first = false
	}
	return min, max, nil
}

// Check if a number is a Palindrome
// @param num: The number to check
// @returns: True if a palindrome (written the same forward and reversed)
func IsPalindrome(num int) bool {
	value := []rune(strconv.Itoa(num))
	begin := 0
	end := len(value) - 1
	for begin < end {
		if value[begin] != value[end] {
			return false
		}
		begin++
		end--
	}
	return true
}
