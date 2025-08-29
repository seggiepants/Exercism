public class SgfTree
{
    public SgfTree(IDictionary<string, string[]> data, params SgfTree[] children)
    {
        Data = data;
        Children = children;
    }

    public IDictionary<string, string[]> Data { get; }
    public SgfTree[] Children { get; }
}

public class SgfParser
{
    public static SgfTree ParseTree(string input)
    {
        string text = input.Trim();
        if (text.Trim().Length == 0)
            throw new ArgumentException("Empty input.");

        Dictionary<string, string[]> data = new();
        List<SgfTree> children = new();
        int i = ParseNode(data, children, text, 0);
        if (i == -1)
            throw new ArgumentException("Malformed input.");
        return new SgfTree(data, children.ToArray<SgfTree>());
    }

    public static int ParseNode(Dictionary<string, string[]> data, List<SgfTree> children, string input, int index)
    {
        int i = index;
        // Expect (
        if (input[i] != '(')
        {
            throw new ArgumentException($"Not a tree '(' missing at index: {i}.");
        }
        i++;

        // Expect properties
        if (input[i] != ';')
            throw new ArgumentException($"No properties found.");
        i++;
        int next = ParseProperty(data, children, input, i);
        while (next != -1)
        {
            i = next;
            next = ParseProperty(data, children, input, i);
        }

        // Expect )
        if (input[i] != ')')
        {
            throw new ArgumentException($"Not a tree ')' missing at index: {i}.");
        }
        i++;
        return i;
    }

    public static int ParseProperty(Dictionary<string, string[]> data, List<SgfTree> children, string input, int index)
    {
        string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.;()\"";
        string allowedCharsName = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string name; // A-Z, 0-9
        int i = index;
        while (i < input.Length && allowedCharsName.Contains(input[i]))
            i++;

        if (i >= input.Length)
            return -1; // Not found.

        name = input.Substring(index, i - index);
        if (name != name.ToUpperInvariant())
            throw new ArgumentException($"Property names must be all upper case \"{name}\".");

        // Read Properties
        List<string> properties = new();
        while (i < input.Length && input[i] == '[')
        {
            // Expect [
            if (i >= input.Length || input[i] != '[')
            {
                return -1;
            }
            i++;

            int valueStart = i;
            while (i < input.Length && allowedChars.Contains(input[i]))
                i++;
            if (i == valueStart) // No value
                return -1;

            string value = input.Substring(valueStart, i - valueStart);

            // Expect ]
            if (i >= input.Length || input[i] != ']')
            {
                return -1;
            }
            i++;
            properties.Add(value);
        }
        if (properties.Count < 1)
            return -1;

        if (i < input.Length && input[i] == ';')
        {
            i++;
            Dictionary<string, string[]> childData = new();
            List<SgfTree> grandChildren = new();
            int next = ParseProperty(childData, grandChildren, input, i);
            if (next != -1)
            {
                children.Add(new SgfTree(childData, grandChildren.ToArray<SgfTree>()));
                i = next;
            }
        }
        else if (i < input.Length && input[i] == '(')
        {
            while (i < input.Length && input[i] == '(')
            {
                Dictionary<string, string[]> childData = new();
                List<SgfTree> grandChildren = new();
                int next = ParseNode(childData, grandChildren, input, i);
                if (next != -1)
                {
                    children.Add(new SgfTree(childData, grandChildren.ToArray<SgfTree>()));
                    i = next;
                }
            }
        }
        if (data.Keys.Contains(name))
                throw new ArgumentException($"Key \"{name}\" exists multiple times.");
        data.Add(name, properties.ToArray<string>());

        return i;
    }
}