using System.Collections;

public class BinarySearchTree : IEnumerable<int>
{
    BinarySearchTree? left;
    BinarySearchTree? right;
    int value;
    public BinarySearchTree(int value)
    {
        left = null;
        right = null;
        this.value = value;
    }

    public BinarySearchTree(IEnumerable<int> values)
    {
        bool first = true;
        foreach (int value in values)
        {
            if (first)
            {
                first = false;
                this.value = value;
            }
            else
            {
                this.Add(value);
            }
        }
    }

    public int Value
    {
        get
        {
            return value;
        }
    }

    public BinarySearchTree? Left
    {
        get
        {
            return left;
        }
    }

    public BinarySearchTree? Right
    {
        get
        {
            return right;
        }
    }

    public BinarySearchTree Add(int value)
    {
        if (value <= this.value)
        {
            // Left;
            if (left == null)
            {
                left = new BinarySearchTree(value);
            }
            else
            {
                left.Add(value);
            }
        }
        else if (value > this.value)
        {
            // Right
            if (right == null)
            {
                right = new BinarySearchTree(value);
            }
            else
            {
                right.Add(value);
            }
        }
        return this;
    }

    public IEnumerator<int> GetEnumerator()
    {
        return Enumerate();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Enumerate();
    }
    
    private IEnumerator<int> Enumerate()
    {
        if (left != null)
        {
            foreach (int i in left)
            {
                yield return i;
            }
        }
        yield return this.value;
        if (right != null)
        {
            foreach (int i in right)
            {
                yield return i;
            }
        }

    }

}