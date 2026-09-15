<?php

// Tournament exercise

declare(strict_types=1);

// Represents a single team
class Team 
{
    public private(set) string $name;
    public private(set) int $matches;
    public private(set) int $win;
    public private(set) int $draw;
    public private(set) int $lose;
    public private(set) int $points;

    // Constructor set everything to initial state.
    // @param $name: The name of the team. This needs to be unique or errors will occur in the tally.
    public function __construct($name) {
        $this->name = $name;
        $this->matches = 0;
        $this->win = 0;
        $this->draw = 0;
        $this->lose = 0;
        $this->CalculatePoints();
    }

    // The team won a game.
    public Function Win() {
        $this->matches++;
        $this->win++;
        $this->CalculatePoints();
    }

    // The team played a game ending in a draw.
    public Function Draw() {
        $this->matches++;
        $this->draw++;
        $this->CalculatePoints();
    }

    // The team lost a game
    public Function Lose() {
        $this->matches++;
        $this->lose++;
        $this->CalculatePoints();
    }

    // Pretty-print the results for this team
    public Function ToString() {
        return sprintf("\n%-30s | %2d | %2d | %2d | %2d | %2d", $this->name, $this->matches, $this->win, $this->draw, $this->lose, $this->points);
    }

    // Update the points total based on win and draw games.
    private function CalculatePoints() {
        $this->points = (3 * $this->win) + $this->draw;
    }
}

// Holds the tally function for computing tournament results.
class Tournament
{

    // Constructor -- currently - do nothing.
    public function __construct() {}
        
    // Tally up the given scores and return the results.
    // Scores are separated by new lines with three semicolon separated values home;away;result where home and away are team names and result is win, loss, or draw.
    // @param $scores: The description of games to tally
    // @returns: Tablular results data with the scores per team computed.
    public function tally($scores) {
        $teams = array();
        foreach(explode("\n", $scores) as $line) {
            $data = explode(";", $line);
            if (count($data) == 3) {
                [$home, $away, $result] = $data;

                if (!array_key_exists($home, $teams)) {
                    $teams[$home] = new Team($home);
                }
                if (!array_key_exists($away, $teams)) {
                    $teams[$away] = new Team($away);
                }
                if ($result == "win") {
                    $teams[$home]->Win();
                    $teams[$away]->Lose();
                } else if ($result == "loss") {
                    $teams[$home]->Lose();
                    $teams[$away]->Win();
                } else {
                    $teams[$home]->Draw();
                    $teams[$away]->Draw();
                }
            }
        }
        usort($teams, function ($a, $b) {
            if ($a->points == $b->points) {
                return strcmp($a->name, $b->name);
            }
            return $b->points - $a->points;
        });

        $results = "Team                           | MP |  W |  D |  L |  P";        
        foreach($teams as $key => $value) {
            $results .= $value->ToString();
        }
        return $results;
    }
}
