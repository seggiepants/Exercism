using Xunit.Sdk;

public static class Dominoes
{
    // Don't need to try to fit a domino at both ends, just one side will work.
    // Don't need to keep track of the sequence just the first number of the 
    // first tile and last number of the last tile.
    // Dominoes can rotate so 1|3 can also be 3|1 I do need to check both.
    public static bool CanChain(IEnumerable<(int, int)> dominoes)
    {
        List<(int, int)> tiles = dominoes.ToList<(int, int)>();

        // Empty set counts as success.
        if (tiles.Count == 0)
            return true;

        // Degenerate case one dominoe is successful only if the first and second part match.
        if (tiles.Count == 1)
            return tiles[0].Item1 == tiles[0].Item2;

        // Try for a match starting with each possible tile until one is found.
        // Return false if no path works.
        for (int i = 0; i < tiles.Count; i++)
        {
            // Save then remove the top tile.
            List<(int, int)> nextRemaining = new List<(int, int)>(tiles);
            (int, int) current = tiles[i];
            nextRemaining.Remove(current);
            bool ret = Step(current.Item1, current.Item2, nextRemaining);
            nextRemaining.Clear();
            if (ret)
                return true;

            // Retry flipped (only if part 1 != part 2)
            if (current.Item1 != current.Item2)
            {
                nextRemaining = new List<(int, int)>(tiles);
                nextRemaining.Remove(current);
                ret = Step(current.Item2, current.Item1, nextRemaining);
                nextRemaining.Clear();
                if (ret)
                    return true;
            }
        }
        return false;
    }

    public static bool Step(int first, int last, List<(int, int)> remaining)
    {
        // Base condition.
        if (remaining.Count == 0)
        {
            // Success if the first and last match.
            return first == last;
        }

        // Check for potential matches. Fail if there are none.
        foreach ((int, int) item in (from item in remaining
                                     where item.Item1 == last || item.Item2 == last
                                     select item))
        {
            // Try at end of list.
            if (item.Item1 == last)
            {
                // Copy the current sequence and remaining.
                List<(int, int)> nextRemaining = new List<(int, int)>(remaining);
                nextRemaining.Remove(item);
                bool ret = Step(first, item.Item2, nextRemaining);
                nextRemaining.Clear();
                if (ret)
                    return true;
            }

            // Try at end of list flipped (if domino ends don't match)
            if (item.Item1 != item.Item2 && item.Item2 == last)
            {
                List<(int, int)> nextRemaining = new List<(int, int)>(remaining);
                nextRemaining.Remove(item);
                bool ret = Step(first, item.Item1, nextRemaining);
                nextRemaining.Clear();
                if (ret)
                    return true;
            }

        }
        return false; // Nothing to connect.
    }
}