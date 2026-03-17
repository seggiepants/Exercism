public class Tree
{
    public string Value {get; set;}
    public List<Tree> Children {get; set;}
    public Tree(string value, params Tree[] children)
    {
        Value = value;
        Children = children.ToList<Tree>();
    }

    public override bool Equals(object? obj)
    {
        Tree? other = obj as Tree;

        if (other == null)
            return false;

        if (Value != other.Value)
            return false;
        
        if (Children.Count() != other.Children.Count())
            return false;

        // Would you look at that. One of the test purposely put the children in the wrong order.
        // I preserved the order, so I think they did it on purpose.
        // So now I am comparing children sorted by name. 
        // Should work as long as we don't have name collisions.
        Tree[] a = (from child in Children orderby child.Value select child).ToArray<Tree>();
        Tree[] b = (from child in other.Children orderby child.Value select child).ToArray<Tree>();
        for(int i = 0; i < a.Count(); i++)
        {
            if (!a[i].Equals(b[i]))
                return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        List<string> codes = (from Tree child in Children select child.GetHashCode().ToString()).ToList<string>();
        codes.Add(Value.GetHashCode().ToString());
        return String.Join(",",codes).GetHashCode();
    }

    public override string ToString()
    {
        string children = string.Join(",", (from child in Children select child.ToString()));
        return $"({Value}: [{children}])";
    }

}

public static class Pov
{
    public static Tree FromPov(Tree tree, string from)
    {
        if (tree.Value == from)
            return tree;
        
        List<Tree> path = new();
        if (!Find(tree, from, path))
        {
            throw new ArgumentException($"Path not found to \"{from}\".");
        }
        Tree root = path[0];
        path.RemoveAt(0);
        while(path.Count() > 0)
        {
            Tree next = path[0];
            path.RemoveAt(0);

            root.Children = root.Children.Where((Tree node) => node.Value != next.Value).ToList<Tree>();
            next.Children.Add(root);
            root = next;
        }
        return root;
    }

    // On exception IEnumerable doesn't throw the exception immediately only when the 
    // iterator is evaluated. 
    // This breaks the test cases..... again
    // To fix this I changed IEnumerable<string> to List<string>
    public static List<string> PathTo(string from, string to, Tree tree)
    {
        if (from == to)
        {
            return new List<string>([from]);
        }
        
        List<Tree> rootFrom = new();
        if (!Find(tree, from, rootFrom))
        {
            throw new ArgumentException($"Cannot find node \"{from}\".");
        }

        List<Tree> rootTo = new();
        if (!Find(tree, to, rootTo))
        {
            throw new ArgumentException($"Cannot find node \"{to}\".");
        }
        
        string[] fromPath = rootFrom.Select((Tree node) => node.Value).ToArray<string>();
        string[] toPath = rootTo.Select((Tree node) => node.Value).ToArray<string>();
        
        string current;
        bool switched = false;
        int index = fromPath.Length - 1;
        List<string>ret = new();
        while (switched == false || index < toPath.Length)
        {
            if (switched)
            {
                current = toPath[index];
                index++;
                ret.Add(current);
            }
            else
            {
                current = fromPath[index];
                int nextIndex = toPath.IndexOf(current);
                if (nextIndex >= 0)
                {
                    switched = true;
                    index = nextIndex + 1;
                }
                else
                    index--;
                

                // Turn around at root.
                if (index < 0)
                {
                    switched = true;
                    index = 1;                
                }

                // Return the current item.
                ret.Add(current);

            }
        }

        return ret;
    }

    private static bool Find(Tree root, string value, List<Tree> path)
    {
        if (root.Value == value)
        {
            path.Add(root);
            return true;
        }
        
        foreach(Tree child in root.Children)
        {
            bool ret = Find(child, value, path);
            if (ret)
            {
                path.Insert(0, root);
                return true; 
            }
        }
        return false;        
    }

}