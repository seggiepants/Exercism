<?php
// Score a game of bowling.

declare(strict_types=1);

// Class to simulate a game of bowling with functions to roll a ball and
// get the final score.
class Game
{
    private array $rolls;
    private $frame;
    private $throws;
    private $remaining;

    // Instance constructor for game of bowling.
    public function __construct() {
        $this->rolls = array();
        $this->frame = 1;
        $this->throws = 0;
        $this->remaining = 10;
    }

    // Score a game of bowling from saved state in the class instance.
    // @returns: final score
    // @throws: Exception if you have too few or too many rolls.
    public function score(): int
    {
        $score = 0; // game score
        $set = 0;   // set 1, 2, or 3 on frame ten.
        $frame = 1; // 1-10
        $total = 0; // total this frame.

        for ($i = 0; $i < count($this->rolls); $i++) {
            $set++;
            $total += $this->rolls[$i];
            if ($total == 10 && $set == 1 && $frame < 10) {
                // Strike -- peek ahead two throws
                if ($i + 1 < count($this->rolls)) {
                    $total += $this->rolls[$i + 1];
                } else {
                    throw new Exception("Cannot score incomplete game.");        
                }
                if ($i + 2 < count($this->rolls)) {
                    $total += $this->rolls[$i + 2];
                } else {
                    throw new Exception("Cannot score incomplete game.");
                }
            } else if ($total == 10 && $set == 2 && $frame < 10) {
                // Spare -- peek ahead one throw
                if ($i + 1 < count($this->rolls)) {
                    $total += $this->rolls[$i + 1];
                } else {
                    throw new Exception("Cannot score incomplete game.");
                }
            }
            // Frames 1-9 ten pins or second throw ends the frame.
            // Frame 10 with two throws and no strike/spare ends the frame.
            // Frame 10 with three throws ends the frame.
            if ((($total >= 10 || $set == 2) && $frame < 10) ||
                ($frame == 10 && $set == 2 && $total < 10) || 
                ($frame == 10 && $set == 3)) {
                $score += $total;
                $total = 0;
                $set = 0;
                $frame++;
            }
        }
        // If we didn't finish frame 10 the game is incomplete.
        if ($frame <= 10) {
            throw new Exception("Cannot score incomplete game.");
        }
        
        return $score;
    }

    // Record a single ball roll in a game of bowling
    // @param $pins: The number of pins knocked down.
    // @raises: If this goes into too many frames or pin count is unacceptable.
    public function roll(int $pins): void
    {
        $previous = 0; // get the previous throw.
        if (count($this->rolls) > 0) {
            $previous = end($this->rolls);
        }

        // Too many pins?
        if ($pins < 0 || $pins > $this->remaining) {
            throw new Exception("Invalid pin count.");
        }

        // Too many frames?
        if ($this->frame > 10) {
            throw new Exception("Too many frames.");
        }
        // Add the pin
        $this->rolls[] = $pins;
        // Update state
        $this->throws++;
        $this->remaining -= $pins;
        
        if (($this->frame < 10 && $this->throws >= 2) || 
            ($this->frame == 10 && $this->throws >= 3) || 
            ($this->frame == 10 && $this->throws == 2 && $pins + $previous < 10) || 
            ($this->frame < 10 && $this->remaining == 0)) {
            // Reset for next frame.
            $this->frame++;
            $this->remaining = 10;
            $this->throws = 0;
        }

        // Special case on frame 10 after spare/strike can still go for another 10.
        if ($this->frame == 10 && $this->remaining == 0) {
            $this->remaining = 10;
        }

    }
}
