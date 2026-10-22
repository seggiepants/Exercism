class Raindrops {

  // Return the raindrop sound for the corresponding number. If the number is not evenly divisible by 3, 5, or 7 it is the number as a string.
  // For numbers divisible by 3, 5, or 7 you get Pling, Plang, or Plong. If you have a number divisible by more than one of 3, 5, 7 you get the
  // sounds for each divisible value in order strung together. For example 15 divisible by 3 and 5 is PlingPlang and 105 divisible by all three
  // would get PlingPlangPlong, but 3 only gets Pling.
  // @param int drops: The number to convert to a raindrop sound.
  // @returns string: The number cast to a string or a combination of Pling, Plang, and/or Plong if evenly divisible by one or more of 3, 5, 7.
  String convert(int drops) {
    if (drops % 3 != 0 && drops % 5 != 0 && drops % 7 != 0)
      return drops.toString();
    
    String result = "";
    if (drops % 3 == 0) 
      result += 'Pling';

    if (drops % 5 == 0)
      result += 'Plang';
    
    if (drops % 7 == 0)
      result += 'Plong';

    return result;
  }
}
