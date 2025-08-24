using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

class Food
{
    public string Name { get; set; }
    public string CatchTarget { get; set; }
    public string Phrase { get; set; }

    public Food(string Name, string CatchTarget, string Phrase)
    {
        this.Name = Name;
        this.CatchTarget = CatchTarget;
        this.Phrase = Phrase;
    }
}


public static class FoodChain
{
    private static Food[] foods = {
        new Food("fly", "fly", "I don't know why she swallowed the fly. Perhaps she'll die."),
        new Food("spider", "spider that wriggled and jiggled and tickled inside her", "It wriggled and jiggled and tickled inside her."),
        new Food("bird", "bird", "How absurd to swallow a bird!"),
        new Food("cat", "cat", "Imagine that, to swallow a cat!"),
        new Food("dog", "dog", "What a hog, to swallow a dog!"),
        new Food("goat", "goat", "Just opened her throat and swallowed a goat!"),
        new Food("cow", "cow", "I don't know how she swallowed a cow!"),
        new Food("horse", "horse", "She's dead, of course!")
    };

    public static string Recite(int verseNumber)
    {
        int index = verseNumber - 1;
        StringBuilder sb = new();
        sb.Append($"I know an old lady who swallowed a {foods[index].Name}.\n");
        sb.Append($"{foods[index].Phrase}");
        if (verseNumber < foods.Length)
        {
            while (index >= 1)
            {
                sb.Append($"\nShe swallowed the {foods[index].Name} to catch the {foods[index - 1].CatchTarget}.");
                index--;
            }
            if (verseNumber > 1)
                sb.Append($"\n{foods[0].Phrase}");
        }
        return sb.ToString();
    }

    public static string Recite(int startVerse, int endVerse)
    {
        return String.Join("\n\n", (from verse in Enumerable.Range(startVerse, endVerse - startVerse + 1)
                                  select Recite(verse)));
    }
}