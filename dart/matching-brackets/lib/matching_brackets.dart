// Exercise Matching Brackets
class MatchingBrackets {
  // Put your code here

  // Check to see if a given block of text has balanced {}, [], and () pairings. When correct there should be 
  // a matching end character for each beginning character in the reverse order of the begin character and with no 
  // excess or insufficient pairing characters.
  // @param String text: The text to check for matching braces/parens/brackets
  // @returns bool: True if the text has balanced pairings.
  bool isPaired(String text) {
    List<String> stack = [];
    for(String char in text.split("")) {
      switch (char) {
        case "[":
          stack.add("]");
        case "(":
          stack.add(")");
        case "{":
          stack.add("}");
        case "]":
        case ")":
        case "}":
          if (stack.length == 0 || stack.last != char) {
            return false;
          }
          stack.removeLast();
          break;
      }
    }
    return stack.length == 0;
  }
}
