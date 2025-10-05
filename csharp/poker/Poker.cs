public static class Poker
{
    // rank/lowRank is used to compute card value. There further in the rank the more valuable.
    // Regular rank has a space in front so that the value of 2-K are consistent between the two arrays.
    static string[] rank = [" ", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"];
    static string[] lowRank = ["A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"];
    public static IEnumerable<string> BestHands(IEnumerable<string> hands)
    {
        var scores = (from string hand in hands
                 select (hand, Poker.ScoreHand(hand))).ToDictionary<string, double>();

        double maxScore = (from pair in scores select pair.Value).Max();
        return (from pair in scores where pair.Value == maxScore select pair.Key);
    }

    public static double ScoreHand(string hand)
    {
        // No five of a kind (we have no joker)
        // Take the top score and return it.
        // ScoreHighCard should always work, 
        // the others may or may not.
        string[] cards = hand.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        double[] scores = [ScoreHighCard(cards)
        , ScorePair(cards)
        , ScoreTwoPair(cards)
        , ScoreThreeOfAKind(cards)
        , ScoreStraight(cards)
        , ScoreFlush(cards)
        , ScoreFullHouse(cards)
        , ScoreFourOfAKind(cards)
        , ScoreStraightFlush(cards)];
        return scores.Max<double>();
    }

    public static double ScoreHighCard(string[] cards)
    {
        // Base score is 100 (higher for better card combinations)
        // Score cards adds the value of the hand in descending value
        // as a tie breaker.
        // The rest is just the best card's value.
        return 100 + (
            from string card in cards
            select Array.FindIndex<string>(rank, value => value == CardNoSuit(card))).Max<int>() + ScoreCards(cards);
    }

    public static double ScorePair(string[] cards)
    {
        // Group by card value and make a dictionary with counts.
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();

        int pairCount = (from pair in countedCards where pair.Value == 2 select pair.Key).Count<string>();
        // 2 pairs will be dealt with in ScoreTwoPair
        if (pairCount != 1)
            return 0;
        int? score = (from pair in countedCards where pair.Value == 2 select Array.FindIndex<string>(rank, value => value == pair.Key)).Max<int>();
        return score == null ? 0 : 200 + (int)score + ScoreCards(cards);
    }

    public static double ScoreTwoPair(string[] cards)
    {
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        int pairCount = (from pair in countedCards where pair.Value == 2 select pair.Key).Count<string>();
        if (pairCount != 2)
            return 0;

        int? score = (from pair in countedCards where pair.Value == 2 select Array.FindIndex<string>(rank, value => value == pair.Key)).Max<int>();
        return score == null ? 0 : 300 + (int)score + ScoreCards(cards);
    }

    public static double ScoreThreeOfAKind(string[] cards)
    {
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        int pairCount = (from pair in countedCards where pair.Value == 3 select pair.Key).Count<string>();
        if (pairCount != 1)
            return 0;

        int? score = (from pair in countedCards where pair.Value == 3 select Array.FindIndex<string>(rank, value => value == pair.Key)).Max<int>();
        return score == null ? 0 : 400 + (int)score + ScoreCards(cards);
    }

    public static double ScoreStraight(string[] cards)
    {
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        // Not a straight if we don't have five groupings.
        if (countedCards.Keys.Count() < 5)
            return 0;
        // Count with ace as best and worst card.
        int[] keyValues = (from string key in countedCards.Keys
            select Array.FindIndex(rank, value => value == key)).ToArray<int>();
        int[] keyLowValues = (from string key in countedCards.Keys 
            select Array.FindIndex(lowRank, value => value == key)).ToArray<int>();
        // Order by didn't seem to work so sort them now.
        Array.Sort(keyValues);
        Array.Sort(keyLowValues);

        bool straightHigh = true;
        for (int i = 1; i < keyValues.Count(); i++)
        {
            if (keyValues[i] != keyValues[i - 1] + 1)
            {
                straightHigh = false;
                break;
            }
        }
        bool straightLow = true;
        for (int i = 1; i < keyLowValues.Count(); i++)
        {
            if (keyLowValues[i] != keyLowValues[i - 1] + 1)
            {
                straightLow = false;
                break;
            }
        }

        if (straightHigh)
        {
            return 500 + keyValues[keyValues.Count() - 1] + ScoreCards(cards);
        }
        else if (straightLow)
        {
            return 500 + keyLowValues[keyLowValues.Count() - 1] + ScoreCards(cards, true);
        }
        return 0;
    }

    public static double ScoreFlush(string[] cards)
    {
        // Group cards by suit.
        Dictionary<string, int> countedSuits = cards.GroupBy<string, string>(card => CardSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();

        // Not a flush if there are multiple suits.
        if (countedSuits.Keys.Count > 1)
            return 0;

        return 600 + ScoreCards(cards);
    }

    public static double ScoreFullHouse(string[] cards)
    {
        // A three of a kind and two of a kind. Group the cards by value, and count
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        
        // If not two kinds of cards only it isn't a full house.
        if (countedCards.Keys.Count != 2)
            return 0;
        
        // Make sure it is 3, 2
        int[] cardCounts = countedCards.Values.ToArray<int>();
        int maxCount = cardCounts.Max();
        int minCount = cardCounts.Min();
        if (maxCount != 3 || minCount != 2) // skip a 4/1 for example
            return 0;
        
        string? suit3 = (from string key in countedCards.Keys
                        where countedCards[key] == 3
                        select key).FirstOrDefault();
        string? suit2 = (from string key in countedCards.Keys
                        where countedCards[key] == 3
                        select key).FirstOrDefault();
        // This shouldn't happen just making the compiler happy.
        if (suit3 == null || suit2 == null)
            return 0;

        int rank3 = Array.FindIndex(rank, value => value == suit3) + 1;
        int rank2 = Array.FindIndex(rank, value => value == suit2) + 1;

        return 700 + rank3 * 10 + rank2 + ScoreCards(cards);        
    }

    public static double ScoreFourOfAKind(string[] cards)
    {
        // Pretty much the same as three of a kind just 4 instead of 3, and a 
        // higher base score.
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        int pairCount = (from pair in countedCards where pair.Value == 4 select pair.Key).Count<string>();
        if (pairCount != 1)
            return 0;

        int? score = (from pair in countedCards where pair.Value == 4 select Array.FindIndex<string>(rank, value => value == pair.Key)).Max<int>();
        return score == null ? 0 : 800 + (int)score + ScoreCards(cards);
    }

    public static double ScoreStraightFlush(string[] cards)
    {
        // Pretty much check for a flush then a straight.

        // Flush
        Dictionary<string, int> countedSuits = cards.GroupBy<string, string>(card => CardSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();

        if (countedSuits.Keys.Count > 1)
            return 0;

        // Straight
        Dictionary<string, int> countedCards = cards.GroupBy<string, string>(card => CardNoSuit(card)).
            Select<IGrouping<string, string>, KeyValuePair<string, int>>(value => new KeyValuePair<string, int>(value.Key, value.Count())).ToDictionary<string, int>();
        if (countedCards.Keys.Count() < 5)
            return 0;
        int[] keyValues = (from string key in countedCards.Keys
            select Array.FindIndex(rank, value => value == key)).ToArray<int>();
        int[] keyLowValues = (from string key in countedCards.Keys 
            select Array.FindIndex(lowRank, value => value == key)).ToArray<int>();
        Array.Sort(keyValues);
        Array.Sort(keyLowValues);

        bool straightHigh = true;
        for (int i = 1; i < keyValues.Count(); i++)
        {
            if (keyValues[i] != keyValues[i - 1] + 1)
            {
                straightHigh = false;
                break;
            }
        }
        bool straightLow = true;
        for (int i = 1; i < keyLowValues.Count(); i++)
        {
            if (keyLowValues[i] != keyLowValues[i - 1] + 1)
            {
                straightLow = false;
                break;
            }
        }

        if (straightHigh)
        {
            return 900 + keyValues[keyValues.Count() - 1] + ScoreCards(cards);
        }
        else if (straightLow)
        {
            return 900 + keyLowValues[keyLowValues.Count() - 1] + ScoreCards(cards, true);
        }
        return 0;
    }

    // Helper get the card value without the suit.
    public static string CardNoSuit(string card)
    {
        string tmp = card.Trim();
        return tmp.Substring(0, tmp.Length - 1);
    }

    // Helper get the suit without the card value.
    public static string CardSuit(string card)
    {
        string tmp = card.Trim();
        return tmp.Substring(tmp.Length - 1, 1);
    }

    // Score the hand as a whole for tie breakers.
    // will be value to add after the decimal point.
    public static double ScoreCards(string[] cards, bool aceLow = false)
    {
        int[] sorted;
        if (aceLow)
        {
            sorted = (from card in cards
                      orderby Array.FindIndex<string>(lowRank, value => value == CardNoSuit(card)) descending
                      select Array.FindIndex<string>(lowRank, value => value == CardNoSuit(card))).ToArray<int>();
        }
        else
        {
            sorted = (from card in cards
                      orderby Array.FindIndex<string>(rank, value => value == CardNoSuit(card)) descending
                      select Array.FindIndex<string>(rank, value => value == CardNoSuit(card))).ToArray<int>();
        }
        int value = 0;
        foreach (int cardValue in sorted)
        {
            value = value << 2;
            value += cardValue;
        }
        // Can get up to seven place decimal for 0xfffff;
        return value / 10000000.0;
    }
}