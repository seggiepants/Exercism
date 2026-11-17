// Exercise: Allergies / Bit Flags
import 'dart:math';

class Allergies {

  // Known allergens in bit order.
  List<String> allergy = [
    'eggs', 'peanuts', 'shellfish', 'strawberries', 'tomatoes', 'chocolate', 'pollen', 'cats',
  ];
  
  // Put your code here

  // Check if a bitFlag matches for a given allergen.
  // @param String allergen: The allergen to test for. Should be all lower case no spaces.
  // @param int bitFlags: Integer treated as a set of binary flags. Each bit indicates allergic to for a given allergen 1 = allergic
  // @returns bool: True if the bitflag indicates allergic to the given allergen.
  bool allergicTo(String allergen, int bitFlags) {
    int index = allergy.indexOf(allergen, 0);
    if (index < 0) {
      return false;
    }
    return bitFlags & (1 << index) != 0;
  }

  // Return a list that shows all of the known allergens matching the given bit flag.
  // @param int bitFlags: A integer treated as bit flags for known allergens. A binary 1 indicates allergic to.
  // @returns List<String>: The names of the known allergens the bit flag indicates.
  List<String> list(int bitFlags) {    
    return allergy.where((allergen) => allergicTo(allergen, bitFlags)).toList();    
  }
}
