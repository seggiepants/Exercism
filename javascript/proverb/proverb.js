//
// This is only a SKELETON file for the 'Proverb' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const proverb = (...args) => {
  let  items = args
  let qualifier = ''
  let ret = []

  if (args.length === 0)
    return ''

  if (typeof args[args.length - 1] === 'object')
  {
    items = args.slice(0, -1)
    qualifier = args[args.length - 1].qualifier + ' '
  }
  
  let previous = ""
  for(let item of items)
  {
    if (previous !== '')
    {
      ret.push(`For want of a ${previous} the ${item} was lost.`)
    }
    previous = item
  }
  ret.push(`And all for the want of a ${qualifier}${items[0]}.`)
  return ret.join('\n')
};
