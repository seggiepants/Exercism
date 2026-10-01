<?php
// Split-Second Stopwatch exercise.

declare(strict_types=1);

// Implement the Split Second Stopwatch.
// You can start, stop the watch. Click lap to save the current time as a lap, and reset the values when in the stopped state.
class SplitSecondStopwatch
{
    const ERR_ALREADY_STARTED = "cannot start an already running stopwatch";
    const ERR_NOT_RUNNING = "cannot stop a stopwatch that is not running";
    const ERR_LAP_NOT_RUNNING = "cannot lap a stopwatch that is not running";
    const ERR_RESET_NOT_STOPPED = "cannot reset a stopwatch that is not stopped";

    const STATE_READY = "ready";
    const STATE_RUNNING = "running";
    const STATE_STOPPED = "stopped";

    const ZERO_TIME = "00:00:00";

    /**
     * In PHP 8.4 and newer you can use Asymmetric Property Visibility to enhance data encapsulation
     * @see https://www.php.net/manual/en/language.oop5.visibility.php#language.oop5.visibility-members-aviz
     */
    public private(set) string $state;
    public private(set) array $previousLaps;

    private string $lapTime;

    // Setup the class for use.
    public function __construct()
    {
        $this->lapTime = self::ZERO_TIME;
        $this->previousLaps = array();
        $this->state = self::STATE_READY;
    }

    // Add methods as expected by the tests!

    // Get the time in the current lap so far.
    // @returns string: The current lap time in hh:mm:ss format
    public function getCurrentLap() : string {
        return $this->lapTime;
    }

    // Return the sum of the current and previous lap times.
    // @returns string: The total time counted so far including current and previous laps
    public function getTotal() : string {
        $result = $this->lapTime;
        foreach($this->previousLaps as $lap) {
            $result = $this->timeAdd($result, $lap);
        }
        return $result;
    }

    // Get the previous lap times
    // @returns array: An array of strings in hh:mm:ss format for the previous but not current laps.
    public function previousLaps() : array {
        return array_copy($this->previousLaps);
    }

    // Start the timer.
    // @throws Exception: When already in the running state.
    public function start() {
        switch ($this->state) {
            case self::STATE_RUNNING:
                throw new Exception(self::ERR_ALREADY_STARTED);
                break;
            default:
                $this->state = self::STATE_RUNNING;
                break;
        }
    }

    // Stop the timer.
    // @throws Exception: When not in the running state.
    public function stop() {
        if ($this->state != self::STATE_RUNNING) {
            throw new Exception(self::ERR_NOT_RUNNING);
        }
        $this->state = self::STATE_STOPPED;

    }

    // Save the current lap into the previous laps and reset the current lap time.
    // @throws Exception: When the timer is not running.
    public function lap() {
        if ($this->state != self::STATE_RUNNING) {
            throw new Exception(self::ERR_LAP_NOT_RUNNING);
            return;
        }
        $this->previousLaps[] = $this->lapTime;
        $this->lapTime = self::ZERO_TIME;
    }

    // Reset the timer. Clear the previous and current lap times.
    // @throws Exception: When the timer is not in the stopped state.
    public function reset() {
        if ($this->state != self::STATE_STOPPED) {
            throw new Exception(self::ERR_RESET_NOT_STOPPED);
        }
        $this->lapTime = self::ZERO_TIME;
        $this->previousLaps = array();
        $this->state = "ready";
    }

    // Advance time by the given amount.
    // @param string $amount: The amount to advance the time by in hh:mm:ss format.    
    public function advanceTime($amount) {
        if ($this->state == self::STATE_RUNNING) {
            $this->lapTime =  $this->timeAdd($this->lapTime, $amount);
        }
    }

    // Add two time strings together. Since we are using strings everywhere else, it seems easier to 
    // sum them up from strings too.
    // @param string $time1: Time value in hh:mm:ss format.
    // @param string $time2: Time value in hh:mm:ss format.
    // @returns string: The sum of the two time values in hh:mm:ss format
    // @throws InvalidArgumentException: If there is a problem parsing $time1 or $time2.
    private function timeAdd($time1, $time2) : string {
        $patternTime = "/(\d{2}):(\d{2}):(\d{2})/i";

        if (preg_match($patternTime, $time1, $matches1) != 1) {
            throw new InvalidArgumentException($time1 . " is not a valid time.");            
        };
        if (preg_match($patternTime, $time2, $matches2) != 1) {
            throw new InvalidArgumentException($time2 . " is not a valid time.");
        }

        $seconds = intval($matches1[3]) + intval($matches2[3]);
        $carry = 0;
        while ($seconds >= 60) {
            $seconds -= 60;
            $carry++;
        }

        $minutes = intval($matches1[2]) + intval($matches2[2]) + $carry;
        $carry = 0;
        while ($minutes >= 60) {
            $minutes -= 60;
            $carry++;
        }

        $hours = intval($matches1[1]) + intval($matches2[1]) + $carry;

        return sprintf("%02d:%02d:%02d", $hours, $minutes, $seconds);

    }
}
