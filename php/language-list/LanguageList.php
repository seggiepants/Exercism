<?php

// Return an associative array with the given items. Turns out we can 
// just return the array of parameters given to the function.
// @param $languages: Items the returned list should contain.
// @returns: Associative array with the desired items.
function language_list(...$languages)
{
    return $languages;
}

// Add an item to an associative array
// @param $language_list: List to add an item to
// @param $language: The item to add to the list.
// @returns: Associative array with the next item added.
function add_to_language_list($language_list, $language) {
    $language_list[] = $language;
    return $language_list;
}

// Remove the first item from an associative array
// @param $language_list: List to remove from
// @returns: Associative array with the remaining items in the list.
function prune_language_list($language_list) {
    array_splice($language_list, 0, 1);
    return $language_list;
}

// Return the current/first item of an associative array
// @param $language_list: List to get the first item from
// @returns: First item in the list (does not check that length > 0).
function current_language($language_list) {
    return $language_list[0];
}

// Return the length of an associative array
// @param $language_list: List to get the length of
// @returns: Length of the given list.
function language_list_length($language_list) {
    return count($language_list);
}