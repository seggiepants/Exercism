using System;

public static class GameOfLife
{
    /// <summary>
    /// Return what happens in the next step of the game of life.
    /// </summary>
    /// <param name="matrix">Two-Dimensional array holding the current game state. 0 = Dead > 0 = Alive</param>
    /// <returns>A new two-dimensional array filled with the state of the game of life after one update.</returns>
    public static int[,] Tick(int[,] matrix)
    {
        int[,] next = new int[matrix.GetLength(0), matrix.GetLength(1)];
        for (int y = 0; y < matrix.GetLength(0); y++)
        {
            for(int x = 0; x < matrix.GetLength(1); x++)
            {
                int neighbors = GetNeighborCount(matrix, x, y);
                bool alive = matrix[x, y] > 0;
                if (alive && (neighbors == 2 || neighbors == 3))
                    next[x, y] = 1;
                else if (!alive && neighbors == 3)
                    next[x, y] = 1;
                else
                    next[x, y] = 0;
            }
        }
        return next;
    }

    /// <summary>
    /// Return the number of living cells at a given point on a matrix.
    /// </summary>
    /// <param name="matrix">Two dimensional integer matrix holding the current state of the game.</param>
    /// <param name="x">x-coordinate to get neighbor count from</param>
    /// <param name="y">y-coordinate to get neighbor count from</param>
    /// <returns>Number of neighboring cells on the grid (no-wrap around, just truncation) at the given x, y coordinate (given coordinate also not counted).</returns>
    private static int GetNeighborCount(int[,] matrix, int x, int y)
    {
        int neighbors = 0;

        for (int j = y - 1; j <= y + 1; j++)
        {
            if (j < 0 || j >= matrix.GetLength(0))
                continue;       // Done include out of bounds.
            for(int i = x - 1; i <= x + 1; i++)
            {
                if (i < 0 || i >= matrix.GetLength(1))
                    continue;   // Don't include out of bounds.
                if (x == i && y == j)
                    continue;   // Don't include self.
                
                neighbors += matrix[i, j] > 0 ? 1 : 0;
            }
        }

        return neighbors;
    }
}
