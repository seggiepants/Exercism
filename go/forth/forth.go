// Forth subset interpreter exercise
package forth

import (
	"errors"
	"regexp"
	"strconv"
	"strings"
)

type Symbol struct {
	Name     string
	Tokens   []*Token
	Lookback int
}

var BuiltIn map[string]func(*Stack) error = map[string]func(*Stack) error{
	"+":    add,
	"-":    subtract,
	"*":    multiply,
	"/":    divide,
	"dup":  dup,
	"drop": drop,
	"swap": swap,
	"over": over,
}

type Stack struct {
	data []int
}

// Create a new Stack
// @returns: pointer to new stack
func NewStack() *Stack {
	return &Stack{data: make([]int, 0)}
}

// Add a value to the stack
// @param value: The value to add
func (s *Stack) Push(value int) {
	s.data = append(s.data, value)
}

// Remove and return the value at the top of the stack
// @returns: Integer at the top of the stack - mutates the stack
// @raises: Error if no data to pop
func (s *Stack) Pop() (int, error) {
	if len(s.data) == 0 {
		return 0, errors.New("No data to pop from the stack")
	}
	keep := len(s.data) - 1
	result := s.data[keep]
	s.data = s.data[:keep]
	return result, nil
}

// Returns the number of items on the stack
// @returns: Int - Number of items on the stack.
func (s *Stack) Count() int {
	return len(s.data)
}

type Runtime struct {
	Symbols []*Symbol
	Stack   *Stack
}

// Create a new Runtime struct
// @returns: Pointer to new initialized runtime struct
func NewRuntime() *Runtime {
	return &Runtime{
		Stack:   NewStack(),
		Symbols: make([]*Symbol, 0),
	}
}

type Token struct {
	Kind  string
	Value string
	Start int
	End   int
}

// Evaluate a series of forth expressions.
// @param input: slice of strings with one forth expression per string.
// @returns: The final value of the stack
// @raises: Error is one encountered during parsing/execution.
func Forth(input []string) ([]int, error) {
	runtime := NewRuntime()
	if len(input) == 0 {
		return runtime.Stack.data, nil
	}

	for _, program := range input {
		i := 0
		done := false
		for !done {
			token, err := GetToken(program, i)
			if err != nil {
				return runtime.Stack.data, err
			}
			switch token.Kind {
			case "Name":
				fallthrough
			case "Number":
				i = token.End
				err := EvaluateToken(runtime, token, -1)
				if err != nil {
					return runtime.Stack.data, err
				}
			case "BeginDefinition":
				i = token.End
				i, err = ParseDefinition(program, i, runtime)
				if err != nil {
					return runtime.Stack.data, err
				}
			case "EndDefinition":
				return runtime.Stack.data, errors.New("Unexpected end of definition")
			case "EOF":
				done = true
			}
			if i > len(program) {
				done = true
			}
		}
	}

	return runtime.Stack.data, nil
}

// Is the stack in the desired state for this function call.
// @param stack: The stack to check
// @param requiredOperands: You must have this many operands on the stack
// @raises: Error if insufficient operands.
func checkStack(stack *Stack, requiredOperands int) error {
	size := stack.Count()
	if size == 0 {
		return errors.New("Stack empty")
	} else if size == 1 && requiredOperands == 2 {
		return errors.New("Only one value on the stack")
	}
	return nil
}

// Evaluate a name or number token. (Just names and number so you can't nest a definition
// inside of a definition). EOF is handled in the main evaluate function.
// @param runtime: The program runtime
// @param token: The token to evaluate
// @param lookback: Searching for definitions should start here. For < 0 start at end of symbol table.
// @raises: Error when parsing a number that isn't one, an unknown command or one bubbled up from calls
func EvaluateToken(runtime *Runtime, token *Token, lookback int) error {
	if lookback < 0 {
		lookback = len(runtime.Symbols) - 1
	}

	switch token.Kind {
	case "Number":
		value, err := strconv.Atoi(token.Value)
		if err != nil {
			return err
		}
		runtime.Stack.Push(value)
	case "Name":
		fn := FindName(runtime.Symbols, token.Value, lookback)
		if fn == nil {
			builtIn, ok := BuiltIn[strings.ToLower(token.Value)]
			if !ok {
				return errors.New("Unknown command: " + token.Value)
			}
			err := builtIn(runtime.Stack)
			if err != nil {
				return err
			}
		} else {
			err := EvaluateTokens(runtime, fn.Tokens, fn.Lookback)
			if err != nil {
				return err
			}
		}
	}
	return nil
}

// Evaluate a slice of tokens
// @param runtime: The runtime the program should run under.
// @param tokens: The slice of tokens to run
// @param lookback: Symbol table lookback so the call finds definitions that are
// correct from when it was evaluated originally
func EvaluateTokens(runtime *Runtime, tokens []*Token, lookback int) error {
	if lookback < 0 {
		lookback = len(runtime.Symbols) - 1
	}
	for _, token := range tokens {
		err := EvaluateToken(runtime, token, lookback)
		if err != nil {
			return err
		}
	}
	return nil
}

// Find a name in the symbol table, starting at lookback/end of list
// and iterating backwards to find it. Return nil if not found.
// @param symbols: The symbol table
// @param name: The name we are looking for
// @param lookback: Offset in the symbol table if needed. Set to -1 if not needed.
func FindName(symbols []*Symbol, name string, lookback int) *Symbol {
	if lookback < 0 {
		lookback = len(symbols) - 1
	}
	target := strings.ToLower(name)
	for i := lookback; i >= 0; i-- {
		if target == symbols[i].Name {
			return symbols[i]
		}
	}
	return nil
}

// Get the next token from the input.
// @param text: The text we are parsing
// @param start: Current position in the text
// @returns: Pointer to generated token
// @raises: Error when there is no recognized token
func GetToken(text string, start int) (*Token, error) {
	reWS := regexp.MustCompile(`^\s+`)                                   // white space
	reNum := regexp.MustCompile(`^-?\d+`)                                // numbers (integer only)
	reName := regexp.MustCompile(`^([a-zA-Z][a-zA-Z0-9_-]*|\+|-|\*|\/)`) // names
	i := start
	for i < len(text) {
		current := text[i:]
		ret := reWS.FindStringIndex(current)
		if ret != nil {
			i += (ret[1] - ret[0])
			continue
		}

		ret = reNum.FindStringIndex(current)
		if ret != nil {
			num := current[ret[0]:ret[1]]
			endIndex := i + ret[1] - ret[0]
			return &Token{
				Kind:  "Number",
				Value: num,
				Start: i,
				End:   endIndex,
			}, nil
		}

		if text[i] == ':' {
			return &Token{
				Kind:  "BeginDefinition",
				Value: ":",
				Start: i,
				End:   i + 1,
			}, nil
		}

		if text[i] == ';' {
			return &Token{
				Kind:  "EndDefinition",
				Value: ";",
				Start: i,
				End:   i + 1,
			}, nil
		}

		ret = reName.FindStringIndex(current)
		if ret != nil {
			endIndex := i + ret[1] - ret[0]
			return &Token{
				Kind:  "Name",
				Value: current[ret[0]:ret[1]],
				Start: i,
				End:   endIndex,
			}, nil
		}

		return nil, errors.New("Unrecognized token at " + strconv.Itoa(i))
	}

	return &Token{
		Kind:  "EOF",
		Value: "",
		Start: i,
		End:   i,
	}, nil
}

// Parse a function definition. Add it to the symbol table if found
// @param program: The forth code being parsed
// @param index: Location in the forth code
// @param runtime: The program runtime (this has the symbol table)
// @returns: index in the program after parsing
// @raises: Error for Invalid Definition, Unexpected End of File of bubbled up error
func ParseDefinition(program string, index int, runtime *Runtime) (int, error) {
	tokens := make([]*Token, 0)
	i := index
	var token *Token
	var err error

	// expect name
	token, err = GetToken(program, i)
	if err != nil {
		return index, err
	}

	if token.Kind != "Name" {
		return index, errors.New("Invalid definition")
	}
	name := token.Value
	i = token.End

	for token.Kind != "EndDefinition" {
		token, err = GetToken(program, i)
		if err != nil {
			return index, err
		}
		if token.Kind == "EOF" {
			return index, errors.New("Unexpected end of file")
		}
		i = token.End
		if token.Kind != "EndDefinition" {
			tokens = append(tokens, token)
		}
	}

	if token.Kind == "EndDefinition" {
		// lookback is to only look at definitions from when the word was defined.
		// you can get some nasty infinite loops otherwise.
		lookback := len(runtime.Symbols) - 1

		// save the word
		// the function to call it encapsulates the symbol table, tokens, and lookback and just calls evaluateTokens on them.
		runtime.Symbols = append(runtime.Symbols, &Symbol{Name: strings.ToLower(name),
			Tokens:   tokens,
			Lookback: lookback,
		})
	}
	return i, nil
}

// --------------------
// Built-in functions
// --------------------

// Pop and add the top two numbers on the stack and put the result back onto the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func add(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}
	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	stack.Push(a + b)
	return nil
}

// Pop and subtract the top number on the stack from the second number on the stack and put the result back onto the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func subtract(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}

	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	stack.Push(a - b)
	return nil
}

// Pop and multiply the top two numbers on the stack and put the result back onto the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func multiply(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}

	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	stack.Push(a * b)
	return nil
}

// Pop and divide second number on the stack by the first and put the result back onto the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func divide(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}

	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	if b == 0 {
		return errors.New("Division by zero")
	}
	stack.Push(a / b)
	return nil
}

// Push a copy of the top item onto the stack
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least one value.
func dup(stack *Stack) error {
	err := checkStack(stack, 1)
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	stack.Push(a)
	stack.Push(a)
	return nil
}

// Remove the top number from the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least one value.
func drop(stack *Stack) error {
	err := checkStack(stack, 1)
	if err != nil {
		return err
	}
	_, err = stack.Pop()
	return err
}

// Switch the top two numbers on the stack.
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func swap(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}
	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}
	stack.Push(b)
	stack.Push(a)
	return nil
}

// Push a copy of the second item in the stack to the top
// @param stack: The stack to read/write data to.
// @raises: Error if stack doesn't have at least two values.
func over(stack *Stack) error {
	err := checkStack(stack, 2)
	if err != nil {
		return err
	}
	b, err := stack.Pop()
	if err != nil {
		return err
	}
	a, err := stack.Pop()
	if err != nil {
		return err
	}

	stack.Push(a)
	stack.Push(b)
	stack.Push(a)
	return nil
}
