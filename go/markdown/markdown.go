// Markdown refactor exercise
package markdown

// implementation to refactor
// * Changed bold, and italics to do regular expression replacement.
// * Regex replacement for list, list item, and headers too.
// * I have two Regular Expressions tell me where all the start and end tags are.
//   Write out any sequences that aren't in a stack of tags as a paragraph.
// * Sorry pretty much a rewrite.

import (
	"fmt"
	"regexp"
	"strings"
)

// Translates markdown subset to HTML
// @param markdown: Markdown text to transform
// @returns: html version of the input markdown
func Render(markdown string) string {
	reEm := regexp.MustCompile(`\b_[^\n_]+_\b`)
	reStrong := regexp.MustCompile(`\b__[^\n_]+__\b`)
	reHeader := regexp.MustCompile(`(?m)^#{1,6} .*$`)
	reListItem := regexp.MustCompile(`(?m)^\* .*$`)
	reList := regexp.MustCompile(`(?m)(<li>.+</li>\n?)+`)
	reBeginTag := regexp.MustCompile(`(?m)<(li|h1|h2|h3|h4|h5|h6|ul)>`)
	reEndTag := regexp.MustCompile(`(?m)</(li|h1|h2|h3|h4|h5|h6|ul)>`)

	html := reStrong.ReplaceAllStringFunc(markdown, func(match string) string {
		return "<strong>" + match[2:len(match)-2] + "</strong>"
	})

	html = reEm.ReplaceAllStringFunc(html, func(match string) string {
		return "<em>" + match[1:len(match)-1] + "</em>"
	})

	html = reHeader.ReplaceAllStringFunc(html, func(match string) string {
		idx := strings.Index(match, " ")
		if idx < 0 {
			return match
		}
		return fmt.Sprintf("<h%d>%s</h%d>", idx, match[idx+1:], idx)
	})

	html = reListItem.ReplaceAllStringFunc(html, func(match string) string {
		return fmt.Sprintf("<li>%s</li>", match[2:])
	})

	html = reList.ReplaceAllStringFunc(html, func(match string) string {
		return fmt.Sprintf("<ul>%s</ul>", match)
	})

	builder := strings.Builder{}
	tags := make([]string, 0)
	tagStart := reBeginTag.FindAllStringIndex(html, -1)
	tagEnd := reEndTag.FindAllStringIndex(html, -1)
	var pos, startTag, endTag, minTag, indexStart, indexEnd int
	for indexStart < len(tagStart) || indexEnd < len(tagEnd) {
		if indexStart < len(tagStart) {
			startTag = tagStart[indexStart][0]
		} else {
			startTag = len(html)
		}
		if indexEnd < len(tagEnd) {
			endTag = tagEnd[indexEnd][0]
		} else {
			endTag = len(html)
		}
		minTag = min(startTag, endTag)

		if pos < startTag && pos < endTag && len(tags) == 0 {
			builder.WriteString("<p>")
			builder.WriteString(html[pos : minTag-1])
			builder.WriteString("</p>")
			pos = minTag
		}

		if startTag < endTag && startTag < len(html) {
			tag := html[tagStart[indexStart][0]+1 : tagStart[indexStart][1]-1]
			tags = append(tags, tag)
			indexStart++
		}
		if endTag < startTag && endTag < len(html) {
			tag := html[tagEnd[indexEnd][0]+2 : tagEnd[indexEnd][1]-1]
			// pop off tag from stack if matched
			if len(tags) > 0 && tags[len(tags)-1] == tag {
				tags = tags[:len(tags)-1]
			}
			if len(tags) == 0 {
				builder.WriteString(html[pos:tagEnd[indexEnd][1]])
				pos = tagEnd[indexEnd][1] + 1
			}
			indexEnd++
		}
	}
	if len(tags) == 0 && pos < len(html)-1 {
		builder.WriteString("<p>")
		start := max(0, pos-1)
		builder.WriteString(html[start:])
		builder.WriteString("</p>")
	}
	return strings.ReplaceAll(builder.String(), "\n", "") // my code left line breaks behind.
}
