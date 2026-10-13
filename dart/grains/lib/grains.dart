// Exercise: grains

// Return the number of pieces for a given square on a chessboard. Where the 
// first piece is 1 and each subsequent piece is double the last. So the fourth
// square will have 8 grains for example.
// @param int n: The square to return the grain count for. Should be an integer 1-64.
// @returns BigInt: The number of grains of rice to add to the square.
// @raises ArgumentError: If the parameter n is outside of 1-64.
BigInt square(int n) {
  if (n < 1 || n > 64) {
    throw new ArgumentError('square must be between 1 and 64');
  }
  return BigInt.from(2).pow(n - 1);
}

// Return the total number of grains on a chessboard filled with one grain on the first
// square and doubling for each subsequent square.
// @returns BigInt: The total for filling the chessboard with each square double the last.
BigInt total() {
  BigInt total = BigInt.from(0);
  for(int i = 1; i <= 64; i++) {
    total = total + square(i);
  }
  return total;
}
