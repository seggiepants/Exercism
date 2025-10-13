using System.Text;

public class RailFenceCipher
{
    int rows;

    public RailFenceCipher(int rails)
    {
        rows = rails;        
    }

    private void FillRails(string input, out List<List<char>> rails)
    {
        rails = new();
        for (int i = 0; i < rows; i++)
            rails.Add(new List<char>());
        int dy = 1;
        int rail = 0;
        foreach (char ch in input)
        {
            if (!Char.IsLetterOrDigit(ch))
                continue;
            rails[rail].Add(ch);
            rail += dy;
            if (rail < 0)
            {
                rail = 1;
                dy = 1;
            }
            if (rail >= rows)
            {
                rail = rows - 2;
                dy = -1;
            }
        }
    }

    public string Encode(string input)
    {
        FillRails(input, out List<List<char>> rails);
        return String.Join("", (from r in rails select new string(r.ToArray<char>())));
    }

    public string Decode(string input)
    {
        FillRails(input, out List<List<char>> rails);
        // Overwrite with the encoded data.
        int ptr = 0;
        for(int i = 0; i < rows; i++)
        {
            for (int j = 0; j < rails[i].Count; j++)
                rails[i][j] = input[ptr++];
        }
        StringBuilder sb = new();
        int[] railPtr = (from r in rails select 0).ToArray<int>();
        int rowNum = 0;
        int dy = 1;
        foreach (char ch in input)
        {
            sb.Append(rails[rowNum][railPtr[rowNum]]);
            railPtr[rowNum]++;

            rowNum += dy;
            if (rowNum < 0)
            {
                rowNum = 1;
                dy = 1;
            }
            if (rowNum >= rows)
            {
                rowNum = rows - 2;
                dy = -1;
            }
        }
        return sb.ToString();

    }
}