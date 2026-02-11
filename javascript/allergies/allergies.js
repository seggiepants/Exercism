//
// This is only a SKELETON file for the 'Allergies' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Allergies {
  constructor(value) {
    this.allergens = ['eggs', 'peanuts', 'shellfish', 'strawberries', 'tomatoes', 'chocolate', 'pollen', 'cats']
    this.score = value
  }

  list() {
    return this.allergens.filter((_, index) => Math.pow(2, index) & this.score)
  }

  allergicTo(allergen) {
    let index = this.allergens.indexOf(allergen.toLowerCase().trim())
    if (index < 0)
      return false
    return Boolean(Math.pow(2, index) & this.score)
  }
}
