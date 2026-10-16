// Implement the Classification enum.
enum Classification { equal, sublist, superlist, unequal }

class Sublist {
  // your code here...

  // Classify two lists as equal, sublist, superlist, or unequal.
  // @param List<int> listOne: The first list.
  // @param List<int> listTwo: The second list.
  // @returns Classification: enumeration value matching either equal, sublist, superlist, or unequal.
  Classification sublist(List<int> listOne, List<int> listTwo) {
    // Check if is equal.
    if (isEqual(listOne, listTwo)) {
      return Classification.equal;
    }
    // If list one is a sublist of list two it is a sublist.
    if (isSublist(listOne, listTwo)) {
      return Classification.sublist;
    }

    // Swap it so that if list two is a sublist of list one it is a superlist.
    if (isSublist(listTwo, listOne)) {
      return Classification.superlist;
    }

    // None of the above means unequal.
    return Classification.unequal;
  }

  // Check to see if two lists are equal.
  // @param List<int> listOne: The first list.
  // @param List<int> listTwo: The second list.
  // @returns bool: True if the same length and same values in the same order between the two lists.
  bool isEqual(List<int> listOne, List<int> listTwo) {
    if (listOne.length != listTwo.length) {
      return false;
    }
    bool success = true;
    for(int i = 0; i < listOne.length; i++) {
      if (listOne[i] != listTwo[i]) {
        success = false;
        break;
      }
    }
    return success;
  }

  // Check to see if listOne is a sublist of listTwo.
  // @param List<int> listOne: The first list.
  // @param List<int> listTwo: The second list.
  // @returns bool: True if list two is the same or greater in length to list one and 
  // list two contains a sequence of values that matches the entirety of list one.
  bool isSublist(List<int> listOne, List<int> listTwo) {
    if (listOne.length > listTwo.length) {
      return false; // Can't be a sublist if the first one is longer.
    } else if (listOne.length == 0) {
      return true; // Empty list is always a sub list of another list.
    }

    int maxIndex = listTwo.length - listOne.length; // Only go as far as you can fit the entirety of list one in list two.
    for(int i = 0; i <= maxIndex; i++) {
      bool success = true;
      for(int j = i; j < i + listOne.length; j++) {
        if (listOne[j - i] != listTwo[j]) {
          success = false; // Found a mismatch bail out and try again with the next candidate.
          break;
        }
      }
      if (success) {
        return true; // no mismatch in that sequence, so that is a sublist.
      }
    }
    return false; // never found a match before running out of candidates so not a sublist.
  }
}
