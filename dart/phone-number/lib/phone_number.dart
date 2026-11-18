// Exercise: Phone Number
class PhoneNumber {
  // Put your code here

  // Clean a phone number input string of unnecessary data.
  // A great example of clean code ruined by requiring a large number of pedantic error messages
  // that could have been handled generally.
  // @param String input: The phone number to clean up.
  // @returns String: The phone number with excess junk removed.
  // @raises FormatException: If we find letters, unexpected punctuation, too many/few digits, 
  // area code/exchange starting with 0 or 1 or generic not a phone number error that would have 
  // handled all of the above.
  String clean(String input) {
    RegExp letters = RegExp(r'[a-zA-Z]');
    int letterCount = letters.allMatches(input, 0).length;
    if (letterCount > 0) {
      throw FormatException('letters not permitted');
    }
    RegExp punctuation = RegExp(r'[^ a-zA-Z0-9\+\.\(\)\-]');
    int punctuationCount = punctuation.allMatches(input, 0).length;
    if (punctuationCount > 0) {
      throw FormatException('punctuations not permitted');
    }
    RegExp digits = RegExp(r'\d');
    Iterable<RegExpMatch> digitsFound = digits.allMatches(input, 0);
    int digitCount = digitsFound.length;
    if (digitCount < 10) {
      throw FormatException('must not be fewer than 10 digits');
    }
    if (digitCount > 11) {
      throw FormatException('must not be greater than 11 digits');
    }
    if (digitCount == 11) {
      if (digitsFound.first.input.substring(digitsFound.first.start, digitsFound.first.end) != "1") {
       throw FormatException('11 digits must start with 1');
      }
    } 
    RegExp exp = RegExp(r'(\+1|1)?\s*\(?(?<area>\d{3})\)?[\.\-\s]*(?<exchange>\d{3})[\.\-\s]*(?<subscriber>\d{4})');    
    RegExpMatch? match = exp.firstMatch(input);
    if (match == null) {
      throw FormatException('Not a phone number');
    }
    String area = match.namedGroup('area')!;
    if (area.startsWith("0")) {
      throw FormatException('area code cannot start with zero');
    }
    if (area.startsWith("1")) {
      throw FormatException('area code cannot start with one');
    }
    String exchange = match.namedGroup('exchange')!;
    if (exchange.startsWith("0")) {
      throw FormatException('exchange code cannot start with zero');
    }
    if (exchange.startsWith("1")) {
      throw FormatException('exchange code cannot start with one');
    }
    return area +
          exchange +
          match.namedGroup('subscriber')!;
  }
}
