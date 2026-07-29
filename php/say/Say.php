<?php
// Say Exercise

declare(strict_types=1);

// I don't want the lookup tables allocated every call.
// so I put them in global space. Is there a better way to 
// have file-global instead of global-global.

$GLOBALS["digits_0_19"] = array( 
    0 => "zero", 1 => "one", 2=> "two", 3=> "three", 4=> "four",
    5 => "five", 6 => "six", 7=> "seven", 8 => "eight", 9=> "nine",
    10 => "ten", 11 => "eleven", 12=>"twelve", 13=>"thirteen", 14=>"fourteen",
    15 => "fifteen", 16=>"sixteen", 17=>"seventeen", 18=>"eighteen", 19=>"nineteen",
);
$GLOBALS["digits_tens"] = array(
    0 => "", 1 => "ten", 2 => "twenty", 3 => "thirty", 4 => "forty",
    5 => "fifty", 6 => "sixty", 7 => "seventy", 8 => "eighty", 9 => "ninety",
);

$GLOBALS["three_digits"] = array(0 => "", 1 => " thousand", 2 => " million", 3 => " billion", );

// Convert a number to text as it would be spoken aloud 100 to One Hundred for example.
// @param $number: The number to say
// @returns: $number as it would be spoken
function say(int $number): string
{
    global $digits_0_19;
    global $digits_tens;
    global $three_digits;

    if ($number < 0 || $number > 999999999999) {
        throw new InvalidArgumentException("Input out of range");
    }
    if ($number == 0) {
        return "zero";
    }
    $result = "";

    
    foreach ($three_digits as $prefix) { 
        if ($number == 0) {
            break;
        }
        if ($number % 1000 > 0 && $prefix != "") {
            if (strlen($result) > 0) {
                $result = $prefix . " " . $result;
            } else {
                $result = $prefix;
            }
        }

        // 0-99
        $two_places = $number % 100;
        if ($two_places < 20 && $two_places > 0) {
            $result = $digits_0_19[$two_places] . $result;
        } else if ($two_places >= 20) {
            $one_place = $number % 10;
            $two_place = ($two_places - $one_place) / 10;
            if ($one_place > 0) {
                $result = $digits_tens[$two_place] . "-" . $digits_0_19[$one_place] . $result;
            } else {
                $result = $digits_tens[$two_place] . $result;
            }
        }
        $number = ($number - $two_places) / 100;
        $one_place = $number % 10;
        if ($one_place > 0) {
            if (strlen($result) > 0) {
                $result = $digits_0_19[$one_place] . " hundred " . $result;
            } else {
                $result = $digits_0_19[$one_place] . " hundred";
            }
        }
        $number = ($number - $one_place) / 10;
    }

    return $result;
}
