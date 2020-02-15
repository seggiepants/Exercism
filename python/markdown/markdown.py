import re

def parse(markdown):
    lines = markdown.split('\n')
    res = ''
    in_list = False
    # rename i to line
    for line in lines:
        i = line
        # Search for a heading
        match = re.match("#+ (.*)", i)
        # More generic sub heading code that handles 1-6
        # and stops at 6 if you go over.
        if (match and len(match.groups()) > 0):
            headingNum = min(6, len(match.string) - len(match.groups()[0]) - 1)
            #output 
            i = f"<h{headingNum}>" + match.group(1) + f"</h{headingNum}>"

        # Search for bold (<strong>)
        # regex is saying anything zero or more characters followed by __ another set 
        # of zero or more characters followed by __ followed again by zero or more characters.
        # example: This is a __Test__ only a test.
        i = wrap_tag(i, '(.*)__(.*)__(.*)', 'strong')
        
        # Search for italics (<em>)
        # regex looks for anything _ anything _ anything
        i = wrap_tag(i, '(.*)_(.*)_(.*)', 'em')

        m = re.match(r'\* (.*)', i)
        if m:
            if not in_list:
                in_list = True
                res = res + '<ul>'
            curr = m.group(1)
            i = '<li>' + curr + '</li>'
        else:
            if in_list:
                res = res + '</ul>'
                in_list = False

        m = re.match('<h|<ul|<p|<li', i)
        if not m:
            i = '<p>' + i + '</p>'        
                
        res += i
    # Add the closing list tag, if we finished the text while in the middle of a list.
    if in_list:
        res += '</ul>'
    return res

def wrap_tag(line, regular_expression, tag):
    # line - the line to process
    # regular_expression - the regular expression to use to find text to replace.
    # tag - the tag to wrap a match in. Does not include < or >
    match = re.match(regular_expression, line)
    output = line
    if match:
        output = match.group(1) + f'<{tag}>' + match.group(2) + f'</{tag}>' + match.group(3)
        # look for more matches recursively
        output = wrap_tag(output, regular_expression, tag)
    return output