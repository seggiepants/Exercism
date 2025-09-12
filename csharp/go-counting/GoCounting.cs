using System.Data;

public enum Owner
{
    None,
    Black,
    White
}

public class GoCounting
{
    Dictionary<char, Owner> lookup = new Dictionary<char, Owner>()
    {
        [' '] = Owner.None,
        ['B'] = Owner.Black,
        ['W'] = Owner.White,
    };

    Dictionary<char, (int, int)> direction = new Dictionary<char, (int, int)>()
    {
        ['N'] = (0, -1),
        ['E'] = (1, 0),
        ['S'] = (0, 1),
        ['W'] = (-1, 0),
    };

    Owner[][] input;
    public GoCounting(string input)
    {
        this.input = (from string row in input.Split('\n')
                      select (from char c in row
                              select lookup.ContainsKey(c) ? lookup[c] : Owner.None).ToArray<Owner>()).ToArray();
    }

    public Tuple<Owner, HashSet<(int, int)>> Territory((int, int) coord)
    {
        bool IsValidLocation(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < input[0].Length && y < input.Length);
        }

        if (!IsValidLocation(coord.Item1, coord.Item2))
            throw new ArgumentException($"Not a valid location ({coord.Item1}, {coord.Item2})");
            
        if (input[coord.Item2][coord.Item1] != Owner.None)
        {
            return new Tuple<Owner, HashSet<(int, int)>>(Owner.None, new HashSet<(int, int)>());
        }
        // Flood fill
        int whiteCount = 0; // how many border squares are white.
        int blackCount = 0; // how many border squares are black.
        Stack<(int, int)> cells = new();
        cells.Push((coord.Item1, coord.Item2));
        HashSet<(int, int)> found = new();
        while (cells.Count > 0)
        {
            int x, y;
            (x, y) = cells.Pop();
            if (input[y][x] == Owner.None)
            {
                found.Add((x, y));

                foreach (KeyValuePair<char, (int, int)> dir in direction)
                {
                    int x1 = x + dir.Value.Item1;
                    int y1 = y + dir.Value.Item2;

                    if (IsValidLocation(x1, y1))
                    {
                        if (!cells.Contains((x1, y1)) && !found.Contains((x1, y1)))
                        {
                            Owner color = input[y1][x1];
                            if (color == Owner.Black)
                                blackCount++;
                            else if (color == Owner.White)
                                whiteCount++;
                            else
                                cells.Push((x1, y1));
                        }
                    }
                }
            }
        }

        Owner owner = Owner.None;
        if (blackCount > 0 && whiteCount == 0)
            owner = Owner.Black;
        else if (whiteCount > 0 && blackCount == 0)
            owner = Owner.White;
        return new Tuple<Owner, HashSet<(int, int)>>(owner, found);
    }

    public Dictionary<Owner, HashSet<(int, int)>> Territories()
    {
        Dictionary<Owner, HashSet<(int, int)>> ret = new()
        {
            [Owner.None] = new HashSet<(int, int)>(),
            [Owner.Black] = new HashSet<(int, int)>(),
            [Owner.White] = new HashSet<(int, int)>(),
        };

        bool IsSpotTaken(int x, int y)
        {
            if (ret[Owner.Black].Contains((x, y)))
                return true;
            else if (ret[Owner.White].Contains((x, y)))
                return true;
            else
                return ret[Owner.None].Contains((x, y));
        }

        (int, int)[]? spots = (from int y in Enumerable.Range(0, input.Length)
                     from int x in Enumerable.Range(0, input[0].Length)
                     where input[y][x] == Owner.None
                     select (x, y)).ToArray<(int, int)>();

        if (spots != null)
        {
            foreach ((int, int) spot in spots)
            {
                if (!IsSpotTaken(spot.Item1, spot.Item2))
                {
                    Tuple<Owner, HashSet<(int, int)>> result = Territory(spot);
                    ret[result.Item1].UnionWith(result.Item2);
                }
            }
        }
        return ret;
    }        
}
