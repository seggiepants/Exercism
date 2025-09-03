using Xunit.Sdk;

public static class ListOps
{
    public static int Length<T>(List<T> input)
    {
        int counter = 0;

        foreach (T item in input)
            counter++;

        return counter;
    }

    public static List<T> Reverse<T>(List<T> input)
    {
        List<T> ret = new();
        foreach (T item in input)
        {
            ret.Insert(0, item);
        }
        return ret;
    }

    public static List<TOut> Map<TIn, TOut>(List<TIn> input, Func<TIn, TOut> map)
    {
        List<TOut> ret = new();
        foreach (TIn item in input)
        {
            ret.Add(map(item));
        }
        return ret;
    }

    public static List<T> Filter<T>(List<T> input, Func<T, bool> predicate)
    {
        List<T> ret = new();
        foreach (T item in input)
        {
            if (predicate(item))
                ret.Add(item);
        }
        return ret;
    }

    public static TOut Foldl<TIn, TOut>(List<TIn> input, TOut start, Func<TOut, TIn, TOut> func)
    {
        TOut ret = start;

        foreach (TIn item in input)
            ret = func(ret, item);

        return ret;
    }

    public static TOut Foldr<TIn, TOut>(List<TIn> input, TOut start, Func<TIn, TOut, TOut> func)
    {
        TOut ret = start;
        for (int i = Length<TIn>(input) - 1; i >= 0; i--)
        {
            ret = func(input[i], ret);
        }
        return ret;
    }

    public static List<T> Concat<T>(List<List<T>> input)
    {
        List<T> ret = new();
        foreach (List<T> items in input)
        {
            ret.AddRange(items);
        }
        return ret;
    }

    public static List<T> Append<T>(List<T> left, List<T> right)
    {
        foreach (T item in right)
        {
            left.Add(item);
        }
        return left;
    }
}