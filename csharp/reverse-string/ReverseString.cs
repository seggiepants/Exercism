using System.Text;

public static class ReverseString
{
    public static string Reverse(string input)
    {
        /* 
        // We can do this the long way.
        StringBuilder temp = new StringBuilder(input);
        int left = 0;
        int right = temp.Length - 1;

        while (left < right)
        {
            char ch = temp[left];
            temp[left] = temp[right];
            temp[right] = ch;
            left++;
            right--;
        }
        return temp.ToString();
        */
        // Or the short way.
        return new String(input.Reverse().ToArray());
    }
}