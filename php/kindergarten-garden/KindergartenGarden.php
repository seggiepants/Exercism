<?php
// Kindergarten Garden exercise.

declare(strict_types=1);

// Simulates a Kindergarten two row window garden.
class KindergartenGarden
{
    private $rows = array();
    private $students = array("Alice", "Bob", "Charlie", "David", "Eve", "Fred", "Ginny", "Harriet", "Ileana", "Joseph", "Kincaid", "Larry");
    private $plants = array("V" => 'violets', "R" => 'radishes', "G" => 'grass', "C" => 'clover');

    // KindergartenGarden class constructor.
    // @param $diagram: "\n" delimited string of plant symbols for each row (only V, C, R, and G are accepted)
    public function __construct(string $diagram)
    {
        $this->rows = explode("\n", $diagram);        
    }

    // Return the plants belonging to the given student.
    // @param $student: The student to return plants for. Only students in the $students class property are accepted. Value is case-sensitive.
    // @returns: Empty array if student not found, otherwise the plants assigned to them from row 1 and 2 in an array.
    public function plants(string $student): array
    {
        $index = array_search($student, $this->students);
        $results = array();
        if (is_numeric($index))
        {
            $index = $index * 2;
            for ($row = 0; $row < count($this->rows); $row++) {
                if (strlen($this->rows[$row]) >= $index + 2) {
                    $results[] = $this->plants[substr($this->rows[$row], $index, 1)];
                    $results[] = $this->plants[substr($this->rows[$row], $index + 1, 1)];
                }
            }
        }
        return $results;
    }
}
