<?php

// Exercise Zebra Puzzle

declare(strict_types=1);

// Populate five houses based on constraints.
class ZebraPuzzle
{
    const colors = ['ivory', 'yellow', 'blue', 'red', 'green'];
    const nationalities = ['Englishman', 'Japanese', 'Spainiard', 'Norwegian', 'Ukranian'];
    const drinks = ['water', 'tea', 'milk', 'coffee', 'orange juice'];
    const pets = ['dog', 'zebra', 'fox', 'horse', 'snail'];
    const hobbies = ['dancing', 'painting', 'reading', 'football', 'chess'];

    private array $data = [];

    // Constructor. Setup the defaults.
    public function __construct() {
        $this->data = [];
        for($i = 0; $i < 5; $i++) {
            $this->data[] = ['color' => '', 'nationality' => '', 'drink' => '', 'pet' => '', 'hobby' => ''];
        }

        // populate the givens
        $this->data[0]['nationality'] = 'Norwegian'; // The Norwegian lives in the first house.
        $this->data[1]['color'] = 'blue'; // The Norwegian lives next to the blue house
        $this->data[2]['drink'] = 'milk'; // The middle house drinks milk

        if (!$this->isFull())
        {
            $undoStack = [];
            if (!$this->fillPuzzle($undoStack)) {
                echo 'Fill failed';
            }
        }
    }

    public function __toString() {
        $result = 'Color, Nationality, Drink, Hobby, Pet' . PHP_EOL;
        foreach($this->data as $i => $house) {
            $index = strval($i + 1);
            $result .= "{$index}: {$house['color']}, {$house['nationality']}, {$house['drink']}, {$house['hobby']}, {$house['pet']}" . PHP_EOL;
        }
        return $result;
    }

    // Populate the puzzle
    private function fillPuzzle(array &$undoStack) {
        for($house = 0; $house < count($this->data); $house++) {
            $pairs = [['color', self::colors]
                , ['nationality', self::nationalities]
                , ['drink', self::drinks]
                , ['pet', self::pets]
                , ['hobby', self::hobbies]];
            foreach($pairs as $pair) {
                $key = $pair[0];
                $values = $pair[1];
                if ($this->data[$house][$key] == '') {
                    $used = array_map(function($value) use($key) {
                        return $value[$key];
                        }, array_filter($this->data, function($value) use($key) {
                            return $value[$key] != '';
                        }));
                    $free = array_filter($values, function($value) use($used) {
                        return !in_array($value, $used);
                        });
                        
                    if (count($free) > 0) {
                        foreach($free as $attempt) {
                            $this->data[$house][$key] = $attempt;
                            $undoStack[] = [$house, $key];
                            $stackPointer = count($undoStack);
                            $success = $this->populate($undoStack);
                            if (!$success) {
                                $this->undo($undoStack, $stackPointer);
                            } else  {
                                $success = $this->fillPuzzle($undoStack);
                                if (!$success) {
                                    $this->undo($undoStack, $stackPointer);
                                } else {
                                    break;
                                }
                            }
                        }
                    }
                }        
            }
            if ($this->data[$house]['color'] == '' || 
                $this->data[$house]['nationality'] == '' || 
                $this->data[$house]['drink'] == '' ||
                $this->data[$house]['hobby'] == '' ||
                $this->data[$house]['pet'] == '') {
                return false;
            }
            if ($this->isFull()) {
                return true;
            }
        }
        return $this->populate($undoStack) && $this->isFull();
    }

    // Find the house that has a key with the given value
    // @param string $key: The key to look at.
    // @param string $value: The expected value for the key.
    // @returns int: The index of the value. -1 if not found.
    private function find(string $key, string $value) : int {
        $result = array_find_key($this->data, function($house) use ($key, $value) {
            return $house[$key] == $value;
        });
        if (is_null($result)) {
            return -1;
        }
        return $result;
    }

    // Check if the puzzle data has been filled in or not.
    // @returns: True if full otherwise False
    private function isFull() : bool {
        return !array_any($this->data, function($value) {
            return $value['color'] == '' ||
                $value['nationality'] == '' ||
                $value['drink'] == '' ||
                $value['pet'] == '' || 
                $value['hobby'] == '';
            });
    }

    // Populate if house[key] == value then house[otherKey] = otherValue
    // @param array $undoStack: Stack of moves to undo if things don't work out.
    // @param string $key1: The key of the first part of the pair.
    // @param string $value1: The value for the first part of the pair.
    // @param string $key2: The key for the second half of the pair.
    // @param string $value2: The value for the second half of the pair.
    // @returns bool: True if a value was populated.
    private function populatePair(array &$undoStack, string $key1, string $value1, string $key2, string $value2) : bool {
        $index = $this->find($key1, $value1);
        if ($index >= 0) {
            if ($this->data[$index][$key2] == '') {
                $undoStack[] = [$index, $key2];
                $this->data[$index][$key2] = $value2;
                return true;
            } else if ($this->data[$index][$key2] != $value2) {
                return false;
            }
        }
    
        $index = $this->find($key2, $value2);
        if ($index >= 0) {
            if ($this->data[$index][$key1] == '') {
                $undoStack[] = [$index, $key1];
                $this->data[$index][$key1] = $value1;
                return true;
            }
            else if ($this->data[$index][$key1] != $value1) {
                return false;
            }
        }
        return true;
    }

    // Populate if house[key] == value then houseToRight[otherKey] = otherValue
    // @param array $undoStack: Stack of moves to undo if things don't work out.
    // @param string $key1: The key of the first part of the pair.
    // @param string $value1: The value for the first part of the pair.
    // @param string $key2: The key for the second half of the pair.
    // @param string $value2: The value for the second half of the pair.
    // @returns bool: True if a value was populated.
    private function populatePairRight(array &$undoStack, string $key1, string $value1, string $key2, string $value2) {
        $index1 = $this->find($key1, $value1);
        $index2 = $this->find($key2, $value2);

        // if you have both they had better match
        if ($index1 >= 0 && $index2 >=0 && ($index1 + 1) != $index2) {
            return false;
        }

        if ($index1 >= 0 && $index1 <= count($this->data) - 2) {
            if ($this->data[$index1 + 1][$key2] == '') {
                $undoStack[] = [$index1 + 1, $key2];
                $this->data[$index1 + 1][$key2] = $value2;
                return true;
            } else if ($this->data[$index1 + 1][$key2] != $value2) {
                return false;
            }
        }

        if ($index2 >= 1 && $index2 < count($this->data)) {
            if ($this->data[$index2 - 1][$key1] == '') {
                $undoStack[] = [$index2 - 1, $key1];
                $this->data[$index2 - 1][$key1] = $value1;
                return true;
            }
            else if ($this->data[$index2 - 1][$key1] != $value1) {
                return false;
            }
        }
        return true;
    }

    // These searches are lengthy to write so I broke this one in two and
    // just call it with both possible combinations (should be redundant, really).
    // @param array $undoStack: Stack of moves to undo if things don't work out.
    // @param string $key1: The key of the first part of the pair.
    // @param string $value1: The value for the first part of the pair.
    // @param string $key2: The key for the second half of the pair.
    // @param string $value2: The value for the second half of the pair.
    // @returns bool: True if a value was populated.
    private function populatePairNeighbor_Step(array &$undoStack, string $key1, string $value1, string $key2, string $value2) {
        $index = $this->find($key1, $value1);
        if ($index >= 0) {
            $sides = [];
            if ($index - 1 >= 0) {
                $sides[] = [$index -1, $this->data[$index - 1][$key2]];
            }
            if ($index + 1 < count($this->data)) {
                $sides[] = [$index + 1, $this->data[$index + 1][$key2]];
            }

            if (count($sides) == 1 && $sides[0][1] != $value2 && $sides[0][1] != '') {
                return false;
            } 
            if (count($sides) == 1 && $sides[0][1] == $value2) {
                return true;
            } 
            
            if (count($sides) == 1 && $sides[0][1] == '') {
                // only one and it is empty, fill it
                $undoStack[] = [$sides[0][0], $key2];
                $this->data[$sides[0][0]][$key2] = $value2;
                return true;
            } 
            
            if (count($sides) == 2 && $sides[0][1] != '' && $sides[1][1] != '' && $sides[0][1] != $value2 && $sides[1][1] != $value2) {
                // both sides not target
                return false;
            }
            
            if (count($sides) == 2 && ($sides[0][1] == $value2 || $sides[1][1] == $value2)) {
                // one side is target
                return true;
            } 
            
            if (count($sides) == 2 && $sides[0][1] == '' && $sides[1][1] != '' && $sides[1][1] != $value2) {
                // left empty, right not empty not target
                $undoStack[] = [$sides[0][0], $key2];
                $this->data[$sides[0][0]][$key2] = $value2;
            } else if (count($sides) == 2 && $sides[0][1] != '' && $sides[1][1] == '' && $sides[0][1] != $value2) {
                // right empty, left not empty not target
                $undoStack[] = [$sides[1][0], $key2];
                $this->data[$sides[1][0]][$key2] = $value2;
            }            
        }
        return true;
    }

    // Populate if house[key] == value then houseToLeftOrRight[otherKey] = otherValue
    // @param array $undoStack: Stack of moves to undo if things don't work out.
    // @param string $key1: The key of the first part of the pair.
    // @param string $value1: The value for the first part of the pair.
    // @param string $key2: The key for the second half of the pair.
    // @param string $value2: The value for the second half of the pair.
    // @returns bool: True if a value was populated.
    private function populatePairNeighbor(array &$undoStack, string $key1, string $value1, string $key2, string $value2) {
        $index1 = $this->find($key1, $value1);
        $index2 = $this->find($key2, $value2);
        // Both populated but more than one spot apart is an error
        if ($index1 >= 0 && $index2 >= 0) {
            if (($this->data[$index1][$key1] == $value1 && $this->data[$index2][$key2] == $value2) ||
                ($this->data[$index2][$key1] == $value1 && $this->data[$index1][$key2] == $value2)) {
                return true;
            }

            if (abs($index2 - $index1) > 1) {
                return false;
            }
        }

        if (!$this->populatePairNeighbor_Step($undoStack, $key1, $value1, $key2, $value2)) {
            return false;
        }
        if (!$this->populatePairNeighbor_Step($undoStack, $key2, $value2, $key1, $value1)) {
            return false;
        }
        return true;
    }

    // Run through all the rules (except the givens -- can be filled in without futher data)
    // if a rule fails the tests return false, if no errors found return true.
    // @param array $undoStack: Moves to undo, may be populated in the function call.
    // @return bool: True if not errors found. False if a rule fails.
    private function populate(array &$undoStack) {
        // 1 There are five houses
        if (count($this->data) != 5) {
            return false;
        }

        // 2 Englishman lives in the red house
        if (!$this->populatePair($undoStack, 'nationality', 'Englishman', 'color', 'red')) {
            return false;
        }

        // 3 Spaniard owns a dog
        if (!$this->populatePair($undoStack, 'nationality', 'Spainiard', 'pet', 'dog')) {
            return false;
        }

        // 4 Green house drinks coffee
        if (!$this->populatePair($undoStack, 'color', 'green', 'drink', 'coffee')) {
            return false;
        }
        
        // 5 Ukranian drinks tea
        if (!$this->populatePair($undoStack, 'nationality', 'Ukranian', 'drink', 'tea')) {
            return false;
        }

        // 6 - Green house right of Ivory house    
        if (!$this->populatePairRight($undoStack, 'color', 'ivory', 'color', 'green')) {
            return false;
        }

        // 7 Snail owner goes dancing
        if (!$this->populatePair($undoStack, 'pet', 'snail', 'hobby', 'dancing')) {
            return false;
        }
        
        // 8 Yellow house likes painting
        if (!$this->populatePair($undoStack, 'color', 'yellow', 'hobby', 'painting')) {
            return false;
        }
        
        // 9 Middle house drinks milk is a given
        
        // 10 1st house is Norwegian is a given
        
        // 11 - Reading next to fox
        if (!$this->populatePairNeighbor($undoStack, 'hobby', 'reading', 'pet', 'fox')) {
            return false;
        }

        // 12 - Painter next to horse
        if (!$this->populatePairNeighbor($undoStack, 'hobby', 'painting', 'pet', 'horse')) {
            return false;
        }

        // 13 Football drinks orange juice
        if (!$this->populatePair($undoStack, 'hobby', 'football', 'drink', 'orange juice')) {
            return false;
        }

        // 14 Japanese plays chess
        if (!$this->populatePair($undoStack, 'nationality', 'Japanese', 'hobby', 'chess')) {   
            return false;
        }
        // 15 is also a given Norwegian (1st house) is next to blue house where 2 is only neighbor 
        return true;
    }

    // Undo or remove frames until you get to a given stack pointer clearing out the changes as you go.
    // @param array $undoStack: The stack of changes.
    // @param int $stackPointer: The point at which to stop popping and clearing out the data.
    private function undo(array &$undoStack, int $stackPointer) {
        while (count($undoStack) >= $stackPointer) {
            [$house, $key] = array_pop($undoStack);
            $this->data[$house][$key] = '';
        }
    }

    // Return the nationality of the house whoes preferred drink is water.
    // @returns string: The nationality of the house that prefers to drink water.
    public function waterDrinker(): string
    {
        return $this->data[$this->find('drink', 'water')]['nationality'];
    }

    // Return the nationality of the house with a zebra
    // @returns string: The nationality of the house that has a pet Zebra
    public function zebraOwner(): string
    {
        return $this->data[$this->find('pet', 'zebra')]['nationality'];
    }
}
