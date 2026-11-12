class Isogram {
  // Put your code here

  // Check if a string is an isogram (letters a-z only used up to one time)
  // characters outside of a-z and A-Z will be ignored and the others treated as
  // case insensitive so a and A are the same character.
  // @param String candidate: The string to be tested if it is an isogram
  // @returns bool: True if an isogram
  bool isIsogram(String candidate) {
    RegExp r = RegExp(r'[a-z]');
    Iterable<RegExpMatch> matches = r.allMatches(candidate.toLowerCase(), 0);
    Map<String, int> letters = {};
    for (RegExpMatch match in matches) {
      String matchText = match.input.substring(match.start, match.end);
      int? count = letters[matchText];
      if (count == null) {
        letters[matchText] = 1;
      } else {
        return false;
      }
    }
    return true;
  }
}
