public class Queen
{
    public Queen(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; }
    public int Column { get; }
}

public static class QueenAttack
{
    public static bool CanAttack(Queen white, Queen black)
    {
        // I have no board, so I am assuming we have no other pieces
        // on the board that could block the queens.
        if ((white.Column == black.Column) && (white.Row == black.Row))
            throw new ArgumentException("Pieces cannot be at the same location.");


        // Horizontal and Vertical.
        if ((white.Row == black.Row) || (white.Column == black.Column))
            return true;

        // Diagonal.
        int dx = Math.Abs(white.Column - black.Column);
        int dy = Math.Abs(white.Row - black.Row);
        if (dx == dy)
            return true;

        return false;
    }

    public static Queen Create(int row, int column)
    {
        if ((column < 0) || (row < 0) || (column >= 8) || (row >= 8))
            throw new ArgumentOutOfRangeException("Queen is not on the board.");

        return new Queen(row, column);
    }
        
}