package parsinglogfiles

import (
	"fmt"
	"regexp"
)

func IsValidLine(text string) bool {
	re := regexp.MustCompile(`^\[(TRC|DBG|INF|WRN|ERR|FTL)\]`)
	return re.MatchString(text)
}

func SplitLogLine(text string) []string {
	re := regexp.MustCompile(`\<[~\*-=]*\>`)
	return re.Split(text, -1)
}

func CountQuotedPasswords(lines []string) int {
	re := regexp.MustCompile(`(?i)\".*password.*\"|\'.*password.*\'`)
	passwordCount := 0
	for _, line := range lines {
		if re.MatchString(line) {
			passwordCount++
		}
	}
	return passwordCount
}

func RemoveEndOfLineText(text string) string {
	re := regexp.MustCompile(`end-of-line\d+`)
	return re.ReplaceAllString(text, "")
}

func TagWithUserName(lines []string) []string {
	re := regexp.MustCompile(`User +([a-zA-Z][a-zA-Z0-9]*) `)
	var result []string
	for _, line := range lines {
		find_result := re.FindStringSubmatch(line)
		if len(find_result) > 0 {
			result = append(result, fmt.Sprintf("[USR] %s %s", find_result[1], line))
		} else {
			result = append(result, line)
		}
	}
	return result
}
