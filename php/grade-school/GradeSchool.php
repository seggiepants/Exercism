<?php

// Exercise: Grade School

declare(strict_types=1);

// Manage a roster of students in a small school
class GradeSchool
{

    private $studentsByGrade = [];

    // Add a student to the school roster in the given name.
    // @param string $name: The student's name.
    // @param int $grade: The grade to add the student to.
    // @returns: True if the student was added, False if the student was not added usually because they already exist in the roster.
    public function add(string $name, int $grade): bool
    {
        if (!array_key_exists($grade, $this->studentsByGrade)) {
            $this->studentsByGrade[$grade] = array();
        }
        $studentExists = array_reduce($this->studentsByGrade, function($carry, $grade) use($name) {
            return $carry || array_any($grade, function($value) use($name) { return $name == $value; });
        }, false);
        if ($studentExists) {
            return false;
        }
        $this->studentsByGrade[$grade][] = $name;
        return true;
    }

    // Return a sorted list of students in a grade.
    // @param int $grade: The grade to return.
    // @returns: Empty array if no match for $grade exists. Otherwise the students in the grade sorted by name.
    public function grade(int $grade): array
    {
        if (!array_key_exists($grade, $this->studentsByGrade)) {
            return [];
        }
        $result = $this->studentsByGrade[$grade];
        sort($result);
        return $result;
    }

    // Get a roster of all students in the school sorted by grade then name.
    // @returns array: Student names sorted by grade then by name.
    public function roster(): array
    {
        $result = [];
        $keys = array_keys($this->studentsByGrade);
        sort($keys);
        foreach($keys as $key) {
            $students = $this->studentsByGrade[$key];
            sort($students);
            $result = array_merge($result, $students);
        }
        return $result;
    }
}
