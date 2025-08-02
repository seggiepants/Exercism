public static class Darts
{
    public static int Score(double x, double y)
    {
        double hypotenuse = Math.Sqrt((x * x) + (y * y));
        if (hypotenuse <= 1.0)
            return 10; // Center
        else if (hypotenuse <= 5.0)
            return 5; // Middle circle
        else if (hypotenuse <= 10.0)
            return 1; // Outer circle
        return 0; // Out of bounds
    }
}
