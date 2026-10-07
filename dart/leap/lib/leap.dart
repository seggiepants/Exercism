// Leap Year Exercise

// Container class for the Leap Year code.
class Leap {
  // Put your code here

  // Detect if a given year is a leap year. A leap year if divisible by 4. 
  // Unless it is divisible by 100, then it is only leap year if it is also 
  //divisible by 400.
  // @param int year: The year to check.
  // @returns bool: True if a leap year.
  bool leapYear(int year) {
    if (year % 4 == 0) {
      if (year % 100 == 0) {
        return year % 400 == 0;
      }
      return true;
    }
    return false;
  }
}
