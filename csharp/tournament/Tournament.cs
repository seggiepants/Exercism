using System.Text;

using Xunit.Internal;

public static class Tournament
{
    public class MatchInfo
    {
        public int MatchesPlayed;
        public int Wins;
        public int Draws;
        public int Losses;
        public int Points;

        public MatchInfo()
        {
            MatchesPlayed = 0;
            Wins = 0;
            Draws = 0;
            Losses = 0;
            Points = 0;
        }
    };

    public static void Tally(Stream inStream, Stream outStream)
    {
        Encoding encoding = new UTF8Encoding();
        StreamReader sr = new(inStream);
        Dictionary<string, MatchInfo> results = new Dictionary<string, MatchInfo>();
        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine() + "";
            string[] cols = line.Split(';'); // Team A;Team B;win/lose
            if (cols.Length < 3)
                continue;
            string teamA = cols[0];
            string teamB = cols[1];
            string win = cols[2].ToLowerInvariant().Trim();

            if (!results.ContainsKey(teamA))
                results.Add(teamA, new MatchInfo());

            if (!results.ContainsKey(teamB))
                results.Add(teamB, new MatchInfo());

            MatchInfo dataA = results[teamA];
            MatchInfo dataB = results[teamB];

            dataA.MatchesPlayed++;
            dataB.MatchesPlayed++;

            if (win == "win")
            {
                dataA.Wins++;
                dataA.Points += 3;
                dataB.Losses++;
            }
            else if (win == "loss")
            {
                dataA.Losses++;
                dataB.Wins++;
                dataB.Points += 3;
            }
            else if (win == "draw")
            {
                dataA.Draws++;
                dataA.Points++;

                dataB.Draws++;
                dataB.Points++;
            }

        }
        // Write the header
        const string header = "Team                           | MP |  W |  D |  L |  P";
        outStream.Write(encoding.GetBytes(header));
        foreach (KeyValuePair<string, MatchInfo> kvp in (from KeyValuePair<string, MatchInfo> kvp in results
                                                         orderby kvp.Value.Points descending, kvp.Key ascending
                                                         select kvp))
        {
            string row = $"\n{kvp.Key,-30} | {kvp.Value.MatchesPlayed,2} | {kvp.Value.Wins,2} | {kvp.Value.Draws,2} | {kvp.Value.Losses,2} | {kvp.Value.Points,2}";
            outStream.Write(encoding.GetBytes(row));
        }
    }
}
