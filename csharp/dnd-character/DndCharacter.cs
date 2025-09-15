public class DndCharacter
{
    public int Strength { get; }
    public int Dexterity { get; }
    public int Constitution { get; }
    public int Intelligence { get; }
    public int Wisdom { get; }
    public int Charisma { get; }
    public int Hitpoints { get; }

    private static Random r = new Random();

    public DndCharacter()
    {
        r = new Random();
        Strength = DndCharacter.Ability();
        Dexterity = DndCharacter.Ability();
        Constitution = DndCharacter.Ability();
        Intelligence = DndCharacter.Ability();
        Wisdom = DndCharacter.Ability();
        Charisma = DndCharacter.Ability();
        Hitpoints = 10 + Modifier(Constitution);
    }

    public static int Modifier(int score)
    {
        // Assuming score = Constitution
        return (int)Math.Floor((double)(score - 10) / (double)2);
    }

    public static int Ability()
    {
        // Return random ability value.
        return (from int i in Enumerable.Range(1, 4)
            select r.Next(1, 7)).OrderDescending().Take<int>(3).Sum();

    }

    public static DndCharacter Generate()
    {
        return new DndCharacter();
    }
}
