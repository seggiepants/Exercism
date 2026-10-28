// Helper data class to associate a nth (first) with a gift (a Partridge in a Pear Tree)
class NthDay {
  late String nth;
  late String gift;

  // Constructor.
  // @param String nth: Which day this is as spoken usually ending in nth
  // @param String gift: The gift the true love is giving on that day.
  NthDay(this.nth, this.gift);
}

// Exercise - Twelve Days of Christmas
class TwelveDays {
  
  // Recite the twelve days of Christmas song from verse start to end.
  // There will a blank line between verses (it multiple verses).
  // @param int start: The verse to start at.
  // @param int end: The verse to end on.
  // @returns String: The desired sections of the song as a string with newlines between verses.
  String recite(int start, int end) 
  {
    Map<int, NthDay> days = Map<int, NthDay>();
    days[1] = NthDay("first", "a Partridge in a Pear Tree");
    days[2] = NthDay("second", "two Turtle Doves");
    days[3] = NthDay("third", "three French Hens");
    days[4] = NthDay("fourth", "four Calling Birds");
    days[5] = NthDay("fifth", "five Gold Rings");
    days[6] = NthDay("sixth", "six Geese-a-Laying");
    days[7] = NthDay("seventh", "seven Swans-a-Swimming");
    days[8] = NthDay("eighth", "eight Maids-a-Milking");
    days[9] = NthDay("ninth", "nine Ladies Dancing");
    days[10] = NthDay("tenth", "ten Lords-a-Leaping");
    days[11] = NthDay("eleventh", "eleven Pipers Piping");
    days[12] = NthDay("twelfth", "twelve Drummers Drumming");

    StringBuffer buffer = StringBuffer();
    for (int day = start; day <= end; day++) {
      if (day > start) {
        buffer.write("\n");
      }
      buffer.write("On the ${days[day]!.nth} day of Christmas my true love gave to me: ");
      for(int nth = day; nth > 1; nth--) {
        buffer.write("${days[nth]!.gift}, ");
      }
      if (day > 1) {
        buffer.write("and ");
      }
      buffer.write("${days[1]!.gift}.");
    }
    return buffer.toString();
  }
}
