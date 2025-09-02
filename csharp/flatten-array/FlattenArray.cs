using System.Collections;

public static class FlattenArray
{
    public static IEnumerable Flatten(IEnumerable input)
    {
        foreach (object item in input)
        {
            if (item == null)
                continue;
            IEnumerable? enumerate = (item as IEnumerable);
            if (enumerate != null)
                foreach (object subItem in Flatten(enumerate))
                    yield return subItem;
            else
                yield return item;
        }
    }
}