public enum Bucket
{
    One,
    Two
}

public class TwoBucketResult
{
    public int Moves { get; set; }
    public Bucket GoalBucket { get; set; }
    public int OtherBucket { get; set; }
}

public class TwoBucket
{
    Bucket startBucket;
    int bucketOneSize;
    int bucketTwoSize;
    public TwoBucket(int bucketOne, int bucketTwo, Bucket startBucket)
    {
        bucketOneSize = bucketOne;
        bucketTwoSize = bucketTwo;
        this.startBucket = startBucket;
    }

    public TwoBucketResult Measure(int goal)
    {            
        List<(int, int)> attempts = new();
        Queue<(int, int, int)> queue = new();
        queue.Enqueue((0, 0, 0));
        TwoBucketResult result = new();
        bool success = false;

        while (!success && queue.Count > 0)
        {
            (int, int, int) next = queue.Dequeue();
            if (!attempts.Contains((next.Item2, next.Item3)))
            {
                attempts.Add((next.Item2, next.Item3));
                success = BucketStep(next.Item1, goal, startBucket, next.Item2, next.Item3, result, queue);
            }
        }

        if (!success)
            throw new ArgumentException("Solution not found.");
        return result;
    }

    bool BucketStep(int moves, int goal, Bucket startBucket, int one, int two, TwoBucketResult result, Queue<(int, int, int)> queue)
    {
        // Invalid board position?
        if ((startBucket == Bucket.One && one == 0 && two == bucketTwoSize) ||
            (startBucket == Bucket.Two && two == 0 && one == bucketOneSize))
            return false;

        // Did we win?
        if (one == goal || two == goal)
        {
            // Check if we got to the goal state.
            result.Moves = moves;
            if (one == goal)
            {
                result.GoalBucket = Bucket.One;
                result.OtherBucket = two;
            }
            else
            {
                result.GoalBucket = Bucket.Two;
                result.OtherBucket = one;
            }
            return true;
        }

        // Now what actions can we do?
        if (moves == 0)
        {
            if (startBucket == Bucket.One)
            {
                // Fill Bucket one
                (int, int, int) item = (moves + 1, bucketOneSize, two);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }
            else
            {
                // Fill Bucket two.
                (int, int, int) item = (moves + 1, one, bucketTwoSize);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }
        }
        else
        {
            // Fill Bucket one
            if (one != bucketOneSize)
            {
                (int, int, int) item = (moves + 1, bucketOneSize, two);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }

            // Fill Bucket two
            if (two != bucketTwoSize)
            {
                (int, int, int) item = (moves + 1, one, bucketTwoSize);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }

            // Empty Bucket one
            if (one != 0)
            {
                (int, int, int) item = (moves + 1, 0, two);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }

            // Empty Bucket two
            if (two != 0)
            {
                (int, int, int) item = (moves + 1, one, 0);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }

            // Pour Bucket one into two
            if (one != 0 && two != bucketTwoSize)
            {
                int delta = Math.Min(one, bucketTwoSize - two);
                (int, int, int) item = (moves + 1, one - delta, two + delta);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }

            // Pour Bucket two into one
            if (two != 0 && one != bucketOneSize)
            {
                int delta = Math.Min(two, bucketOneSize - one);
                (int, int, int) item = (moves + 1, one + delta, two - delta);
                if (!queue.Contains(item))
                    queue.Enqueue(item);
            }
        }
        return false;
    }
}
