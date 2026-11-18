// Exercise: Acronym

class Acronym {
  // Put your code here

  // Turn a string of words into an acronym. Words are separated by anything that isn't a-z, A-Z, or apostrophe.
  // @param String words: The words to turn into an acronym
  // @returns String: Acronym version of the input (all uppercase).
  String abbreviate(String words) {
    RegExp exp = RegExp("([A-Za-z\\']+)+");
    Iterable<RegExpMatch> matches = exp.allMatches(words);
    String result = "";
    for (final m in matches) {
      result += m.input.substring(m.start, m.start + 1).toUpperCase();
    }
    return result;
  }
}
