public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        IEnumerable<int> digits = (from char c in number.ToString().Trim()
                        where c >= '0' && c <= '9'
                        select (int)c - (int)'0');
        int num_digits = digits.Count();
        return (from int digit in digits
                select Math.Pow(digit, num_digits)).Sum() == number;
    }
}