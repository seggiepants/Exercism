// Exercise: Perfect Numbers

// define the Classification enum
enum Classification { perfect, abundant, deficient}

class PerfectNumbers {
  // Put your code here

  // Based on its aliquot sum (sum of factors of a number between 1 and number - 1), 
  // classify a number as deficient (sum < number), abundant (sum > number) or perfect (sum > number)
  // @param int number: The number to classify.
  // @returns Classification: Classification result perfect, abundant, or deficient from the Classification enum.
  Classification classify(int number) {
    if (number <= 0) {
      throw new ArgumentError("Number must be a positive integer greater than zero.");
    }
    int aliquot = 0;
    for(int i = 1; i <= number / 2; i++) {
      if (number % i == 0)
        aliquot += i;
    }
    if (aliquot < number)
      return Classification.deficient;
    if (aliquot > number) 
      return Classification.abundant;
    return Classification.perfect;
  }
}
