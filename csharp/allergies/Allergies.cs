using System.Linq;
using System.Security.Cryptography;

public enum Allergen
{
    Eggs = 1,
    Peanuts = 2,
    Shellfish = 4,
    Strawberries = 8,
    Tomatoes = 16,
    Chocolate = 32,
    Pollen = 64,
    Cats = 128,
}

public class Allergies
{
    static Dictionary<Allergen, string>allergenName = new() {
        [Allergen.Eggs] = "Eggs",
        [Allergen.Peanuts] = "Peanuts",
        [Allergen.Shellfish] = "Shellfish",
        [Allergen.Strawberries] = "Strawberries",
        [Allergen.Tomatoes] = "Tomatoes",
        [Allergen.Chocolate] = "Chocolate",
        [Allergen.Pollen] = "Pollen",
        [Allergen.Cats] = "Cats"
    };
    int mask;
    public Allergies(int mask)
    {
        this.mask = mask;
    }

    public bool IsAllergicTo(Allergen allergen)
    {
        return ((int)allergen & mask) != 0;
    }

    public Allergen[] List()
    {
        return (from entry in allergenName
         where ((int)entry.Key & this.mask) != 0
         select entry.Key).ToArray<Allergen>();
    }
}