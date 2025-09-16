using System.ComponentModel;

public enum Direction
{
    North,
    East,
    South,
    West
}

public class RobotSimulator
{
    int x, y;
    Dictionary<Direction, (int, int)> delta = new Dictionary<Direction, (int, int)>()
    {
        [Direction.North] = (0, 1),
        [Direction.West] = (-1, 0),
        [Direction.South] = (0, -1),
        [Direction.East] = (1, 0),
    };
    Direction direction;

    public RobotSimulator(Direction direction, int x, int y)
    {
        this.direction = direction;
        this.x = x;
        this.y = y;
    }

    public Direction Direction
    {
        get
        {
            return direction;
        }
    }

    public int X
    {
        get
        {
            return x;
        }
    }

    public int Y
    {
        get
        {
            return y;
        }
    }

    public void Move(string instructions)
    {
        foreach (char c in instructions.ToUpperInvariant().Trim())
        {
            switch (c)
            {
                case 'L':
                    RotateLeft();
                    break;
                case 'R':
                    RotateRight();
                    break;
                case 'A':
                    Advance();
                    break;
                default:
                    throw new ArgumentException($"Invalid operation: \"{c}\".");
            }
        }
    }

    private void Advance()
    {
        x += delta[Direction].Item1;
        y += delta[Direction].Item2;
    }

    private void RotateRight()
    {
        switch (direction)
        {
            case Direction.North:
                direction = Direction.East;
                break;
            case Direction.East:
                direction = Direction.South;
                break;
            case Direction.South:
                direction = Direction.West;
                break;
            case Direction.West:
                direction = Direction.North;
                break;
        }
    }

    private void RotateLeft()
    {
        switch (direction)
        {
            case Direction.North:
                direction = Direction.West;
                break;
            case Direction.West:
                direction = Direction.South;
                break;
            case Direction.South:
                direction = Direction.East;
                break;
            case Direction.East:
                direction = Direction.North;
                break;
        }
    }
}