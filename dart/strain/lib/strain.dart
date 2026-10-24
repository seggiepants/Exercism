// Exercise: Strain
class Strain {
  // your code here...
  
  // Iterate over values and return those where the function returns true discarding the rest.
  // @param Iterable values: An iterable of values of undefined type.
  // @param Function fn: A function that takes a value and returns true or false.
  // @returns Iterable: The values in the iterable where fn(value) == true;
  Iterable keep(Iterable values, Function fn) {
    return values.where((value) => fn(value));
  }

  // Iterate over values and discarding those where the function returns true and return the rest.
  // @param Iterable values: An iterable of values of undefined type.
  // @param Function fn: A function that takes a value and returns true or false.
  // @returns Iterable: The values in the iterable where fn(value) != true;
  Iterable discard(Iterable values, Function fn) {
    return values.where((value) => !fn(value));
  }
}
