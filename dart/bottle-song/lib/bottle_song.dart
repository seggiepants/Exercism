// Exercise - Bottle Song
class BottleSong {
  // Put your code here

  // Zero to Ten as numbers for the song. Note that zero is no.
  List<String> num_to_string = [
    'No', 'One', 'Two', 'Three', 'Four', 'Five',
    'Six', 'Seven', 'Eight', 'Nine', 'Ten',
  ];

  // Recite one or more verses in the green bottles song. To get the whole song pass 10 for start_verse and count.
  // @param int start_verse: A number 1 to 10. The Verse to start from. Verses count down from this.
  // @param int count: The number of verses to return. Should be 1 to start_verse and less than or equal to 10.
  // @returns List<String>: A list of strings one string per line in the verse with an extra blank line between verses.
  // @raises ArgumentError: If the start_verse, or count brings verse number out of bounds of 1-10.
  List<String> recite(int start_verse, int count) {
    if (start_verse < 1 || start_verse > 10) {
      throw new ArgumentError("Only verses one to ten are available.");
    }
    if (count < 1 || count > start_verse || count > 10) {
      throw new ArgumentError("Number of verses or start verse is out of bounds.");
    }

    int remaining = count;
    int verse = start_verse;
    List<String> results = [];
    while (remaining > 0) {
      if (verse < start_verse) {
        results.add('');
      }
      String count_text = num_to_string[verse];
      String count_text_next = num_to_string[verse - 1].toLowerCase();
      String bottles = verse == 1 ? "bottle" : "bottles";
      String bottles_next = verse - 1 == 1 ? "bottle" : "bottles";
      for (int i = 0; i < 2; i++) {
        results.add("${count_text} green ${bottles} hanging on the wall,");
      }
      results.add("And if one green bottle should accidentally fall,");
      results.add("There'll be ${count_text_next} green ${bottles_next} hanging on the wall.");

      remaining--;
      verse--;
    }

    return results;
  }
}
