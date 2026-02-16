#!/usr/bin/env node

// The above line is a shebang. On Unix-like operating systems, or environments,
// this will allow the script to be run by node, and thus turn this JavaScript
// file into an executable. In other words, to execute this file, you may run
// the following from your terminal:
//
// ./grep.js args
//
// If you don't have a Unix-like operating system or environment, for example
// Windows without WSL, you can use the following inside a window terminal,
// such as cmd.exe:
//
// node grep.js args
//
// Read more about shebangs here: https://en.wikipedia.org/wiki/Shebang_(Unix)

const fs = require('fs');
const path = require('path');

/**
 * Reads the given file and returns lines.
 *
 * This function works regardless of POSIX (LF) or windows (CRLF) encoding.
 *
 * @param {string} file path to file
 * @returns {string[]} the lines
 */
function readLines(file) {
  const data = fs.readFileSync(path.resolve(file), { encoding: 'utf-8' });
  return data.split(/\r?\n/);
}

const VALID_OPTIONS = [
  'n', // add line numbers
  'l', // print file names where pattern is found
  'i', // ignore case
  'v', // reverse files results
  'x', // match entire line
];

const ARGS = process.argv;

//
// This is only a SKELETON file for the 'Grep' exercise. It's been provided as a
// convenience to get you started writing code faster.
//
// This file should *not* export a function. Use ARGS to determine what to grep
// and use console.log(output) to write to the standard output.

let files = []
let flags = []
let searchString = ""
for(let arg of ARGS.slice(2))
{
  if (arg.startsWith('-') && arg.length === 2 && VALID_OPTIONS.includes(arg[1]))
    flags.push(arg[1])
  else if (searchString === "")
    searchString = arg
  else
    files.push(arg)
}

let singleFile = files.length === 1
let caseInsensitive = flags.includes('i')
let invertMatch = flags.includes('v')
let fullLine = flags.includes('x')
let onlyFilenames = flags.includes('l')
let showLineNumber = flags.includes('n')
let fileMatches = []

if (caseInsensitive)
  searchString = searchString.toLowerCase()

for(let fileName of files)
{
  let text = readLines(fileName)
  let anyMatch = false
  for(let i = 0; i < text.length; i++)
  {
    let line = text[i]
    if (caseInsensitive)    
      line = line.toLowerCase()
    let isMatch = false
    if (fullLine)
      isMatch = line === searchString
    else 
      isMatch = line.indexOf(searchString) >= 0

    if ((invertMatch && !isMatch) || (!invertMatch && isMatch))
    {
      anyMatch = true
      if (!onlyFilenames)
      {
        let output = ""
        if (!singleFile)
          output += `${fileName}:`
        if (showLineNumber)
          output += `${i + 1}:`
        output += text[i]
        console.log(output)
      }
    }
  }
  if (anyMatch && onlyFilenames)
    fileMatches.push(fileName)
}
if (fileMatches.length > 0)
{
  for(let match of fileMatches)
    console.log(match)
}