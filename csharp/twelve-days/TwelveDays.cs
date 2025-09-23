public class Verse
{
    public string VerseNumber { get; set; }
    public string Gift { get; set; }

    public Verse(string num, string gift)
    {
        VerseNumber = num;
        Gift = gift;
    }
}
public static class TwelveDays
{
    private static Dictionary<int, Verse> verses = new Dictionary<int, Verse>() {
        [1] = new Verse("first", "a Partridge in a Pear Tree"),
        [2] = new Verse("second", "two Turtle Doves"),
        [3] = new Verse("third", "three French Hens"),
        [4] = new Verse("fourth", "four Calling Birds"),
        [5] = new Verse("fifth", "five Gold Rings"),
        [6] = new Verse("sixth", "six Geese-a-Laying"),
        [7] = new Verse("seventh", "seven Swans-a-Swimming"),
        [8] = new Verse("eighth", "eight Maids-a-Milking"),
        [9] = new Verse("ninth", "nine Ladies Dancing"),
        [10] = new Verse("tenth", "ten Lords-a-Leaping"),
        [11] = new Verse("eleventh", "eleven Pipers Piping"),
        [12] = new Verse("twelfth", "twelve Drummers Drumming"),
    };
    private static string AccumulatedGifts(int verseNumber)
    {
        return String.Join(", ", (from int i in Enumerable.Range(1, verseNumber)
                                  select verses[verseNumber - i + 1].Gift));
    }
    public static string Recite(int verseNumber)
    {
        if (!verses.ContainsKey(verseNumber))
            throw new ArgumentException("Invalid Verse Number");

        Verse v = verses[verseNumber];
        string ret = $"On the {v.VerseNumber} day of Christmas my true love gave to me: {AccumulatedGifts(verseNumber)}";
        if (verseNumber > 1)
            ret = ret.Replace(verses[1].Gift, "and " + verses[1].Gift);
        ret = ret + ".";
        return ret;
    }

    public static string Recite(int startVerse, int endVerse)
    {
        return String.Join("\n", (from int i in Enumerable.Range(startVerse, endVerse - startVerse + 1)
        select Recite(i)));
    }
}