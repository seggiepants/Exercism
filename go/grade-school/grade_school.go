package gradeschool

import (
	"maps"
	"slices"
)

type School map[int][]string

// Create and return a reference to a new school
func New() *School {
	return &School{}
}

// Check if we have a student of a given name in any grade.
// @param student: Name of the student we are searching for
// @returns: True if we have a student with that name.
func (s *School) HasStudent(student string) bool {
	for grade := range maps.Keys((*s)) {
		if slices.Contains((*s)[grade], student) {
			return true
		}
	}
	return false
}

// Add a student to the roster
// @param student: Name of the student
// @param grade: The grade to place them into
// @returns: True if added to the roster
func (s *School) Add(student string, grade int) bool {
	if s.HasStudent(student) {
		return false
	}
	_, ok := (*s)[grade]
	if !ok {
		(*s)[grade] = make([]string, 0)
	}
	(*s)[grade] = append((*s)[grade], student)
	return true
}

// Return a sorted slice of student names for the given grade
// @param level: The grade level to return (empty result if doesn't exist)
// @returns: Slice of students in that grade in sorted order
func (s *School) Grade(level int) []string {
	ret := make([]string, len((*s)[level]))
	val, ok := (*s)[level]
	if !ok {
		return ret // Empty slice if level doesn't exist
	}
	copy(ret, val)
	slices.Sort(ret)
	return ret
}

// Return all students sorted first by grade then by name (ascending)
// @returns: Slice of sorted student names
func (s *School) Enrollment() []string {
	keys := make([]int, len(*s))
	copy(keys, slices.Collect(maps.Keys(*s)))
	slices.Sort(keys)
	ret := []string{}
	for _, key := range keys {
		ret = slices.Concat(ret, s.Grade(key))
	}
	return ret
}
