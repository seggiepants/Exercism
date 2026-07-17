<?php
// Triangle Categorization Exercise - Isosceles, Equilateral, or Scalene

declare(strict_types=1);

// Class that categorizes triangle type by side lengths
class Triangle
{
    // Create a class instance that checks triangle types for the instance side lengths.
    // @param $a: First side length
    // @param $b: Second side length
    // @param $c: Third side length
    public function __construct(int $a, int $b, int $c)
    {
        $this->a = abs($a);
        $this->b = abs($b);
        $this->c = abs($c);
    }

    // Check if the given side lengths can form a triangle.
    // @returns: True if a triangle can be made from the side lengths.
    private function isTriangle(): bool 
    {        
        if ($this->a + $this->b + $this->c == 0) {
            return false;
        }
        if (($this->a + $this->b < $this->c) || ($this->a + $this->c < $this->b) || ($this->b + $this->c < $this->a)) {
            return false;
        }
        return true;
    }

    // Check if triangle is Equilateral (all sides have the same length)
    // @returns: True if a Equilateral triangle.
    public function isEquilateral(): bool
    {
        return $this->isTriangle() && (($this->a == $this->b) && ($this->a == $this->c));
    }

    // Check if triangle is Isosceles (at least two sides are the same length)
    // Note: An Equilateral triangle is also Isosceles.
    // @returns: True if a Isosceles triangle.
    public function isIsosceles(): bool
    {
        return $this->isTriangle() && (($this->a == $this->b) || ($this->a == $this->c) || ($this->b == $this->c));
    }

    // Check if triangle is Scalene (all sides have different lengths)
    // @returns: True if a Scalene triangle.
    public function isScalene(): bool
    {
        return $this->isTriangle() && (($this->a != $this->b) && ($this->a != $this->c) && ($this->b != $this->c));
    }
}
