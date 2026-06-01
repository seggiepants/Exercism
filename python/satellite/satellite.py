"""
Reconstruct a binary tree from pre_order and in_order traversal of the node values
"""

def tree_from_traversals(pre_order, in_order):
    """
    Reconstruct a binary tree from a combination of preorder and inorder traversals
    :param pre_order: The preorder traversal
    :param in_order: The inorder traversal:
    :returns: Root node of the reconstructed tree made of dictionaries with keys v = value, l = left, and r = right
    :raises: ValueError if traversal lengths don't match, don't have the same items or have duplicate values
    """
    if len(pre_order) != len(in_order):
        raise ValueError('traversals must have the same length')

    if len(set(pre_order)) != len(pre_order) or len(set(in_order)) != len(in_order):
        raise ValueError('traversals must contain unique items')
    
    if sorted(pre_order) != sorted(in_order):
        raise ValueError('traversals must have the same elements')
  
    if len(pre_order) == 0:
        return {}

    return traverse([*pre_order], in_order, 0, len(pre_order) - 1)


def traverse(pre_order, in_order, start, stop):
    """
    Reconstruct the tree by taking the first item remaining in our pre-order copy and then recursively
    doing the traversals for the left and right side of those traversals as nodes under this new root
    (branch)
    :pre_order: remainder of preorder traversal
    :in_order: inorder traversal
    :start: beginning of node range
    :stop: end of node range
    :returns: Tree-node dictionary with the desired root at the root or at least root of this branch.
    """
    if len(pre_order) == 0:
        return {}
    root = pre_order.pop(0)
    root_index = [index for index, value in enumerate(in_order) if value == root and start <= index  <= stop][0]
    left = {}
    if root_index - start > 0:
        left = traverse(pre_order, in_order, start, root_index - 1)
    
    right = {}
    if stop - root_index > 0:
        right = traverse(pre_order, in_order, root_index + 1, stop)

    return { 'v': root, 'l': left, 'r': right}
