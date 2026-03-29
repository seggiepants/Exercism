//
// This is only a SKELETON file for the 'Rail Fence Cipher' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const encode = (message, numRails) => {
  let rails = Array.from({ length: numRails }, () => []);
  let zigZag = {railNum: 0, dy: 1, numRails: numRails}

  for(let ch of message) 
  {
    rails[zigZag.railNum].push(ch)
    bounce(zigZag)   
  }
  return rails.map((rail) => rail.join("")).join("")
};

export const decode = (message, numRails) => {
  let railCounts = Array.from({ length: numRails }).fill(0);
  let zigZag = {railNum: 0, dy: 1, numRails: numRails}

  // Make a pass through as if encoding just to get the row lengths.
  for(let i = 0; i < message.length; i++) 
  {
    railCounts[zigZag.railNum] += 1
    bounce(zigZag)
  }  

  // Chop up the message into rows of lengths found
  let rails = Array.from({ length: numRails });
  let offset = 0  
  for (let i = 0; i < numRails; i++)
  {
    rails[i] = message.substring(offset, offset + railCounts[i]).split('')
    offset += railCounts[i]
  }

  // Reassemble the message.
  zigZag.railNum = 0
  zigZag.dy = 1
  let result = ""

  for(let i = 0; i < message.length; i++) 
  {
    result += rails[zigZag.railNum].shift()
    bounce(zigZag)
  }  
  return result

};

const bounce = (zigZag) => {

    zigZag.railNum += zigZag.dy
    if (zigZag.railNum < 0)
    {
      zigZag.railNum = 1
      zigZag.dy = 1
    }
    else if (zigZag.railNum >= zigZag.numRails)
    {
      zigZag.railNum = zigZag.numRails - 2
      zigZag.dy = -1
    }
}
