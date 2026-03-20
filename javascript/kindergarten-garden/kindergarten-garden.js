//
// This is only a SKELETON file for the 'Kindergarten Garden' exercise.
// It's been provided as a convenience to get you started writing code faster.
//

const DEFAULT_STUDENTS = [
  'Alice',
  'Bob',
  'Charlie',
  'David',
  'Eve',
  'Fred',
  'Ginny',
  'Harriet',
  'Ileana',
  'Joseph',
  'Kincaid',
  'Larry',
];

const PLANT_CODES = {
  G: 'grass',
  V: 'violets',
  R: 'radishes',
  C: 'clover',
};

export class Garden {
  constructor(diagram, students = DEFAULT_STUDENTS) {
    this.rows = diagram.split('\n')
    this.students = students.sort()
  }

  plants(student) {    
    let index = this.students.indexOf(student)
    if (index < 0)
      throw new Error(`${student} is not in this class.`)

    return this.rows
      .map((row) => row.slice(index * 2, (index + 1) * 2))
      .join("")
      .split('')
      .map((ch) => PLANT_CODES[ch])
  }
}
