public static class Alphametics
{
    public static IDictionary<char, int> Solve(string equation)
    {

        string[] sides = equation.Split("==");
        string[] lhs = (from value in sides[0].Split("+")
                        select value.Trim().ToUpperInvariant()).ToArray<string>();
        string[] rhs = (from value in sides[1].Split("+")
                        select value.Trim().ToUpperInvariant()).ToArray<string>();

        HashSet<char> notZero = (from value in lhs.Concat(rhs)
                                 select value[0]).ToHashSet<char>();

        HashSet<char> uniqueCharacters = lhs.Concat(rhs).SelectMany(ch => ch).ToHashSet<char>();
        Dictionary<char, int> ret = (from ch in uniqueCharacters
                                     select new KeyValuePair<char, int>(ch, -1)).ToDictionary<char, int>();

        bool success = TrySolve(ret, lhs, rhs, notZero);
        if (!success)
            throw new ArgumentException("No solution found.");
        return ret;
    }

    public static bool TrySolve(Dictionary<char, int> solution, string[] lhs, string[] rhs, HashSet<char> NotZero)
    {
        // If complete but inconsistent return false
        // If complete and consistent return true if left hand side converted to integers and summed is equal to the right hand side with the same summing.
        if (IsComplete(solution))
        {
            if (ReadyToCheck(solution))
            {
                if (!SumNumbers(solution, lhs, out long sumLHS))
                    return false;

                if (!SumNumbers(solution, rhs, out long sumRHS))
                    return false;

                return sumLHS == sumRHS;
            }
            else
            {
                // Complete but not valid. Failed
                return false;
            }
        }

        // Not done so pick the next character.
        char next = (from kvp in solution
                     where kvp.Value == -1
                     select kvp.Key).First();
        for (int i = 0; i < 10; i++)
        {
            bool skip = (from kvp in solution
                         where kvp.Value == i
                         select kvp.Key).Count() != 0;

            if (NotZero.Contains(next) && i == 0)
                skip = true;

            if (!skip)
            {
                solution[next] = i;
                bool ret = TrySolve(solution, lhs, rhs, NotZero);
                if (ret)
                    return true;
                solution[next] = -1; // undo so we can try the next.
            }
        }

        return false;
    }

    public static bool SumNumbers(Dictionary<char, int> key, string[] values, out long sum)
    {
        sum = 0;

        foreach (string value in values)
        {
            long num = ParseNumber(key, value);
            if (num == -1)
                return false;
            else
                sum += num;
        }
        return true;
    }

    public static long ParseNumber(Dictionary<char, int> key, string value)
    {
        string temp = value;
        foreach (char c in key.Keys)
        {
            temp = temp.Replace(c, (char)(key[c] + (int)'0'));
        }

        bool success = long.TryParse(temp, out long result);
        if (success)
            return result;
        else
            return -1;
    }

    public static bool IsComplete(Dictionary<char, int> solution)
    {
        return (from kvp in solution
                where kvp.Value == -1
                select kvp.Key).Count() == 0;
    }

    public static bool ReadyToCheck(Dictionary<char, int> solution)
    {
        // All filled in and unique
        return (from kvp in solution
                where kvp.Value != -1
                select kvp.Key).ToHashSet().Count() == solution.Count;
    }
}
