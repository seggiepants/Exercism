// Poker Hand exercise. This is way too long.
package poker

import (
	"errors"
	"slices"
	"strings"
)

// Cards mapped to their integer value for comparison. Things will crash out if
// your card value isn't in this map.
var CardValue map[string]int = map[string]int{
	"A":  14,
	"K":  13,
	"Q":  12,
	"J":  11,
	"10": 10,
	"9":  9,
	"8":  8,
	"7":  7,
	"6":  6,
	"5":  5,
	"4":  4,
	"3":  3,
	"2":  2,
}

var Suits string = "♢♡♧♤" // Allowed card suits. Parsing crashes if suit isn't one of these

// Data structure to model a hand of cards.
type Hand struct {
	Cards    []Card // Parsed cards
	Original string // Original string representation so the test cases don't fail.
}

// Data structure to model a single card. I cache the value didn't want to look it up all the time
type Card struct {
	Rank  string
	Suit  string
	Value int
}

// String value for a card -- don't think it is used anymore.
// was going to rebuild the hand, but the order was different so I cached the original in Hand
// @returns: string representation of the card.
func (c Card) ToString() string {
	return c.Rank + c.Suit
}

// Build a new Card struct from the given card string rank then suit no spaces
// @param card: The string to build a card from
// @returns: new card struct
// @raises: Error if invalid rank or suit or missing value.
func NewCard(card string) (Card, error) {
	runes := []rune(card)
	rank := string(runes[0 : len(runes)-1])
	value, ok := CardValue[rank]
	if !ok {
		return Card{}, errors.New("Invalid card rank")
	}
	suit := string(runes[len(runes)-1])
	if !strings.Contains(Suits, suit) {
		return Card{}, errors.New("Invalid card suit")
	}
	return Card{Rank: rank, Suit: suit, Value: value}, nil
}

// Build a hand struct from the given string.
// @param hand: The string to parse to build a hand
// @returns: New hand object.
// @raises: Error if incorrect card count or card parse error
func NewHand(hand string) (Hand, error) {
	substrings := strings.Split(hand, " ")
	if len(substrings) != 5 {
		return Hand{}, errors.New("Hand doesn't have five cards")
	}
	// Check if each card is a rank A-J,10-2 and a suit
	cards := make([]Card, 5)
	for i, substring := range substrings {
		card, err := NewCard(substring)
		if err != nil {
			return Hand{}, err
		}
		cards[i] = card
	}
	return Hand{Cards: cards, Original: hand}, nil
}

// From a list of given card hands choose the best and return it. Return multiple hands if they are equal.
// @param hands: slice of string with one hand per string.
// @returns: slice of string with the best hand(s)
// @raises: error if no hands, or malformed cards.
func BestHand(hands []string) ([]string, error) {
	if len(hands) == 0 {
		return []string{}, errors.New("No hands to check")
	}
	playerHands := make([]Hand, 0)
	for _, hand := range hands {
		playerHand, err := NewHand(hand)
		if err != nil {
			return []string{}, err
		}
		playerHands = append(playerHands, playerHand)
	}
	slices.SortFunc(playerHands, HandCompare)
	result := make([]string, 0)
	biggest := playerHands[len(playerHands)-1]
	for i := len(playerHands) - 1; i >= 0; i-- {
		if HandCompare(biggest, playerHands[i]) == 0 {
			result = append([]string{playerHands[i].Original}, result...)
		} else {
			break
		}
	}
	return result, nil
}

// Analyze a hand to get the number of distinct card ranks and suits and how often they occur.
// Useful for things like 3 of a kind.
// @param hand: The hand to analyze:
// @returns rank to occurence count map, and suit to occurence map. (Why didn't I use this for isFlush)
func AnalyzeHand(hand Hand) (map[string]int, map[string]int) {
	ranks := make(map[string]int, 0)
	suits := make(map[string]int, 0)
	for _, card := range hand.Cards {
		ranks[card.Rank]++
		suits[card.Suit]++
	}
	return ranks, suits
}

// Compare two hands and return a compare number to say which is better
// @param a: The first hand
// @param b: The second hand
// @returns > 0: a, == 0: tie, < 0: p2
func HandCompare(a, b Hand) int {
	var result int = 0
	var done bool = false

	result, done = StraightFlush(a, b)
	if !done {
		return result
	}

	result, done = FourOfAKind(a, b)
	if !done {
		return result
	}

	result, done = FullHouse(a, b)
	if !done {
		return result
	}

	result, done = Flush(a, b)
	if !done {
		return result
	}

	result, done = Straight(a, b)
	if !done {
		return result
	}

	result, done = ThreeOfAKind(a, b)
	if !done {
		return result
	}

	result, done = TwoPair(a, b)
	if !done {
		return result
	}

	result, done = OnePair(a, b)
	if !done {
		return result
	}
	return HighCard(a, b)
}

// Return a comparison between the values of two cards.
// @returns > 0: a, == 0: tie, < 0: p2
func CardCompare(a, b Card) int {
	return a.Value - b.Value
}

// Return the maximum value from an integer slice (this should be built-in shouldn't it)
// @param a: Slice of integers
// @returns: 0 if empty otherwise the largest one O(N)
func maxIntSlice(a []int) int {
	if len(a) == 0 {
		return 0
	}
	var maxInt = a[0]
	for i := 1; i < len(a); i++ {
		if a[i] > maxInt {
			maxInt = a[i]
		}
	}
	return maxInt
}

// Return the minimum value from an integer slice (this should be built-in shouldn't it)
// @param a: Slice of integers
// @returns: 0 if empty otherwise the smallest one O(N)
func minIntSlice(a []int) int {
	if len(a) == 0 {
		return 0
	}
	var minInt = a[0]
	for i := 1; i < len(a); i++ {
		if a[i] < minInt {
			minInt = a[i]
		}
	}
	return minInt
}

// If one or more hands is a straight and a flush return the better straight (if both match)
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func StraightFlush(p1 Hand, p2 Hand) (int, bool) {
	flushP1 := IsFlush(p1)
	flushP2 := IsFlush(p2)
	_, p1Cancel := Straight(p1, p1)
	_, p2Cancel := Straight(p2, p2)

	if flushP1 && flushP2 {
		if !p1Cancel && !p2Cancel {
			return Straight(p1, p2)
		}
		if !p1Cancel && p2Cancel {
			return 1, false
		}
		if p1Cancel && !p2Cancel {
			return -1, false
		}
		if p1Cancel && p2Cancel {
			return 0, true
		}
	}

	if flushP1 && !flushP2 {
		if !p1Cancel {
			return 1, false
		}
	}

	if !flushP1 && flushP2 {
		if !p2Cancel {
			return -1, false
		}
	}

	return 0, true
}

// If one or more hands has a four of a kind (4 cards of same rank) return the one with the better rank or leftover if ranks match.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func FourOfAKind(p1 Hand, p2 Hand) (int, bool) {
	var quadP1 bool = false
	var quadP2 bool = false
	var highP1 int = 0
	var highP2 int = 0
	var kickerP1 int = 0
	var kickerP2 int = 0

	p1Rank, _ := AnalyzeHand(p1)
	for key, value := range p1Rank {
		if value == 4 {
			quadP1 = true
			highP1 = CardValue[key]
		} else {
			kickerP1 = CardValue[key]
		}
	}
	p2Rank, _ := AnalyzeHand(p2)
	for key, value := range p2Rank {
		if value == 4 {
			quadP2 = true
			highP2 = CardValue[key]
		} else {
			kickerP2 = CardValue[key]
		}
	}

	if !quadP1 && !quadP2 {
		return 0, true
	}

	if quadP1 && !quadP2 {
		return 1, false
	}
	if !quadP1 && quadP2 {
		return -1, false
	}
	cmpP1 := highP1
	cmpP2 := highP2
	if cmpP1 == cmpP2 {
		cmpP1 = kickerP1
		cmpP2 = kickerP2
	}
	return cmpP1 - cmpP2, false
}

// If one or more hands has a three of a kind return the one with the better value or best other value if their triplets match.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func ThreeOfAKind(p1 Hand, p2 Hand) (int, bool) {
	var tripleP1 bool = false
	var tripleP2 bool = false
	var kickersP1 []int = make([]int, 0)
	var kickersP2 []int = make([]int, 0)
	var highP1 int = 0
	var highP2 int = 0

	p1Rank, _ := AnalyzeHand(p1)
	for key, value := range p1Rank {
		if value == 3 {
			tripleP1 = true
			highP1 = CardValue[key]
		} else {
			kickersP1 = append(kickersP1, CardValue[key])
		}
	}
	p2Rank, _ := AnalyzeHand(p2)
	for key, value := range p2Rank {
		if value == 3 {
			tripleP2 = true
			highP2 = CardValue[key]
		} else {
			kickersP2 = append(kickersP2, CardValue[key])
		}
	}

	if !tripleP1 && !tripleP2 {
		return 0, true
	}

	if tripleP1 && !tripleP2 {
		return 1, false
	}
	if !tripleP1 && tripleP2 {
		return -1, false
	}
	cmpP1 := highP1
	cmpP2 := highP2
	if cmpP1 == cmpP2 {
		cmpP1 = maxIntSlice(kickersP1)
		cmpP2 = maxIntSlice(kickersP2)
	}
	if cmpP1 == cmpP2 {
		cmpP1 = minIntSlice(kickersP1)
		cmpP2 = minIntSlice(kickersP2)
	}
	return cmpP1 - cmpP2, false

}

// If one or more hands has a full house (one triplet and one double) return the one with the better triplet or double if the triplets match.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func FullHouse(p1 Hand, p2 Hand) (int, bool) {
	ranks1, _ := AnalyzeHand(p1)
	ranks2, _ := AnalyzeHand(p2)

	var fullHouse1 bool = false
	var fullHouse2 bool = false
	var found3 bool = false
	var found2 bool = false
	var rank1_3, rank1_2 string
	for key, value := range ranks1 {
		if value == 3 {
			found3 = true
			rank1_3 = key
		}
		if value == 2 {
			found2 = true
			rank1_2 = key
		}
	}
	if found2 && found3 {
		fullHouse1 = true
	}
	found2 = false
	found3 = false
	var rank2_3, rank2_2 string
	for key, value := range ranks2 {
		if value == 3 {
			found3 = true
			rank2_3 = key
		}
		if value == 2 {
			found2 = true
			rank2_2 = key
		}
	}
	if found2 && found3 {
		fullHouse2 = true
	}

	if !(fullHouse1 || fullHouse2) {
		return 0, true
	}
	if fullHouse1 && !fullHouse2 {
		return 1, false
	}
	if !fullHouse1 && fullHouse2 {
		return -1, false
	}

	cmp1 := CardValue[rank1_3]
	cmp2 := CardValue[rank2_3]
	if cmp1 == cmp2 {
		cmp1 = CardValue[rank1_2]
		cmp2 = CardValue[rank2_2]
	}
	return cmp1 - cmp2, false
}

// Check if a hand is a flush (all cards have the same suit)
// @param hand: The hand to check
// @returns: boolean, true if a flush.
func IsFlush(hand Hand) bool {
	cmpSuit := hand.Cards[0].Suit
	for i := 0; i < 5; i++ {
		if hand.Cards[i].Suit != cmpSuit {
			return false
		}
	}
	return true
}

// If one or more hands is a flush return the one with the better high card.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func Flush(p1 Hand, p2 Hand) (int, bool) {
	flushP1 := IsFlush(p1)
	flushP2 := IsFlush(p2)

	if !(flushP1 || flushP2) {
		return 0, true
	}
	if flushP1 && !flushP2 {
		return 1, false
	}
	if !flushP1 && flushP2 {
		return -1, false
	}
	values1 := make([]int, 5)
	values2 := make([]int, 5)
	for i := 0; i < 5; i++ {
		values1[i] = p1.Cards[i].Value
		values2[i] = p2.Cards[i].Value
	}
	slices.Sort(values1)
	slices.Sort(values2)
	for i := 4; i >= 0; i-- {
		if values1[i] != values2[i] {
			return values1[i] - values2[i], false
		}
	}
	return 0, false
}

// If one or more hands is a straight return the one with the better high card.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func Straight(p1 Hand, p2 Hand) (int, bool) {
	var valuesP1 []int = make([]int, 5)
	var valuesP2 []int = make([]int, 5)
	var valuesP1Low []int = make([]int, 5)
	var valuesP2Low []int = make([]int, 5)
	for i := 0; i < 5; i++ {
		valuesP1[i] = p1.Cards[i].Value
		valuesP2[i] = p2.Cards[i].Value
		if p1.Cards[i].Rank == "A" {
			valuesP1Low[i] = 1
		} else {
			valuesP1Low[i] = p1.Cards[i].Value
		}
		if p2.Cards[i].Rank == "A" {
			valuesP2Low[i] = 1
		} else {
			valuesP2Low[i] = p2.Cards[i].Value
		}
	}
	slices.Sort(valuesP1)
	slices.Sort(valuesP2)
	slices.Sort(valuesP1Low)
	slices.Sort(valuesP2Low)
	var straightP1 bool = true
	var straightP2 bool = true
	var straightP1Low bool = true
	var straightP2Low bool = true
	for i := 1; i < 5; i++ {
		if valuesP1[i] != valuesP1[i-1]+1 {
			straightP1 = false
		}
		if valuesP2[i] != valuesP2[i-1]+1 {
			straightP2 = false
		}
	}
	for i := 1; i < 5; i++ {
		if valuesP1Low[i] != valuesP1Low[i-1]+1 {
			straightP1Low = false
		}
		if valuesP2Low[i] != valuesP2Low[i-1]+1 {
			straightP2Low = false
		}
	}
	if straightP1Low && !straightP1 {
		straightP1 = true
		for i, value := range valuesP1 {
			if value == CardValue["A"] {
				valuesP1[i] = 1
			}
		}
	}
	if straightP2Low && !straightP2 {
		straightP2 = true
		for i, value := range valuesP2 {
			if value == CardValue["A"] {
				valuesP2[i] = 1
			}
		}
	}
	straightP1 = straightP1 || straightP1Low
	straightP2 = straightP2 || straightP2Low

	if !(straightP1 || straightP2) {
		return 0, true
	}
	if straightP1 && !straightP2 {
		return 1, false
	}
	if !straightP1 && straightP2 {
		return -1, false
	}
	return maxIntSlice((valuesP1)) - maxIntSlice(valuesP2), false
}

// If one or more hands has a two pairs return the one with the better pair.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func TwoPair(p1 Hand, p2 Hand) (int, bool) {
	var pairsP1 int = 0
	var pairsP2 int = 0
	var pairValuesP1 []int = make([]int, 0)
	var pairValuesP2 []int = make([]int, 0)
	var kickerP1 int = 0
	var kickerP2 int = 0

	p1Rank, _ := AnalyzeHand(p1)
	for key, value := range p1Rank {
		if value == 2 {
			pairsP1++
			pairValuesP1 = append(pairValuesP1, CardValue[key])
		}
		if value == 1 {
			kickerP1 = CardValue[key]
		}
	}
	p2Rank, _ := AnalyzeHand(p2)
	for key, value := range p2Rank {
		if value == 2 {
			pairsP2++
			pairValuesP2 = append(pairValuesP2, CardValue[key])
		}
		if value == 1 {
			kickerP2 = CardValue[key]
		}
	}

	if pairsP1 != 2 && pairsP2 != 2 {
		return 0, true
	}

	if pairsP1 == 2 && pairsP2 != 2 {
		return 1, false
	}
	if pairsP1 != 2 && pairsP2 == 2 {
		return -1, false
	}
	cmpP1 := maxIntSlice(pairValuesP1)
	cmpP2 := maxIntSlice(pairValuesP2)
	if cmpP1 == cmpP2 {
		cmpP1 = minIntSlice(pairValuesP1)
		cmpP2 = minIntSlice(pairValuesP2)
	}
	if cmpP1 == cmpP2 {
		cmpP1 = kickerP1
		cmpP2 = kickerP2
	}
	return cmpP1 - cmpP2, false
}

// If one or more hands has a single pair return the one with the better pair.
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
// also returns keep trying flag. If set treat it as if no desired value was found.
func OnePair(p1 Hand, p2 Hand) (int, bool) {
	var pairP1 bool = false
	var pairP2 bool = false
	var kickersP1 []int = make([]int, 0)
	var kickersP2 []int = make([]int, 0)
	var valueP1, valueP2 int
	p1Rank, _ := AnalyzeHand(p1)
	valueP1 = 0
	for key, value := range p1Rank {
		if value == 2 {
			pairP1 = true
			if CardValue[key] > valueP1 {
				valueP1 = CardValue[key]
			}
		} else {
			kickersP1 = append(kickersP1, CardValue[key])
		}
	}
	p2Rank, _ := AnalyzeHand(p2)
	valueP2 = 0
	for key, value := range p2Rank {
		if value == 2 {
			pairP2 = true
			if CardValue[key] > valueP2 {
				valueP2 = CardValue[key]
			}
		} else {
			kickersP2 = append(kickersP2, CardValue[key])
		}
	}

	if !(pairP1 || pairP2) {
		return 0, true
	}
	if pairP1 && !pairP2 {
		return 1, false
	}
	if !pairP1 && pairP2 {
		return -1, false
	}
	cmpP1 := valueP1
	cmpP2 := valueP2
	if cmpP1 == cmpP2 {
		slices.Sort(kickersP1)
		slices.Sort(kickersP2)
		for i := len(kickersP1) - 1; i >= 0; i-- {
			cmpP1 = kickersP1[i]
			cmpP2 = kickersP2[i]
			if cmpP1 != cmpP2 {
				break
			}
		}
	}
	return cmpP1 - cmpP2, false
}

// Return which hand has the high card
// @param p1: First hand
// @param p2: Second hand
// @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
func HighCard(p1 Hand, p2 Hand) int {
	slices.SortFunc(p1.Cards, CardCompare)
	slices.SortFunc(p2.Cards, CardCompare)
	for i, j := len(p1.Cards)-1, len(p2.Cards)-1; i >= 0 && j >= 0; i, j = i-1, j-1 {
		result := CardCompare(p1.Cards[i], p2.Cards[j])
		if result != 0 {
			return result
		}
	}
	return len(p1.Cards) - len(p2.Cards)
}
