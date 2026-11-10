// Exercise: Resistor Color Duo
class ResistorColorDuo {
  // Put your code here

  // value for a color is its index in the following list
  final List<String> lookup = ['black', 'brown', 'red', 'orange', 'yellow', 'green', 'blue','violet', 'grey', 'white'];
  
  // Return the value for up to the first two bands of a resistor.
  // @param List<String> colors: The up to two color bands to compute a value for.
  // @returns int: The computed value of the resitor band(s) (only the first two)
  int value(List<String> colors) {
    int result = 0;
    for(int i = 0; i < colors.length; i++) {
      if (i >= 2) 
        break;
      
      int value = lookup.indexOf(colors[i].toLowerCase());
      if (value >= 0 && value < lookup.length) {
        result = (result * 10) + value;
      }
    }
    return result;
  }
}
