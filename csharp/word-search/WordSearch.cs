using Xunit.Internal;

public class WordSearch
{
    string[] grid;
    /// <summary>
    /// Constructor. Saves the grid for later word search calls
    /// </summary>
    /// <param name="grid">A string where each row is separated
    /// by a '\n' character and the rows are the values inbetween.
    /// It is assumed that all rows are of equal length.
    /// Case Sensitive.
    /// </param>
    public WordSearch(string grid)
    {
        this.grid = grid.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    /// <summary>
    /// Return a dictionary of coordinates (if found) of a given 
    /// word in the word search. If not found the entry in the 
    /// dictionary should be null.
    /// </summary>
    /// <param name="wordsToSearchFor">Array of strings to search for (case-sensitive).</param>
    /// <returns>Dictionary where the key is the word searched for and the value
    /// is a null if not found otherwise a pair of coordinates for the start
    /// and end of the word in the grid expressed as a tuple of integer tuple pairs.</returns>
    public Dictionary<string, ((int, int), (int, int))?> Search(string[] wordsToSearchFor)
    {
        Dictionary<string, ((int, int), (int, int))?> ret = new();

        // Initialize the dictionary with nulls for each word.
        wordsToSearchFor.ForEach<string>(word => ret.Add(word, null));

        // We do the top-bottom with a transposed copy of the grid.
        string[]? transpose = (from int i in Enumerable.Range(0, grid[0].Length)
                               select new string((from row in grid
                                                  select row[i]).ToArray<char>())
            ).ToArray();

        // Get a list of possible diagonals so we can use index of instead of 
        // searching for letters individually 
        // (with the transposed grid it makes things more consistent too).
        List<List<Tuple<int, int>>> diagonals = Diagonals(grid.Length, grid[0].Length);

        foreach (string word in wordsToSearchFor)
        {
            string reversed = ReverseString(word);
            // left to right, or right to left each line.
            foreach ((int rowNum, string row) in grid.Index())
            {
                if (row.Length == 0)
                    continue;

                int index = row.IndexOf(word);
                if (index != -1) // -1 == Not found.
                {
                    ret[word] = ((index + 1, rowNum + 1), (index + word.Length, rowNum + 1));
                    break;
                }

                index = row.IndexOf(reversed);
                if (index != -1)
                {
                    ret[word] = ((index + reversed.Length, rowNum + 1), (index + 1, rowNum + 1));
                    break;
                }
            }
        }

        // Vertical
        if (transpose != null) 
        {
            foreach (string word in wordsToSearchFor)
            {
                // Skip if we already found one.
                if (ret[word] != null)
                    continue;

                string reversed = ReverseString(word);

                // top to bottom, or bottom to top each line.
                foreach ((int colNum, string col) in transpose.Index())
                {
                    if (col.Length == 0)
                        continue;
                    int index = col.IndexOf(word);
                    if (index != -1) // -1 == Not found.
                    {
                        ret[word] = ((colNum + 1, index + 1), (colNum + 1, index + word.Length));
                        break;
                    }

                    index = col.IndexOf(reversed);
                    if (index != -1) // -1 == Not found.
                    {
                        ret[word] = ((colNum + 1, index + reversed.Length), (colNum + 1, index + 1));
                        break;
                    }
                }
            }
        }
        // Diagonals
        foreach (string word in wordsToSearchFor)
        {
            // Skip if we already found one.
            if (ret[word] != null)
                continue;

            foreach (List<Tuple<int, int>> diagonal in diagonals)
            {
                // No use searching if the word won't fit.
                if (diagonal.Count < word.Length)
                    continue;
                    
                // Convert diagonal list of indexes into a string.
                string? row = new string((from pair in diagonal
                                          select grid[pair.Item2][pair.Item1]).ToArray<char>());
                string reversed = ReverseString(word);
                if (row != null)
                {
                    int index = row.IndexOf(word);
                    if (index != -1) // -1 == Not found.
                    {
                        ret[word] = ((diagonal[index].Item1 + 1, diagonal[index].Item2 + 1), (diagonal[index + word.Length - 1].Item1 + 1, diagonal[index + word.Length - 1].Item2 + 1));
                        break;
                    }

                    index = row.IndexOf(reversed);
                    if (index != -1)
                    {
                        ret[word] = ((diagonal[index + word.Length - 1].Item1 + 1, diagonal[index + word.Length - 1].Item2 + 1), (diagonal[index].Item1 + 1, diagonal[index].Item2 + 1));
                        break;
                    }
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// Calculate the diagonals of a given grid size.
    /// </summary>
    /// <param name="rows">Rows in the grid (y)</param>
    /// <param name="cols">Columns in the grid (x)</param>
    /// <returns>A list each diagonal where each 
    /// diagonal is a list of (int x, int y) coordinates
    /// </returns>
    public List<List<Tuple<int, int>>> Diagonals(int rows, int cols)
    {
        List<List<Tuple<int, int>>> ret = new();
        // Diagonal left to right.
        for (int j = 0; j < rows; j++)
        {
            List<Tuple<int, int>> diagonal = new();
            int x = 0;
            int y = j;
            while (x < cols && y < rows)
            {
                diagonal.Add(new Tuple<int, int>(x, y));
                x++; y++;
            }
            ret.Add(diagonal);
        }
        for (int i = 1; i < cols; i++)
        {
            List<Tuple<int, int>> diagonal = new();
            int x = i;
            int y = 0;
            while (x < cols && y < rows)
            {
                diagonal.Add(new Tuple<int, int>(x, y));
                x++; y++;
            }
            ret.Add(diagonal);
        }

        // Diagonal Right to Left
        for (int j = 0; j < rows; j++)
        {
            List<Tuple<int, int>> diagonal = new();
            int x = 0;
            int y = j;
            while (x < cols && y >= 0)
            {
                diagonal.Add(new Tuple<int, int>(x, y));
                x++; y--;
            }
            ret.Add(diagonal);
        }
        for (int i = 1; i < cols; i++)
        {
            List<Tuple<int, int>> diagonal = new();
            int x = i;
            int y = rows - 1;
            while (x < cols && y >= 0)
            {
                diagonal.Add(new Tuple<int, int>(x, y));
                x++; y--;
            }
            ret.Add(diagonal);
        }
        return ret;
    }

    /// <summary>
    /// Return a reversed copy of a given string
    /// </summary>
    /// <param name="value">The string to return a reversed copy of.</param>
    /// <returns>The given value, but reversed</returns>
    string ReverseString(string value)
    {
        return new string(value.Reverse().ToArray());
    }
}