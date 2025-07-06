public static class MatchingBrackets
{
    public static bool IsPaired(string input)
    {
        Dictionary<char, char> pairs = new()
        {
            ['['] = ']',
            ['('] = ')',
            ['{'] = '}',
        };
        Stack<char> stack = new();
        foreach (char c in input)
        {
            if (pairs.ContainsKey(c))
            {
                stack.Push(pairs[c]);
            }
            else if (stack.Count() > 0 && c == stack.Peek())
            {
                char _ = stack.Pop();
            }
            else if (pairs.ContainsValue(c))
            {
                return false;
            }
        }

        return stack.Count() == 0;
    }
}
