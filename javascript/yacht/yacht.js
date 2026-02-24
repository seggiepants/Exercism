//
// This is only a SKELETON file for the 'Yacht' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const score = (cards, scoreType) => {
  const counts = {1: 0, 2: 0, 3: 0, 4: 0, 5: 0, 6: 0}
  cards.map((card) => counts[card] += 1)
  switch(scoreType)
  {
    case 'ones':
      return Number(counts[1]) * 1
    case 'twos':
      return Number(counts[2]) * 2
    case 'threes':
      return Number(counts[3]) * 3
    case 'fours':
      return Number(counts[4]) * 4
    case 'fives':
      return Number(counts[5]) * 5
    case 'sixes':
      return Number(counts[6]) * 6
    case 'full house':
    {
      const three = (Number((Object.entries(counts).filter((card) => card[1] === 3)[0] || [0, 0])[0]) * 3)
      const two = (Number((Object.entries(counts).filter((card) => card[1] === 2)[0] || [0, 0])[0]) * 2)
      return  three !== 0 && two !== 0 ? three + two : 0             
    }
    case 'four of a kind':
      return Number((Object.entries(counts).filter((card) => card[1] >= 4)[0] || [0, 0])[0]) * 4
    case 'little straight':
      return counts['1'] === 1 && counts['2'] === 1 && counts['3'] === 1 && counts['4'] === 1 && counts['5'] === 1 ? 30 : 0
    case 'big straight':
      return counts['2'] === 1 && counts['3'] === 1 && counts['4'] === 1 && counts['5'] === 1 && counts['6'] === 1 ? 30 : 0
    case 'choice':
      return cards.reduce((accumulator, card) => accumulator + card, 0)
    case 'yacht':
      return (Object.entries(counts).filter((card) => card[1] === 5)[0] || [0, 0])[1] === 0 ? 0 : 50
  }
  return 0
};
