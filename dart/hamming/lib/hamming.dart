import 'dart:math';
// Exercise - Hamming Distance.
class Hamming {
  // Put your code here

  // Calculate the hamming distance (number of rune differences) between two nucleotide strings.
  // @param String strandA: The first strand to compare.
  // @param String strandB: The second strand to compare.
  // @returns int: The number of differences between the strands.
  // @raises: Argument Error if the strands are not the same length.
  int distance(String strandA, String strandB) {
    if (strandA.length != strandB.length) {
      throw new ArgumentError('strands must be of equal length');
    }
    int result = 0;
    for(int i = 0; i < min<int>(strandA.length, strandB.length); i++) {
      if (strandA.substring(i, i + 1) != strandB.substring(i, i + 1)) {
        result++;
      }
    }
    return result;
  }
}
