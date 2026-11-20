// Exercise: Prime Factors
class PrimeFactors {
  // Put your code here

  // Find the prime factors of a given number. (The set of smallest prime numbers that divide evenly into the number)
  // @param int total: The number to find factors for.
  // @returns List<int>: The factors found.
  List<int> factors(int total) {
    List<int> factors = [];
    while (total >= 2) {
      int candidate = 2;
      while (candidate <= total && total % candidate != 0) { candidate++; }
      if (total % candidate == 0) {
        factors.add(candidate);
        total ~/= candidate;
      }
    }
    return factors;
  }
}
