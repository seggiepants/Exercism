<?php
// Create a clock object that doesn't use date functions. It should have
// accuracy to minutes.

declare(strict_types=1);

class Clock
{    
    // Construct a clock object. 
    // @param $hour: Hour the clock is set to (defaults to 0)
    // @param $minute: Minute the clock is set to (defaults to 0)
    public function __construct(int $hour = 0, int $minute = 0) {        
        $this->minutes = ($hour * 60) + $minute;
        $this->normalize();
    }

    // Implements class toString function.
    // @returns: The string representation of the Clock object
    public function __toString(): string
    {
        $minutes = $this->minutes % 60;
        $hours = ($this->minutes - $minutes) / 60;
        return sprintf("%02d:%02d", $hours, $minutes);        
    }

    // Mystery function you don't know you need to implement until the tests fail.
    // Add minutes to the clock and return the clock object (was this supposed to be
    // a new clock object?)
    // @param minutes: The number of minutes to add.
    // @returns: This updated clock object.
    public function add(int $minutes) : Clock {
        $this->minutes += $minutes;
        $this->normalize();
        return $this;
    }

    // Mystery function you don't know you need to implement until the tests fail.
    // Subtrac minutes from the clock and return the clock object (was this supposed to be
    // a new clock object?)
    // @param minutes: The number of minutes to subtract.
    // @returns: This updated clock object.    
    public function sub(int $minutes) : Clock {
        $this->minutes -= $minutes;
        $this->normalize();
        return $this;
    }

    // Get the hours on the clock.
    // Gold Plating by me.
    // @returns: Hours on the clock.
    public function getHour(): int
    {
        return ($this->minutes - ($this->minutes % 60)) / 60;
    }

    // Get the minutes on the clock.
    // Gold Plating by me.
    // @returns: Number of minutes on the clock.
    public function getMinute(): int
    {
        return $this->minutes % 60;
    }

    // Normalize the time on the clock to between 00:00 and 23:59 minutes
    private function normalize() {
        $maxTime = 24 * 60;
        $minTime = 0;
        while ($this->minutes < $minTime) {
            $this->minutes += $maxTime;
        }

        while ($this->minutes >= $maxTime) {
            $this->minutes -= $maxTime;
        }
    }
}
