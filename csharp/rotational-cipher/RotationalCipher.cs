using System.Text;

public static class RotationalCipher
{
    public static string Rotate(string text, int shiftKey)
    {
        char A = 'A';
        char a = 'a';
        StringBuilder sb = new();
        foreach (char c in text)
        {
            if (c >= 'a' && c <= 'z')
            {
                int ch = ((int)c - (int)a + shiftKey) % 26;
                sb.Append((char)(ch + a));
            }
            else if (c >= 'A' && c <= 'Z')
            {
                int ch = ((int)c - (int)A + shiftKey) % 26;
                sb.Append((char)(ch + A));
            }
            else
            {
                // Unchanged if not a-z or A-Z
                sb.Append(c);
            }
        }
        return sb.ToString();
    }
}