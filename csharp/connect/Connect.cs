public enum ConnectWinner
{
    White,
    Black,
    None
}

public class Connect
{
    /* Can move left, right, upper-left, upper-right, lower-left, or lower-right
    O - connect from top to bottom.
    X - connect from left to right.
    */
    // board array (square)
    char[][] board;

    // Possible moves on the hex board
    Tuple<int, int>[] moves = {
        new Tuple<int, int>( -1, -1), new Tuple<int, int>( 1, -1 ),
        new Tuple<int, int>( -2, 0 ), new Tuple<int, int>( 2, 0 ),
        new Tuple<int, int>( -1, 1 ), new Tuple<int, int>( 1, 1 )
    };

    // Make a square board from the given input padding with spaces as needed.
    public Connect(string[] input)
    {
        int height = input.Length;
        int width = (from s in input select s.Length).Max();

        board = (from row in input
                 select (from char c in row.PadRight(width, ' ') select c).ToArray<char>()).ToArray();

    }

    public bool isValidPoint(int x, int y)
    {
        int height = board.Length;
        int width = board[0].Length;
        if (x < 0 || x >= width || y < 0 || y >= height)
            return false;

        return board[y][x] != ' ';
    }

    List<Tuple<int, int>> GetMoves(int x, int y, char player)
    {
        return (from move in moves
         where isValidPoint(x + move.Item1, y + move.Item2) && board[y + move.Item2][x + move.Item1] == player
         select new Tuple<int, int>(x + move.Item1, y + move.Item2)).ToList<Tuple<int, int>>();
    }

    void ClearVisited(bool[][] visitied)
    {
        for (int j = 0; j < visitied.Length; j++)
            for (int i = 0; i < visitied[j].Length; i++)
                visitied[j][i] = false;
    }

    public ConnectWinner Result()
    {
        // Search O - Top to bottom.
        // Get any O on the top row
        Tuple<int, int>[]? candidates = (from pair in board[0].Index()
                                         where pair.Item == 'O'
                                         select new Tuple<int, int>(pair.Index, 0)).ToArray();
        bool[][]? visited = (from row in board
                            select (from ch in row select false).ToArray<bool>()).ToArray();
                            
        if (candidates != null && visited != null)
        {
            ClearVisited(visited);
            foreach (Tuple<int, int> candidate in candidates)
            {
                Stack<Tuple<int, int>> next = new();
                next.Push(candidate);
                while (next.Count > 0)
                {
                    Tuple<int, int> current = next.Pop();
                    visited[current.Item2][current.Item1] = true;

                    // Check if we won
                    if (current.Item2 >= board.Length - 1)
                    {
                        // White made it to bottom side.
                        return ConnectWinner.White;
                    }

                    foreach (Tuple<int, int> item in GetMoves(current.Item1, current.Item2, 'O'))
                    {
                        if (visited[item.Item2][item.Item1] == false)
                        {
                            next.Push(item);
                        }
                    }
                }

            }
        }
        // Search x, left to right.
        candidates = (from row in board.Index()
                      where row.Item[row.Index] == 'X'
                      select new Tuple<int, int>(row.Index, row.Index)).ToArray();

        if (candidates != null && visited != null)
        {
            ClearVisited(visited);
            foreach (Tuple<int, int> candidate in candidates)
            {
                Stack<Tuple<int, int>> next = new();
                next.Push(candidate);
                while (next.Count > 0)
                {
                    Tuple<int, int> current = next.Pop();
                    visited[current.Item2][current.Item1] = true;

                    // Check if we won
                    if (current.Item1 >= board[0].Length - 1 || board[current.Item2][current.Item1 + 2] == ' ')
                    {
                        // X made it to right hand side.
                        return ConnectWinner.Black;
                    }

                    foreach (Tuple<int, int> item in GetMoves(current.Item1, current.Item2, 'X'))
                    {
                        if (visited[item.Item2][item.Item1] == false)
                        {
                            next.Push(item);
                        }
                    }
                }
            }                               
        }
        return ConnectWinner.None;
    }
}