<?php
// HighScores exercise - an introduction to getters/setters or as PHP calls them Property Hooks.

declare(strict_types=1);

// Class to hold High Scores for a game.
class HighScores
{
    /**
     * You will need to add the methods and properties to store and present the
     * desired values yourself. You will want to consider using property hooks:
     * https://www.php.net/manual/en/language.oop5.property-hooks.php
     */
    public array $scores; // Regular array for high scores

    // Most recent high score.    
    // @returns: Most recently added score.
    // @raises: ArgumentException if no high-scores found.
    public int $latest {
        get {
            if (count($this->scores) == 0) {
                throw new ArgumentException("No scores.");
            }
            return end($this->scores); 
        }
    }

    // Best high score.    
    // @returns: Most maximum of the scores.
    // @raises: ArgumentException if no high-scores found.
    public int $personalBest {
        get {
            if (count($this->scores) == 0) {
                throw new ArgumentException("No scores.");
            }
            return max($this->scores);
        }
    }

    // Best three scores from the scores array.
    // @returns: Array of size three with the best three scores.
    // @raises: ArgumentException if no high-scores found.    
    public array $personalTopThree {
        get {
            if (count($this->scores) == 0) {
                throw new ArgumentException("No scores.");
            }
            $copy = $this->scores;
            sort($copy);
            $copy = array_reverse($copy);
            return array_slice($copy, 0, 3);
        }
    }

    // Constructor. Just assign the scores
    public function __construct(array $scores)
    {
        $this->scores = $scores;
    }

}
