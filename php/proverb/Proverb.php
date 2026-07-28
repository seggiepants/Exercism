<?php
// Recreate the for want of a nail, a kingdom was lost proverb with code.

declare(strict_types=1);

// Generate the for want of a nail, a kingdom was lost proverb with code.
class Proverb
{
    // Recreate the for want of a nail, a kingdom was lost proverb
    // with the pieces recieved as an argument.
    // @param $pieces: The objects lost in order.
    // @returns: Array of strings for each line of the proverb.
    public function recite($pieces)
    {
        $ret = array();

        if (count($pieces) >= 2) {
            for($i = 0; $i < count($pieces) - 1; $i++) {
                $ret[] = "For want of a {$pieces[$i]} the {$pieces[$i + 1]} was lost.";
            }
        }
        if (count($pieces) >= 1) {
            $ret[] = "And all for the want of a {$pieces[0]}.";
        }

        return $ret;
    }
}
