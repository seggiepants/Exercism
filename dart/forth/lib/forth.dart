// Iterator for the Stack data type. Thought I could do this instead of 
// overriding the toList function in Stack but that didn't work out. 
// This class manages iterating over a stack (oldest to newest).
class StackIterator<T> implements Iterator<T> {
  late Stack<T> _data;
  late int _current;

  // Initialize the stack iterator for the given stack.
  // iteration starts at the oldest/first item in the stack storage.
  StackIterator(Stack<T> data) {
    _data = data;
    _current = 0;
  }

  // Return the current element of the stack iteration.
  // @returns: Element at the current index in the stack.
  @override
  T get current => _data._storage[_current];

  // Move to the next element in the stack.
  // @returns bool: False if at/past the end of the stack.
  @override
  bool moveNext() {
    _current++;
    return _current < _data.length;
  }
}

// Stack implementation. Templated so you can make a stack for any given data type.
class Stack<T> extends Iterable<T> {
  late List<T> _storage;

  // Initialize an empty stack.
  Stack() {
    _storage = [];
  }
  
  // Push a value onto the stack 
  // @param value: The value to add to the end of the stack.
  void push(T value) {
    _storage.add(value);
  }

  // Pop off a value from the stack and return it.
  // @returns T: The value at the end of the stack.
  T pop() {
    return _storage.removeLast();
  }

  // Look at what is at the end of the stack (top). You could call this peek as well.
  // @returns T: The value at the end of the stack.
  T top() => _storage.last;

  // Returns an iterator for the stack.
  @override
  Iterator<T> get iterator {
    return StackIterator<T>(this);
  }

  // Return the length of the stack.
  // @returns int: The length of the stack.
  @override
  int get length => _storage.length;

  // Return the stack as a list. Needed to make the tests happy.
  // @returns List<int>: Stack converted to a list of ints.
  @override List<T> toList({bool growable = true}) {
    return _storage;
  }
  
}

// An entry in the Symbol table.
class SymbolTableEntry{
  String name;
  Function value;

  // Intialize a new Symbol Table entry.
  // @param String name: The name of the function.
  // @param Function value: The body of the function.
  SymbolTableEntry({required this.name, required this.value});
}

// A symbol table for the small forth interpreter.
class SymbolTable {
  late List<SymbolTableEntry> _symbols;

  // Initialize a new symbol table.
  SymbolTable() {
    _symbols = [];
  }

  // Add a name and function body to the symbol table.
  // @param String name: The name of the function
  // @param Function value: The body of the function.
  void add({required String name, required Function value}) {
    _symbols.add(SymbolTableEntry(name: name, value: value));
  }

  // Return the length of the symbol table.
  // @returns int: The length of the symbol table.
  int length() => _symbols.length;

  // find a name in the symbol table, starting at lookback/end of list 
  // and iterating backwards to find it. Return null if not found.
  // @param String name: The symbol we are looking for
  // @param otpional int lookback: If not set will use the end of the symbol table. 
  // Where to start at when looking for previously defined symbols.
  Function? lookup(String name, [int? lookback = null]) {
    String target = name.toLowerCase();
    int begin = lookback != null ? lookback : length() - 1;
    for(int i = begin; i >= 0; i--) {
      if (target == _symbols[i].name) {
        return _symbols[i].value;
      }
    }
    return null;
  }
}

// A token in the program.
class Token {
  late String type;
  late var value;
  late int start;
  late int end;

  // Initialize a new Token instance.
  Token({required this.type, required this.value, required this.start, required this.end});

  // For debugging - output a token as a string.
  // @returns: String representation of the token.
  @override
  String toString() {
    return 'Type: ${this.type}, Value: ${this.value}, [${this.start.toString()} - ${this.end.toString()}]';
  }
}

// Forth interpreter (just a small subset).
class Forth {
  // Put your code here

  late Stack<int> _stack;
  late SymbolTable _symbols;

  // Setup a new instance of the forth interpreter. Restarts the stack and 
  // Sets up a new Symbol Table with the built-in functions.
  Forth() {
    _stack = Stack<int>();
    _symbols = new SymbolTable();
    _symbols.add(name: '+', value: Add);
    _symbols.add(name: '-', value: Subtract);
    _symbols.add(name: '*', value: Multiply);
    _symbols.add(name: '/', value: Divide);
    _symbols.add(name: 'dup', value: Dup);
    _symbols.add(name: 'drop', value: Drop);
    _symbols.add(name: 'swap', value: Swap);
    _symbols.add(name: 'over', value: Over);
  }
  
  // Is the stack in the desired state for this function call.
  // @param Stack stack: The runtime stack.
  // @param optional int requiredOperands: 2 if not specified. How many operands the function expects the stack to have.
  // @returns: True if ok.
  // @raises Exception: if the stack it not ok. You never really get a false returned.
  bool CheckStack(Stack<int> stack, [int requiredOperands = 2]) {
    if ((stack.length == 0) || (stack.length == 1 && requiredOperands == 2)) {
      throw new Exception('Stack empty');
    }
    return true;
  }

  // Interpret the given program. Stack and Symbol Table will maintain state
  // across calls.
  // @param String program: The text of the program to evaluate.
  // @returns: None but the stack and symbol table may be updated.
  void evaluate(String program) {
    if (program.length == 0) {
      return;  // do nothing on empty program.
    }

    int i = 0;
    bool done = false;
    while(!done) {
      Token token = GetToken(program, i);
      switch (token.type) {
        case 'Name':
        case 'Number':
          i = token.end;
          EvaluateToken(this._stack, this._symbols, token);
          break;
        case 'BeginDefinition':
          i = token.end;
          i = this.ParseDefinition(program, i);
          break;
        case 'EndDefinition':
          throw new Exception('Unexpected end of definition');
        case 'EOF':
          done = true;
          break;
      }
      if (i > program.length)
        done = true;
    }
  }

  // Evaluate a name or number token. (Just names and number so you can't nest a definition
  // inside of a definition). EOF is handled in the main evaluate function.
  // @param Stack stack: The runtime stack
  // @param SymbolTable symbols: The symbols (functions) available to the program.
  // @param Token token: The token to evaluate.
  // @param int? lookback = null: Null when not specified. Pointer in the symbol table to look back from so that a function 
  // looks at symbols that were defined when it was defined instead of those defined later. Default is end of the symbol table.
  // @returns: None, the stack will be updated with the result if any.
  void EvaluateToken(Stack<int> stack, SymbolTable symbols, Token token, [int? lookback = null]) {
    Function? fn = null;
    switch (token.type) {
    case 'Number':
      stack.push(token.value);
      break;
    case 'Name':
      fn = symbols.lookup(token.value, lookback);
      if (fn == null) {
        throw new Exception('Unknown command');
      }
      fn(stack);
      break;
    }
  }

  // Evaluate a list of tokens
  // @param Stack stack: The runtime stack
  // @param SymbolTable symbols: The symbols (functions) available to the program.
  // @param List<Token> tokens: The tokens being evaluated.
  // @param int? lookback = null: Null when not specified. Pointer in the symbol table to look back from so that a function 
  // looks at symbols that were defined when it was defined instead of those defined later. Default is end of the symbol table.
  // @returns: None, the stack will be updated with the result.
  void EvaluateTokens(Stack<int> stack, SymbolTable symbols, List<Token> tokens, [int? lookback = null]) {
    tokens.forEach((token) {
      EvaluateToken(stack, symbols, token, lookback);
    });
  }

  // Parse the next token and return it. Skipping white space.
  // @param String text: the program being evaluated.
  // @param int start: Where to continue from in the program.
  // @returns Token: The parsed non-whitespace token.
  // @raises Exception: When there is not recognized token found.
  Token GetToken(String text, int start)
  {
    RegExp reWS = RegExp(r'^\s+');                                     // white space
    RegExp reNum = RegExp(r'^-?\d+');                                  // numbers (integer only)
    RegExp reName = RegExp(r'^([a-zA-Z][a-zA-Z0-9_-]*|\+|-|\*|\/)');   // names
    int i = start;
    while (i < text.length)
    {
      String current = text.substring(i);
      RegExpMatch? match = reWS.firstMatch(current);
      if (match != null) {
        i += match[0]!.length;
        continue;
      }

      match = reNum.firstMatch(current);
      if (match != null) {
        int num = int.parse(match[0]!);
        int endIndex = i + match[0]!.length;
        return Token(
          type: 'Number',
          value: num,
          start: i,
          end: endIndex,
        );
      }

      if (text.substring(i, i + 1) == ':'){
        return Token(
          type: 'BeginDefinition',
          value: ':',
          start: i,
          end: i + 1,
        );
      }

      if (text.substring(i, i + 1) == ';')
      {
        return Token(
          type: 'EndDefinition',
          value: ';',
          start: i,
          end: i + 1,
        );
      }

      match = reName.firstMatch(current);
      if (match != null) {
        int endIndex = i + match[0]!.length;
        return Token( 
          type: 'Name',
          value: match[0],
          start: i,
          end: endIndex,
        );
      }

      throw new Exception('Unrecognized token at $i');

    }
    return Token(
      type: 'EOF',
      value: '',
      start: i,
      end: i
    );
  }  

  // Parse a word definition.
  // @param String program: The program text being evaluated.
  // @param int index: Where we are in the program.
  // @returns int: Where to continue from.
  // @raises Exception: Where there is an error unexpected token or EOF.
  int ParseDefinition(String program, int index) {
    List<Token> tokens = [];
    int i = index;

    // expect name
    Token token = GetToken(program, i);
    if (token.type != 'Name') {
      throw new Exception('Invalid definition');
    }      
    String name = token.value; 
    i = token.end;

    while (token.type != 'EndDefinition') {
      token = GetToken(program, i);
      if (token.type == 'EOF') {
        throw new Exception('Unexpected end of file');
      }
      i = token.end;
      if (token.type != 'EndDefinition') {
        tokens.add(token);
      }
    }

    if (token.type == 'EndDefinition') {
      // lookback is to only look at definitions from when the word was defined.
      // you can get some nasty infinite loops otherwise.
      int lookback = this._symbols.length() - 1;

      // save the word
      // the function to call it encapsulates the symbol table, tokens, and lookback and just calls evaluateTokens on them.
      this._symbols.add(
        name: name.toLowerCase(), 
        value: (Stack<int> stack) =>  EvaluateTokens(stack, _symbols, tokens, lookback)
      );
    }
    return i;
  }

  // Return a list of int representing the stack for the tests to evaluate.
  // @returns List<int>: Stack expressed as a list of integers.
  List<int> get stack {
    return _stack.toList();
  }

  // --------------------
  // Built-in functions
  // --------------------

  // Built-in function addition
  // @param Stack stack: The stack to operate on.
  void Add(Stack<int> stack) {
    CheckStack(stack);
    
    var b = stack.pop();
    var a = stack.pop();
    stack.push(a + b);
  }

  // Built-in function for subtraction
  // @param Stack stack: The stack to operate on.
  void Subtract(Stack<int> stack) {
    CheckStack(stack);

    var b = stack.pop();
    var a = stack.pop();
    stack.push(a - b);
  }

  // Built-in function for multiplication
  // @param Stack stack: The stack to operate on.
  void Multiply(Stack<int> stack) {
    CheckStack(stack);

    var b = stack.pop();
    var a = stack.pop();
    stack.push(a * b);
  }

  // Built-in function for division
  // @param Stack stack: The stack to operate on.
  void Divide(Stack<int> stack) {
    CheckStack(stack);

    var b = stack.pop();
    var a = stack.pop();
    if (b == 0) {
      throw new Exception('Division by zero');
    }
    stack.push((a / b).floor()); // remove the Math.floor for non-integer division
  }

  // Built-in function Dup
  // @param Stack stack: The stack to operate on.
  void Dup(Stack<int> stack) {
    CheckStack(stack, 1);
    var a = stack.pop();
    stack.push(a);
    stack.push(a);
  }

  // Built-in function Drop
  // @param Stack stack: The stack to operate on.
  void Drop(Stack<int> stack) {
    CheckStack(stack, 1);
    stack.pop();
  }

  // Built-in function Swap
  // @param Stack stack: The stack to operate on.
  void Swap(Stack<int> stack) {
    CheckStack(stack);
    var b = stack.pop();
    var a = stack.pop();
    stack.push(b);
    stack.push(a);
  }

  // Built-in function Over
  // @param Stack stack: The stack to operate on.
  void Over(Stack<int> stack) {
    CheckStack(stack);
    var b = stack.pop();
    var a = stack.pop();
  
    stack.push(a);
    stack.push(b);
    stack.push(a);
  }

}
