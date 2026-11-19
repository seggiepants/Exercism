// Exercise: Triangle
class Triangle {
  // Put your code here

  // A triangle is equliateral if all sides are the same length.
  // @param num a: Length of the first side.
  // @param num b: Length of the second side.
  // @param num c: Length of the third side.
  // @returns bool: True if can form a triangle that is equilateral
  bool equilateral(num a, num b, num c) {
    if (!isTriangle(a, b, c)) {
      return false;
    }
    return a == b && b == c;
  }

  // A triangle is isosceles if at least two sides are the same length.
  // @param num a: Length of the first side.
  // @param num b: Length of the second side.
  // @param num c: Length of the third side.
  // @returns bool: True if can form a triangle that is isosceles
  bool isosceles(num a, num b, num c) {
    if (!isTriangle(a, b, c)) {
      return false;
    }
    return a == b || a == c ||  b == c;
  }

  // A triangle is scalene if all sides are different lengths.
  // @param num a: Length of the first side.
  // @param num b: Length of the second side.
  // @param num c: Length of the third side.
  // @returns bool: True if can form a triangle that is scalene
  bool scalene(num a, num b, num c) {
    if (!isTriangle(a, b, c)) {
      return false;
    }
    return a != b && a != c && b != c;
  }

  // Check if a triangle can be formed from three side lengths.
  // @param num a: Length of the first side.
  // @param num b: Length of the second side.
  // @param num c: Length of the third side.
  // @returns bool: True if can form a triangle, false if any side is 
  // zero length or the sum of any two sides is greater or equal to the 
  // remaining side.
  bool isTriangle(num a, num b, num c)
  {
    if (a == 0 || b == 0 || c == 0) {
      return false;
    }
    if (a + b < c || a + c < b || b + c < a) {
      return false;
    }
    return true;
  }
}
