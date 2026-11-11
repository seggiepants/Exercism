import 'dart:math';

// Exercise - Resistor Color Trio
class ResistorColorTrio {
  // Put your code here

  // Lookups for the color bands and prefix
  final List<String> colors = ['black', 'brown', 'red', 'orange', 'yellow', 'green', 'blue', 'violet', 'grey', 'white'];
  final List<String> prefix = [' ohms', ' kiloohms', ' megaohms', ' gigaohms'];

  // Generate a label for a set of bands of size up to three.
  // @param List<String> bands: The bands to generate a label for. The first two are number, the third is the power of ten multiplier
  // @returns String: The label for the given set of band colors
  // @raises ArgumentError: Thrown if a band is not found in the color lookup list.
  String label(List<String> bands) {
    int bandCount = bands.length >= 2 ? 2 : bands.length;
    num ohms = 0;
    for(int i = 0; i < bandCount; i++) {
      int index = colors.indexOf(bands[i]);
      if (index < 0) {
        throw new ArgumentError("Invalid band");
      }
      ohms = (ohms * 10) + index;
    }

    if (bandCount < bands.length) {
      int power = colors.indexOf(bands[bandCount]);
      if (power < 0) {
        throw new ArgumentError("Invalid multiplier band");
      }
      ohms *= pow(10, power);
    }
    int prefixIndex = 0;
    while (prefixIndex < prefix.length && ohms > 1000) {
      prefixIndex++;
      ohms = ohms ~/ 1000;
    }

    return ohms.toString() + prefix[prefixIndex];
  }

}
