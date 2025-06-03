using System;

public class Player
{
    Random r;

    public Player()
    {
        r = new();
    }
    public int RollDie()
    {
        return r.Next(1, 18);
    }

    public double GenerateSpellStrength()
    {
        return r.NextDouble() * 100.0d;
    }
}
