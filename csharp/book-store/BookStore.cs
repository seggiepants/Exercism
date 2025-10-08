public static class BookStore
{
    const decimal SINGLE_BOOK = 8.0M;
    static Dictionary<int, decimal> Discount = new Dictionary<int, decimal>()
    {
        [2] = 0.05M,
        [3] = 0.10M,
        [4] = 0.20M,
        [5] = 0.25M,
    };

    static Dictionary<int, List<List<int>>> allCombinations = new()
    {

        [5] = new List<List<int>>()
        {
            new() {1, 2, 3, 4, 5 },
        },
        [4] = new List<List<int>>()
        {
            new() {1, 2, 3, 4},
            new() {1, 2, 3, 5},
            new() {1, 2, 4, 5},
            new() {1, 3, 4, 5},
            new() {2, 3, 4, 5},
        },
        [3] = new List<List<int>>()
        {
            new() {1, 2, 3 },
            new() {1, 2, 4 },
            new() {1, 2, 5 },
            new() {1, 3, 4 },
            new() {1, 3, 5 },
            new() {1, 4, 5 },
            new() {2, 3, 4 },
            new() {2, 3, 5 },
            new() {2, 4, 5 },
            new() {3, 4, 5 },
        },
        [2] = new List<List<int>>()
        {
            new() {1, 2 },
            new() {1, 3 },
            new() {1, 4 },
            new() {1, 5 },
            new() {2, 3 },
            new() {2, 4 },
            new() {2, 5 },
            new() {3, 4 },
            new() {3, 5 },
            new() {4, 5 },
        }
    };

    static Dictionary<string, decimal> cache = new Dictionary<string, decimal>();

    public static decimal Total(IEnumerable<int> books)
    {
        Dictionary<int, int> booksGrouped = (from int book in books
                                             group book by book into pair
                                             select new KeyValuePair<int, int>(pair.Key, pair.Count())).ToDictionary<int, int>();

        return CalcDiscount(booksGrouped);
    }

    public static decimal CalcDiscount(Dictionary<int, int> books)
    {
        decimal minTotal;

        // Single book price for all as base case.
        minTotal = (from pair in books select pair.Value).Sum() * SINGLE_BOOK;

        for (int i = 2; i <= 5; i++)
        {
            if (books.Keys.Count >= i)
            {
                decimal total = CheckGroup(books, i);
                if (total < minTotal)
                    minTotal = total;
            }
        }

        return minTotal;
    }

    static decimal CheckGroup(Dictionary<int, int> books, int groupSize)
    {
        decimal minTotal = (from pair in books select pair.Value).Sum() * SINGLE_BOOK;        
        if (allCombinations.ContainsKey(groupSize))
        {
            List<List<int>> combinations = allCombinations[groupSize];

            var available = (from combination in combinations
                         where combination.All<int>(n => books.ContainsKey(n))
                         select combination);
            if (available != null)
            {
                foreach (List<int> combination in available)
                {
                    Dictionary<int, int> current = new(books);
                    foreach (int i in combination)
                    {
                        current[i] = current[i] - 1;
                        if (current[i] <= 0)
                            current.Remove(i);
                    }
                    if (!Discount.TryGetValue(groupSize, out decimal discount))
                        discount = 0.0M;
                    decimal total = groupSize * SINGLE_BOOK * (1.00M - discount);

                    // Don't bother with a recursive call unless we are smaller than the current minimum
                    if (current.Count > 0 && total < minTotal)
                        total += CalcDiscountMemoize(current);
                        
                    if (total < minTotal)
                            minTotal = total;
                }
            }
        }
        return minTotal;
    }

    public static decimal CalcDiscountMemoize(Dictionary<int, int> books)
    {
        // It doesn't matter if we have 2,2,3 or 2,3,3 the discount will be the same.
        // so sort the counts ascending. Then turn that into a string so it will be keyed
        // on value not reference. Then cache/retrieve it so we don't have to
        // do it a million times. I had __HUGE__ runtimes before I got this working.

        int[] keyArr = (from int value in books.Values orderby value select value).ToArray<int>();
        string key = String.Join(',', (from int value in keyArr select value.ToString()));
        if (cache.ContainsKey(key))
            return cache[key];
        decimal ret = CalcDiscount(books);
        cache.Add(key, ret);
        return ret;
    } 
}