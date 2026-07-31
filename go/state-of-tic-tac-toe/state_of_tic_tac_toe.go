package stateoftictactoe

import (
	"errors"
	"strings"
)

type State string

const (
	Win     State = "win"
	Ongoing State = "ongoing"
	Draw    State = "draw"
	Invalid State = ""
)

// Determine game state for tic tac toe
// @param board: slice of strings of board state with a row for each string.
// @returns: State as "", "win", "ongoing", or "draw". "" only when there is an error.
// @raises: Error when players played out of order, or continued playing after a win
func StateOfTicTacToe(board []string) (State, error) {
	countX, countO := CountChar(board)
	if countX+countO == 0 { // No moves yet
		return Ongoing, nil
	}

	if countO > countX || countX > countO+1 {
		return Invalid, errors.New("Invalid player order")
	}
	xWin := IsWinner(board, 'X')
	oWin := IsWinner(board, 'O')
	if xWin && oWin || (xWin && countO >= countX) || (oWin && countX > countO) {
		// Kept playing after win
		return Invalid, errors.New("Invalid board")
	}
	if xWin || oWin {
		return Win, nil
	}
	if countX+countO == 9 {
		return Draw, nil
	}
	return Ongoing, nil
}

// Count the X's and O's on a tic-tac-toe board
// @param board: slice of string where each string is a row containing 'X', 'O', or ' ' (3 rows 3 columns assumed)
// @returns: Number of X's found, and Number of O's found.
func CountChar(board []string) (int, int) {
	x := 0
	o := 0
	for _, row := range board {
		for _, char := range row {
			switch char {
			case 'O':
				o++
			case 'X':
				x++
			}
		}
	}
	return x, o
}

// Check if a given player has won a game of tic-tac-toe
// @param board: Game board
// @param player: The player to check
// @param returns: True/False True if they have won.
func IsWinner(board []string, player rune) bool {
	targetRow := strings.Repeat(string(player), 3)
	for _, row := range []string{
		board[0],
		board[1],
		board[2],
		string(board[0][0]) + string(board[1][0]) + string(board[2][0]),
		string(board[0][1]) + string(board[1][1]) + string(board[2][1]),
		string(board[0][2]) + string(board[1][2]) + string(board[2][2]),
		string(board[0][0]) + string(board[1][1]) + string(board[2][2]),
		string(board[2][0]) + string(board[1][1]) + string(board[0][2]),
	} {
		if row == targetRow {
			return true
		}
	}
	return false
}
