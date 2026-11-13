// Exercise: Nth Prime

class NthPrime {
  // Put your code here  

  // Return/Calculate the nth prime number. May have an error if the nth prime is not in the integer range.
  // @param int nth: The nth prime number to find.
  // @returns: The nth prime number where the first is 2 and the 6th is 13.
  // @raises ArgumentError: If the parameter nth is less than one.
  int prime(int nth) {
    if (nth < 1) {
      throw new ArgumentError("There is no zeroth prime");
    }

    List<int> primes = [2, 3, 5, 7]; // Prepopulate a bit.
    // Add more primes if not enough.
    while (primes.length < nth) {
      // Prime if greater than last but not divisible by a prior prime.
      int candidate = primes.last + 1;
      bool isPrime = false;
      while (!isPrime) {
        isPrime = true;
        for(int number in primes)
        {
          if (candidate % number == 0) {
            isPrime = false;
            break;
          }
        }
        if (isPrime) {
          primes.add(candidate);
          break;
        }
        candidate++;
      } 
    }
    return primes[nth - 1];
  }
}
