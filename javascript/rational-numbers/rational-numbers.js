//
// This is only a SKELETON file for the 'Rational Numbers' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Rational {
  constructor(numerator, denominator) {
    this.numerator = numerator
    this.denominator = denominator
  }

  add(other) {
    return new Rational(
      (this.numerator * other.denominator) + (this.denominator * other.numerator),
      (this.denominator * other.denominator)
    ).reduce()

  }

  sub(other) {
    return new Rational(
      (this.numerator * other.denominator) - (this.denominator * other.numerator),
      (this.denominator * other.denominator)
    ).reduce()
  }

  mul(other) {
    return new Rational(
      (this.numerator * other.numerator),
      (this.denominator * other.denominator)
    ).reduce()
  }

  div(other) {
    return new Rational(
      (this.numerator * other.denominator),
      (this.denominator * other.numerator)
    ).reduce()

  }

  abs() {
    return new Rational(
      Math.abs(this.numerator),
      Math.abs(this.denominator)).reduce()
  }

  exprational(n) {
    if (n < 0)
    {
      let m = Math.abs(n)
      return new Rational(
      Math.pow(this.denominator, m),
      Math.pow(this.numerator, m)).reduce()  
    }
    return new Rational(
      Math.pow(this.numerator, n),
      Math.pow(this.denominator, n)).reduce()
    
  }

  expreal(n) {
    return Math.pow(Math.pow(n, 1/this.denominator), this.numerator)
  }

  reduce() {
    if ((this.numerator < 0 && this.denominator < 0) || (this.numerator >= 0 && this.denominator < 0))
    {
      this.numerator *= -1
      this.denominator *= - 1
    }

    let common = gcd(Math.abs(this.numerator), Math.abs(this.denominator))
    this.numerator /= common
    this.denominator /= common
    return this
  }
}

function gcd(a, b)
{
  let smaller = Math.min(a, b)
  let larger = Math.max(a, b)
  
  if (smaller === 0)
    return larger

  let remainder = larger % smaller

  while (remainder !== 0)
  {
    larger = smaller
    smaller = remainder 
    remainder = larger % smaller
  }
  return smaller
}
