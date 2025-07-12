using Sprache;

public static class Forth
{
    public abstract class ParseEntity
    {
    };

    public class Empty : ParseEntity
    {
        public Empty() { }
    }

    public class FunctionBody : ParseEntity
    {
        public Func<Stack<int>, int>? builtIn = null;
        public string? body = null;
        public Dictionary<string, FunctionBody>? symbolTable = null;

        public FunctionBody() { }
        public FunctionBody(string body, Dictionary<string, FunctionBody>? symbolTable)
        {
            this.body = body;
            this.symbolTable = symbolTable == null ? null : new Dictionary<string, FunctionBody>(symbolTable);
        }


        public FunctionBody(Func<Stack<int>, int> fn, Dictionary<string, FunctionBody>? symbolTable)
        {
            this.builtIn = fn;
            this.symbolTable = symbolTable == null ? null : new Dictionary<string, FunctionBody>(symbolTable);
        }
    }

    public class NumericConstant : ParseEntity
    {
        public int value { get; set; }

        public NumericConstant(string number)
        {
            if (!int.TryParse(number, out int value))
                value = 0;
            this.value = value;
        }
    }

    public class Identifier : ParseEntity
    {
        public string value { get; set; }

        public Identifier(string name)
        {
            if (name != "-")
            {
                if (!name.Any((c) => Char.IsDigit(c) == false && c != '-'))
                    throw new InvalidOperationException("Identifiers cannot be numbers");
            }
            value = name;
        }
    }

    public class WordDeclare : ParseEntity
    {
        public string name { get; set; }
        public string value { get; set; }

        public WordDeclare(string name, string value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public static class Grammar
    {
        static char[] allowedIdentifierChars = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G',
            'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R',
            'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2',
            '3', '4', '5', '6', '7', '8', '9', '-', '_', '+', '-',
            '*', '/', '.'};

        static Parser<NumericConstant> PositiveNumber = Parse.Digit.AtLeastOnce().Text().Select(value => new NumericConstant(value));
        static Parser<NumericConstant> NegativeNumber = (
            from sign in Parse.Char('-')
            from digits in Parse.Digit.AtLeastOnce().Text()
            select new NumericConstant(sign.ToString() + digits));
        static Parser<NumericConstant> Number = PositiveNumber.XOr(NegativeNumber);
        static Parser<Empty> NonToken = Parse.WhiteSpace.Many().Return(new Empty()).Named("Empty");
        static Parser<Identifier> Add = Parse.Char('+').Token().Return(new Identifier("+")).Named("+");
        static Parser<Identifier> Subtract = (from ch in Parse.Char('-')
                                              from next in Parse.Not(Parse.Digit)
                                              select ch).Token().Return(new Identifier("-")).Named("-");
        static Parser<Identifier> Multiply = Parse.Char('*').Token().Return(new Identifier("*")).Named("*");
        static Parser<Identifier> Divide = Parse.Char('/').Token().Return(new Identifier("/")).Named("/");

        static Parser<Identifier> Word = (
            from _Id in Parse.Chars(allowedIdentifierChars).Many().Text()
            where _Id.Length > 0
            select new Identifier(_Id)).Named("IDENTIFIER");

        static Parser<WordDeclare> Declaration =
        (from colon in Parse.Char(':').Once().Text()
         from ws1 in Parse.Optional(Parse.WhiteSpace.AtLeastOnce())
         from name in Word
         from ws2 in Parse.Optional(Parse.WhiteSpace.AtLeastOnce())
         from body in Parse.CharExcept(c => c == ';', "End on Semicolon").Many().Text()
         from ws3 in Parse.Optional(Parse.WhiteSpace.AtLeastOnce())
         from semicolon in Parse.Char(';').Once().Text()
         select new WordDeclare(name.value, body.Trim())
        ).Named("WORD_DECLARE");
        static Parser<Identifier> mathExpression = Add.Or(Subtract).Or(Multiply).Or(Divide);
        static Parser<ParseEntity> expression = NonToken.Or<ParseEntity>(Number).Or<ParseEntity>(Declaration).Or<ParseEntity>(Word).Or<ParseEntity>(mathExpression);
        public static Parser<IEnumerable<ParseEntity>> expressions = expression.Many();
    }


    public static void ParseLine(string instruction, Stack<int> stack, Dictionary<string, FunctionBody> symbolTable)
    {
        foreach (var op in Grammar.expressions.Parse(instruction))
        {
            if (op == null || op.GetType() == typeof(Forth.Empty))
            {
                continue;
            }
            else if (op.GetType() == typeof(Forth.NumericConstant))
            {
                NumericConstant? c = op as NumericConstant;
                if (c == null) continue;
                stack.Push(c.value);
            }
            else if (op.GetType() == typeof(Forth.WordDeclare))
            {
                WordDeclare? word = op as Forth.WordDeclare;
                if (word == null)
                    continue;

                // In forth you can overwrite function.
                // They also only run against their original symbol table to stop infinite loops.                
                symbolTable[word.name.ToLowerInvariant()] = new FunctionBody(word.value, symbolTable);
            }
            else if (op.GetType() == typeof(Forth.Identifier))
            {
                Identifier? word = op as Forth.Identifier;
                if (word == null)
                    continue;

                if (symbolTable.ContainsKey(word.value.ToLowerInvariant()))
                {
                    FunctionBody body = symbolTable[word.value.ToLowerInvariant()];
                    if (body.builtIn != null)
                    {
                        int _ = body.builtIn(stack);
                    }
                    else if (body.body != null)
                    {
                        ParseLine(body.body, stack, body.symbolTable != null ? body.symbolTable : symbolTable);
                    }
                    else
                    {
                        throw new Exception($"No function body for \"{word.value}\".");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Word: \"{word.value}\" not found.");
                }
            }
            else
            {
                throw new Exception($"Unhandled grammar: {op.ToString()}");
            }
        }
    }
    public static string Evaluate(string[] instructions)
    {
        Stack<int> stack = new();
        // Initialize with the built-in functions.
        // They do not require a symbol table.
        Dictionary<string, FunctionBody> symbolTable = new()
        {
            { "dup", new FunctionBody(DUP, null) },
            { "drop", new FunctionBody(DROP, null) },
            { "swap", new FunctionBody(SWAP, null) },
            { "over", new FunctionBody(OVER, null) },
            { "+", new FunctionBody(ADD, null) },
            { "-", new FunctionBody(SUBTRACT, null) },
            { "*", new FunctionBody(MULTIPLY, null) },
            { "/", new FunctionBody(DIVIDE, null) },
            { ".", new FunctionBody(PRINT, null) },
        };

        foreach (string instruction in instructions)
        {
            ParseLine(instruction, stack, symbolTable);

        }
        return String.Join(' ', (from elem in stack.Reverse()
                                 select elem.ToString()).ToList<string>());
    }

    public static int ADD(Stack<int> stack)
    {
        int first;
        int second;

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        first = stack.Pop();

        int ret = first + second;
        stack.Push(ret);
        return ret;
    }

    public static int SUBTRACT(Stack<int> stack)
    {
        int first;
        int second;

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        first = stack.Pop();

        int ret = first - second;
        stack.Push(ret);
        return ret;
    }

    public static int MULTIPLY(Stack<int> stack)
    {
        int first;
        int second;

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        first = stack.Pop();

        int ret = first * second;
        stack.Push(ret);
        return ret;
    }

    public static int DIVIDE(Stack<int> stack)
    {
        int first;
        int second;

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Math operation requires two operands.");

        first = stack.Pop();

        int ret = first / second;
        stack.Push(ret);
        return ret;
    }

    public static int DUP(Stack<int> stack)
    {
        if (stack.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty, cannot duplicate.");
        }
        int duplicate = stack.Peek();
        stack.Push(duplicate);
        return duplicate;
    }

    public static int DROP(Stack<int> stack)
    {
        if (stack.Count == 0)
            throw new InvalidOperationException("Stack is empty, cannot drop.");
        return stack.Pop();
    }

    public static int SWAP(Stack<int> stack)
    {
        int first;
        int second;
        if (stack.Count < 1)
            throw new InvalidOperationException("Swap requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Swap requires two operands.");

        first = stack.Pop();

        stack.Push(second);
        stack.Push(first);
        return first;
    }

    public static int OVER(Stack<int> stack)
    {
        int first;
        int second;

        if (stack.Count < 1)
            throw new InvalidOperationException("Over requires two operands.");

        second = stack.Pop();

        if (stack.Count < 1)
            throw new InvalidOperationException("Over requires two operands.");

        first = stack.Pop();

        stack.Push(first);
        stack.Push(second);
        stack.Push(first);

        return first;
    }
    
    public static int PRINT(Stack<int> stack)
    {
        if (stack.Count < 1)
            throw new InvalidOperationException("PRINT requires something be on the stack.");

        int first = stack.Pop();

        Console.WriteLine(first);

        return first;
    }
/*
    public static void Main(String[] args)
    {

        Stack<int> stack = new();
        // Initialize with the built-in functions.
        // They do not require a symbol table.
        Dictionary<string, FunctionBody> symbolTable = new()
        {
            { "dup", new FunctionBody(DUP, null) },
            { "drop", new FunctionBody(DROP, null) },
            { "swap", new FunctionBody(SWAP, null) },
            { "over", new FunctionBody(OVER, null) },
            { "+", new FunctionBody(ADD, null) },
            { "-", new FunctionBody(SUBTRACT, null) },
            { "*", new FunctionBody(MULTIPLY, null) },
            { "/", new FunctionBody(DIVIDE, null) },
            { ".", new FunctionBody(PRINT, null) },
        };

        bool EOF = false;
        while (!EOF)
        {

            string? instruction = Console.ReadLine();
            if (instruction == null)
                EOF = true;
            else
                ParseLine(instruction, stack, symbolTable);
        }
    }
*/
}

