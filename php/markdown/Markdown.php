<?php
// Markdown parser refactor exercise.

/* Change Log:
 * Add Function comments
 * Changed Header function to one regex instead of 6 and changed 7+ to paragraph in the else clause
 * Changed strong, em, and li to regex replacements.
 * ul is added to the html after the lines are rejoined as another regex replacement.
 * p is added to lines that are not a header or line item.
 * I think I finally got it pretty.
*/

declare(strict_types=1);

// Parse a string of markdown converting it into html
// @param $markdown: The markdown text to parse.
// @returns: html version of the markdown text.
function parseMarkdown($markdown)
{
    $lines = explode("\n", $markdown);

    foreach ($lines as &$line) {
        // # repeats one to six times followed by space then whatever to end of line.
        if (preg_match("/^#{1,6} .*$/", $line, $matches)) {
            $idx = strpos($matches[0], " ");
            if ($idx >= 0 && $idx <= 6) {
                $line = sprintf("<h%d>%s</h%d>", $idx, substr($matches[0], $idx + 1), $idx);
            } else {
                $line = "<p>" . trim($line) . "</p>";            
            }
        }
        
        // word boundary then 1 or 2 _'s followed by not end of line and ending with same 1 to 2 underscores
        $line = preg_replace("/\b(__)([^\n_]+)(__)\b/", "<strong>$2</strong>", $line);
        $line = preg_replace("/\b(_)([^\n_]+)(_)\b/", "<em>$2</em>", $line);
        // starts with "* " then whatever to end of line.
        $line = preg_replace("/^\* (.*)$/", "<li>$1</li>", $line);        
        // Add a paragraph if not a header line, or a list item 
        if (!preg_match("/^<h\d+>(.+)<\/h\d>$/", $line) &&
            !preg_match("/^<li>(.+)<\/li>$/", $line)) {
            $line = "<p>$line</p>";
        }
    }    
    $html = join($lines);
    
    // add unordered list ul grouping to 1 or more rows of li.
    $html = preg_replace("/(?m)(<li>.+<\/li>\n?)+/", "<ul>$0</ul>", $html);
    return $html;
}