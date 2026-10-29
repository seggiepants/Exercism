// Exercise - Anagram
class Anagram {
  // For a given word, see which candidate words are an anagram of the original word.
  // @param String word: The word to find anagrams of.
  // @param List<String> candidates: A list of words that may or may not be anagrams of the parameter word.
  // @returns List<String>: All words from parameter candidates that are an anagram of parameter word.
  List<String> findAnagrams(String word, List<String>candidates) 
  {
    List<String> anagrams = [];
    word = word.toLowerCase();
    String sorted = sortString(word);

    for(String candidate in candidates) {
      String lower = candidate.toLowerCase();
      // Cannot be anagram of itself.
      if (lower == word) {
        continue;
      }
      if (sorted == sortString(lower)) 
      {
        anagrams.add(candidate);
      }
    }
    return anagrams;
  }

  // Given a string, return the string but with characters in sorted order.
  // @param String text: The string to sort.
  // @returns String: String with the characters in text but in sorted order.
  String sortString(String text) 
  {
    List<String> chars = text.split('');
    chars.sort();
    return chars.join('');
  }
}


