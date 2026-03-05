public class BinTree
{
    public BinTree(int value, BinTree? left, BinTree? right)
    {
        this.Value = value;
        this.Left = left;
        this.Right = right;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is BinTree)
        {
            BinTree? other = (BinTree?)obj;
            if (other == null)
                return false;

            // Values match
            if (this.Value != other.Value)
                return false;

            // both left and right on this and other should
            // both be either null or not null.
            if ((this.Left == null && other.Left != null) ||
                (this.Left != null && other.Left == null) || 
                (this.Right == null && other.Right != null) || 
                (this.Right != null && other.Right == null))
                return false;
            
            // If the lefts aren't null check they are equal
            if (this.Left != null && other.Left != null && !this.Left.Equals(other.Left))
                return false;
            
            // If the rights aren't null check they are equal
            if (this.Right != null && other.Right != null && !this.Right.Equals(other.Right))
                return false;
            
            // Passed the tests so equal.
            return true;

        }
        return false;
    }

    /// <summary>
    /// Dot net complains if you have an equals and not a GetHashCode()
    /// </summary>
    /// <returns>Hashcode for this object</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(this.Value, this.Left == null ? -1 : this.Left.GetHashCode(), this.Right == null ? -1 : this.Right.GetHashCode());
    }

    /// <summary>
    /// Debugging tool to see if objects are equal.
    /// </summary>
    /// <returns>String representation of this object</returns>
    public override string ToString()
    {
        string left = this.Left == null ? "null" : this.Left.ToString();
        string right = this.Right == null ? "null" : this.Right.ToString();
        return $"(value: {this.Value}, left: {left}, right: {right})";
    }

    public int Value { get; }
    public BinTree? Left { get; }
    public BinTree? Right { get; }
}

public class Zipper
{   
    private string _moveStack;
    private BinTree? _tree;
    private BinTree? _focus;

    Zipper(BinTree? tree, string? moveStack)
    {
        this._tree = tree;
        if (moveStack == null)
            this._moveStack = "";
        else
            this._moveStack = moveStack;
        this._focus = Zipper.findFocus(this._tree, this._moveStack);
    }

    // Find the node we are focused on by following the moves
    private static BinTree? findFocus(BinTree? tree, string? moveStack)
    {
        if (tree == null)
            return null;
        BinTree? current = tree;
        if (moveStack != null && moveStack.Length > 0)
        {
            string moves = moveStack.Substring(0, moveStack.Length);
            foreach(char move in moves)
            {
                if (move == 'L' && current != null && current.Left != null)
                    current = current.Left;
                else if (move == 'R' && current != null && current.Right != null)
                    current = current.Right;
            }
        }
        return current;
    }

    // Copy a tree optionally replacing a given node with a new value, left tree, and/or right tree.
    // Those read-only fields really messed things up and required the replacements.
    private static BinTree? TreeCopy(BinTree? tree, BinTree? replaceNode, bool replaceValue, int newValue, bool replaceLeft, BinTree? newLeft, bool replaceRight, BinTree? newRight)
    {
        if (tree == null)
            return null;
        bool replace = replaceNode != null && tree == replaceNode;
        BinTree? left = replace && replaceLeft ? newLeft : Zipper.TreeCopy(tree.Left, replaceNode, replaceValue, newValue, replaceLeft, newLeft, replaceRight, newRight);
        BinTree? right = replace && replaceRight ? newRight : Zipper.TreeCopy(tree.Right, replaceNode, replaceValue, newValue, replaceLeft, newLeft, replaceRight, newRight);
        int value = replace && replaceValue ? newValue : tree.Value;
        return new BinTree(value, left, right);
  }

    public int Value()
    {
        if (this._focus == null)
            throw new Exception("Can't get the value when we don't have a node in focus.");
        return this._focus.Value;

    }

    public Zipper SetValue(int newValue)
    {
        BinTree? newTree = Zipper.TreeCopy(this._tree, this._focus, true, newValue, false, null, false, null);        
        if (newTree == null)
            throw new Exception("Cannot set value on a null tree.");

        return new Zipper(newTree, this._moveStack);
    }

    public Zipper SetLeft(BinTree? binTree)
    {
        BinTree? newTree = Zipper.TreeCopy(this._tree, this._focus, false, 0, true, binTree, false, null);        
        if (newTree == null)
            throw new Exception("Cannot set value on a null tree.");

        return new Zipper(newTree, this._moveStack);
    }

    public Zipper SetRight(BinTree? binTree) 
    {
        BinTree? newTree = Zipper.TreeCopy(this._tree, this._focus, false, 0, false, null, true, binTree);        
        if (newTree == null)
            throw new Exception("Cannot set value on a null tree.");

        return new Zipper(newTree, this._moveStack);        
    }

    public Zipper? Left()
    {
        if (this._focus == null || this._focus.Left == null)
            return null;
        return new Zipper(this._tree, this._moveStack + "L");
    }

    public Zipper? Right()
    {
        if (this._focus == null || this._focus.Right == null)
            return null;
        return new Zipper(this._tree, this._moveStack + "R");
    }

    public Zipper? Up()
    {
        if (this._focus == null || this._moveStack == null || this._moveStack.Length == 0)
            return null;
        string newStack = this._moveStack.Substring(0, this._moveStack.Length - 1);
        return new Zipper(this._tree, newStack);

    }

    public BinTree ToTree()
    {
        if (this._tree == null)
            throw new Exception("Cannot call ToTree when tree is null.");
        return this._tree;
    }

    public static Zipper FromTree(BinTree tree)
    {
        return new Zipper(tree, null);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is Zipper)
        {
            Zipper? other = (Zipper?)obj;
            if (other == null)
                return false;
            if ((this._tree == null && other._tree != null) ||
                (this._tree != null && other._tree == null))
                return false;

            if (this._tree != null && other._tree != null && !this._tree.Equals(other._tree))
                return false;

            if (this._moveStack != other._moveStack)
                return false;
            
            // Focus is computed from tree and movestack so should be the same.
            
            return true;

        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this._tree == null ? "" : this._tree.ToString(), this._moveStack == null ? "" : this._moveStack);
    }

    public override string ToString()
    {
        string treeValue = this._tree == null ? "null" : this._tree.ToString();
        string movesValue = this._moveStack == null ? "null" : this._moveStack;
        return $"(Tree: {treeValue}, Moves: {movesValue})";
    }

}