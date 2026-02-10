//
// This is only a SKELETON file for the 'Camicia' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const royalty = {
  'J': 1,
  'Q': 2, 
  'K': 3,
  'A': 4,
}

export const simulateGame = (playerA, playerB) => {
  let ret = { status: '', cards: 0, tricks: 0 }; 
  let moves = new Set()
  let pile = []
  let turn = 0 // will use 1 - turn to toggle between 0 and 1.
  let flag = [0, 0]
  
  moves.add(state2String(playerA, playerB, pile))
  while (ret.status.length === 0)
  {
    let card = nextCard(turn, playerA, playerB)
    flag[turn] = 1
    if (card === undefined)
      {
      const result = undefinedCard(1 - turn, playerA, playerB, pile, ret)
      playerA = result.playerA
      playerB = result.playerB
      pile = result.pile
    }
    else 
    {
      pile.push(card)
      ret.cards++      
    }
    if (card in royalty)
    {
      let penalty = royalty[card]
      let recipient = turn
      turn = 1 - turn
      while (penalty > 0)
      {
        let next = nextCard(turn, playerA, playerB)
        flag[turn] = 1
        if (next === undefined)
        {
          const result = undefinedCard(recipient, playerA, playerB, pile, ret)
          playerA = result.playerA
          playerB = result.playerB
          pile = result.pile
          break;
        }
        else
        {
          pile.push(next)
          ret.cards++
          penalty--
        }
        if (next in royalty)
        {          
          penalty = royalty[next]
          recipient = turn
          turn = 1 - turn
        }
      }
      if (penalty === 0)
      {
        const result = collectPile(recipient, playerA, playerB, pile, ret)        
        playerA = result.playerA
        playerB = result.playerB
        pile = result.pile
        turn = 1 - recipient
      }
    }
    turn = 1 - turn
    if (flag[0] === flag[1] && flag[0] === 1)
    {
      updateMoves(playerA, playerB, pile, moves, ret)
      flag[0] = 0
      flag[1] = 0
    }
    isFinished(playerA, playerB, pile, ret)
  }
 return ret
}

function collectPile(turn, playerA, playerB, pile, status)
{
  turn === 0 ? playerA = playerA.concat(pile) : playerB = playerB.concat(pile)
  status.tricks++
  pile = []
  return {playerA, playerB, pile}
}

function isFinished(playerA, playerB, pile, ret)
{
  const FINISHED = 'finished'
  if (ret.status.length !== 0) // Don't break an already set status
    return true
  let total = playerA.length + playerB.length + pile.length 
  if (playerA.length === total || playerB.length === total)
  {
    ret.status = FINISHED
    return true
  }
  return false
}

function nextCard(turn, playerA, playerB)
{
  return turn === 0 ? playerA.shift() : playerB.shift()
}

function state2String(playerA, playerB)
{

  return playerA.map((ch) => ch in royalty ? ch : 'N').join('') + "|" + playerB.map((ch) => ch in royalty ? ch : 'N').join('')
}

function undefinedCard(turn, playerA, playerB, pile, status)
{
  const ret = collectPile(turn, playerA, playerB, pile, status)
  playerA = ret.playerA
  playerB = ret.playerB
  pile = ret.pile
  isFinished(playerA, playerB, pile, status)
  return {playerA, playerB, pile}
}

function updateMoves(playerA, playerB, pile, moves, status)
{
  const LOOP = 'loop'
  let move = state2String(playerA, playerB, pile)
  if (moves.has(move))
    status.status = LOOP
  moves.add(move)
}