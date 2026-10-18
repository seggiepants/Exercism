// Word Count Exercise
class WordCount {
  // Put your code here

  // Count the words in a string and return the words and number of appearences as a map.
  // handles single quote followed by more word characters as a contraction, but shouldn't get
  // tripped up on single quoted words.
  // @param String text: The text to count words on.
  // @returns Map<String, int>: A map where the key is the word found and the value is the number
  // of occurences (1+).
  Map<String, int> countWords(String text) {
    Map<String, int> results = Map<String, int>();
    RegExp re = RegExp(r"\w+(\'\w+)?");
    Iterable<RegExpMatch> matches = re.allMatches(text.toLowerCase());
    for (final RegExpMatch m in matches) {
      String key = m.group(0) ?? "";
      results[key] = (results[key] ?? 0) + 1;
    }
    return results;
  }
}
