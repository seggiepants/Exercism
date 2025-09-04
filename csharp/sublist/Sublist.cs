public enum SublistType
{
    Equal,
    Unequal,
    Superlist,
    Sublist
}

public static class Sublist
{
    public static SublistType Classify<T>(List<T> list1, List<T> list2)
        where T : IComparable
    {
        if (IsEqual(list1, list2))
            return SublistType.Equal;

        if (IsSublist(list1, list2))
            return SublistType.Sublist;

        if (IsSublist(list2, list1))
            return SublistType.Superlist;

        return SublistType.Unequal;
    }

    static bool IsEqual<T>(List<T> list1, List<T> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            T item1 = list1[i];
            T item2 = list2[i];
            if (item1 == null && item2 != null)
                return false;
            else if (item2 != null && item2 == null)
                return false;
            else if (item1 != null && item2 != null && !item1.Equals(item2))
                return false;
        }
        return true;
    }

    static bool IsSublist<T>(List<T> list1, List<T> list2)
    {
        // This code assumes IsEqual Will have been called previously.

        // Special case empty list is always a sub list.
        if (list1.Count == 0)
            return true;

        int minIndex = 0;
        int index;


        index = list2.IndexOf(list1[0], minIndex);
        while (index != -1 && minIndex < list2.Count)
        {
            minIndex = index + 1;

            // Can you fit list 1 in at this point?
            if (list2.Count >= index + list1.Count)
            {
                // It can fit, is it equal?
                if (IsEqual(list1, list2.GetRange(index, list1.Count)))
                {
                    return true;
                }
            }
            else
            {
                // Doesn't fit so not a sub list.
                return false;
            }

            // try the next occurrence starting with first item of list 1
            index = list2.IndexOf(list1[0], minIndex);
        }
        return false;
    }
}