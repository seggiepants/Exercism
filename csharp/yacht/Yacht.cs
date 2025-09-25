public enum YachtCategory
{
    Ones = 1,
    Twos = 2,
    Threes = 3,
    Fours = 4,
    Fives = 5,
    Sixes = 6,
    FullHouse = 7,
    FourOfAKind = 8,
    LittleStraight = 9,
    BigStraight = 10,
    Choice = 11,
    Yacht = 12,
}

public static class YachtGame
{
    public static int Score(int[] dice, YachtCategory category)
    {
        Dictionary<int, int> faceCount = ProcessDice(dice);
        switch (category)
        {
            case YachtCategory.Ones:
                return faceCount.GetValueOrDefault(1, 0);
            case YachtCategory.Twos:
                return faceCount.GetValueOrDefault(2, 0) * 2;
            case YachtCategory.Threes:
                return faceCount.GetValueOrDefault(3, 0) * 3;
            case YachtCategory.Fours:
                return faceCount.GetValueOrDefault(4, 0) * 4;
            case YachtCategory.Fives:
                return faceCount.GetValueOrDefault(5, 0) * 5;
            case YachtCategory.Sixes:
                return faceCount.GetValueOrDefault(6, 0) * 6;
            case YachtCategory.FullHouse:
                return IsFullHouse(faceCount) ? (from pair in faceCount select pair.Key * pair.Value).Sum() : 0;
            case YachtCategory.FourOfAKind:
                // Sure five or six of a kind still count.
                return faceCount.Values.Max() >= 4 ? 4 * (from pair in faceCount where pair.Value >= 4 select pair.Key).First() : 0;
            case YachtCategory.LittleStraight:
                return faceCount.GetValueOrDefault(1, 0) >= 1 && faceCount.GetValueOrDefault(2, 0) >= 1 && faceCount.GetValueOrDefault(3, 0) >= 1 && faceCount.GetValueOrDefault(4, 0) >= 1 && faceCount.GetValueOrDefault(5, 0) >= 1 ? 30 : 0;
            case YachtCategory.BigStraight:
                return faceCount.GetValueOrDefault(2, 0) >= 1 && faceCount.GetValueOrDefault(3, 0) >= 1 && faceCount.GetValueOrDefault(4, 0) >= 1 && faceCount.GetValueOrDefault(5, 0) >= 1 && faceCount.GetValueOrDefault(6, 0) >= 1 ? 30 : 0;
            case YachtCategory.Choice:
                return (from pair in faceCount select pair.Key * pair.Value).Sum();
            case YachtCategory.Yacht:
                return faceCount.Values.Max() == dice.Length ? 50 : 0;
            default:
                return 0;
        }
    }

    static bool IsFullHouse(Dictionary<int, int> dice)
    {
        // Remove zeros.
        int[] ret = (from pair in dice
                                    where pair.Value != 0
                                    orderby pair.Value
                                    select pair.Value).ToArray<int>();
        if (ret.Length != 2)
            return false;

        return (ret[0] == 2 && ret[1] == 3);
    }

    static Dictionary<int, int> ProcessDice(int[] dice)
    {
        return (from num in dice
                group num by num into pair
                select (pair.Key, pair.Count())).ToDictionary<int, int>();
    }
}

