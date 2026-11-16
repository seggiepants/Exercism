// Exercise - Luhn

class Luhn {
  // Put your code here

  // Check if a given string passes the luhn checksum test.
  // @param String digits: The string of digits to check. May only be spaces and digits.
  // @returns bool: True if passes the luhn test.
  bool valid(String digits) {

    // Single digit as string to integer counterpart. 
    // Sorry didn't want to mess with runes.
    Map<String, int> lookup = {
      "0": 0, "1": 1, "2": 2,
      "3": 3, "4": 4, "5": 5,
      "6": 6, "7": 7, "8": 8,
      "9": 9,
    };

    bool double = false; // toggle this as you go.
    int digitCount = 0; // how many digits 1 or less and the test fails.
    int total = 0;  // Total of the digits (with every other from the end doubled and subracted by 9 if doubled value is greater or equal to 10).

    for (int i = digits.length - 1; i >= 0; i--) {  // Iterate end to beginning
      String element = digits.substring(i, i + 1);  // Get the current loop digit/space
      int? number = lookup[element];                // Change it to a digit or null if not in the lookup table.
      if (number != null) {                         // If digit
        if (double) {                               // Do we need to double the number.
          number = number * 2;                      // Double
          if (number > 9) {
            number -= 9;                            // Subtract 9 if too big.
          }
        }
        total += number;                            // Add to the total
        double = !double;                           // Toggle
        digitCount++;                               // Increment number of digits processed.
      } else if (element != " ") {
        return false;                               // If non-digit and not a space fail.
      }
    }

    if (digitCount <= 1) {
      return false;                                 // Too few digits.
    }
    return total % 10 == 0;                         // True if evenly divisible by 10.
  }
}
