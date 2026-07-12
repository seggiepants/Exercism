<?php
// Phone number cleanup exercise.

declare(strict_types=1);

// Holds a phone number.
class PhoneNumber
{
    // Construct a new phone number object with a phone-number member.
    // @param $number: The number for this class instance
    public function __construct($number)
    {
        $this->phone_number = $number;

        // test the number. The instructions should have said to test
        // the number on create. Don't like to trip over things like this is the
        // test cases.
        $temp = $this->number();
    }

    // Return the canonical form of the instance phone number variable.
    // @returns: Instance's phone number with the excess cruft removed.
    public function number(): string
    {
        // The regex could have caught this and punctuation if you didn't require specific error messages.
        if (strpbrk($this->phone_number, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")) {
            throw new InvalidArgumentException("letters not permitted");
        }
        if (strpbrk($this->phone_number, '!@#$%^&*?,;:')) {
            throw new InvalidArgumentException("punctuations not permitted");
        }

        $phone_re = '/^(\+?(?<country_code>\d))?\s*\(?(?<area_code>\d{3})\)?[ \.-]*(?<exchange>\d{3})[ \.-]*(?<subscriber>\d{4})\s*$/u';
        if (!preg_match($phone_re, $this->phone_number, $matches)) {
            throw new InvalidArgumentException("Not a phone number: " . $this->phone_number);
        }
        // This would have been a lot simpler if you let the reg-ex filter out the
        // bad phone numbers and didn't expect specific messages.
        if (array_key_exists("country_code", $matches) && strlen($matches["country_code"]) > 0 && $matches["country_code"] != "1") {
            throw new InvalidArgumentException("11 digits must start with 1");
        }
        if ($matches["area_code"][0] == "0") {
            throw new InvalidArgumentException("area code cannot start with zero");
        }
        if ($matches["area_code"][0] == "1") {
            throw new InvalidArgumentException("area code cannot start with one");
        }
        if ($matches["exchange"][0] == "0") {
            throw new InvalidArgumentException("exchange code cannot start with zero");
        }
        if ($matches["exchange"][0] == "1") {
            throw new InvalidArgumentException("exchange code cannot start with one");
        }
        $canonical = $matches["area_code"] . $matches["exchange"] . $matches["subscriber"];        
        if (strlen($canonical) != 10) {
            throw new InvalidArgumentException("Not a phone number (wrong number of digits): " . $this->phone_number);
        }
        return $canonical;
    }
}
