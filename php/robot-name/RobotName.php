<?php
// Robot Name exercise.

declare(strict_types=1);

// Robot
class Robot
{
    private static array $robotNames = []; // Private static array is much better than a global.
    public string $name = "";
    // Return the name of this robot.
    // @returns: The random name of the robot in AANNN format where A is a letter A-Z and N is a digit 0-9
    public function getName(): string
    {
        // Give the robot a name if it doesn't have one.
        if (strlen($this->name) == 0) {
            $this->reset();
        }
        return $this->name;
    }

    // Reset the robot by generating a new name for it. Old names may not be reused.
    public function reset(): void
    {

        while (true) {
            $name = chr(ord("A") + rand(0, 25)) . 
                chr(ord("A") + rand(0, 25)) . 
                chr(ord("0") + rand(0, 9)) . 
                chr(ord("0") + rand(0, 9)) . 
                chr(ord("0") + rand(0, 9));
            
            // Access private static with self::
            if (!in_array($name, self::$robotNames)) {
                self::$robotNames[] = $name;
                $this->name = $name;
                break;
            }
        }        
    }
}
