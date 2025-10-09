public class CustomSet
{
    private List<int> data = new();
    public CustomSet(params int[] values)
    {
        foreach(int value in values)
        {
            Add(value);
        }
    }

    public CustomSet Add(int value)
    {
        if (!data.Contains(value))
            data.Add(value);
        return this;
    }

    public bool Empty()
    {
        return data.Count() == 0;
    }

    public bool Contains(int value)
    {
        return data.Contains(value);
    }

    public bool Subset(CustomSet right)
    {
        foreach (int value in data)
            if (!right.Contains(value))
                return false;
        return true;
    }

    public bool Disjoint(CustomSet right)
    {
        foreach (int value in data)
            if (right.Contains(value))
                return false;
        return true;
    }

    public override bool Equals(Object? obj)
    {
        if (obj == null)
            return false;
        if (obj.GetType() == typeof(CustomSet))
        {
            CustomSet? other = obj as CustomSet;
            if (other == null)
                return false;
            List<int> otherData = new(other.data);
            List<int> selfData = new(data);
            if (selfData.Count() != otherData.Count())
                return false;
            otherData.Sort();
            selfData.Sort();
            for (int i = 0; i < selfData.Count; i++)
            {
                if (otherData[i] != selfData[i])
                    return false;
            }
            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return data.GetHashCode();
    }

    
    public CustomSet Intersection(CustomSet right)
    {
        CustomSet ret = new();
        foreach (int value in data)
            if (right.Contains(value))
                ret.Add(value);
        return ret;
    }

    public CustomSet Difference(CustomSet right)
    {
        CustomSet ret = new();
        foreach (int value in data)
            if (!right.Contains(value))
                ret.Add(value);
        return ret;
    }

    public CustomSet Union(CustomSet right)
    {
        CustomSet ret = new();
        foreach (int value in data)
            ret.Add(value);
        foreach (int value in right.data)
            ret.Add(value);
        return ret;
    }
}