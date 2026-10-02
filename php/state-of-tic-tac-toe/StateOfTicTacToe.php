<?php

// State of Tic Tac Toe Exercise

declare(strict_types=1);

enum State
{
    case Win;
    case Ongoing;
    case Draw;
}

// State of Tic Tac Toe - check if a game is Ongoing, a Draw, has been Won or 
// if there is a logical error in the board state via a Runtime Exception.
class StateOfTicTacToe
{
    // Check if a game is currenty ongoing, a draw, or has a winner.
    // @param array $board: The game board presented as an array of three 
    // strings where each string is three characters long containing only 
    // X, O, or a space.
    // @returns State: State::Win if there is a winner, State::Ongoing if 
    // no winner but still has blank spaces, State::Draw if filled with 
    // no winner.
    // @raises RuntimeException: thrown when multiple winners or kept playing 
    // after a win or wrong turn order detected.
    public function gameState(array $board): State
    {
        $countX = 0;
        $countO = 0;
        $countFree = 0;
        $countXWin = 0;
        $countOWin = 0;

        // Check for full Rows and count how many of each of X, O, and blank.
        foreach($board as $row) {
            for($i = 0; $i < strlen($row); $i++) {
                switch($row[$i]) {
                case "X":
                    $countX++;
                    break;
                case "O":
                    $countO++;
                    break;
                default:
                    $countFree++;
                    break;

                }
            }

            if ($row == "OOO") {
                    $countOWin++;
            } else if ($row == "XXX") {
                $countXWin++;
            }
        }

        // Check if we have a full column
        for ($col = 0; $col < 3; $col++) {
            $column = array_reduce($board, function($carry, $value) use($col) {
                return $carry . $value[$col];
            }, "");
            if ($column == "OOO") {
                $countOWin++;
            } else if ($column == "XXX") {
                $countXWin++;
            }

        }

        // Check the diagonals
        $diagonal = $board[0][0] . $board[1][1] . $board[2][2];
        if ($diagonal == "OOO") {
            $countOWin++;
        } else if ($diagonal == "XXX") {
            $countXWin++;
        }
        $diagonal = $board[0][2] . $board[1][1] . $board[2][0];
        if ($diagonal == "OOO") {
            $countOWin++;
        } else if ($diagonal == "XXX") {
            $countXWin++;
        }

        if ($countXWin > 0 && $countOWin > 0) {
            throw new RuntimeException("Impossible board: game should have ended after the game was won");
        }

        if ($countO > $countX) {
            throw new RuntimeException("Wrong turn order: O started");
        } else if ($countX > ($countO + 1)) {
            throw new RuntimeException("Wrong turn order: X went twice");
        }

        if ($countXWin > 0 && $countX == $countO) {
            throw new RuntimeException("Impossible board: game should have ended after the game was won");
        }
        if ($countOWin > 0 && $countX > $countO) {
            throw new RuntimeException("Impossible board: game should have ended after the game was won");
        }

        if ($countXWin + $countOWin > 0) {
            return State::Win;
        }

        if ($countFree > 0) {
            return State::Ongoing;
        }
        return State::Draw;
    }
}
