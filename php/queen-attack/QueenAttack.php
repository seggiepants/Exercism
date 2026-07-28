<?php
// Queen Attack Exercise

declare(strict_types=1);

// Check if you can place a queen on a chessboard at the given coordinates.
// @param $xCoordinate: x-coordinate of position (should be 0-7)
// @param $yCoordinate: y-coordinate of position (should be 0-7)
// @returns: True if a valid position on the board.
// @raises: InvalidArgumentException if position is out of bounds (never returns false).
function placeQueen(int $xCoordinate, int $yCoordinate): bool
{
    if ($xCoordinate < 0  || $xCoordinate > 7 || $yCoordinate < 0 || $yCoordinate > 7) {
        throw new InvalidArgumentException("Out of bounds");
    };
    return true;
}

// Check if one queen on a chessboard can attack another.
// @param $whiteQueen: 2 element integer array with white queen's location on the chessboard
// @param $blackQueen: 2 element integer array with black queen's location on the chessboard
// @returns: True if they can attack one another.
// @raises: InvalidArgumentException if white or black queen are on the same spot, or out of bounds.
function canAttack(array $whiteQueen, array $blackQueen): bool
{
    placeQueen($whiteQueen[0], $whiteQueen[1]);
    placeQueen($blackQueen[0], $blackQueen[1]);
    if ($whiteQueen[0] == $blackQueen[0] && $whiteQueen[1] == $blackQueen[1]) {
        // Same spot throw an error.
        throw new InvalidArgumentException("Both queens are on the same spot.");
    }
    if ($whiteQueen[0] == $blackQueen[0] || $whiteQueen[1] == $blackQueen[1]) {
        // Horizontal or Vertical match.
        return true;
    }

    $dx = abs($whiteQueen[0] - $blackQueen[0]);
    $dy = abs($whiteQueen[1] - $blackQueen[1]);
    return $dx == $dy;
}
