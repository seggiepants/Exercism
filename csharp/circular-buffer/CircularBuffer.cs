public class CircularBuffer<T>
{
    T[] buffer;
    int read, write, capacity, stored;
    public CircularBuffer(int capacity)
    {
        buffer = new T[capacity];
        this.capacity = capacity;
        Clear();        
    }

    public T Read()
    {
        if (stored <= 0)
        {
            throw new InvalidOperationException("Circular Buffer is empty");
        }
        T ret = buffer[read];
        read = (read + 1) % buffer.Length;
        stored--;
        return ret;
    }

    public void Write(T value)
    {
        if (stored >= capacity)
        {
            throw new InvalidOperationException("Circular Buffer is full");
        }        
        buffer[write] = value;
        write = (write + 1) % buffer.Length;
        stored++;
    }

    public void Overwrite(T value)
    {
        while (stored >= capacity)
        {
            read = (read + 1) % buffer.Length;
            stored--;
        }
        buffer[write] = value;
        write = (write + 1) % buffer.Length;
        stored++;
    }

    public void Clear()
    {
        read = 0;
        write = 0;
        stored = 0;
    }
}