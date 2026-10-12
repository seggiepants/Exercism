// Implement a custom InvalidNucleotideException exception

// Nucleotide Count class container.
class NucleotideCount {
  // Put your code here

  // Count the nucleotides A, C, G, and T in the given Nucleotide strand.
  // @param String strang: Nucleotide strand to count from
  // @returns Map<String, int>: Map of the nucleotide mapped to the number of occurences of it in the strand.
  Map<String, int> count(String strand) {
    Map<String, int> result = {"A": 0, "C": 0, "G": 0, "T": 0, };
    strand.split('').forEach((String nucleotide) {
      int? current = result[nucleotide];
      if (current == null) {
        throw new InvalidNucleotideException("$nucleotide is not an expected nucleotide");
      }      
      result[nucleotide] = current + 1;
    });
    return result;
  }
}

// Custom exception class expected by the tests.
class InvalidNucleotideException implements Exception {
  late String message;
  // Constructor, just save the message.
  InvalidNucleotideException(this.message);
}
