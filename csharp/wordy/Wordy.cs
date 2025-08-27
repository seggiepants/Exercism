using System.Text.RegularExpressions;

public static class Wordy
{

    static string RE_NUMBER = @"-?\d+"; // Regular expression for a series of digits with an optional - at the beginning.
    static Dictionary<string, int> precedence = new Dictionary<string, int>()
    {
        ["+"] = 2,
        ["-"] = 2,
        ["*"] = 3,
        ["/"] = 3,
    };

    static Dictionary<string, Func<int, int, int>> operations = new Dictionary<string, Func<int, int, int>>()
    {
        ["+"] = (a, b) => a + b,
        ["-"] = (a, b) => a - b,
        ["*"] = (a, b) => a * b,
        ["/"] = (a, b) => a / b,
    };

    // Clean up the input.
    static string SanitizeInput(string question)
    {
        Dictionary<string, string> operations = new Dictionary<string, string>()
        {
            ["plus"] = "+",
            ["minus"] = "-",
            ["multiplied by"] = "*",
            ["divided by"] = "/",
        };

        // Clean up the input;
        string query = question.ToLowerInvariant();
        if (query.StartsWith("what is "))
            query = query.Substring("what is ".Length);
        if (query.EndsWith("?"))
            query = query.Substring(0, query.Length - 1);
        query = query.Trim();

        foreach (KeyValuePair<string, string> pair in operations)
            query = query.Replace(pair.Key, pair.Value);

        return query;
    }

    // Check if the expression is infix order: num (op num)*
    private static bool IsInfix(string[] tokens)
    {
        bool expectNum = true;
        Regex r = new(RE_NUMBER);
        foreach (string token in tokens)
        {
            Match m = r.Match(token);
            if (m.Success && !expectNum) // number but expect operator.
                return false;
            if (precedence.Keys.Contains(token) && expectNum) //operator but expected a number.
                return false;
            expectNum = !expectNum; // should get numbers separated by operators.
        }
        return !expectNum; // Must end on a number.
    }

    // Adapted from: https://en.wikipedia.org/wiki/Shunting_yard_algorithm
    private static Stack<string> ShuntingYard(string[] tokens)
    {
        Stack<string> outputStack = new();
        Stack<string> operatorStack = new();
        Regex r = new(RE_NUMBER);
        foreach (string token in tokens)
        {
            Match m = r.Match(token);
            if (m.Success)
            {
                outputStack.Push(token);
            }
            else if (precedence.Keys.Contains(token))
            {
                int precedenceCurrent = precedence[token];
                while (operatorStack.Count > 0 && precedence[operatorStack.Peek()] >= precedenceCurrent)
                {
                    outputStack.Push(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
            else
            {
                throw new ArgumentException($"Invalid Token: \"{token}\".");
            }
        }
        while (operatorStack.Count > 0)
        {
            outputStack.Push(operatorStack.Pop());
        }

        return outputStack;
    }
    // Son of a <REDACTED> I thought it said to do precedence, not skip it.
    // It was a lot of work to get the Shunting Yard Algorithm working so I am keeping the code.
    // Maybe I will refer back to it some day.
    public static int AnswerWithPrecedence(string question)
    {
        string query = SanitizeInput(question);
        if (query.Length == 0)
            return 0;

        string[] tokens = query.Split(' ');

        if (!IsInfix(tokens))
            throw new ArgumentException("Not an infix expression.");

        Stack<string> outputStack = ShuntingYard(tokens);
        Stack<int> numbers = new();
        // Stack.Reverse() doesn't return a new stack just a reversed enumeration
        // so just do a for loop and use it as given.
        foreach (string token in outputStack.Reverse())
        {
            if (precedence.Keys.Contains(token))
            {
                if (numbers.Count < 2)
                    throw new ArgumentException($"Insufficient operands for operator {token}");
                int num2 = numbers.Pop();
                int num1 = numbers.Pop();
                numbers.Push(operations[token](num1, num2));
            }
            else
            {
                bool success = int.TryParse(token, out int value);
                if (!success)
                    throw new ArgumentException($"Token is not a number: {token}.");
                numbers.Push(value);
            }
        }
        if (numbers.Count > 1)
            throw new ArgumentException("Invalid expression");

        return numbers.Pop();
    }

    public static int Answer(string question)
    {
        string query = SanitizeInput(question);
        if (query.Length == 0)
            return 0;

        string[] tokens = query.Split(' ');

        if (!IsInfix(tokens))
            throw new ArgumentException("Not an infix expression.");

        bool success = int.TryParse(tokens[0], out int total);
        if (!success)
            throw new ArgumentException($"Expected a number found: {tokens[0]}.");

        for (int i = 1; i < tokens.Count(); i += 2)
        {
            string op = tokens[i];
            success = int.TryParse(tokens[i + 1], out int num2);
            if (!success)
                throw new ArgumentException($"Expected a number found: {tokens[i + 1]}.");

            if (operations.Keys.Contains(op))
            {
                total = operations[op](total, num2);
            }
            else
            {
                throw new ArgumentException($"Expected an operator found: {op}.");
            }
        }
        return total;
    }
}