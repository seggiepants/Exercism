using System.Linq;

public class GradeSchool
{
    Dictionary<string, int> students = new();
    public bool Add(string student, int grade)
    {
        bool ret = students.TryAdd(student, grade);
        return ret;
    }

    public IEnumerable<string> Roster()
    {
        return (from pair in students
                orderby pair.Value, pair.Key
                select pair.Key).ToList<string>();
    }

    public IEnumerable<string> Grade(int grade)
    {
        return (from pair in students
                where pair.Value == grade
                orderby pair.Key
                select pair.Key).ToList<string>();
        
    }
}