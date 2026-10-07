import 'dart:math';

// Contains code for encoding and decoding text using the Atbash Cipher
class AtbashCipher {
  // Put your code here

  // Translates a string of text reversing a-z and letting 0-9 pass through everthing.
  // You can call translate to encode or decode.
  // else is excluded.
  // @param String text: The text to translate.
  // @returns: text with 0-9 as-is a-z swapped in order, and everything else excluded.
  String translate(String text) {
    int a = "a".codeUnitAt(0);
    int z = "z".codeUnitAt(0);
    int zero = "0".codeUnitAt(0);
    int nine = "9".codeUnitAt(0);
    StringBuffer buffer = StringBuffer();
    text.toLowerCase().runes.forEach((int rune) {
      if (rune >= a && rune <= z) {
        buffer.write(String.fromCharCode(a + z - rune));
      } else if (rune >= zero && rune <= nine) {
        buffer.write(String.fromCharCode(rune));
      }
    });
    return buffer.toString();
  }

  // Translate the string and chunk it out to block size with spaces between blocks
  // @param String text: The text to transform.
  // @param int blockSize = 5: The number of characters in a block - defaults to 5.
  // @returns: The encoded and chunked text.
  String encode(String text, [int blockSize = 5]) {
    String translated = translate(text);
    StringBuffer chunked = StringBuffer();
    for(int i = 0; i < translated.length; i += blockSize) {
      if (i > 0) {
        chunked.write(" ");
      }
      chunked.write(translated.substring(i, min(translated.length, i + blockSize)));
    }
    return chunked.toString();
  }

  // Translate an encoded string back to text.
  // @param String text: The text to decode.
  // @returns: The decoded text.
  String decode(String text) {
    return translate(text);
  }
}
