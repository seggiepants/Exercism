<?php
// Exercise: Baffling Birthdays

declare(strict_types=1);

// Check if a group of a given size has a shared birthday.
class BafflingBirthdays
{
    private array $daysInMonth; // How many days are in a given month.

    // Intitialize the Baffling Birthday class. Just means setting up the days in month array.
    public function __construct() {
        $this->daysInMonth = array( 
            1 => 31, 2 => 28, 3 => 31, 4 => 30, 
            5 => 31, 6 => 30, 7 => 31, 8 => 31, 
            9 => 30, 10 => 31, 11 => 30, 12 => 31,);
    }

    // Get the number of shared birthdates in an array of birth dates.
    // @param $birthdates: Array of birthdays as strings four digit year - two digit month - two digit day of month;
    // @returns: count of birthdays that are shared.
    public function countSharedBirthday(array $birthdates): int
    {
        // Years makes it so I can't just use array_count_values directly.
        $month_day = array();

        // I tried array_map but even with use($month_day) it didn't update the array.
        foreach($birthdates as $birthday) {
            $key = substr($birthday, 5);
            if (array_key_exists($key, $month_day)) {
                $month_day[$key] += 1;
            } else {
                $month_day[$key] = 1;
            }
        }
        
        return array_reduce($month_day, function ($carry, $item) {
            if ($item > 1) {
                return $carry + $item - 1;
            }
            return $carry;
            }, 0);
    }

    // Check if there are any shared birthdays in an array of birthday strings.
    // @param $birthdates: Array of birthdays as strings four digit year - two digit month - two digit day of month;
    // @returns: True if at least one shared birthday.
    public function sharedBirthday(array $birthdates): bool
    {        
        return $this->countSharedBirthday($birthdates) > 0;
    }

    // Could you please not make the function signature wrong. It was missing the parameter and the correct return data type.
    // Anyway this generates a series of birth dates between 1900 and current year excluding leap years.
    // Uses the $daysInMonth class property to get the right number of days in a chosen month.
    // @param $count: The number of birthdays to return.
    // @returns: Array of strings of the birthdays formatted in "Y-m-d" or yyyy-mm-dd format.
    public function randomBirthdates(int $count): array
    {
        $now = getdate();
        $results = array();
        for($i = 0; $i < $count; $i++) {
            do {
                $year = random_int(1900, $now["year"]);
                $month = random_int(1, 12);
                $day = random_int(1, $this->daysInMonth[$month]);
                $date=date_create();
                date_date_set($date, $year, $month, $day);
                $isLeapYear = $date->format("L") == "1";
                if ($isLeapYear == false) {
                    $results[] = $date->format("Y-m-d");
                }
            } while ($isLeapYear !== false);
        }
        
        return $results;
    }

    // Return probability of a shared birthday for a given group size. Runs a number of trials and 
    // returns the ratio of trials with at least on shared birthday.
    // @param $groupSize: The number of people in a group.
    // @returns: Percentage (2%, not 0.02) of groups with a shared birthday
    public function estimatedProbabilityOfSharedBirthday(int $groupSize): float
    {
        $total = 0;
        $batch_size = 1000;
        for($i = 0; $i < $batch_size; $i++) {
            if ($this->SharedBirthday($this->randomBirthdates($groupSize))) {
                $total++;
            }
        }
        return floatVal($total * 100) / floatVal($batch_size);
    }
}
