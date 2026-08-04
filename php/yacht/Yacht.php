<?php
// Yacht exercise. Score a roll of five dice in yacht.

declare(strict_types=1);

class Yacht
{
    // Score a roll of five dice in yacht for the selected category.
    // @param $rolls: Array of five dice faces 1-6.
    // @param $category: One of "ones", "twos", "threes", "fours", "fives", 
    // "sixes", "four of a kind", "full house", "big straight", 
    // "little straight", "choice", or "yacht"
    // @returns: Computed score for the rolls and category
    public function score(array $rolls, string $category): int
    {
        switch ($category) {
            case "ones":
                return $this->Score1to6($rolls, 1);
            case "twos":
                return $this->Score1to6($rolls, 2);
            case "threes":
                return $this->Score1to6($rolls, 3);
            case "fours":
                return $this->Score1to6($rolls, 4);
            case "fives":
                return $this->Score1to6($rolls, 5);
            case "sixes":
                return $this->Score1to6($rolls, 6);
            case "four of a kind":
                return $this->ScoreFourOfAKind($rolls);
            case "full house":
                return $this->ScoreFullHouse($rolls);
            case "big straight":
                return $this->ScoreStraight($rolls, 2);
            case "little straight":
                return $this->ScoreStraight($rolls, 1);
            case "choice":
                return array_sum($rolls);
            case "yacht":
                return $this->ScoreYacht($rolls);
        }        
        return 0;
    }

    // Score "ones", "twos", "threes", "fours", "fives", or "sixes"
    // for each entry in the rolls array that has the chosen number increment
    // the score by that number.
    // @param $rolls: Array of dice values (5)
    // @param $number: The number to count (1-6)
    // @returns: Count of $number in $rolls times $number.
    public function Score1to6($rolls, $number) {
        $score = 0;
        foreach($rolls as $roll) {
            if ($roll == $number) {
                $score += $number;
            }
        }
        return $score;
    }

    // Score "four of a kind" if there are 4+ duplicate values in the rolls array 
    // sum them up and return them (why not use choice);
    // @param $rolls: Array of dice values (5)
    // @returns: 0 if not four matching things, otherwise that thing times four.
    public function ScoreFourOfAKind($rolls) {
        $kinds = array_count_values($rolls);
        foreach ($kinds as $index => $count) {
            if ($count >= 4) {
                return $index * 4;
            }
        }
        return 0;
    }

    // Score "full house" if there are 2 of one number and three of another sum them up
    // and return it. Again why not just use choice.
    // @param $rolls: Array of dice values (5)
    // @returns: 0 if not a pair and triple of values, otherwise the sum of all values.
    public function ScoreFullHouse($rolls) {
        $kinds = array_count_values($rolls);
        $score3 = 0;
        $score2 = 0;
        foreach ($kinds as $index => $count) {
            if ($count == 3) {
                $score3 = $index * 3;
            } else if ($count == 2) {
                $score2 = $index * 2;
            }
        }
        if ($score3 > 0 && $score2 > 0) {
            return $score3 + $score2;
        }
        return 0;
    }

    // Score "big straight" 2,3,4,5,6 or "little straight" 1,2,3,4,5 
    // @param $rolls: Array of dice values (5)
    // @param $start: Where to start 1 for little straight, 2 for a big straight.
    // @returns: 0 if not the desired straight, otherwise 30 points.
    public function ScoreStraight($rolls, $start) {
        for($i = $start; $i < $start + count($rolls); $i++) {
            if (!in_array($i, $rolls)) {
                return 0;
            }
        }
        return 30;
    }

    // Score "yacht" checks if all cards are the same value
    // @param $rolls: Array of dice values (5)
    // @returns: 50 for yach, 0 if not.    
    public function ScoreYacht($rolls) {        
        $score = 50;
        for ($i = 1; $i < count($rolls); $i++) {
            if ($rolls[$i] != $rolls[0]) {
                $score = 0;
                break;
            }
        }
        return $score;
    }
}
