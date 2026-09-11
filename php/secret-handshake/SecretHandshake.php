<?php
// Secret Handshake Exercise.

declare(strict_types=1);


// Class that holds the commands function that generates a secret handshake from a code.
class SecretHandshake
{
    // Generate the secret handshake sequence for the given number.
    // @param $handshake: Integer between 0 and 31 inclusive that encodes the desired handshake.
    // @returns: Array with the processed handshake steps.
    public function commands(int $handshake): array
    {
        if ($handshake < 0 || $handshake > 31) {
            throw new InvalidArugmentException("Out of Range, found: " . strval($handshake) . ", only integers between 0 and 31 inclusive are accepted.");
        }
        $actions = array("wink", "double blink", "close your eyes", "jump", "reverse");
        $reverse = false;
        $steps = array();
        $code = $handshake;
        foreach($actions as $action) {
            if ($code & 0b1 == 1) {
                if ($action == "reverse") {
                    $reverse = true;
                } else {
                    $steps[] = $action;
                }
            }
            $code >>= 1;
            //echo strval($code) . PHP_EOL;
        }
        if ($reverse) {
            $steps = array_reverse($steps, false);
        }
        return $steps;

    }
}
