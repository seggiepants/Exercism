public static class SquareRoot
{
    public static int Root(int number, int x0 = 1)
    {
        // I basically swiped this from Wikipedia.org
        // ==========================================
        //isqrt via Newton-Heron iteration with specified initial guess. 
        //Uses 2-cycle oscillation detection.
        //
        //Preconditions:
        //    n >= 0                    # isqrt(0) = 0
        //    x0 > 0, defaults to 1     # initial guess
        //
        //Output:
        //    isqrt(n)

        if (number < 0 || x0 <= 0)
            throw new Exception("Invalid input");

        // isqrt(0) = 0; 
        // isqrt(1) = 1
        if (number < 2)
            return number;

        int prev2 = -1; // x_{i-2}
        int prev1 = x0; // x_{i-1}

        while (true)
        {
            int x1 = (prev1 + number / prev1) / 2;

            // Case 1: converged (steady value)
            if (x1 == prev1)
                return x1;

            // Case 2: oscillation (2-cycle)
            if (x1 == prev2 && x1 != prev1)
            {
                // We’re flipping between prev1 and prev2
                // Choose the smaller one (the true integer sqrt)
                return prev1 < x1 ? prev1 : x1;
            }

            // Move forward
            prev2 = prev1;
            prev1 = x1;
        }

    }
}
