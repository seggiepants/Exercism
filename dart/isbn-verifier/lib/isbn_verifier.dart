// Exercise: ISBN Verifier

// Put your code here

// Check if a string is a valid ISBN-10 number. It doesn't check the dashes
// very well you can have all, none, but each dash may be individually present/absent too.
// @param String isbn: Candidate isbn number
// @returns bool: True if a valid ISBN-10 number and false otherwise.
bool isValid(String isbn) {
  RegExp exp = RegExp(r'^(\d-\d{3}-\d{5}-(\d|X)|\d{9}(\d|X))$');
  bool isMatch =  exp.hasMatch(isbn);
  if (!isMatch) 
    return false;

  Runes runes = isbn.runes;
  int zero = "0".runes.first;
  int nine = "9".runes.first;
  int dash = "-".runes.first;
  int x = "X".runes.first;
  int total = 0;  
  int multiplier = 10;

  for(int i = 0; i < runes.length; i++) {
    int ch = runes.elementAt(i);
    if (ch >= zero && ch <= nine) {
      total += (ch - zero) * multiplier;
      multiplier--;
    } else if (ch == x && i == runes.length - 1) {
      total += 10;
      multiplier--;
    } else if (ch != dash) {
      return false;
    }

    if (multiplier < 0) {
      return false; // too big.
    }    
  }
  return total % 11 == 0;
}
