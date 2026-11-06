// Exercise: Collatz Conjecture

class CollatzConjecture {
  // Put your code here
  
  // Find out how many steps it takes for a number to reach 1 using the 
  // Collatz Conjecture.
  // @param int value: The number to check.
  // @returns int: The number of steps the conjecture took to reach 1.
  // @raises ArgumentError: When value parameter is not a positive non-zero integer.
  int steps(int value) {
    int stepCount = 0;
    if (value <= 0) {
      throw new ArgumentError('Only positive integers are allowed');
    }

    while (value != 1) {
      stepCount++;
      if (value % 2 == 0) {
        // Even 
        value = value ~/ 2;
      } else {
        // Odd
        value = (value * 3) + 1;
      }
    }

    return stepCount;
  }

}
