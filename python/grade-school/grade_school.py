import functools

class School(object):
    def __init__(self):
        # setup the school data structure.
        # will be a list of students where each student is a tuple.
        # the first item of the tuple is the student name, the second is their grade.
        self.students = []

    def add_student(self, name, grade):
        # add a student into a given grade into the school roster.
        self.students.append((name, grade))

    def roster(self):
        # return a list of students in the school. Empty list if no students
        # list should be sorted by grade then by name.
        return [item[0] for item in sorted(self.students, key=functools.cmp_to_key(self.compare))]

    def grade(self, grade_number):
        # return an alphabetically sorted list of students with a given grade number.
        # empty list if the grade has no students.
        return [item[0] for item in sorted(filter(lambda x: x[1] == grade_number, self.students))]

    def compare(self, item1, item2):
        # compare two tuples and say which is earlier in the sort.
        # returns -1 for item1 < item2, 0 if item1 == item2, and 1 if item1 > item2

        # first compare by grade.
        if (item1[1] == item2[1]):
            # same grade, so
            # compare by name
            if item1[0] == item2[0]:
                # same grade and name 1 == name 2
                return 0
            elif item1[0] < item2[0]:
                # name 1 < name 2
                return -1
            else:
                # only greater than remains
                return 1
        elif (item1[1] < item2[1]):
            # less than
            return -1
        else:
            # only greater than remains
            return 1
