<?php
// Strain (Keep/Discard) exercise

declare(strict_types=1);

// Strain class returns a list of items the match/do not match a function
class Strain
{
    // Discard - Return a list of items from the given list that match the predicate.
    // @param $list: List of items to include/exclude
    // @param $predicate: Function to run over the items in the list.
    // @returns: A list of items where the predicate(item) is true.
    public function keep(array $list, callable $predicate): array
    {
        $result = array();
        foreach($list as $item) {
            if ($predicate($item)) {
                $result[] = $item;
            }
        }
        return $result;
    }

    // Discard - Return a list of items from the given list that do NOT match the predicate.
    // @param $list: List of items to include/exclude
    // @param $predicate: Function to run over the items in the list.
    // @returns: A list of items where the predicate(item) is false.
    public function discard(array $list, callable $predicate): array
    {
        $result = array();
        foreach($list as $item) {
            if (!$predicate($item)) {
                $result[] = $item;
            }
        }
        return $result;
    }
}
