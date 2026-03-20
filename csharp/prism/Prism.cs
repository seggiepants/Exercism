public static class Prism
{
    public readonly record struct LaserInfo(double X, double Y, double Angle);

    public readonly record struct PrismInfo(int Id, double X, double Y, double Angle);

    /// <summary>
    /// Find the path of prisms hit by a laser.
    /// </summary>
    /// <param name="laser">Beam source, has a location on the cartesian grid and an angle (degrees) it is pointed in</param>
    /// <param name="prisms">Target prisms that have a location on a cartesian grid and deflection angle (degrees).</param>
    /// <returns>Array of laser ID values in order of when each was hit.</returns>
    public static int[] FindSequence(LaserInfo laser, PrismInfo[] prisms)
    {
        double lx = laser.X;
        double ly = laser.Y;
        double langle = laser.Angle;

        List<int> intersections = new();
        bool done = false;
        
        while(!done)
        {
            done = true;
            double minDistance = double.MaxValue;
            PrismInfo? hit = null;
            double angle = double.DegreesToRadians(langle);

            // This could probably be done with a higher-order function, but why?
            // Anyway find the nearest intersection setting done to false if we find one.
            foreach(PrismInfo prism in prisms)
            {
                double distance = Intersect(lx, ly, angle, prism.X, prism.Y);
                if (distance < minDistance)
                {
                    hit = prism;
                    minDistance = distance;
                    done = false;
                }
            }
            if (!done && hit.HasValue)
            {
                lx = hit.Value.X;
                ly = hit.Value.Y;
                langle += hit.Value.Angle; // Add the angle, don't replace.
                intersections.Add(hit.Value.Id);
            }
        }

        return intersections.ToArray<int>();   // Just turn into an array at the end. I wanted easy insertion up to this point.
    }

    /// <summary>
    /// We figure out if the laser hits the prism with the Pythagorean Theorem.
    /// You get dx, dy between the points. Then calculate the hypotenuse Sqrt(dx^2 + dy^2)
    /// Then figure out where that would be on the cartesian grid with:
    /// x1 = x0 + hypotenuse * Cos(angle)
    /// y1 = y0 + hypotenuse * Sin(angle)
    /// If the calculated values are not equal to the prism coordinates (allow for some presion uncertainty)
    /// then there is no intersection. I probably could have compared the angles too, but
    /// with cos/sin inverse you may have to figure out the unit circle quadrants as well.
    /// </summary>
    /// <param name="x0">x-coordinate of the laser</param>
    /// <param name="y0">y-coordinate of the laser</param>
    /// <param name="angle">angle of the laser converted into radians</param>
    /// <param name="x1">x-coordinate of candidate prism</param>
    /// <param name="y1">y-coordinate of candidate prism</param>
    /// <returns>double.MaxValue if no intersection, otherwise the distance between the points on success
    /// so that you can sort to find the nearest hit.</returns>
    private static double Intersect(double x0, double y0, double angle, double x1, double y1)
    {
        // Apparantely the correct error term is a secret, that can only be found by checking
        // failed tests. This is far coarser than I originally allowed. (I did expect some rounding problems)
        const double errorTerm = 0.01; 
        double dx = x1 - x0;
        double dy = y1 - y0;
        if (dx == 0.0 && dy == 0.0)
            return double.MaxValue;

        double hypotenuse = Math.Sqrt((dx * dx) + (dy * dy));
        double targetX = hypotenuse * Math.Cos(angle) + x0;
        double targetY = hypotenuse * Math.Sin(angle) + y0;

        return Math.Abs(x1 - targetX) < errorTerm && Math.Abs(y1 - targetY) < errorTerm ? hypotenuse : double.MaxValue;
    }
}
