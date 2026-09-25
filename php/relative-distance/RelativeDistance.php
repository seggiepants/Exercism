<?php
// Relative Distance Exercise

declare(strict_types=1);

// Find parents, children, and siblings of a given name (one degree of separation)
// @param $tree: Array of family tree members. Key is name, and value is an array of their children.
// @param $name: The name of the person you are finding direct relatives of.
// @returns: An array containinng the children, parents and siblings of $name, not including $name.
function oneDegree($tree, $name) : array {
    $ret = array();
    $treeKeys = array_keys($tree);
    
    // children
    if (in_array($name, $treeKeys)) {
        $ret = array_merge($ret, $tree[$name]);
    }    

    // Parents & siblings
    foreach($tree as $key => $value) {
        if (in_array($name, $value)) {
            $ret = array_merge($ret, [$key], $value);
        }
    }
    // don't include name in the return set
    return array_filter($ret, function($value) use($name) {
        return strcmp($value, $name) != 0; 
        });
}

// Find the number of degrees of separation between two names in a family tree.
// @param $familyTree: Array of family tree members. Key is name, and value is an array of their children.
// @param $personA: The first of the people you are trying to find a path between.
// @param $personB: The second of the people you are trying to find a path between.
// @returns: -1 if no relation in family tree. Otherwise the number of steps between person A and personB.
function degreeOfSeparation(array $familyTree, string $personA, string $personB): int {
    // searched is none, new to search is one degree separation    
    $familyA = [$personA];
    $newFamilyA = oneDegree($familyTree, $personA);
    $familyB = [$personB];
    $newFamilyB = oneDegree($familyTree, $personB);
    $distance = 0;
    
    while (true) {
        // check if the family groups overlapped in the last round
        $distance += 1;
        $familyA = array_merge($familyA, $newFamilyA);        
        $familyB = array_merge($familyB, $newFamilyB);

        if (in_array($personB, $familyA) && in_array($personA, $familyB)) {
            return $distance;
        }

        // Get the next set of relatives from the new search set
        $nextFamilyA = [];
        foreach($newFamilyA as $person) {
            $current = oneDegree($familyTree, $person);
            $nextFamilyA = array_merge($nextFamilyA, $current);
        }
        $nextFamilyB = [];
        foreach($newFamilyB as $person) {
            $current = oneDegree($familyTree, $person);
            $nextFamilyB = array_merge($nextFamilyB, $current);
        }
    
        // set the new search list. Only include names we haven't searched yet.
        $newFamilyA = array_filter($nextFamilyA, function($name) use ($familyA) {
            return !in_array($name, $familyA);
        });
        $newFamilyB = array_filter($nextFamilyB, function($name) use ($familyB) {
            return !in_array($name, $familyB);
        });

        // if no new names they are not related
        if (count($newFamilyA) + count($newFamilyB) === 0) {
            return -1;
        }
    }
}
