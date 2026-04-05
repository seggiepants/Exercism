// Change log:
// * remove unused function isTag
// * rename parse__ to parseBold
// * rename parse_ to parseItalics
// * rename parser(markdown, delimiter, tag) to parserSearchReplace(markdown, delimiter, tag)
// * changed parseHeader to match the #'s with a regular expression.
// * parse, parseHeader, parseParagraph puts the isList check inline so we don't repeat basically the same code twice.
// * parseText changed final if to an ternary
// * parseLineItem should look for '* ' not just '*' could be bold otherwise
// * parseBold speaking of which added support for ** text ** to parse as bold. I even added a test case locally


function wrap(text, tag) {
  return `<${tag}>${text}</${tag}>`;
}

function parserSearchReplace(markdown, delimiter, tag) {
  const pattern = new RegExp(`${delimiter}(.+)${delimiter}`);
  const replacement = `<${tag}>$1</${tag}>`;
  return markdown.replace(pattern, replacement);
}

function parseBold(markdown) {
  return parserSearchReplace(parserSearchReplace(markdown, '\\*\\*', 'strong'), '__', 'strong');
}

function parseItalics(markdown) {
  return parserSearchReplace(markdown, '_', 'em');
}

function parseText(markdown, list) {
  const parsedText = parseItalics(parseBold(markdown));
  return list ? parsedText : wrap(parsedText, 'p');  
}

function parseHeader(markdown, list) {
  let result = markdown.match(/^(#){1,6}\s/gm)
  let count = result !== null && result.length > 0 ? result[0].length - 1 : 0;

  if (count === 0 || count > 6) {
    return [null, list];
  }
  const headerTag = `h${count}`;
  const headerHtml = wrap(markdown.substring(count + 1), headerTag);
  return [`${list ? '</ul>' : ''}${headerHtml}`, false];  
}

function parseLineItem(markdown, list) {
  if (markdown.startsWith('* ')) {
    const innerHtml = wrap(parseText(markdown.substring(2), true), 'li');
    if (list) {
      return [innerHtml, true];
    } else {
      return [`<ul>${innerHtml}`, true];
    }
  }
  return [null, list];
}

function parseParagraph(markdown, list) {
  return [`${list ? '</ul>' : ''}${parseText(markdown, false)}`, false];
}

function parseLine(markdown, list) {
  let [result, inListAfter] = parseHeader(markdown, list);
  if (result === null) {
    [result, inListAfter] = parseLineItem(markdown, list);
  }
  if (result === null) {
    [result, inListAfter] = parseParagraph(markdown, list);
  }
  if (result === null) {
    throw new Error('Remove this line and implement the function');
  }
  return [result, inListAfter];
}

export function parse(markdown) {
  const lines = markdown.split('\n');
  let result = '';
  let list = false;
  for (let i = 0; i < lines.length; i++) {
    let [lineResult, newList] = parseLine(lines[i], list);
    result += lineResult;
    list = newList;
  }
  return `${result}${list ? '</ul>' : ''}`;
}
