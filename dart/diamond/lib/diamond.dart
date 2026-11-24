// Exercise: Diamond
class Diamond {
  // Put your code here
  
  // Calcluate a diamond for the given character.
  // @param String char: Must be a single upper case character between A and Z.
  // Build a diamond starting at A and going to that character and back down again.
  // @returns List<String>: A list of strings where each string is a row of output.
  List<String> rows(String char) {
    String alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    List<String> results = [];

    int max = alphabet.indexOf(char);
    if (max < 0) {
      return [];
    }
    for(int i = 0; i <= max; i++) {
      int numSpaces = max == 0 ? 0 : max - i;
      int numInterior = (2 * i ) - 1;
      if (i == 0) {
        results.add(' ' * numSpaces + alphabet[i] + ' ' * numSpaces);
      } else {
        results.add(' ' * numSpaces + alphabet[i] + ' ' * numInterior + alphabet[i] + ' ' * numSpaces);
      }
    }
    for(int i = max - 1; i >= 0; i--) {
      results.add(results[i]);
    }
    return results;
  }
}
