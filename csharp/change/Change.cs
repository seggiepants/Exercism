public static class Change
{
    static int lengthCutoff = int.MaxValue;
    public static int[] FindFewestCoins(int[] coins, int target)
    {
        lengthCutoff = int.MaxValue;
        int[] result = FindFewestHelper(coins.ToList<int>(), [], target, []);
        if (result.Sum() != target)
            throw new ArgumentException("No match found.");
        return (from item in result orderby item ascending select item).ToArray<int>();
    }

    private static int[] FindFewestHelper(List<int> coins, List<int> used, int target, int[] currentBest)
    {
        if (target <= 0)
            return [];

        int usedSum = used.Sum();        
        if (lengthCutoff < int.MaxValue)
        {
            // Can't make it with remaining coins.
            if (usedSum + ((lengthCutoff - used.Count()) * coins.Max()) < target)
                return used.ToArray<int>();
        }

        if (usedSum >= target || coins.Count == 0 || used.Count() + 1 >= lengthCutoff)
            return used.ToArray<int>();

        int[] candidates = (from coin in coins orderby coin descending where usedSum + coin <= target select coin).ToArray<int>();
        int[] best = (int[]) currentBest.Clone();
        foreach(int coin in candidates)
        {
            List<int> currentUsed = new List<int>(used);
            int coinsAdded = 1;
            // Front load as many coins as possible to skip to the end state if possible then crawl back if needed.
            while (target - currentUsed.Sum() >= coin)
            {
                coinsAdded++;
                currentUsed.Add(coin);
            }
            if (currentUsed.Count >= lengthCutoff)
                    break;
            do
            {
                int[] current = FindFewestHelper(coins, currentUsed, target, best);
                if (current.Sum() == target)
                {
                    if (current.Length <= lengthCutoff && (best.Length == 0 || best.Count() > current.Length))
                    {
                        best = current.ToArray<int>();
                        lengthCutoff = Math.Min(lengthCutoff, best.Count());
                    }
                }
                if (coinsAdded > 1)
                {
                    currentUsed.Remove(coin);
                    coinsAdded--;
                }
            } while (coinsAdded > 1);
        }
        if (best.Sum() == target)
            return best;
        return used.ToArray<int>();
    }
}