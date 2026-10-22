// Exercise: gigasecond


// Return the given time plus one billion seconds.
// @param DateTime dateTime: The reference time.
// @returns DateTime: The reference time plus one billion seconds
DateTime addGigasecondTo(final DateTime dateTime) {
  return dateTime.add(Duration(seconds: 1000000000));
}
