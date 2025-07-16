public enum Classification
{
    Perfect,
    Abundant,
    Deficient
}

public static class PerfectNumbers
{
    public static Classification Classify(int number)
    {
        int aliquotSum = (
            from int i in Enumerable.Range(1, number - 1)
            where (number % i) == 0
            select i
        ).Sum();

        if (number > aliquotSum)
            return Classification.Deficient;
        else if (number < aliquotSum)
            return Classification.Abundant;
        return Classification.Perfect;
    }
}
