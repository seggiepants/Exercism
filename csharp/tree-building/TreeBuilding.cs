using System.Runtime.CompilerServices;

public class TreeBuildingRecord
{
    public int ParentId { get; set; }
    public int RecordId { get; set; }
}

public class Tree
{
    public Tree()
    {
        Children = new List<Tree>();
    }

    public Tree(int Id, int ParentId)
    {
        this.Id = Id;
        this.ParentId = ParentId;
        Children = new List<Tree>();
    }
    public int Id { get; set; }
    public int ParentId { get; set; }

    public List<Tree> Children { get; set; }

    public bool IsLeaf => Children.Count == 0;
}

public static class TreeBuilder
{
    public static Tree BuildTree(IEnumerable<TreeBuildingRecord> records)
    {
        if (records.Count() == 0)
            throw new ArgumentException("No records passed in.");

        if (!isContinuous(records))
            throw new ArgumentException("Continuous ID values expected.");

        var sorted = (from record in records orderby record.ParentId, record.RecordId select record);
        Dictionary<int, Tree> index = new();

        foreach (TreeBuildingRecord record in sorted)
        {
            if ((record.RecordId == record.ParentId && record.RecordId != 0) ||
                (record.RecordId < record.ParentId))
                throw new ArgumentException("Malformed Tree Building Record");
            Tree current = new(record.RecordId, record.ParentId);
            index.TryGetValue(record.ParentId, out Tree? parent);
            if (parent != null)
                parent.Children.Add(current);
            else if (parent != null && record.RecordId != 0)
                throw new ArgumentException("Parent expected but not found.");
            index.Add(record.RecordId, current);
        }

        bool found = index.TryGetValue(0, out Tree? ret);
        if (!found || ret == null)
            throw new ArgumentException("No root node found.");
        return ret;
    }

    private static bool isContinuous(IEnumerable<TreeBuildingRecord> records)
    {
        var sequence = (from record in records orderby record.RecordId ascending select record.RecordId);
        bool first = true;
        bool continuous = true;
        int previous = 0;
        foreach (int current in sequence)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                if (current != previous + 1)
                {
                    continuous = false;
                    break;
                }
            }
            previous = current;
        }
        return continuous;
    }
}