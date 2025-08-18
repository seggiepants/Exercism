using System.Collections;
using System.Collections.Generic;

public enum Plant
{
    Violets,
    Radishes,
    Clover,
    Grass
}

public class KindergartenGarden
{
    List<string> students = new() {
        "Alice", "Bob", "Charlie", "David",
        "Eve", "Fred", "Ginny", "Harriet",
        "Ileana", "Joseph", "Kincaid", "Larry"
    };
    Dictionary<char, Plant> CharToPlant = new() {
        ['V'] = Plant.Violets,
        ['R'] = Plant.Radishes,
        ['C'] = Plant.Clover,
        ['G'] = Plant.Grass,
    };

    Plant[][] garden;
    public KindergartenGarden(string diagram)
    {
        string[] rows = diagram.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        garden = (from row in rows
            select (from ch in row
             select CharToPlant[ch]).ToArray<Plant>()).ToArray();
                
    }
    public IEnumerable<Plant> Plants(string student)
    {
        int studentIndex = students.IndexOf(student);
        if (studentIndex == -1)
            throw new ArgumentException($"Student \"{student}\" not found");

        return (from row in garden
                 select row.Skip<Plant>(studentIndex * 2).Take<Plant>(2)).SelectMany(plant => plant);
    }
}