// Exercise - Sum of Multiples
class SumOfMultiples {
  // Put your code here

  // For a given list of values return the multiples of those values less than limit
  // and return their sum.
  // @param List<int> multiples: The values to find multiples of less than limit.
  // @param int limit: The value to stop when you reach/exceed.
  // @returns: Sum of unique multiples of the values given in the multiples list less than limit.
  int sum(List<int> multiples, int limit) {
    Set<int> results = {};
    for(int multiple in multiples)
    {
      if (multiple > 0) {
        for(int value = multiple; value < limit; value += multiple) {
          results.add(value);
        }
      }
    }

    return results.length == 0 ? 0 : results.reduce((int accumulator, int value) => accumulator + value);
  }
}
