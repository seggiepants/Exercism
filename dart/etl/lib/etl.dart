// ETL exercise.

// Class that holds the methods needed for the ETL exercise.
class Etl {
  // Put your code here

  // Transform a given map that has a number (as string) mapped to a list of matching characters
  // to a Map of a character string and the points for the character in the string. Lowercase the 
  // character too. There is no check that the input key can be parsed to an int.
  // @param Map<String, List<String>>input: The input value to transform.
  // @returns Map<String, int>: The transformed output.
  Map<String, int> transform(Map<String, List<String>>input) {
    Map<String, int> result = Map<String, int>();
    input.forEach((String points, List<String>runes) {
      int numPoints = int.parse(points);
      for(String rune in runes) {
        result[rune.toLowerCase()] = numPoints;
      }
    });
    return result;
  }
}
