class Proverb {
  // Put your code here

  // Recite a version of the all for the want of a nail proverb given the chain of 
  // pieces to the cascading loss.
  // @param List<String> pieces: The missing piece and its cascading losses.
  // @returns String: The proverb customized with the list of pieces.
  String recite(List<String> pieces) {
    List<String> result = [];

    for (int i = 0; i < pieces.length - 1; i++) {
      result.add('For want of a ${pieces[i]} the ${pieces[i + 1]} was lost.');
    }

    if (pieces.length > 0) {
      result.add('And all for the want of a ${pieces.first}.');
    }

    return result.join('\n');
  }
}
