using System.Linq;
using System.Security.Cryptography;

public class HighScores
{
    List<int> scores = new();
    public HighScores(List<int> list)
    {
        scores.AddRange(list);        
    }

    public List<int> Scores()
    {
        return scores;        
    }

    public int Latest()
    {
        return scores.Last<int>();        
    }

    public int PersonalBest()
    {
        return scores.Max<int>();
    }

    public List<int> PersonalTopThree()
    {

        var ret = (from score in scores
                   orderby score descending
                   select score).Take<int>(3).ToList<int>();

        return ret;        
    }
}