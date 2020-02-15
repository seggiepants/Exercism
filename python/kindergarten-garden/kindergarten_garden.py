class Garden(object):
    def __init__(self, diagram, students = ["Alice", "Bob", "Charlie", "David", "Eve", "Fred", "Ginny", "Harriet", "Ileana", "Joseph", "Kincaid", "Larry"]):
        self.plantCode = {
            "C": "Clover", 
            "G": "Grass",
            "R": "Radishes",
            "V": "Violets"
        }

        self.students = sorted(students)
        self.rows = diagram.split("\n", 2)

    def plants(self, student):
        try:            
            index = self.students.index(student)
            plants = []
            for j in range(2):
                for i in range(index * 2, (index + 1) * 2):
                    try:
                        code = self.rows[j][i]
                        plants.append(self.plantCode[code])
                    except:
                        raise Exception(f"{code} does not match a known plant.")
            return plants
        except:
            raise Exception(f"Student {student} was not found in the class role.")
