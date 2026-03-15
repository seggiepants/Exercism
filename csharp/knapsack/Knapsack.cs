public static class Knapsack
{
    public static int MaximumValue(int maximumWeight, (int weight, int value)[] items)
    {        
        List<(int weight, int value)>? sorted = (from pair in items 
        orderby pair.weight, pair.value descending
        select pair).ToList<(int weight, int value)>();
        
        return MaximumValue_Helper(maximumWeight, sorted, new Dictionary<string, int>());        
    }
    
    private static int MaximumValue_Helper(int maximumWeight, List<(int weight, int value)> items, Dictionary<string, int> memoized)
    {
        List<int> scores = new();
        for(int i = 0; i < items.Count; i++)
        {
            (int weight, int value) = items[i];
            if (weight < maximumWeight)
            {
                List<(int weight, int value)>? nextItems = (from pair in items.Index()
                    where pair.Index != i && pair.Item.weight <= maximumWeight
                    select pair.Item).ToList();
                
                if (nextItems == null)
                    continue;
                int nextWeight = maximumWeight - weight;                
                string itemKey = String.Join(",", (from item in nextItems
                select $"{item.weight}|{item.value}"));
                int remainingValue;
                string key = $"{nextWeight},[{itemKey}]";
                if (memoized.ContainsKey(key))
                    remainingValue = memoized[key];
                else 
                {
                    remainingValue = MaximumValue_Helper(nextWeight, nextItems, memoized);
                    memoized[key] = remainingValue;
                }
                
                scores.Add(value + remainingValue);
            }
            else if (weight == maximumWeight)
            {
                scores.Add(value);
            }
        }
        return scores.Count == 0 ? 0 : scores.Max();
    }
}
