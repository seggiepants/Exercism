public class SpiralMatrix
{
    enum Direction
    {
        LEFT, DOWN, RIGHT, UP
    };
    public static int[,] GetMatrix(int size)
    {
        int[,] ret = new int[size, size];
        int counter = 0;
        int x = -1;
        int y = 0;
        int xMin = 0;
        int xMax = size - 1;
        int yMin = 0;
        int yMax = size - 1;
        Direction dir = Direction.RIGHT;
        while (counter < size * size)
        {
            if (dir == Direction.RIGHT)
            {
                x++;
                if (x > xMax)
                {
                    x = xMax;
                    dir = Direction.DOWN;
                    yMin++;
                }
            }

            if (dir == Direction.DOWN)
            {
                y++;
                if (y > yMax)
                {
                    y = yMax;
                    dir = Direction.LEFT;
                    xMax--;
                }
            }

            if (dir == Direction.LEFT)
            {
                x--;
                if (x < xMin)
                {
                    x = xMin;
                    dir = Direction.UP;
                    yMax--;
                }

            }

            if (dir == Direction.UP)
            {
                y--;
                if (y < yMin)
                {
                    y = yMin;
                    dir = Direction.RIGHT;
                    xMin++;
                    x++; // Won't get to RIGHT updating x before use so update now.
                }
            }
            ret[y, x] = ++counter;
        }
        return ret;
    }
}
