<?php
// Gigasecond exercise.

declare(strict_types=1);

const GIGA = 1000000000;

// Calcluate a gigasecond after the given date.
// @param $date: The date to add a gigasecond to.
// @returns: DateTime value a gigasecond after the given date.
function from(DateTimeImmutable $date): DateTimeImmutable
{
    return $date->add(new DateInterval("PT" . strval(GIGA) . "S"));
}
