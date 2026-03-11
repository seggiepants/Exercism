using System.Reflection.Metadata.Ecma335;

public record Tree(char Value, Tree? Left, Tree? Right);

public static class Satellite
{
    /// <summary>
    /// Reconstruct a binary tree with the information from the given pre-order and 
    /// in-order traversals.
    /// </summary>
    /// <param name="preOrder">tree as array of values in pre-order traversal order</param>
    /// <param name="inOrder">tree as array of values in in-order traversal order</param>
    /// <returns>Tree? object, null if an empty tree, otherwise filled with reconstructed tree</returns>
    /// <exception cref="ArgumentException">Thrown if argments are not the same length, don't have 
    /// unique items (no repeats) or the same elements in both</exception>
    public static Tree? TreeFromTraversals(char[] preOrder, char[] inOrder)
    {
        if (preOrder.Length != inOrder.Length)
            throw new ArgumentException("traversals must have the same length");

        if ((new HashSet<char>(preOrder).Count != preOrder.Length) ||
            (new HashSet<char>(inOrder).Count != inOrder.Length))
            throw new ArgumentException("traversals must contain unique items");
  
        String preText = String.Join("", preOrder.Select(c => c.ToString()).OrderBy(key => key));
        String inText = String.Join("", inOrder.Select(c => c.ToString()).OrderBy(key => key));
        if (inText != preText)
            throw new ArgumentException("traversals must have the same elements");

        if (preText.Length == 0)
            return null;

        Queue<char> preorderQueue = new(preOrder);
        return traverse(preorderQueue, inOrder, 0, inOrder.Length - 1);
    }

    private static Tree? traverse(Queue<char> preorder, char[] inorder, int start, int stop)
    {
        if (!preorder.TryDequeue(out char root))
            return null;

        int rootIndex = inorder
                .Index()
                .Where(pair => pair.Item == root && pair.Index >= start && pair.Index <= stop)
                .Select(pair => pair.Index).First();

        Tree? left = null;
        Tree? right = null;        
  
        if (rootIndex - start > 0)
        {
            left = traverse(preorder, inorder, start, rootIndex - 1);
        }

        if (stop - rootIndex > 0)
        {
            right = traverse(preorder, inorder, rootIndex + 1, stop);
        }

        return new Tree(root, left, right);
    }
}
