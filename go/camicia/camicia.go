package camicia

import (
	"maps"
	"slices"
	"strings"
)

type Outcome struct {
	finishes bool
	cards    int
	tricks   int
}

var royalty map[string]int = map[string]int{
	"J": 1,
	"Q": 2,
	"K": 3,
	"A": 4,
}

// Simulate a game of camicia with the given cards for players A and B
// @param playerA: slice of string where each string is a card (A, K, Q, J, 10-2)
// @param playerB: slice of string where each string is a card (A, K, Q, J, 10-2)
// @returns: Outcome struct. This says if the play finished (if not finished it looped), and
// how many cards were played and tricks.
func SimulateGame(playerA, playerB []string) Outcome {
	loops := false
	var ret = Outcome{false, 0, 0}
	royalSuits := slices.Collect(maps.Keys(royalty))

	moves := make([]string, 0)
	turn := 0 // will use (1 - turn) to toggle between 0 and 1.
	flag := []int{0, 0}

	_playerA := make([]string, len(playerA))
	copy(_playerA, playerA)
	_playerB := make([]string, len(playerB))
	copy(_playerB, playerB)
	_pile := make([]string, 0)

	moves = append(moves, State2String(_playerA, _playerB))
	for loops == false && ret.finishes == false {
		var card string
		card, _playerA, _playerB = NextCard(turn, _playerA, _playerB)
		flag[turn] = 1
		if len(card) == 0 {
			_playerA, _playerB, _pile = NullCard(1-turn, _playerA, _playerB, _pile, loops, &ret)
		} else {
			_pile = append(_pile, card)
			ret.cards++
		}

		if len(card) > 0 && slices.Contains(royalSuits, card) {
			penalty := royalty[card]
			recipient := turn
			turn = 1 - turn
			for penalty > 0 {
				var next string
				next, _playerA, _playerB = NextCard(turn, _playerA, _playerB)
				flag[turn] = 1
				if len(next) == 0 {
					_playerA, _playerB, _pile = NullCard(recipient, _playerA, _playerB, _pile, loops, &ret)
					break
				} else {
					_pile = append(_pile, next)
					ret.cards++
					penalty--
				}

				if slices.Contains(royalSuits, next) {
					penalty = royalty[next]
					recipient = turn
					turn = 1 - turn
				}
			}

			if penalty == 0 {
				_playerA, _playerB, _pile = CollectPile(recipient, _playerA, _playerB, _pile, &ret)
				turn = 1 - recipient
			}
		}
		turn = 1 - turn
		if flag[0] == flag[1] && flag[0] == 1 {
			loopFound := false
			loopFound, moves = UpdateMoves(_playerA, _playerB, _pile, moves, &ret)
			loops = loops || loopFound
			flag[0] = 0
			flag[1] = 0
		}
		isFinished(_playerA, _playerB, _pile, loops, &ret)
	}
	return ret
}

// Collect the pile.
// @param turn: Whoes turn was it 0 = playerA, 1 = playerB
// @param playerA: first player's cards
// @param playerB: second player's cards
// @param pile: discard pile
// @param status: Update the game status tricks.
// @returns: new version of playerA, playerB, and pile
func CollectPile(turn int, playerA []string, playerB []string, pile []string, status *Outcome) ([]string, []string, []string) {
	if turn == 0 {
		playerA = slices.Concat(playerA, pile)
	} else {
		playerB = slices.Concat(playerB, pile)
	}
	status.tricks++
	pile = pile[:0]
	return playerA, playerB, pile
}

// Return the next card.
// @param turn: Whoes turn was it 0 = playerA, 1 = playerB
// @param playerA: first player's cards
// @param playerB: second player's cards
// @param pile: discard pile
// @returns: card and the new version of playerA, and playerB
func NextCard(turn int, playerA []string, playerB []string) (string, []string, []string) {
	if turn == 0 {
		if len(playerA) == 0 {
			return "", playerA, playerB
		}
		var next string = playerA[0]
		playerA = playerA[1:]
		return next, playerA, playerB
	}
	if len(playerB) == 0 {
		return "", playerA, playerB
	}
	var next string = playerB[0]
	playerB = playerB[1:]
	return next, playerA, playerB
}

// Check if the game completed.
// @param playerA: first player's cards
// @param playerB: second player's cards
// @param pile: discard pile
// @param loops: has the game looped?
// @param status: Update the game status.
// @returns: true if game over will update status.finishes if one player has all the cards.
func isFinished(playerA []string, playerB []string, pile []string, loops bool, status *Outcome) bool {
	if loops || status.finishes { // Don't break an already set status
		return true
	}

	var total int = len(playerA) + len(playerB) + len(pile)
	if len(playerA) == total || len(playerB) == total {
		status.finishes = true
		return true
	}
	return false
}

// What to do when the drawing player had no card.
// @param turn: Whoes turn was it 0 = playerA, 1 = playerB
// @param playerA: first player's cards
// @param playerB: second player's cards
// @param pile: discard pile
// @param loops: has the game looped
// @param status: Update the game status.
// @returns: new version of playerA, playerB, and pile
func NullCard(turn int, playerA []string, playerB []string, pile []string, loops bool, status *Outcome) ([]string, []string, []string) {
	playerA, playerB, pile = CollectPile(turn, playerA, playerB, pile, status)
	isFinished(playerA, playerB, pile, loops, status)
	return playerA, playerB, pile
}

// Make a state string for the current hands. number cards are treated as if they are any number card
// @param playerA: first player's cards
// @param playerB: second player's cards
// @returns: status string for the players cards.
func State2String(playerA []string, playerB []string) string {
	handA := strings.Builder{}
	handB := strings.Builder{}
	royalSuits := slices.Collect(maps.Keys(royalty))

	for _, card := range playerA {
		if slices.Contains(royalSuits, card) {
			handA.WriteString(card)
		} else {
			handA.WriteString("N")
		}
	}

	for _, card := range playerB {
		if slices.Contains(royalSuits, card) {
			handB.WriteString(card)
		} else {
			handB.WriteString("N")
		}
	}

	return handA.String() + "|" + handB.String()
}

// Collect the pile.
// @param playerA: first player's cards
// @param playerB: second player's cards
// @param pile: discard pile
// @param moves: What moves have already been done.
// @param status: Update the game status tricks.
// @returns: new loops flag, and the updated moves
func UpdateMoves(playerA []string, playerB []string, pile []string, moves []string, status *Outcome) (bool, []string) {
	loops := false
	move := State2String(playerA, playerB)
	if slices.Contains(moves, move) {
		loops = true
	}
	moves = append(moves, move)
	return loops, moves
}
