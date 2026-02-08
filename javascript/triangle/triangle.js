//
// This is only a SKELETON file for the 'Triangle' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Triangle {
  constructor(...sides) {
    if (sides.length !== 3)
      throw new Error(`Not a triangle, has ${sides.length} sides instead of 3`)
    let sorted = sides.sort()
    // I though you could do something like [a, b, c] = [1, 2, 3], but I couldn't get it to work and assigned individually.
    this.sideA = sorted[0]
    this.sideB = sorted[1]
    this.hypotenuse = sorted[2]
  }

  isValidTriangle()
  {
    if (this.hypotenuse >= this.sideA + this.sideB)
      return false
    if (this.hypotenuse <= 0 || this.sideA <= 0 || this.sideB <= 0)
      return false
    return true
  }

  get isEquilateral() {
    return this.isValidTriangle() && ((this.sideA === this.sideB) && (this.sideA === this.hypotenuse))
  }

  get isIsosceles() {
    return this.isValidTriangle() && ((this.sideA === this.sideB) || (this.sideA === this.hypotenuse) || (this.sideB === this.hypotenuse))
  }

  get isScalene() {
    return this.isValidTriangle() && ((this.sideA !== this.sideB) && (this.sideA !== this.hypotenuse) && (this.sideB !== this.hypotenuse))
  }
}
