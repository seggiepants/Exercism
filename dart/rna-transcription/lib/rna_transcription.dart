class RnaTranscription {
  // Put your code here
  String toRna(String strand) {
    const Map<String, String> lookup = {
      'G': 'C',
      'C': 'G',
      'T': 'A',
      'A': 'U',
    };
    StringBuffer result = StringBuffer();
    strand.toUpperCase().split('').forEach((ch) => result.write(lookup[ch] ?? ch));
    return result.toString();
  }
}
