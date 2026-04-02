//
// This is only a SKELETON file for the 'Poker' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const sortRank = 'A K Q J 109 8 7 6 5 4 3 2 '       // Ace above king
const sortRankMinor = 'K Q J 109 8 7 6 5 4 3 2 A '  // Ace below 2

/**
 * Return the best poker hand in a given list. I there are two or more of equivalent matching 
 * the best score, return all best matches
 * @param {Array[string]} hands Each string of an array is a hand. Each has has one or more cards
 * separated by spaces. Each card is a two character string with the first being the value 2-10,J,Q,K,A, and the
 * other being the suit H = Heart, S = Space, D = Diamond, C = Club
 * @returns {Array[string]} Array of hands that have the best score.
 */
export const bestHands = (hands) => {
  let ScoredHands = {}
  for(let i = 0 ; i < hands.length; i++)
  {
    ScoredHands[hands[i]] = ScoreHand(hands[i])
  }
  let maxScore = Math.max(...Object.values(ScoredHands))
  let candidates = hands.filter((hand) => ScoredHands[hand] === maxScore)

  let i = 0
  // Check for equivalent hands if socres are the same
  while (i < candidates.length - 1)
  {
    let result = CompareHands(candidates[i], candidates[i + 1])
    if (result === ">")
    {
      // A > B
      candidates = candidates.filter((value, index) => index !== (i + 1))
      i = 0 // restart
    }
    else if (result === "<")
    {
      // A < B
      candidates = candidates.filter((value, index) => index !== i)
      i = 0 // restart
    }
    else 
    {
      // A === B
      i = i + 1
    }
  }
  return candidates
};

// Return the rank of a card
const CardRank = (card) =>
{
  return card.substring(0, card.length - 1)
}

// Return the suit of a card
const CardSuit = (card) =>
{
  return card.substring(card.length - 1, card.length)
}

// This only works with Ace high
// Convert card to number 2=2, 10=10, K=13, A=14
const ScoreCardRank = (card) => {
  return (28 - sortRank.indexOf(card)) / 2
}

// Compare two sorted hands return "=" if they match, ">" if hand A has the first higher card, and "<" if hand B does
const CompareHands = (handA, handB) => {
  let rankA = handA.split(' ').map((value) => CardRank(value))
  let rankB = handB.split(' ').map((value) => CardRank(value))
  rankA = SortRanks(rankA)
  rankB = SortRanks(rankB)
  for(let i = 0; i < rankA.length; i++)
  {
    let idxA = sortRank.indexOf(rankA[i])
    let idxB = sortRank.indexOf(rankB[i])
    if (idxA < idxB)
      return ">"
    else if (idxA > idxB)
      return "<"
    // otherwise equal, keep going.
  }
  return "="
}

const ScoreHand = (hand) => {
  let minScore = 10_000

  // Five of a Kind
  let ret = FiveOfAKind(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1_000

  // Royal Flush
  // I guess we just let straight flush handle this.
  minScore -= 1_000

  // Straight Flush
  ret = StraightFlush(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1_000

  // Four of a Kind
  ret = FourOfAKind(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1_000

  // Full House
  ret = FullHouse(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1_000

  // Flush
  ret = Flush(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1_000

  // Straight
  ret = Straight(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1000

  // Three of a Kind
  ret = ThreeOfAKind(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1000

  // Two Pair
  ret = TwoPair(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1000

  // One Pair
  ret = OnePair(hand)
  if (ret.length > 0)
    return minScore + ScoreCardRank(ret[0])
  minScore -= 1000

  // High Card
  ret = HighCard(hand)
  return minScore + ScoreCardRank(ret[0]) // ret.map((cardRank) => ScoreCardRank(cardRank)).reduce((total, current) => total + current, 0)

}

const FiveOfAKind = (hand) => {
  return NOfAKind(hand, 5)
}

const FourOfAKind = (hand) => {
  return NOfAKind(hand, 4)
}

const StraightFlush = (hand) => {
  let straight = Straight(hand)
  let flush = Flush(hand)
  if (straight.length > 0 && flush.length > 0)
    return straight
  return []
}

const FullHouse = (hand) => {
  let cards = hand.split(' ')
  let ranks = cards.map((card) => CardRank(card))
  let counts = {}
  for(let rank of ranks)
  {
    if (rank in counts)
      counts[rank] += 1
    else 
      counts[rank] = 1
  }
  if (Object.keys(counts).length === 2)
  {
    // Should have a 3, 2
    let three = Object.keys(counts).filter((key) => counts[key] === 3)[0]
    let two = Object.keys(counts).filter((key) => counts[key] === 2)[0]
    let cardsThree = cards.filter((card) => CardRank(card) === three)
    let cardsTwo = cards.filter((card) => CardRank(card) === two)
    cardsThree.push(...cardsTwo)
    return cardsThree.map((card) => CardRank(card))
  }
  else
  {
    return []
  }
}

const Flush = (hand) => {
  let cards = hand.split(' ')
  let suits = new Set(cards.map((card) => CardSuit(card)))
  if (suits.size !== 1)
    return []
  else
    return SortRanks(cards.map((value) => CardRank(value)))
}

const Straight = (hand) => {
  let ranks = StraightHelper(hand, true)
  if (ranks.length > 0)
    return ranks
  else
    return StraightHelper(hand, false)
}

const StraightHelper = (hand, high) => {
  let cards = hand.split(' ')
  let ranks = cards.map((value) => CardRank(value))
  ranks = SortRanks([...ranks], high ? sortRank : sortRankMinor)
  let previous = ranks[0]
  let sortKey = high ? sortRank : sortRankMinor
  for(let i = 1; i < ranks.length; i++)
  {
    let indexPrevious = sortKey.indexOf(previous)
    let indexCurrent = sortKey.indexOf(ranks[i])
    if (indexCurrent !== (indexPrevious + 2))
      return []
    previous = ranks[i]
  }
  return ranks
}

const ThreeOfAKind = (hand) => {
  return NOfAKind(hand, 3)
}

const NOfAKind = (hand, n) => {
  let cards = hand.split(' ')
  let ranks = cards.map((value) => CardRank(value))
  let counts = {}
  for(let card of ranks)
  {
    if (card in counts)
      counts[card] += 1
    else
      counts[card] = 1
  }
  let matches = Object.keys(counts).filter((rank) => counts[rank] === n)
  if (matches.length !== 1)
    return []
  else
    return matches
}


const TwoPair = (hand) => {
  let cards = hand.split(' ')
  let ranks = cards.map((value) => CardRank(value))
  let counts = {}
  for(let card of ranks)
  {
    if (card in counts)
      counts[card] += 1
    else
      counts[card] = 1
  }
  let matches = Object.keys(counts).filter((rank) => counts[rank] === 2)
  if (matches.length !== 2)
    return []
  else
    return SortRanks(matches)
}

const OnePair = (hand) => {
  let cards = hand.split(' ')
  let ranks = cards.map((value) => CardRank(value))
  let counts = {}
  for(let card of ranks)
  {
    if (card in counts)
      counts[card] += 1
    else
      counts[card] = 1
  }
  if (Object.keys(counts).length !== 4)
    return []
  return Object.keys(counts).filter((rank) => counts[rank] === 2)
}

const HighCard = (hand) => {
  let cards = hand.split(' ')
  let ranks = new Set(cards.map((value) => CardRank(value)))
  return SortRanks([...ranks])
}

const SortRanks = (matches, key = sortRank) => {
  // Sort in descending order (b-a)
  return matches.sort((a, b) => (28 - key.indexOf(b)) - (28 - key.indexOf(a)))
}

