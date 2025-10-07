public static class Say
{
    static Dictionary<long, string> zeroToNineteen = new Dictionary<long, string>()
    {
        [0] = "zero",
        [1] = "one",
        [2] = "two",
        [3] = "three",
        [4] = "four",
        [5] = "five",
        [6] = "six",
        [7] = "seven",
        [8] = "eight",
        [9] = "nine",
        [10] = "ten",
        [11] = "eleven",
        [12] = "twelve",
        [13] = "thirteen",
        [14] = "fourteen",
        [15] = "fifteen",
        [16] = "sixteen",
        [17] = "seventeen",
        [18] = "eighteen",
        [19] = "nineteen",
    };

    public static Dictionary<long, string> tens = new Dictionary<long, string>()
    {
        [10] = "ten",
        [20] = "twenty",
        [30] = "thirty",
        [40] = "forty",
        [50] = "fifty",
        [60] = "sixty",
        [70] = "seventy",
        [80] = "eighty",
        [90] = "ninety",

    };

    public static string InEnglish(long number)
    {
        if (number > 999_999_999_999)
            throw new ArgumentOutOfRangeException("Number is too large.");
        else if (number < 0)
            throw new ArgumentOutOfRangeException("Only positive numbers are accepted.");

        return SayBillions(number);
    }

    private static string SayTens(long number)
    {
        long num = number % 100L;
        long secondDigit = num % 10;
        long firstDigit = (num - secondDigit);

        if (firstDigit == 0 || firstDigit == 10) // 0-19
        {
            return zeroToNineteen[num];
        }
        else if (secondDigit == 0)
        {
            return tens[num];
        }
        else
        {
            return $"{tens[firstDigit]}-{zeroToNineteen[secondDigit]}";
        }
    }

    private static string SayHundreds(long number)
    {
        long num = number % 1000L;
        long tens = number % 100L;
        long hundreds = (num - tens) / 100;

        if (hundreds == 0)
            return SayTens(tens);
        else
        {
            if (tens == 0)
                return $"{zeroToNineteen[hundreds]} hundred";
            else
                return $"{zeroToNineteen[hundreds]} hundred {SayTens(tens)}";
        }
    }

    private static string SayThousands(long number)
    {
        long num = number % 1_000_000L;
        long hundreds = number % 1_000L;
        long thousands = (num - hundreds) / 1_000;

        if (thousands == 0)
            return SayHundreds(hundreds);
        else
        {
            if (hundreds == 0)
                return $"{SayHundreds(thousands)} thousand";
            else
                return $"{SayHundreds(thousands)} thousand {SayHundreds(hundreds)}";
        }
    }

    private static string SayMillions(long number)
    {
        long num = number % 1_000_000_000L;
        long thousands = number % 1_000_000L;
        long millions = (num - thousands) / 1_000_000;

        if (millions == 0)
            return SayThousands(thousands);
        else
        {
            if (thousands == 0)
                return $"{SayThousands(millions)} million";
            else
                return $"{SayThousands(millions)} million {SayThousands(thousands)}";
        }
    }
    
    
    private static string SayBillions(long number)
    {
        long num = number; // %  1_000_000_000_000L;
        long millions = number % 1_000_000_000L;
        long billions = (num - millions) / 1_000_000_000;

        if (billions == 0)
            return SayMillions(millions);
        else
        {
            if (millions == 0)
                return $"{SayMillions(billions)} billion";
            else
                return $"{SayMillions(billions)} billion {SayMillions(millions)}";
        }
    }
}