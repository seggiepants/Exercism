using System.Collections;

public class Node : IEnumerable<Attr>, IComparable<Node>
{
    public string Value;
    List<Attr> Attrs;

    public Node(string value)
    {
        Value = value;
        Attrs = new List<Attr>();
    }

    public Node(string value, List<Attr> attrs)
    {
        Value = value;
        Attrs = attrs;
    }

    public void Add(Attr attr)
    {
        Attrs.Add(attr);
    }

    public void Add(string a, string b)
    {
        Attrs.Add(new Attr(a, b));
    }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(obj is Node))
            return false;
        Node? other = (obj as Node);

        if (other == null) // mute the garbage warning about possible null.
            return false;

        if (Attrs.Count != other.Attrs.Count)
            return false;
        for (int i = 0; i < Attrs.Count; i++)
            if (!Attrs[i].Equals(other.Attrs[i]))
                return false;
        return other.Value == Value;
    }

    public IEnumerator<Attr> GetEnumerator() => Attrs.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public int CompareTo(Node? other)
    {
        if (other == null)
            return 1;
        if (Value == other.Value)
        {
            if (Attrs.Count == other.Attrs.Count)
            {
                if (Attrs.Count == 0) return 0;
                if (Attrs.Equals(other.Attrs)) return 0;
                string a = String.Join(", ", (from Attr attr in Attrs select attr.ToString()));
                string b = String.Join(", ", (from Attr attr in other.Attrs select attr.ToString()));
                return a.CompareTo(b);
            }
            else if (Attrs.Count > other.Attrs.Count)
                return 1;
            else // Attrs.Count < other.Attrs.Count
                return -1;
        }
        else
            return Value.CompareTo(other.Value);
    }

    public override string ToString()
    {
        string attributes = String.Join(", ", (
            from Attr attr in Attrs
            select attr.ToString()
        ));
        return $"Node: {Value} Attributes: {attributes}";
    }

}

public class Edge : IEnumerable<Attr>, IComparable<Edge>
{
    public string A;
    public string B;

    List<Attr> Attrs;

    public Edge(string a, string b)
    {
        A = a;
        B = b;
        Attrs = new List<Attr>();
    }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(obj is Edge))
            return false;

        Edge? other = obj as Edge;
        if (other == null) // checking twice to supress the potential null warning
            return false;
        if (Attrs.Count != other.Attrs.Count)
            return false;
        for (int i = 0; i < Attrs.Count; i++)
            if (!Attrs[i].Equals(other.Attrs[i]))
                return false;
        return other.A == A && other.B == B;
    }

    public void Add(Attr attr)
    {
        Attrs.Add(attr);
    }

    public void Add(string a, string b)
    {
        Attrs.Add(new Attr(a, b));
    }

    public IEnumerator<Attr> GetEnumerator() => Attrs.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public int CompareTo(Edge? other)
    {
        if (other == null)
            return 1; // Bigger

        if (A == other.A)
        {
            if (B == other.B)
            {
                string a = String.Join(", ", (from Attr attr in Attrs select attr.ToString()));
                string b = String.Join(", ", (from Attr attr in other.Attrs select attr.ToString()));
                return a.CompareTo(b);
            }
            return B.CompareTo(other.B); // If A's equal but not B, B decides.
        }
        else
            return A.CompareTo(other.A); // If not equal A's can decide.

    }

    public override string ToString()
    {
        string attributes = String.Join(", ", (
            from Attr attr in Attrs
            select attr.ToString()
        ));
        return $"Edge: {A}->{B} Attributes: {attributes}";
    }
}

public class Attr : IComparable<Attr>
{
    public string A;
    public string B;

    public Attr(string a, string b)
    {
        A = a;
        B = b;
    }

    public int CompareTo(Attr? other)
    {
        if (other == null)
            return 1; // Something bigger than nothing.

        int ret = A.CompareTo(other.A);
        if (ret == 0)
            ret = B.CompareTo(other.B);
        return ret;
    }


    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(obj is Attr))
            return false;

        Attr? other = obj as Attr;
        if (other == null) // checking twice to supress the potential null warning we already checked
            return false;
        return other.A == A && other.B == B;
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode();
    }

    public override string ToString()
    {
        return $"\"{A}\": \"{B}\"";
    }

}

public class Graph : IEnumerable<Node>
{
    public List<Node> Nodes;
    public List<Edge> Edges;
    public List<Attr> Attrs;

    public Graph()
    {
        Nodes = new List<Node>();
        Edges = new List<Edge>();
        Attrs = new List<Attr>();
    }

    public void Add(Node node)
    {
        Nodes.Add(node);
        Nodes.Sort();
    }

    public void Add(Edge edge)
    {
        Edges.Add(edge);
        Edges.Sort();
    }

    public void Add(Attr attr)
    {
        Attrs.Add(attr);
        Attrs.Sort();
    }

    public void Add(string a, string b)
    {
        Attrs.Add(new Attr(a, b));
        Attrs.Sort();
    }

    public override bool Equals(object? obj)
    {
        if ((obj == null) || !(obj is Graph))
            return false;
        Graph? other = (obj as Graph);
        if (other == null) // dumb warning makes no sense we already checked for null.
            return false;

        return (Nodes.Equals(other.Nodes) &&
            Edges.Equals(other.Edges) &&
            Attrs.Equals(other.Attrs));
    }

    public override int GetHashCode()
    {
        return ToString().GetHashCode(); 
    }

    public IEnumerator<Node> GetEnumerator() => Nodes.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    public override string ToString()
    {
        string nodes = String.Join(", ", (from Node node in Nodes select node.ToString()));
        string edges = String.Join(", ", (from Edge edge in Edges select edge.ToString()));
        string attributes = String.Join(", ", (from Attr attr in Attrs select attr.ToString()));
        return $"Graph: [Nodes: {nodes}\nEdges: {edges}\nAttributes: {attributes}]\n";
    }
}