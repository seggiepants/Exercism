"""
Parse a markdown file
"""

import re

def parse(markdown):
    """Parse the given markdown
    :param markdown the markdown text
    """
    lines = markdown.split('\n')
    res = ''
    in_list = False
    # rename i to line
    for line in lines:
        current_line = line
        # Search for a heading
        match = re.match('#+ (.*)', current_line)
        # More generic sub heading code that handles 1-6
        # and stops at 6 if you go over.
        if match and len(match.groups()) > 0:
            heading_num = len(match.string) - len(match.groups()[0]) - 1
            #output 
            if 1 <= heading_num <= 6:
                current_line = f"<h{heading_num}>" + match.group(1) + f"</h{heading_num}>"

        # Search for bold (<strong>)
        # regex is saying anything zero or more characters followed by __ another set 
        # of zero or more characters followed by __ followed again by zero or more characters.
        # example: This is a __Test__ only a test.
        current_line = wrap_tag(current_line, '(.*)__(.*)__(.*)', 'strong')
        
        # Search for italics (<em>)
        # regex looks for anything _ anything _ anything
        current_line = wrap_tag(current_line, '(.*)_(.*)_(.*)', 'em')

        matched = re.match(r'\* (.*)', current_line)
        if matched:
            if not in_list:
                in_list = True
                res = res + '<ul>'
            curr = matched.group(1)
            current_line = '<li>' + curr + '</li>'
        else:
            if in_list:
                res = res + '</ul>'
                in_list = False

        matched = re.match('<h|<ul|<p|<li', current_line)
        if not matched:
            current_line = '<p>' + current_line + '</p>'        
                
        res += current_line
    # Add the closing list tag, if we finished the text while in the middle of a list.
    if in_list:
        res += '</ul>'
    return res

def wrap_tag(line, regular_expression, tag):
    """
    Wrap a line with a given tag if a regular expression matches.
    param line - the line to process
    :param regular_expression - the regular expression to use to find text to replace.
    :param tag - the tag to wrap a match in. Does not include < or >
    """
    match = re.match(regular_expression, line)
    output = line
    if match:
        output = match.group(1) + f'<{tag}>' + match.group(2) + f'</{tag}>' + match.group(3)
        # look for more matches recursively
        output = wrap_tag(output, regular_expression, tag)
    return output