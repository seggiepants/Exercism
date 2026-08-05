<?php
// Allergies exercise - Really a test in bit flags

declare(strict_types=1);

// Class for Allergy related functions.
class Allergies
{
    private $score;

    // Create a new Allergies class
    // @param $score: 
    public function __construct(int $score)
    {
        $this->score = $score;
    }

    // Check if the score shows that we are allergic to a given allergen
    // @param $allergen: The allergen to check against.
    // @returns: true if the score shows being allergic to that allergen.
    public function isAllergicTo(Allergen $allergen): bool
    {
        return ($this->score & $allergen->value) != 0;
    }

    // Return an array of allergens the score matches
    // @returns: array of allergens matching the score.
    public function getList(): array
    {
        $result = array();
        foreach(Allergen::allergenList() as $key => $value) {
            $allergen = new Allergen($key);
            if ($this->isAllergicTo($allergen)) {
                $result[] = $allergen;
            }
        }
        return $result;
    }
}

// Abstracts an Allergen
class Allergen
{
    const EGGS = 1;
    const PEANUTS = 2;
    const SHELLFISH = 4;
    const STRAWBERRIES = 8;
    const TOMATOES = 16;
    const CHOCOLATE = 32;
    const POLLEN = 64;
    const CATS = 128;

    public int $value;
    
    // Create a new Allergen
    // @param $value: Numeric Allergen Bit Flag
    public function __construct(int $value)
    {
        $this->value = $value;
    }

    // Return an array of known allergens
    // @returns: array of known allergens
    public static function allergenList(): array
    {
        return array(1 => "eggs", 2 => "peanuts", 4 => "shellfish", 8 => "strawberries", 
        16 => "tomatoes", 32 => "chocolate", 64 => "pollen", 128 => "cats");
    }
}
