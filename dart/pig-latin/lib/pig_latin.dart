// Exercise: Pig Latin

// Translate a word or phrase to Pig Latin. Words should be separated by whitespace (not conserved). No punctuation please.
// @param String message: The word or phrase to translate.
// @returns String: Message split into words, the words translated to pig latin, and glued back together with spaces between.
String translate(String message) {
  String result = "";
  for(String word in message.trim().toLowerCase().split(RegExp(r'\s+'))) {
    if (result.length > 0 && word.length > 0) {
      result += " ";
    }
    result += translateWord(word);
  }

  return result;
}

// Translate a single word to pig latin. No puntuation, spaces or digits please just a-z
// @param String word: The word to translate.
// @returns: Word translated to lower case pig latin.
String translateWord(String word) {
  String vowels = "aeiou";
  String result = word.trim().toLowerCase();

  // Rule 1
  if (vowels.indexOf(result[0]) >= 0 || result.startsWith("xr") || result.startsWith("yt")) {
    return result + "ay";
  }

  // Rule 3
  RegExp reQU = RegExp("^[bcdfghjklmnpqrstvwxyz]*qu");
  RegExpMatch? match = reQU.firstMatch(result);
  if (match != null) {
    return result.substring(match.end) + result.substring(match.start, match.end) + "ay";
  }

  // Rule 2
  RegExp reConsonant = RegExp("^y?[bcdfghjklmnpqrstvwxz]*[aeiouy]");
  match = reConsonant.firstMatch(result);
  if (match != null) {
    return result.substring(match.end - 1) + result.substring(match.start, match.end - 1) + "ay";
  }

  return result;
}