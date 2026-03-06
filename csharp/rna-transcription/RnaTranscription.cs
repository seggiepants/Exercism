using System.Linq;
public static class RnaTranscription
{
    public static string ToRna(string strand)
    {
        Dictionary<char, char> translate = new Dictionary<char, char> {
            ['G'] = 'C',
            ['C'] = 'G',
            ['T'] = 'A',
            ['A'] = 'U'
        };

        return new string((
            from char ch in strand 
            select translate.GetValueOrDefault(ch, ch)).ToArray<char>());
    }
}