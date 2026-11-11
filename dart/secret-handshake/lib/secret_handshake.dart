// Exercise - Secret Handshake
class SecretHandshake {
  // Put your code here

  // Create a secret handshake for the given number (used as bit flags)
  // @param int flags: The number to produce a secret handshake from.
  // @returns List<String>: Actions to perform for the secret handshake (order important)
  List<String> commands(int flags) {
    List<String> result = [];
    if (flags & 0x01 != 0) {
      result.add("wink");
    }
    if (flags & 0x02 != 0) {
      result.add("double blink");
    }
    if (flags & 0x04 != 0) {
      result.add("close your eyes");
    }
    if (flags & 0x08 != 0) {
      result.add("jump");
    }
    if (flags & 0x10 != 0) {
      return result.reversed.toList();
    }
    return result;
  }
}
