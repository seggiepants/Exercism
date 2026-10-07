// Egg Counter Exercise.

// Class to hold the Egg Counter count function.
class EggCounter {
  // Your code goes here.

  // Count the number of binary 1's in a integer number. 
  // example 1 = 1 and 0 = 0, but 3 = 2 because 3 = 0b011
  // @param int number: The number to count the 1s in.
  // @returns: The number of 1s in the binary representation of number.
  int count(int number) {
    int result = 0;
    
    while (number != 0) {
      result += number % 2;
      number >>= 1;
    }
    return result;
  }
}
