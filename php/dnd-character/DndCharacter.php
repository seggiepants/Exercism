<?php
// DND Character Exercise

declare(strict_types=1);

// Model a character in a DND Role Playing game.
class DndCharacter
{
    public $hitpoints;
    public $strength;
    public $dexterity;
    public $constitution;
    public $intelligence;
    public $wisdom;
    public $charisma;

    // DND Character Generator constructor.
    public function __construct()
    {
        $this->strength = self::ability();
        $this->dexterity = self::ability();
        $this->constitution = self::ability();
        $this->intelligence = self::ability();
        $this->wisdom = self::ability();
        $this->charisma = self::ability();
        $this->hitpoints = 10 + self::modifier($this->constitution);
    }

    // Generate a new DND Character and return it.
    // @returns: A new DND Character that was randomly generated.
    public static function generate() {
        return new DndCharacter();
    }

    // Generate a random ability rating by rolling four dice. Skipping the minimum value
    // and returning the sum of the remaining rolls.
    // @returns: The generated ability rating.
    public static function ability() {
        $rolls = array(rand(1, 6), rand(1, 6), rand(1, 6), rand(1,6));
        return array_sum($rolls) - min($rolls);
    }

    // Add methods as expected by the tests

    // Generate the hit point modifier given a character's constitution
    // @param $constitution: The character's constitution
    // @returns: modifier based on the character's constitution
    public static function modifier($constitution) {
        return floor(($constitution - 10) / 2);

    }

    // To String function for debugging.
    // @returns: String representation of the character class
    public function ToString() {
        return <<<TOSTRING
HP:\t{$this->hitpoints}
Strength:\t{$this->strength}
Dexterity:\t{$this->dexterity}
Constitution:\t{$this->constitution}
Intelligence:\t{$this->intelligence}
Wisdom:\t{$this->wisdom}
Charisma: \t{$this->charisma}

TOSTRING;
    }


}
