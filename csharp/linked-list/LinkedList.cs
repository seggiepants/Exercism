public class Item<T>
{
    public T Value { get; set; }
    public Item<T>? previous;
    public Item<T>? next;

    public Item(T value)
    {
        Value = value;
        previous = null;
        next = null;
    }
}
public class Deque<T>
{
    Item<T>? first;
    Item<T>? last;

    public Deque()
    {
        first = null;
        last = null;
    }
    public void Push(T value)
    {
        Item<T> top = new Item<T>(value);
        top.next = first;
        if (first != null)
            first.previous = top;
        first = top;
        if (last == null)
            last = first;
    }

    public T Pop()
    {
        if (first == null)
            throw new ArgumentException("Deque is empty.");

        Item<T> top = first;
        first = first.next;
        if (first != null)
        {
            first.previous = null;
        }
        if (last == top)
            last = first;

        return top.Value;        
    }

    public void Unshift(T value)
    {
        Item<T> bottom = new Item<T>(value);
        bottom.previous = last;
        if (last != null)
            last.next = bottom;
        last = bottom;
        if (first == null)
            first = last;
    }

    public T Shift()
    {
        if (last == null)
            throw new ArgumentException("Deque is empty.");
        Item<T>? bottom = last;
        last = last.previous;
        if (last != null)
            last.next = null;

        if (bottom == first)
            first = last;

        return bottom.Value;
    }
}