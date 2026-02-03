//
// This is only a SKELETON file for the 'Grade School' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class GradeSchool {
  
  
  constructor()
  {
    this.students = []
  }

  sortFunction(a, b)
  {
    if (a.grade !== b.grade)
    {
      if (a.grade < b.grade)
        return -1
      else if (a.grade > b.grade)
        return 1
      else
        return 0 // Shouldn't get here.
    }
    return a.name.localeCompare(b.name)
  }

  // returns array of names sorted by grade then name
  roster() {
    return this.students.sort(this.sortFunction).map((student) => student.name)
  }
  // returns false if the user has been previously added. True if
  // they were added to the roster
  add(name, grade) {
    let allowed = typeof this.students.find((student) => student.name === name) === "undefined"
    if (allowed)
    {
      this.students.push({
        name: name, grade: grade
      })
    }
    return allowed
  }

  // returns array of names sorted by name where each name is in the given grad
  // empty array if no matches
  grade(gradeLevel) {
    return this.students.filter((student) => student.grade === gradeLevel).sort(this.sortFunction).map((student) => student.name)
  }
}
