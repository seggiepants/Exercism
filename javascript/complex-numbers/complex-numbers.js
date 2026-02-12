//
// This is only a SKELETON file for the 'Complex Numbers' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class ComplexNumber {
  constructor(realComponent = 0, imaginaryComponent = 0) {
    this._real = realComponent
    this._imag = imaginaryComponent
  }

  get real() {
    return this._real
  }

  get imag() {
    return this._imag
  }

  add(other) {
    return new ComplexNumber(this._real + other.real, this._imag + other.imag)
  }

  sub(other) {
    return new ComplexNumber(this._real - other.real, this._imag - other.imag)
  }

  div(other) {
    // Can't multiply by reciprocal otherwise one test is off by .00001ish
    let a = ((this._real * other.real)  + (this._imag * other.imag)) / (other.real * other.real + other.imag * other.imag)
    let b = ((this._imag * other.real)  - (this._real * other.imag)) / (other.real * other.real + other.imag * other.imag)
    return new ComplexNumber(a, b)
  }

  mul(other) {
    return new ComplexNumber((this._real * other.real) - (this._imag * other.imag), (this._imag * other.real) + (this._real * other.imag))
  }

  reciprocal() {
    let aSquaredPlusBSquared = (this.real * this.real) + (this.imag * this.imag)
    return new ComplexNumber(this.real / aSquaredPlusBSquared, -1 * this.imag / aSquaredPlusBSquared)
  }

  get abs() {
    return Math.sqrt((this._real * this._real) + (this._imag * this._imag))
  }

  get conj() {
    return new ComplexNumber(this.real, this.imag === 0 ? 0 : -1 * this.imag)
  }

  get exp() {
    let expPart = Math.exp(this._real)
    return new ComplexNumber(expPart * Math.cos(this._imag), expPart * Math.sin(this._imag))
  }
}
