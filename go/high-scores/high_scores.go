// High Score Table exercise.
package highscores

import (
	"slices"
)

type HighScores struct {
	scores []int
}

// NewHighScores returns a new HighScores object.
// @param scores: Scores to add to the High Scores struct
// @returns: Pointer to the new High Scores struct.
func NewHighScores(scores []int) *HighScores {
	var ret HighScores = HighScores{}
	ret.scores = append([]int{}, scores...)
	return &ret
}

// Scores returns all the scores. In order added
// @returns: The scores in the High Scores struct
func (s *HighScores) Scores() []int {
	return s.scores
}

// Latest returns the latest (last) score.
// @returns: The last score entered into the High Scores struct. Zero if no high scores
func (s *HighScores) Latest() int {
	index := len(s.scores) - 1
	if index < 0 {
		return 0
	}
	return s.scores[index]
}

// PersonalBest returns the best (highest) score.
// @returns: The maximum score in the High Scores struct. Zero if no high scores
func (s *HighScores) PersonalBest() int {
	if len(s.scores) == 0 {
		return 0
	}
	return slices.Max(s.scores)
}

// TopThree returns the top three scores.
// @returns: A slice with the top three scores in soreted order. Returns all scores if less than three entries
func (s *HighScores) TopThree() []int {
	sortedScores := append([]int{}, s.scores...)
	slices.SortFunc(sortedScores, func(a int, b int) int {
		if a > b {
			return -1
		} else if a < b {
			return 1
		}
		return 0
	})
	if len(sortedScores) < 3 {
		return sortedScores
	}
	return sortedScores[0:3]
}
