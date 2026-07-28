// grep subset exercise.
package grep

import (
	"bytes"
	"os"
	"slices"
	"strconv"
	"strings"
)

// Search for a pattern within the given files and according to the flags.
// @param pattern: The string to search for
// @param flags: A slice of zero or more of the following flag strings.
//
//	-n Prepend the line number and a colon (':') to each line in the output, placing the number after the filename (if present).
//	-l Output only the names of the files that contain at least one matching line.
//	-i Match using a case-insensitive comparison.
//	-v Invert the program -- collect all lines that fail to match.
//	-x Search only for lines where the search string matches the entire line.
//
// @param files: slice of strings containing filenames search within
// @returns: slice of string with results.
func Search(pattern string, flags []string, files []string) []string {
	var flagLineNumber bool = slices.Contains(flags, "-n")
	var flagFileNameOnly bool = slices.Contains(flags, "-l")
	var flagCaseInsensitive bool = slices.Contains(flags, "-i")
	var flagInvert bool = slices.Contains(flags, "-v")
	var flagMatchWholeLine bool = slices.Contains(flags, "-x")
	var multipleFiles bool = len(files) > 1

	matches := make([]string, 0)

	var patternCmp string = pattern
	if flagCaseInsensitive {
		patternCmp = strings.ToLower(pattern)
	}

	for _, fileName := range files {
		text, err := ReadFile(fileName)
		if err == nil {
			for lineNum, line := range strings.Split(text, "\n") {
				if len(line) == 0 {
					continue
				}
				var match bool = false
				var lineCmp string = line
				if flagCaseInsensitive {
					lineCmp = strings.ToLower(line)
				}
				if (flagMatchWholeLine && lineCmp == patternCmp) || (!flagMatchWholeLine && strings.Contains(lineCmp, patternCmp)) {
					match = true
				}

				if flagInvert {
					match = !match
				}
				if match {
					if flagFileNameOnly {
						matches = append(matches, fileName)
						break
					}
					var prefix string = ""
					if multipleFiles {
						prefix = prefix + fileName + ":"
					}
					if flagLineNumber {
						prefix = prefix + strconv.Itoa(lineNum+1) + ":"
					}
					matches = append(matches, prefix+line)
				}
			}
		}
	}
	return matches
}

// Read a file from the filesystem and return the text as a string.
// @param filename: File to read.
// @returns: Text of the file in the string value otherwise the error from the open call.
func ReadFile(filename string) (string, error) {
	f, err := os.Open(filename)
	if err != nil {
		return "", err
	}
	defer f.Close()
	buf := new(bytes.Buffer)
	buf.ReadFrom(f)
	return buf.String(), nil
}
