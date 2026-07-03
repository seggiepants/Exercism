<?php
// Compute the result of a game of Hex/Polygon

declare(strict_types=1);

// Calculate who won a game of Hex/Polygon.
// @param $lines: board expressed as a series of strings with padded beginning for the hex pattern.
// @returns: null on empty board, "" on populated board with no winner 
// or "white", "black" for winning board states.
function winner(array $lines): ?string
{
    // O top to bottom
    // X left to right

    // Don't use strpos, that returns 0 if found at location 0 which 
    // evaluates to false as in not found.
    $foundO = str_contains($lines[0], "O");
    $foundX = false;
    for ($i = 0; $i < count($lines); $i++) {
        $foundX |= ltrim($lines[$i])[0] == "X";
    }
    
    if ($foundO == false && $foundX == false) return null;

    for ($i = 0; $i < strlen($lines[0]); $i++) {
        if ($lines[0][$i] == "O") {
            if (search($lines, $i, 0, "O", "BOTTOM")) return "white";
        }
    }


    for ($i = 0; $i < count($lines); $i++) {
        $offset = strlen($lines[$i]) - strlen(ltrim($lines[$i]));
        if ($lines[$i][$offset] == "X") {
            if (search($lines, $offset, $i, "X", "RIGHT")) return "black";
        }
    }
    return "";

}

// Search for a path from the given location to the desired side of the board.
// @param $lines: The board to search on.
// @param $x: x-coordinate of position to evaluate.
// @param $y: y-coordinate of position to evaluate.
// @param $char: The character expected.
// @param $side: "RIGHT", or "BOTTOM" what is the target side to reach.
// @returns: True if we found a way from left->right or top->bottom (depending on $side).
function search(array $lines, int $x, int $y, string $char, string $side): bool {
    // check bounds and that we have the desired character there.
    if ($x < 0 || 
        $y < 0 || 
        $y >= count($lines) || 
        $x >= strlen($lines[$y]) ||
        $lines[$y][$x] != $char) {
        return false;
    }

    // Return true if we hit the desired side.
    if (($side == "RIGHT" && $x == strlen($lines[$y]) - 1) ||
        ($side == "BOTTOM" && $y == count($lines) - 1)) {
            return true;
    }

    // directions
    $step = array(array(-2, 0), array(-1, -1), array(1, -1), array(2, 0), array(1, 1), array(-1, 1));
    
    // mark current spot so we don't recurse forever.
    $lines[$y][$x] = "?";
    // search all connecting spots
    for($i = 0; $i < count($step); $i++) {
        $x1 = $x + $step[$i][0];
        $y1 = $y + $step[$i][1];
        if (search($lines, $x1, $y1, $char, $side)) {
            $lines[$y][$x] = $char; // return starting spot to normal.
            return true;
        }
    }
    // return the current spot to normal
    $lines[$y][$x] = $char;
    // Nothing found at self or children so return false.
    return false;
}
