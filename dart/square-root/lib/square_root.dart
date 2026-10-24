// Exercise - Square Root (integer)

// Why yes I did get this from Wikipedia. Integer square root by binary search.
class SquareRoot {
  // your code here...

  // Integer square root
  // @param int number: The number to get the square root of.
  // @returns integer square root of number.
  // @raises ArgumentExceptionError if number < 0.
  int squareRoot(int number) {
    if (number < 0) {
      throw new ArgumentError("Number must be positive");
    }
    int lowerBound = 0;           // lower bound of the square root
    int upperBound = number + 1;  // upper bound of the square root

    while (lowerBound != upperBound - 1) {
        int middle = (lowerBound + upperBound) ~/ 2;  // midpoint to test
        if (middle * middle <= number) {
            lowerBound = middle;
        }
        else {
            upperBound = middle;
        }
    }

    return lowerBound;
  }
}
