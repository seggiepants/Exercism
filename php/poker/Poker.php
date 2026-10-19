<?php

// Exercise - Poker hand evaluation

declare(strict_types=1);

const cardValue = [
        'A' => 14, 
        'K' => 13, 
        'Q' => 12, 
        'J' => 11, 
        '10' => 10, 
        '9' => 9, 
        '8' => 8, 
        '7' => 7, 
        '6' => 6, 
        '5' => 5, 
        '4' => 4, 
        '3' => 3, 
        '2' => 2,
    ];

const suits = "DHSC"; // Allowed card suits. Parsing crashes if suit isn't one of these

class Card 
{
    // Data structure to model a single card. I cache the value didn't want to look it up all the time
	public string $rank;
	public string $suit;
	public int $value;

    public function __construct(string $card) {
        $this->rank = substr($card, 0, strlen($card) - 1);
        $this->suit = substr($card, strlen($card) - 1, 1);
        if (!array_key_exists($this->rank, cardValue)) {
            throw new InvalidArgumentException('Invalid card rank');
        }
        if (!str_contains(suits, $this->suit)) {
            throw new InvalidArgumentException('Invalid card suit');
        }
        $this->value = cardValue[$this->rank] ?? 0;
    }

    public function __toString() : string {
        return "{$this->rank}{$this->suit}";
    }

    public function compare(Card $other) : int {
        return $this->value - $other->value;
    }

}

class Hand 
{
    // Data structure to model a hand of cards.
	public array $cards; // Parsed cards
	public string $original; // Original string representation so the test cases don't fail.

    public function __construct(string $hand) {
        $this->original = $hand;
        $this->cards = [];
        $cards = explode(",", $hand);

        foreach($cards as $card) {
            $this->cards[] = new Card($card);
        }
        if (count($this->cards) != 5) {
            throw new InvalidArgumentError('A hand should contain five cards.');
        }
    }

    // Analyze a hand to get the number of distinct card ranks and suits and how often they occur.
    // Useful for things like 3 of a kind.
    // @param hand: The hand to analyze:
    // @returns rank to occurence count map, and suit to occurence map. (Why didn't I use this for isFlush)
    public function analyze() : array {
        $ranks = [];
        $suite = [];
        foreach($this->cards as $card)  {
            $ranks[$card->rank] = ($ranks[$card->rank] ?? 0) + 1;
            $suits[$card->suit] = ($suits[$card->suit] ?? 0) + 1;
        }
	    return ['ranks' => $ranks, 'suits' => $suits];
    }

    public function sort() {
        usort($this->cards, function($cardA, $cardB) { return $cardA->value - $cardB->value; });
    }

    public function __toString() : string {
        $ret = "[";
        $first = true;
        foreach($this->cards as $card) {
            if (!$first) { 
                $ret .= ", ";
            }
            $first = false;
            $ret .= $card;
        }
        $ret .= "]";
        return $ret;
    }
}

class Poker
{    

    /**
     * In PHP 8.4 and newer, you can use to enhance data encapsulation:
     * - Asymmetric Property Visibility
     *   @see https://www.php.net/manual/en/language.oop5.visibility.php#language.oop5.visibility-members-aviz
     * Or:
     * - Property hooks
     *   @see https://www.php.net/manual/en/language.oop5.property-hooks.php
     */

    public array $bestHands = [];

    public function __construct(array $hands)
    {
        $this->bestHands = array_reverse($this->BestHand($hands));
    }


    // From a list of given card hands choose the best and return it. Return multiple hands if they are equal.
    // @param hands: slice of string with one hand per string.
    // @returns: slice of string with the best hand(s)
    // @raises: error if no hands, or malformed cards.
    private function BestHand(array $hands) : array {
        if (count($hands) == 0) {
            throw new InvalidArgumentError('No hands to check');
        }
        $playerHands = [];
        foreach ($hands as $hand)  {
            $playerHands[] = new Hand($hand);
        }
        usort($playerHands, function($handA, $handB) {
            return $this->HandCompare($handA, $handB);
        });
        $result = [];
        $biggest = end($playerHands);
        for ($i = count($playerHands) - 1; $i >= 0; $i--) {
            if ($this->HandCompare($biggest, $playerHands[$i]) == 0) {
                $result[] = $playerHands[$i]->original;
            } else {
                break;
            }
        }
        return $result;
    }

    // Compare two hands and return a compare number to say which is better
    // @param a: The first hand
    // @param b: The second hand
    // @returns > 0: a, == 0: tie, < 0: p2
    private function HandCompare(Hand $a, Hand $b) : int {
        $result = 0;
        $done = false;

        $result = $this->StraightFlush($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->FourOfAKind($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->FullHouse($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->Flush($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->Straight($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->ThreeOfAKind($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->TwoPair($a, $b, $done);
        if (!$done) {
            return $result;
        }

        $result = $this->OnePair($a, $b, $done);
        if (!$done) {
            return $result;
        }
        return $this->HighCard($a, $b, $done);
    }


    // If one or more hands is a straight and a flush return the better straight (if both match)
    // @param p1: First hand
    // @param p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also returns keep trying flag. If set treat it as if no desired value was found.
    private function StraightFlush(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $flushP1 = $this->IsFlush($p1);
        $flushP2 = $this->IsFlush($p2);
        $p1Cancel = false;
        $this->Straight($p1, $p1, $p1Cancel);
        $p2Cancel = false;
        $this->Straight($p2, $p2, $p2Cancel);

        if ($flushP1 && $flushP2) {
            if (!$p1Cancel && !$p2Cancel) {
                return $this->Straight($p1, $p2, $keepTrying);
            }
            if (!$p1Cancel && $p2Cancel) {
                $keepTrying = false;
                return 1;
            }
            if ($p1Cancel && !$p2Cancel) {
                $keepTrying = false;
                return -1;
            }
            if ($p1Cancel && $p2Cancel) {
                $keepTrying = true;
                return 0;
            }
        }

        if ($flushP1 && !$flushP2) {
            if (!$p1Cancel) {
                $keepTrying = false;
                return 1;
            }
        }

        if (!$flushP1 && $flushP2) {
            if (!$p2Cancel) {
                $keepTrying = false;
                return -1;
            }
        }

        $keepTrying = true;
        return 0;
    }

    // If one or more hands has a four of a kind (4 cards of same rank) return the one with the better rank or leftover if ranks match.
    // @param p1: First hand
    // @param p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also returns keep trying flag. If set treat it as if no desired value was found.
    private function FourOfAKind(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $quadP1 = false;
        $quadP2 = false;
        $highP1 = 0;
        $highP2 = 0;
        $kickerP1 = 0;
        $kickerP2 = 0;

        $p1Rank = $p1->analyze()['ranks'];
        foreach ($p1Rank as $key => $value) {
            if ($value == 4) {
                $quadP1 = true;
                $highP1 = cardValue[$key];
            } else {
                $kickerP1 = cardValue[$key];
            }
        }
        $p2Rank = $p2->analyze()['ranks'];
        foreach($p2Rank as $key => $value) {
            if ($value == 4) {
                $quadP2 = true;
                $highP2 = cardValue[$key];
            } else {
                $kickerP2 = cardValue[$key];
            }
        }

        if (!$quadP1 && !$quadP2) {
            $keepTrying = true;
            return 0;
        }

        if ($quadP1 && !$quadP2) {
            $keepTrying = false;
            return 1;
        }
        if (!$quadP1 && $quadP2) {
            $keepTrying = false;
            return -1;
        }
        $cmpP1 = $highP1;
        $cmpP2 = $highP2;
        if ($cmpP1 == $cmpP2) {
            $cmpP1 = $kickerP1;
            $cmpP2 = $kickerP2;
        }
        $keepTrying = false;
        return $cmpP1 - $cmpP2;
    }

    // If one or more hands has a three of a kind return the one with the better value or best other value if their triplets match.
    // @param p1: First hand
    // @param p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also returns keep trying flag. If set treat it as if no desired value was found.
    private function ThreeOfAKind(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $tripleP1 = false;
        $tripleP2 = false;
        $kickersP1 = [];
        $kickersP2 = [];
        $highP1 = 0;
        $highP2 = 0;

        $p1Rank = $p1->analyze($p1);
        foreach($p1Rank['ranks'] as $key => $value) {
            if ($value == 3) {
                $tripleP1 = true;
                $highP1 = cardValue[$key];
            } else {
                $kickersP1[] = cardValue[$key];
            }
        }
        $p2Rank = $p2->analyze($p2);
        foreach ($p2Rank['ranks'] as $key => $value) {
            if ($value == 3) {
                $tripleP2 = true;
                $highP2 = cardValue[$key];
            } else {
                $kickersP2[] = cardValue[$key];
            }
        }

        if (!$tripleP1 && !$tripleP2) {
            $keepTrying = true;
            return 0;
        }

        if ($tripleP1 && !$tripleP2) {
            $keepTrying = false;
            return 1;
        }
        if (!$tripleP1 && $tripleP2) {
            $keepTrying = false;
            return -1;
        }
        $cmpP1 = $highP1;
        $cmpP2 = $highP2;
        if ($cmpP1 == $cmpP2) {
            $cmpP1 = max($kickersP1);
            $cmpP2 = max($kickersP2);
        }
        if ($cmpP1 == $cmpP2) {
            $cmpP1 = min($kickersP1);
            $cmpP2 = min($kickersP2);
        }
        $keepTrying = false;
        return $cmpP1 - $cmpP2;

    }

    // If one or more hands has a full house (one triplet and one double) return the one with the better triplet or double if the triplets match.
    // @param p1: First hand
    // @param p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also returns keep trying flag. If set treat it as if no desired value was found.
    private function FullHouse(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $ranks1 = $p1->analyze()['ranks'];
        $ranks2 = $p2->analyze()['ranks'];

        $fullHouse1 = false;
        $fullHouse2 = false;
        $found3 = false;
        $found2 = false;
        $rank1_3 = "";
        $rank1_2 = "";
        foreach($ranks1 as $key => $value) {
            if ($value == 3) {
                $found3 = true;
                $rank1_3 = $key;
            }
            if ($value == 2) {
                $found2 = true;
                $rank1_2 = $key;
            }
        }
        if ($found2 && $found3) {
            $fullHouse1 = true;
        }
        $found2 = false;
        $found3 = false;
        $rank2_3 = "";
        $rank2_2 = "";
        foreach ($ranks2 as $key => $value) {
            if ($value == 3) {
                $found3 = true;
                $rank2_3 = $key;
            }
            if ($value == 2) {
                $found2 = true;
                $rank2_2 = $key;
            }
        }
        if ($found2 && $found3) {
            $fullHouse2 = true;
        }

        if (!$fullHouse1 && !$fullHouse2) {
            $keepTrying = true;
            return 0;
        }
        if ($fullHouse1 && !$fullHouse2) {
            $keepTrying = false;
            return 1;
        }
        if (!$fullHouse1 && $fullHouse2) {
            $keepTrying = false;
            return -1;
        }

        $cmp1 = cardValue[$rank1_3];
        $cmp2 = cardValue[$rank2_3];
        if ($cmp1 == $cmp2) {
            $cmp1 = cardValue[$rank1_2];
            $cmp2 = cardValue[$rank2_2];
        }
        $keepTrying = false;
        return $cmp1 - $cmp2;
    }

    // Check if a hand is a flush (all cards have the same suit)
    // @param hand: The hand to check
    // @returns: boolean, true if a flush.
    private function IsFlush(Hand $hand) : bool {
        $cmpSuit = $hand->cards[0]->suit;
        for ($i = 0; $i < 5; $i++) {
            if ($hand->cards[$i]->suit != $cmpSuit) {
                return false;
            }
        }
        return true;
    }

    // If one or more hands is a flush return the one with the better high card.
    // @param p1: First hand
    // @param p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also returns keep trying flag. If set treat it as if no desired value was found.
    private function Flush(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $flushP1 = $this->IsFlush($p1);
        $flushP2 = $this->IsFlush($p2);

        if (!$flushP1 && !$flushP2) {
            $keepTrying = true;
            return 0;
        }

        if ($flushP1 && !$flushP2) {
            $keepTrying = false;
            return 1;
        }
        if (!$flushP1 && $flushP2) {
            $keepTrying = false;
            return -1;
        }
        $values1 = [];
        $values2 = [];
        for ($i = 0; $i < 5; $i++) {
            $values1[$i] = $p1->cards[$i]->value;
            $values2[$i] = $p2->cards[$i]->value;
        }
        sort($values1);
        sort($values2);
        for ($i = 4; $i >= 0; $i--) {
            if ($values1[$i] != $values2[$i]) {
                $keepTrying = false;
                return $values1[$i] - $values2[$i];
            }
        }
        $keepTrying = false;
        return 0;
    }

    // If one or more hands is a straight return the one with the better high card.
    // @param Hand $p1: First hand
    // @param Hand $p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also updates keep trying flag. If set treat it as if no desired value was found.
    private function Straight(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $valuesP1 = [];
        $valuesP2 = [];
        $valuesP1Low = [];
        $valuesP2Low = [];
        for ($i = 0; $i < 5; $i++) {
            $valuesP1[$i] = $p1->cards[$i]->value;
            $valuesP2[$i] = $p2->cards[$i]->value;
            if ($p1->cards[$i]->rank == "A") {
                $valuesP1Low[$i] = 1;
            } else {
                $valuesP1Low[$i] = $valuesP1[$i];
            }
            if ($p2->cards[$i]->rank == "A") {
                $valuesP2Low[$i] = 1;
            } else {
                $valuesP2Low[$i] = $valuesP2[$i];
            }
        }
        sort($valuesP1);
        sort($valuesP2);
        sort($valuesP1Low);
        sort($valuesP2Low);

        // echo 'Values P1 ' . $p1 . PHP_EOL;        
        // echo 'Values P2 ' . $p2 . PHP_EOL;
        
        $straightP1 = true;
        $straightP2 = true;
        $straightP1Low = true;
        $straightP2Low = true;
        for ($i = 1; $i < 5; $i++) {
            if ($valuesP1[$i] != $valuesP1[$i - 1] + 1) {
                $straightP1 = false;
            }
            if ($valuesP2[$i] != $valuesP2[$i - 1] + 1) {
                $straightP2 = false;
            }
            if ($valuesP1Low[$i] != $valuesP1Low[$i - 1] + 1) {
                $straightP1Low = false;
            }
            if ($valuesP2Low[$i] != $valuesP2Low[$i - 1] + 1) {
                $straightP2Low = false;
            }
        }
        if ($straightP1Low && !$straightP1) {
            $straightP1 = true;
            foreach($valuesP1 as $i => $value) {
                if ($value == cardValue["A"]) {
                    $valuesP1[$i] = 1;
                }
            }
        }
        if ($straightP2Low && !$straightP2) {
            $straightP2 = true;
            foreach ($valuesP2 as $i => $value) {
                if ($value == cardValue["A"]) {
                    $valuesP2[$i] = 1;
                }
            }
        }
        $straightP1 = $straightP1 || $straightP1Low;
        $straightP2 = $straightP2 || $straightP2Low;

        if (!$straightP1 && !$straightP2) {
            $keepTrying = true;
            return 0;
        }
        if ($straightP1 && !$straightP2) {
            $keepTrying = false;
            return 1;
        }
        if (!$straightP1 && $straightP2) {
            $keepTrying = false;
            return -1;
        }
        $keepTrying = false;
        return max($valuesP1) - max($valuesP2);
    }

    // If one or more hands has a two pairs return the one with the better pair.
    // @param Hand $p1: First hand
    // @param Hand $p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns: Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also updates  keep trying flag. If set treat it as if no desired value was found.
    private function TwoPair(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $pairsP1 = 0;
        $pairsP2 = 0;
        $pairValuesP1 = [];
        $pairValuesP2 = [];
        $kickerP1 = 0;
        $kickerP2 = 0;

        $p1Rank = $p1->analyze();
        foreach($p1Rank['ranks'] as $key => $value) {
            if ($value == 2) {
                $pairsP1++;
                $pairValuesP1[] = cardValue[$key];
            }
            if ($value == 1) {
                $kickerP1 = cardValue[$key];
            }
        }
        $p2Rank = $p2->analyze();
        foreach($p2Rank['ranks'] as $key => $value) {
            if ($value == 2) {
                $pairsP2++;
                $pairValuesP2[] = cardValue[$key];
            }
            if ($value == 1) {
                $kickerP2 = cardValue[$key];
            }
        }

        if ($pairsP1 != 2 && $pairsP2 != 2) {
            $keepTrying = true;
            return 0;
        }

        if ($pairsP1 == 2 && $pairsP2 != 2) {
            $keepTrying = false;
            return 1;
        }
        if ($pairsP1 != 2 && $pairsP2 == 2) {
            $keepTrying = false;
            return -1;
        }
        $cmpP1 = max($pairValuesP1);
        $cmpP2 = max($pairValuesP2);
        if ($cmpP1 == $cmpP2) {
            $cmpP1 = min($pairValuesP1);
            $cmpP2 = min($pairValuesP2);
        }
        if ($cmpP1 == $cmpP2) {
            $cmpP1 = $kickerP1;
            $cmpP2 = $kickerP2;
        }
        $keepTrying = false;
        return $cmpP1 - $cmpP2;
    }

    // If one or more hands has a single pair return the one with the better pair.
    // @param Hand $p1: First hand
    // @param Hand $p2: Second hand
    // @param bool $keepTrying: Reference boolean set to true if found something
    // @returns: Card comparison value where < 0: p2, = 0: tie, > 0: p1
    // also sets keep trying flag. If set treat it as if no desired value was found.
    private function OnePair(Hand $p1, Hand $p2, bool &$keepTrying) : int {
        $pairP1 = false;
        $pairP2 = false;
        $kickersP1 = [];
        $kickersP2 = [];

        $p1Rank = $p1->analyze();
        $valueP1 = 0;
        foreach($p1Rank['ranks'] as $key => $value) {
            if ($value == 2) {
                $pairP1 = true;
                if (cardValue[$key] > $valueP1) {
                    $valueP1 = cardValue[$key];
                }
            } else {
                $kickersP1[] = cardValue[$key];
            }
        }

        $p2Rank = $p2->analyze();
        $valueP2 = 0;
        foreach($p2Rank['ranks'] as $key => $value)  {
            if ($value == 2) {
                $pairP2 = true;
                if (cardValue[$key] > $valueP2) {
                    $valueP2 = cardValue[$key];
                }
            } else {
                $kickersP2[] = cardValue[$key];
            }
        }

        if (!$pairP1 && !$pairP2) {
            $keepTrying = true;
            return 0;
        }
        if ($pairP1 && !$pairP2) {
            $keepTrying = false;
            return 1;
        }
        if (!$pairP1 && $pairP2) {
            $keepTrying = false;
            return -1;
        }

        $cmpP1 = $valueP1;
        $cmpP2 = $valueP2;
        if ($cmpP1 == $cmpP2) {
            sort($kickersP1);
            sort($kickersP2);
            for ($i = count($kickersP1) - 1; $i >= 0; $i--) {
                $cmpP1 = $kickersP1[$i];
                $cmpP2 = $kickersP2[$i];
                if ($cmpP1 != $cmpP2) {
                    break;
                }
            }
        }
        $keepTrying = false;
        return $cmpP1 - $cmpP2;
    }

    // Return which hand has the high card
    // @param p1: First hand
    // @param p2: Second hand
    // @returns Card comparison value where < 0: p2, = 0: tie, > 0: p1
    private function HighCard(Hand $p1, Hand $p2) : int {
        $p1->sort();
        $p1->sort();
        $i = count($p1->cards) - 1;
        $j = count($p2->cards) - 1;
        while ($i >= 0 && $j >= 0) {
            $result = $p1->cards[$i]->compare($p2->cards[$j]);
            if ($result != 0) {
                return $result;
            }
            $i--;
            $j--;
        }
        // Tie if same card count otherwise larger count wins.
        return count($p1->cards) - count($p2->cards);
    }

}
