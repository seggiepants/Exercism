// Exercise: Resistor Color

class ResistorColor {
  // Put your code here
  List<String> colors = ['black', 'brown', 'red', 'orange', 'yellow', 'green', 'blue', 'violet', 'grey', 'white'];

  // Find the index associated with the given color code.
  // @param String color: The color code to search for. No spaces all lower case, grey not gray.
  // @returns int: -1 if not found otherwise the associated index 0-9
  int colorCode(String color) {
    return colors.indexOf(color);
  }
}
