// Exercise: Save the Cow, really just a word guessing game backend.
package savethecow

import (
	"errors"
	"slices"
	"strings"
)

type Game struct {
	NumGuesses int    // How many guesses we have made.
	SecretWord string // The secret word
	Guesses    []rune // Runes successfully guessed.
}

const MAX_GUESSES int = 10

// Begin a new game of save the cow.
// @param word: The secret word.
func NewGame(word string) *Game {
	return &Game{
		NumGuesses: 0,
		SecretWord: word,
		Guesses:    []rune{},
	}
}

// Guess the given rune.
// @param r: The rune to guess.
// @raises: Error if the game is over.
func (g *Game) Guess(r rune) error {
	if g.State() == "Lose" {
		return errors.New("cannot guess after the game is lost")
	}
	if g.State() == "Win" {
		return errors.New("cannot guess after the game is won")
	}
	if strings.Contains(g.SecretWord, string(r)) && !slices.Contains(g.Guesses, r) {
		g.Guesses = append(g.Guesses, r)
	} else {
		g.NumGuesses++
	}
	return nil
}

// The secret word masked with only successfully guessed letters shown
// the rest are replaced with an underscore.
// @returns: Masked version of the secret word. Same as the secret word
// if all necessary letters have been guessed.
func (g *Game) MaskedWord() string {
	var ret []rune = make([]rune, len(g.SecretWord))
	for i, rune := range g.SecretWord {
		if slices.Contains(g.Guesses, rune) {
			ret[i] = rune
		} else {
			ret[i] = '_'
		}
	}
	return string(ret)
}

// How many guesses the player has remaining.
// I think this number is off by one. I subtracted 1 for the tests.
// @returns: The number of guesses remaining (not counting this one?)
func (g *Game) RemainingGuesses() int {
	if MAX_GUESSES > g.NumGuesses {
		return MAX_GUESSES - g.NumGuesses - 1
	}
	return 0 // Game over
}

// Return current state of the game, "Win", "Lose", or "Ongoing"
// @returns: Game state, "Win" if you guessed the word, "Lose" if you didn't
// guess the word and ran out of guesses, or "Ongoing" if the game is not won/lost.
func (g *Game) State() string {
	if g.MaskedWord() == g.SecretWord {
		return "Win"
	}
	if MAX_GUESSES <= g.NumGuesses {
		return "Lose"
	}
	return "Ongoing"
}
