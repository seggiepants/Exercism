<?php
// Exercise is to reproduce the Nursery Rhyme: This is the house that Jack built.
declare(strict_types=1);

// Interleaved array with the parts from the rhyme.
$GLOBALS['parts'] = array(
    "malt", "lay",
    "rat", "ate",
    "cat", "killed",
    "dog", "worried",
    "cow with the crumpled horn", "tossed",
    "maiden all forlorn", "milked",
    "man all tattered and torn", "kissed", 
    "priest all shaven and shorn", "married",
    "rooster that crowed in the morn", "woke",
    "farmer sowing his corn", "kept", 
    "horse and the hound and the horn", "belonged to");

class House
{
    // Recite a single verse from the Nursery Rhyme This is the house that Jack built.
    // @param $verseNumber: The verse to generate.
    // @returns: Array with the lines from the verse.
    public function verse(int $verseNumber): array
    {
        global $parts;
        if ($verseNumber == 1) {
            return array("This is the house that Jack built.");
        }
        $result = array();
        $offset = ($verseNumber - 2) * 2;
        $thing = $parts[$offset];
        $action = $parts[$offset + 1];
        //echo "verse: " . $verseNumber . ", offset: " . strval($offset) . ", Thing: " . $thing . ", Action: " . $action . PHP_EOL;
        $result[] = "This is the " . $thing;
        while ($offset > 0) {
            $old_action = $action;
            $offset -= 2;
            $thing = $parts[$offset];
            $action = $parts[$offset + 1];
            $result[] = "that " . $old_action . " the " . $thing;
        }
        $result[] = "that " . $action . " in the house that Jack built.";
        return $result;
    }

    // Recite a set of versed from start to end.
    // @param $start: The verse to start at [1-12] are the only supported values
    // @param $end: The verse to end on should be >= $start and <= 12.
    // @returns: Array with the lines from the verses and a extra empty line between verses.
    public function verses(int $start, int $end): array
    {
        $result = array();
        for($index = $start; $index <= $end; $index++) {
            if ($index > $start) {
                $result[] = "";
            }
            $result = [...$result, ...$this->verse($index)];
        }
        return $result;
    }
}
