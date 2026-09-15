use strict;
use warnings;
use v5.40;

package Lasagna;

our $ExpectedMinutesInOven = 40;

sub remaining_minutes_in_oven ($actual_minutes_in_oven) {
    return $ExpectedMinutesInOven - $actual_minutes_in_oven;
}

sub preparation_time_in_minutes ($number_of_layers) {    
    return $number_of_layers * 2;
}

sub total_time_in_minutes ($number_of_layers, $actual_minutes_in_oven) {
    return $actual_minutes_in_oven + preparation_time_in_minutes($number_of_layers);
}

sub oven_alarm () {
    return "Ding!";
}

1;
