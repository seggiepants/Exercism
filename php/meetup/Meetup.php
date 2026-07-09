<?php
// Meetup exercise.

declare(strict_types=1);

// Find the day of a given meetup
// @param $year: Year of the meetup
// @param $month: Month of the meetup
// @param $which: Which week. One of "first", "second", "third", "fourth", "teenth", or "last"
// @param $weekday: What day of the week. Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, or Sunday
function meetup_day(int $year, int $month, string $which, string $weekday): DateTimeImmutable
{
    $one_day = new DateInterval("P1D");
    $one_week = new DateInterval("P7D");
    $two_weeks = new DateInterval("P14D");
    $three_weeks = new DateInterval("P21D");

    $one_month = new DateInterval("P1M");

    $week_one = new \DateTime();
    $week_one->setTimestamp(mktime(0, 0, 0, $month, 1, $year));    
    
    // Last week is first day + 1 month - 1 week
    $week_last = new \DateTime();
    $week_last->setTimestamp(mktime(0, 0, 0, $month, 1, $year));
    $week_last = date_add($week_last, $one_month);
    $week_last = date_sub($week_last, $one_week);
    
    // Week teen starts on the 13th.
    $week_teen = new \DateTime();
    $week_teen->setTimestamp(mktime(0, 0, 0, $month, 13, $year));

    $extra = "";
    switch ($which) {
        case "first":
            $start = $week_one;
            break;
        case "second":
            $start = $week_one;
            $extra = $one_week;
            break;
        case "third":
            $start = $week_one;
            $extra = $two_weeks;
            break;
        case "fourth":
            $start = $week_one;
            $extra = $three_weeks;
            break;
        case "teenth":
            $start = $week_teen;
            break;
        default:
            $start = $week_last;
            break;
    }

    while ($start->format("l") != $weekday) {
        $start = date_add($start, $one_day);        
    }

    if ($extra != "") {
        $start = date_add($start, $extra);
    }

    return DateTimeImmutable::createFromMutable($start);
}
