// Holds data about a specific verse in the nursery rhyme
class VerseData {
  late String subject;
  late String verb;

  VerseData({required this.subject, required this.verb});
}

// Class for reciting the nursery rhyme 'This is the house that jack built'.
class House {
  // Put your code here

  // Verse specific data. One for each verse.
  List<VerseData> data = [
    new VerseData(subject: 'house that Jack built', verb: ''),
    new VerseData(subject: 'malt', verb: 'lay in'),
    new VerseData(subject: 'rat', verb: 'ate'),
    new VerseData(subject: 'cat', verb: 'killed'),
    new VerseData(subject: 'dog', verb: 'worried'),
    new VerseData(subject: 'cow with the crumpled horn', verb: 'tossed'),
    new VerseData(subject: 'maiden all forlorn', verb: 'milked'),
    new VerseData(subject: 'man all tattered and torn', verb: 'kissed'),
    new VerseData(subject: 'priest all shaven and shorn', verb: 'married'),
    new VerseData(subject: 'rooster that crowed in the morn', verb: 'woke'),
    new VerseData(subject: 'farmer sowing his corn', verb: 'kept'),
    new VerseData(subject: 'horse and the hound and the horn', verb: 'belonged to'),
  ];

  //Recite all or a group of verses from the nursery rhyme 'This is the house 
  //that Jack built'.
  //@param int start: The verse to start with 1 to 12
  //@param int end: The verse to end on 1 to 12 but greater or equal to start.
  //@returns string: The desired range of verses from the nursery rhyme with 
  //a line breach between verses.
  String recite(int start, int end) {
    StringBuffer sb = new StringBuffer();

    for(int j = start - 1; j < end; j++) 
    {
      sb.write('This is the ${data[j].subject}');
      for(int i = j - 1; i >= 0; i--) {
        sb.write(' that ${data[i + 1].verb} the ${data[i].subject}');
        
      }
      sb.write('.');
      if (j < end - 1) {
        sb.write('\n');
      }
    }
    return sb.toString();
  }
}
