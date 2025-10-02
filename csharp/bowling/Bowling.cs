public class BowlingGame
{
    int frameNum = 1;
    int throwNum = 1;
    List<int> scores = new List<int>();
    public void Roll(int pins)
    {
        // No more if fames are filled or 3rd roll with less than 10 pins on last frame.
        int maxThrows = 2;
        
        if (throwNum == 3)
            maxThrows = 3;
        else if (frameNum == 10
            && throwNum == 2
            && scores[scores.Count - 1] + pins >= 10)
            maxThrows = 3;

        if (frameNum < 10
            && throwNum == maxThrows
            && scores[scores.Count - 1] + pins > 10)
        {
            throw new ArgumentException("Second throw of a frame cannot be greater than spare value.");
        }

        if (pins > 10 || pins < 0)
                throw new ArgumentException("Invalid pin count only 0-10 are allowed.");        
            
        if (frameNum > 10)
                throw new ArgumentException("Game Over! No more throws allowed.");
        
        if (frameNum == 10
            && throwNum == 3
            && scores[scores.Count - 1] != 10
            && scores[scores.Count - 2] + scores[scores.Count - 1] != 10
            && pins + scores[scores.Count - 1] > 10)
            throw new ArgumentException("Bad Pin Count can't throw a strike when a spare is expected.");

        scores.Add(pins);
        if (throwNum == 1 && pins == 10 && frameNum < 10)
        {
            // Advance to next throw on frames 1-9 if a strike.
            throwNum = 1;
            frameNum++;
        }
        else
        {
            throwNum++;
            if (throwNum > maxThrows)
            {
                throwNum = 1;
                frameNum++;
            }
        }
    }

    public int? Score()
    {
        int frameNum = 1;
        int throwNum = 1;
        int totalScore = 0;
        for (int i = 0; i < scores.Count; i++)
        {
            int maxThrows = 2;

            if (frameNum == 10 && throwNum == 2 && scores[scores.Count - 2] + scores[scores.Count - 1] >= 10)
                maxThrows = 3;

            int score = scores[i];
            totalScore += score;
            bool strike = score == 10;
            bool spare = throwNum == 2 && i > 0 && scores[i - 1] + score == 10;

            if ((spare || strike) && (i + 1 < scores.Count) && frameNum < 10)
                totalScore += scores[i + 1];

            if (strike && (i + 2 < scores.Count) && frameNum < 10)
                totalScore += scores[i + 2];

            if (throwNum == 1 && score == 10 && frameNum < 10)
            {
                // Advance to next throw on frames 1-9 if a strike.
                throwNum = 1;
                frameNum++;
            }
            else
            {
                throwNum++;
                if (throwNum > maxThrows)
                {
                    throwNum = 1;
                    frameNum++;
                }
            }
        }
        if (frameNum <= 10)
            throw new ArgumentException("Game is incomplete and not ready to score.");
        return totalScore;
    }
}