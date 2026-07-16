package queenattack

import (
	"errors"
	"strconv"
	"strings"
)

// Can two queens on a chessboard attack each other.
// @param whitePosition: spreadsheet style location of the white queen.
// @param blackPosition: spreadsheet style location of the black queen.
// @returns: True if they can attack each other.
// @raises: Error for invalid locations on the board.
func CanQueenAttack(whitePosition, blackPosition string) (bool, error) {
	whiteX, whiteY, err := positionToCoordinates(whitePosition)
	if err != nil {
		return false, err
	}
	blackX, blackY, err := positionToCoordinates(blackPosition)
	if err != nil {
		return false, err
	}

	if blackX == whiteX && blackY == whiteY {
		return false, errors.New("Pieces are on the same square.")
	}

	if blackX == whiteX || blackY == whiteY {
		return true, nil
	}
	dx := blackX - whiteX
	if dx < 0 {
		dx *= -1
	}
	dy := blackY - whiteY
	if dy < 0 {
		dy *= -1
	}
	return dx == dy, nil
}

// Convert a board position to x, y coordinates on a 2D grid.
// @param position: Position in letter number format
// @returns: x, and y coordinate of the position or an error.
// @raises: Error if not two character, trailing part is not a number, or out of bounds.
func positionToCoordinates(position string) (int, int, error) {
	if len(position) != 2 {
		return -1, -1, errors.New("Invalid position string - wrong length")
	}
	pos := strings.ToLower(position)
	x := int(pos[0] - 'a')
	y, err := strconv.Atoi(pos[1:])
	if err != nil {
		return -1, -1, errors.New("Numeric value cannot be parsed.")
	}
	y = 8 - y
	if x < 0 || x >= 8 || y < 0 || y >= 8 {
		return -1, -1, errors.New("Coordinate out of bounds.")
	}
	return x, y, nil
}
