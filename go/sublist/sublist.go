package sublist

// Relation type is defined in relations.go file.

// Check if two lists are equal, or if one is a sub/super list of the other, or unique.
// @param l1: The first list
// @param l2: The second list
// @returns: Relationship between the two
func Sublist(l1, l2 []int) Relation {
	var lenL1 int = len(l1)
	var lenL2 int = len(l2)

	// Length checks -- handle length == 0
	if lenL1 == 0 && lenL2 == 0 {
		return RelationEqual
	}
	if lenL1 > 0 && lenL2 == 0 {
		return RelationSuperlist
	}
	if lenL1 == 0 && lenL2 > 0 {
		return RelationSublist
	}

	// Equal
	if ListEqual(l1, l2) {
		return RelationEqual
	}

	// l1 Superlist l2
	for i := 0; i <= lenL1-lenL2; i++ {
		if l1[i] == l2[0] && len(l1)-i-len(l2) >= 0 {
			if ListEqual(l1[i:len(l2)+i], l2) {
				return RelationSuperlist
			}
		}
	}

	// l1 Sublist l2
	for i := 0; i <= lenL2-lenL1; i++ {
		if l1[0] == l2[i] && len(l2)-i-len(l1) >= 0 {
			if ListEqual(l1, l2[i:len(l1)+i]) {
				return RelationSublist
			}
		}
	}

	return RelationUnequal
}

// Check if two lists are equal
// @param l1: The first list
// @param l2: The second list.
// @returns: True if equal, otherwise false.
func ListEqual(l1, l2 []int) bool {
	if len(l1) != len(l2) {
		return false
	}

	for i := 0; i < len(l1); i++ {
		if l1[i] != l2[i] {
			return false
		}
	}

	return true
}
