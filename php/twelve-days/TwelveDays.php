<?php
// Twelve Days exercise - Recite verse(s) from the 12 days of christmas song
declare(strict_types=1);

class DayOfChristmas {
    public string $number;
    public string $gift;

    function __construct($number, $gift) {
        $this->number = $number;
        $this->gift = $gift;
    }
}

$GLOBALS['gifts'] = array(
    1 => new DayOfChristmas("first", "a Partridge in a Pear Tree."),
    2 => new DayOfChristmas("second", "two Turtle Doves"),
    3 => new DayOfChristmas("third", "three French Hens"),
    4 => new DayOfChristmas("fourth", "four Calling Birds"),
    5 => new DayOfChristmas("fifth", "five Gold Rings"),
    6 => new DayOfChristmas("sixth", "six Geese-a-Laying"),
    7 => new DayOfChristmas("seventh", "seven Swans-a-Swimming"),
    8 => new DayOfChristmas("eighth", "eight Maids-a-Milking"),
    9 => new DayOfChristmas("ninth", "nine Ladies Dancing"),
    10 => new DayOfChristmas("tenth", "ten Lords-a-Leaping"),
    11 => new DayOfChristmas("eleventh", "eleven Pipers Piping"),
    12 => new DayOfChristmas("twelfth", "twelve Drummers Drumming"),
);

class TwelveDays
{
    // Recite verse(s) from the 12 days of christmas song.
    // @param $start: What verse to start on.
    // @param $end: What verse to finish on (both must be 1-12 and $end should be >= $start)
    // @returns: String of the days of christmas song with verses separated by an end of line.
    public function recite(int $start, int $end): string
    {
        global $gifts;
        $lines = array();

        if ($start < 1 || $end < 1 || $start > 12 || $end > 12 || $start > $end) {
            throw new InvalidArgumentException("Start and End must be between 1 and 12 with end >= start");
        }

        for ($i = $start; $i <= $end; $i++) {
            if ($i != $start) {
                $lines[] = PHP_EOL;
            }

            $nth = $gifts[$i]->number;            
            $lines[] = "On the $nth day of Christmas my true love gave to me: ";
            for($j = $i; $j >= 1; $j--) {
                $gift = $gifts[$j]->gift;
                if ($j == $i) {
                    $lines[] = $gift;
                } else if ($j == 1) {
                    $lines[] = ", and $gift";
                } else {
                    $lines[] = ", $gift";
                }
            }
        }
        return implode("", $lines);
    }
}
