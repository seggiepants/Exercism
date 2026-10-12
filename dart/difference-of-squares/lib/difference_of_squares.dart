// Difference of Squares Exercise

// Difference of Squares class with the desired methods for difference of squares.
class DifferenceOfSquares {
  // Put your code here

  // The sum of the numbers 1 to count multiplied by itself.
  // @param int count: The natural number to count nup to.
  // @returns int the square of the sum of the numbers 1 to count.
  int squareOfSum(int count) {
    int result = 0;
    for(int i = 1; i <= count; i++) {
      result += i;
    }
    return result * result;
  }

  // The sum of the numbers 1 to count where each number is squared.
  // @param int count: The natural number to count up to.
  // @returns int: Each number 1 to count squared and added up.
  int sumOfSquares(int count) {
    int result = 0;
    for(int i = 1; i <= count; i++) {
      result += (i * i);
    }
    return result;
  }

  // The difference of the square of sums and sum of squares
  // @param int count: What natural number to count up to.
  // @returns int: The square of the sum of number 1 to count minus the sum of 
  // the numbers 1 to count each squared.  Example: (1 + 2 + 3) ^3 - (1^2 + 2^2 + 3^2)
  int differenceOfSquares(int count) {
    return squareOfSum(count) - sumOfSquares(count);
  }
}
