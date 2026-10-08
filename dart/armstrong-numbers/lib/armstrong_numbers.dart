class ArmstrongNumbers {
  // Put your code here

  // Check if a given number is an armstrong number. That is the number
  // is equal to the sum of each digit raised to the power of the number of 
  // digits in the number. 9 = 9^1, but 10 != 1^2 + 0^2
  // Got a bit fancy with the higher order functions .where and .fold
  // @param String input: The input number expressed as a string.
  // @returns bool: True if the number is an Armstrong Number.
  bool isArmstrongNumber(String input) {
    // Remove all of the non-digits from the string.
    int zero = "0".codeUnitAt(0);
    int nine = "9".codeUnitAt(0);
    String digits = input.runes.where((int rune) => rune >= zero && rune <= nine).fold<String>("",
    (accum, elem) => accum + String.fromCharCode(elem));

    BigInt expected = BigInt.parse(digits);
    int power = digits.length;
    BigInt candidate = digits.runes.fold<BigInt>(BigInt.from(0), (accum, elem) => accum + BigPow(elem - zero, power));
    return candidate == expected;
  }

  // Emulate the Pow() function for BigInt numbers.
  // Regular numbers can't contain the values we get to.
  // @param int digit: The digit to raise to a power.
  // @param int power: How many times the number is multiplied by itself.
  // @returns BigInt: The caluclated power value expressed as a BigInt.
  BigInt BigPow(int digit, int power) {
    if (digit == 0) {
      return BigInt.from(0);
    } else if (digit == 1) {
      return BigInt.from(1);
    }
    BigInt result = BigInt.from(1);
    BigInt bigDigit = BigInt.from(digit);
    for(int i = 0; i < power; i++) {
      result *= bigDigit;
    }
    return result;
  }
}
