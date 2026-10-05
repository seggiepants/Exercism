<?php

// Swift Scheduling exercise

declare(strict_types=1);

class SwiftScheduling
{
    // These are probably a constant somewhere else. I just don't know where.
    const Monday = 1;
    const Tuesday = 2;
    const Wednesday = 3;
    const Thursday = 4;
    const Friday = 5;
    const Saturday = 6;
    const Sunday = 7;

    public private(set) DateTime $startTime; // Reference date (meeting start date/time).

    // Initialize the class by saving the meeting start date/time.
    // @param DateTime $meetingStart: Reference date to compute delivery dates from.
    public function __construct(DateTime $meetingStart)
    {
        $this->startTime = $meetingStart;
    }

    // Compute a delivery date based on the class' reference date and the given description.
    // @param string $description: Valid values are NOW, ASAP, EOW, Q[1-4], and
    // [1-12]N where [1-2] and [1-12] are a number you pick from a range. They 
    // translate to Now, As Soon as Possible, End of Week, Nth Quarter and Nth Month.
    public function deliveryDate(string $description): DateTime
    {
        $patternMonth = '/(\d+)M/';
        $patternQuarter = '/Q(\d+)/';
        $result = $this->startTime;

        switch($description) {
            case "NOW":
                $result = $this->startTime;
                $result = $result->add(DateInterval::createFromDateString("2 hours"));
                break;
            case "ASAP":
                $result = $this->startTime;
                $hour = intval($result->format("H"));
                if ($hour < 13) {
                    $result = $this->dateSetTime($result, "17:00");
                } else {
                    $result = $result->add(DateInterval::createFromDateString("1 days"));
                    $result = $this->dateSetTime($result, "13:00");
                }
                break;
            case "EOW":
                $result = $this->startTime;
                $weekDay = intval($result->format("N"));
                if ($weekDay >= self::Monday && $weekDay <= self::Wednesday) {
                    $result = $this->advanceToDayOfWeek($this->dateSetTime($result, "17:00"), self::Friday, 1);
                } else {
                    $result = $this->advanceToDayOfWeek($this->dateSetTime($result, "20:00"), self::Sunday, 1);
                }
                break;
            default:
                $year = intVal($result->format("Y"));
                if (preg_match($patternMonth, $description, $matchMonth) == 1) {
                    $month = intVal($matchMonth[1]);
                    $currentMonth = intval($result->format("m"));
                    if ($currentMonth >= $month) {
                        $year++;
                    }
                    $dateString = sprintf("%04d-%02d-01T08:00", $year, $month);
                    $result = new DateTime($dateString);
                    $weekDay = intval($result->format("N"));
                    if ($weekDay == self::Saturday || $weekDay == self::Sunday) {
                        $result = $this->advanceToDayOfWeek($result, self::Monday, 1);
                    }
                } else if (preg_match($patternQuarter, $description, $matchQuarter) == 1) {
                    $quarter = intVal($matchQuarter[1]);
                    $currentQuarter = ceil($result->format('n') / 3);
                    if ($currentQuarter > $quarter) {
                        $year++;
                    }
                    $dateString = sprintf("%04d-%02d-01T08:00", $year, $quarter * 3);
                    $result = new DateTime($dateString);
                    $interval = DateInterval::createFromDateString("1 months -1 days");
                    $result = $result->add($interval);
                    $weekDay = intval($result->format("N"));
                    if ($weekDay == self::Saturday || $weekDay == self::Sunday) {
                        $result = $this->advanceToDayOfWeek($result, self::Friday, -1);
                    }
                } else {
                    throw new Exception("Unknown delivery description: " . $description);                    
                }
        }
        return $result;
    }

    // Advance the day to a given weekday. Return the given date if already there.
    // @param DateTime $date: The date and time to start at.
    // @param int $weekDay: 1 = Monday to 7 = Sunday. The day of week to advance to.
    // @param int $direction: 1 = forward one day, -1 = backward one day. Can use other numbers theorectically.
    // @returns: DateTime of the given $date advanced to the desired week day by moving in the given direction.
    private function advanceToDayOfWeek($date, $weekDay, $direction) {
        $result = $date;
        $day = intval($result->format("N"));
        $interval = DateInterval::createFromDateString(strval($direction) . " days");
        while ($day != $weekDay) {
            $result = $result->add($interval);
            $day = intval($result->format("N"));
        }
        return $result;
    }

    // For a given dateTime value return a new Date Time with the same month, day, year but the given time string.
    // @param DateTime $date: The date to base the result off of.
    // @param string $time: The new time to base the result off of.
    // @returns DateTime: DateTime with the date from $date and the time from $time.
    private function dateSetTime($date, $time) {
        return new DateTime($date->format("Y-m-d") . "T" . $time);
    }
}
