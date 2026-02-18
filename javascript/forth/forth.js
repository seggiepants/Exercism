//
// This is only a SKELETON file for the 'Forth' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Forth {
  constructor() {
    this._stack = []
    // Initialize the built in functions with their proper names
    this._symbols = [
      {name: '+', value: add},
      {name: '-', value: subtract},
      {name: '*', value: multiply},
      {name: '/', value: divide},
      {name: 'dup', value: dup},
      {name: 'drop', value: drop},
      {name: 'swap', value: swap},
      {name: 'over', value: over},
    ]    
  }

  // Interpret the given program. Stack and Symbol Table will maintain state
  // across calls.
  evaluate(program) {
    if (program.length === 0)
      return  // do nothing on empty program.

    let i = 0
    let done = false 
    while(!done)
    {
      let token = this.getToken(program, i)
      switch (token.type)
      {
        case 'Name':
        case 'Number':
          i = token.end
          evaluateToken(this._stack, this._symbols, token)
          break;
        case 'BeginDefinition':
          i = token.end
          i = this.parseDefinition(program, i)
          break
        case 'EndDefinition':
          throw new Error('Unexpected end of definition')
        case 'EOF':
          done = true
          break;
      }
      if (i > program.length)
        done = true
    }

  }

  get stack() {
    return this._stack
  }

  getToken(text, start)
  {
    let reWS = /^\s+/                                     // white space
    let reNum = /^-?\d+/                                  // numbers (integer only)
    let reName = /^([a-zA-Z][a-zA-Z0-9_-]*|\+|-|\*|\/)/   // names
    let i = start 
    while (i < text.length)
    {
      let current = text.substring(i)
      let ret = reWS.exec(current)
      if (ret !== null)
      {
        i += ret[0].length
        continue
      }

      ret = reNum.exec(current)
      if (ret !== null)
      {
        let num = Number(ret[0])
        let endIndex = i + ret[0].length
        return { 
          type: 'Number',
          value: num,
          start: i,
          end: endIndex,
        }    
      }

      if (text[i] === ':')
      {
        return {
          type: 'BeginDefinition',
          value: ':',
          start: i,
          end: i + 1,
        }
      }

      if (text[i] === ';')
      {
        return {
          type: 'EndDefinition',
          value: ';',
          start: i,
          end: i + 1,
        }
      }

      ret = reName.exec(current)
      if (ret !== null)
      {
        let endIndex = i + ret[0].length
        return { 
          type: 'Name',
          value: ret[0],
          start: i,
          end: endIndex,
        }    
      }

      throw new Error(`Unrecognized token at ${i}`)

    }
    return {
      type: 'EOF',
      value: '',
      start: i,
      end: i
    }
  }  

  parseDefinition(program, index)
  {
    let tokens = []
    let i = index

    // expect name
    let token = this.getToken(program, i)
    if (token.type !== 'Name')
      throw new Error('Invalid definition')
    let name = token.value 
    i = token.end 

    while (token.type !== 'EndDefinition')
    {
      token = this.getToken(program, i)
      if (token.type === 'EOF') 
        throw new Error('Unexpected end of file')
      i = token.end
      if (token.type !== 'EndDefinition')
        tokens.push(token)
    }

    if (token.type === 'EndDefinition')
    {
      // lookback is to only look at definitions from when the word was defined.
      // you can get some nasty infinite loops otherwise.
      let lookback = this._symbols.length - 1

      // save the word
      // the function to call it encapsulates the symbol table, tokens, and lookback and just calls evaluateTokens on them.
      this._symbols.push({name: name.toLowerCase(), 
        value: (stack) => 
          evaluateTokens(stack, this._symbols, tokens, lookback)
      })
    }
    return i
  }
}

// Is the stack in the desired state for this function call.
function checkStack(stack, requiredOperands = 2)
{
  if (stack.length === 0)
    throw new Error('Stack empty')
  else if (stack.length === 1 && requiredOperands === 2)
    throw new Error('Only one value on the stack')

  return true 
}

// find a name in the symbol table, starting at lookback/end of list 
// and iterating backwards to find it. Return null if not found.
function findName(symbols, name, lookback = null) {
  let target = name.toLowerCase()
  let begin = lookback || symbols.length - 1
  for(let i = begin; i >= 0; i--)
  {
    if (target === symbols[i].name)
      return symbols[i].value 
  }
  return null
}

// Evaluate a name or number token. (Just names and number so you can't nest a definition
// inside of a definition). EOF is handled in the main evaluate function.
function evaluateToken(stack, symbols, token, lookback = null) {
  let fn = null
  switch (token.type)
  {
    case 'Number':
      stack.push(token.value)
      break;
    case 'Name':
      fn = findName(symbols, token.value, lookback)
      if (fn === null)
        throw new Error('Unknown command')
      fn(stack)
      break      
  }
}

// Evaluate a list of tokens
function evaluateTokens(stack, symbols, tokens, lookback = null)
{
  for(let token of tokens)
  {
    evaluateToken(stack, symbols, token, lookback)
  }
}

// --------------------
// Built-in functions
// --------------------
function add(stack)
{
  checkStack(stack)

  let b = stack.pop()
  let a = stack.pop()
  stack.push(a + b)
}

function subtract(stack)
{
  checkStack(stack)

  let b = stack.pop()
  let a = stack.pop()
  stack.push(a - b)
}

function multiply(stack)
{
  checkStack(stack)

  let b = stack.pop()
  let a = stack.pop()
  stack.push(a * b)
}

function divide(stack)
{
  checkStack(stack)

  let b = stack.pop()
  let a = stack.pop()
  if (b === 0)
    throw new Error('Division by zero')
  stack.push(Math.floor(a / b)) // remove the Math.floor for non-integer division
}

function dup(stack)
{
  checkStack(stack, 1)
  let a = stack.pop()
  stack.push(a)
  stack.push(a)
}

function drop(stack)
{
  checkStack(stack, 1)
  stack.pop()
}

function swap(stack)
{
  checkStack(stack)
  let b = stack.pop()
  let a = stack.pop()
  stack.push(b)
  stack.push(a)
}

function over(stack)
{
  checkStack(stack)
  let b = stack.pop()
  let a = stack.pop()
  
  stack.push(a)
  stack.push(b)
  stack.push(a)
}

