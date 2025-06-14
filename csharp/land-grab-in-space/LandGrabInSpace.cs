// I was going to sort the points in a plot to have a robust equality comparison.
// That required implementing IComparable. However, once I got to the side length
// part I had to toss that out and hope points were always in the same order.
//
// One the plus side I needed to implement IEquatable to make Equals work (I think)
// and I could piggyback onto my IComparable code for that.
//
// I expected it to get mad I didn't implement GetHashCode(), but it didn't.

public struct Coord : IComparable<Coord>, IEquatable<Coord>
{
    public Coord(ushort x, ushort y)
    {
        X = x;
        Y = y;
    }

    public ushort X { get; }
    public ushort Y { get; }

    public int CompareTo(Coord other)
    {
        int ret = this.X.CompareTo(other.X);
        if (ret == 0)
            return this.Y.CompareTo(other.Y);
        return ret;
    }

    public bool Equals(Coord other)
    {
        return this.CompareTo(other) == 0;
    }


    public double SideLength(Coord pt)
    {
        int dx = pt.X - X;
        int dy = pt.Y - Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }

}

public struct Plot : IComparable<Plot>, IEquatable<Plot>
{
    List<Coord> points;

    public Plot(Coord p1, Coord p2, Coord p3, Coord p4)
    {
        points = [p1, p2, p3, p4];
    }

    public int CompareTo(Plot other)
    {
        for (int i = 0; i < points.Count; i++)
        {
            int ret = points[i].CompareTo(other.points[i]);
            if (ret != 0)
                return ret;
        }
        return 0;
    }

    public bool Equals(Plot other)
    {
        return this.CompareTo(other) == 0;
    }


    public double LongestSide()
    {
        double maxSideLength = points.Last().SideLength(points[0]);  // Last edge that loops back
        for (int i = 0; i < points.Count - 1; i++)
        {
            double sideLength = points[i].SideLength(points[i + 1]);
            maxSideLength = Math.Max(maxSideLength, sideLength);
        }
        return maxSideLength;
    }

}


public class ClaimsHandler
{
    List<Plot> claimed = new();
    public void StakeClaim(Plot plot)
    {
        if (!IsClaimStaked(plot))
            claimed.Add(plot);
    }

    public bool IsClaimStaked(Plot plot)
    {
        foreach (Plot claimedPlot in claimed)
        {
            if (claimedPlot.Equals(plot))
                return true;
        }

        return false;
    }

    public bool IsLastClaim(Plot plot)
    {
        return plot.Equals(claimed.Last<Plot>());
    }

    public Plot GetClaimWithLongestSide()
    {
        double maxSideLength = 0.0;
        if (claimed.Count <= 0)
            throw new IndexOutOfRangeException("No plots.");

        Plot ret = claimed[0]; // default value;
        foreach (Plot p in claimed)
        {
            double sideLength = p.LongestSide();
            if (sideLength > maxSideLength)
            {
                ret = p;
                maxSideLength = sideLength;
            }
        }
        return ret;        
    }
}
