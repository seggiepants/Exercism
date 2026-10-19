// Exercise: Bob
class Bob {

  // Get the response to your message from a simulated Teenager 'Bob'
  // @param String message: The message you say to 'Bob'.
  // @returns String: The teenager's response.
  String response(String message) {
    message = message.trim();
    String upperCase = message.toUpperCase();
    bool isQuestion = message.endsWith('?');
    RegExp checkForLetters = RegExp('[A-Za-z]+');
    bool yelling = (upperCase == message) && checkForLetters.hasMatch(message);

    if (message.length == 0) {
      return 'Fine. Be that way!';
    }
    if (isQuestion && !yelling ) {
      return 'Sure.';
    }
    if (isQuestion && yelling ) {
      return 'Calm down, I know what I\'m doing!';
    }
    if (yelling) {
      return 'Whoa, chill out!';
    }

    return 'Whatever.';
  }
}
