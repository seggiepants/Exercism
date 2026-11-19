// Exercise: Pangram

class Pangram {
  // Put your code here

  // Check if a message is a pangram / uses all letters a-z
  // @param String message: The text to check if it is a pangram or not.
  // @returns bool: True if a pangram.
  bool isPangram(String message) {
    Map<String, int> lookup = {
      "a": 0, "b": 0, "c": 0, "d": 0, "e": 0, "f": 0, "g": 0, "h": 0,
      "i": 0, "j": 0, "k": 0, "l": 0, "m": 0, "n": 0, "o": 0, "p": 0,
      "q": 0, "r": 0, "s": 0, "t": 0, "u": 0, "v": 0, "w": 0, "x": 0, 
      "y": 0, "z": 0,
    };
    for(String letter in message.toLowerCase().split("")) {
      if (lookup.containsKey(letter)) {
        lookup[letter] = lookup[letter]! + 1;
      }
    }

    return !lookup.containsValue(0);
  }
}
