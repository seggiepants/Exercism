// Exercise: Reverse String

// Return a reversed version of the given string
// @param String original: The string to process
// @returns String: original string but in reverse order.
String reverse(String original) {
  return original.split('').reversed.join('');
}
