using System.Collections;

// I had things set up so it would be a double linked list so the reverse
// iterator would be nice and easy. 
// I made a forward and a reverse iterator which used the previous links.
// I made a reverse function that never got called.
// I tried making it an extension method that also never got called.
// With it removed the tests passed because it was never called anyway. Grrrr.
//
// I think if you pass an enumeration of values to a constuctor you expect them to be
// in the order given. Since we only have push that would reverse them.
// so I reversed the items so they would stay in order.
// That was NOT what they wanted. They want them reversed. Grrrr

// My Enumerator is odd. It looks like the head needs to be a throw away
// value as it expects to always pre-move before access. So I did have
// to go in and add in a throw-away value to the start of the enumeration.
// This is harder when the function is Generic and you can't just say 0.
// So the value is the same as whatever head has. This feels dumb. Grrr.

public class Node<T>
{
    public Node<T>? next;
    public T value;

    public Node(T value)
    {
        next = null;
        this.value = value;
    }
}

public class SimpleLinkedList<T> : IEnumerable<T>
{
    public Node<T>? head;

    public SimpleLinkedList()
    {
        head = null;
    }

    public SimpleLinkedList(T value)
    {
        Node<T> newValue = new(value);
        newValue.next = null;
        head = newValue;
    }

    public SimpleLinkedList(IEnumerable<T> values)
    {
        foreach (T value in values)
        {
            Push(value);
        }
    }
    public int Count
    {
        get
        {
            if (head == null)
                return 0;
            return Enumerable.Count<T>(this);
        }
    }

    public void Push(T value)
    {
        Node<T> newValue = new Node<T>(value);

        if (head == null)
        {
            head = newValue;
        }
        else
        {
            newValue.next = head;
            head = newValue;
        }
    }

    public T Pop()
    {
        if (head == null)
            throw new InvalidOperationException("Nothing to Pop from the stack");

        Node<T>? newHead = head.next;
        T value = head.value;

        if (newHead == null)
        {
            head = null;
        }
        else
        {
            head = newHead;
        }
        return value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (head == null)
            throw new InvalidOperationException("Nothing to enumerate");

        return new SimpleLinkedListEnumerator<T>(head);
    }
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class SimpleLinkedListEnumerator<T> : IEnumerator<T>
{
    Node<T>? current = null;
    Node<T>? head = null;
    public T Current()
    {
        if (current == null)
        {
            if (head == null)
                throw new InvalidOperationException("No current item.");
            else
                current = head;
        }
        return current.value;
    }
    object IEnumerator.Current => (object)Current()!;

    T IEnumerator<T>.Current => Current();

    public SimpleLinkedListEnumerator(Node<T> head)
    {
        this.head = new Node<T>(head.value);
        this.head.next = head;

        this.current = this.head;
    }

    public void Reset()
    {
        current = head;
    }
    public void Dispose()
    {
        // Nothing to dispose
    }

    public bool MoveNext()
    {
        if (current == null)
            return false;
        current = current.next;
        return current != null;
    }
}
