//
// This is only a SKELETON file for the 'Satellite' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

/**
 * Reconstruct a binary tree with the information from the given pre-order and 
 * in-order traversals.
 * @param {Array} preorder - tree as array of values in pre-order traversal order
 * @param {Array} inorder - tree as array of values in in-order traversal order
 * @returns {Object} - tree node object with values "value" a string, "left", and "right". Left and right are 
 * empty objects for leaf nodes ({}), or another tree node object
 */
export const treeFromTraversals = (preorder, inorder) => {

  if (preorder.length !== inorder.length)
    throw new Error('traversals must have the same length')

  if ((new Set(preorder).size !== preorder.length) ||
      (new Set(inorder).size !== inorder.length))
    throw new Error('traversals must contain unique items')
  
  if ([...preorder].sort().join('') !== [...inorder].sort().join(''))
    throw new Error('traversals must have the same elements')
  
  if (preorder.length === 0)
    return {}

  return traverse([...preorder], inorder, 0, preorder.length - 1)
};

function traverse(preorder, inorder, start, stop)
{
  let root = preorder.shift();
  if (typeof root === 'undefined')
    return {}
  let rootIndex = inorder.findIndex((value, index) => value === root && index >= start && index <= stop)
  let left = {}
  if (rootIndex - start > 0)
  {
    left = traverse(preorder, inorder, start, rootIndex - 1)
  }
  let right = {}
  if (stop - rootIndex > 0)
  {
    right = traverse(preorder, inorder, rootIndex + 1, stop)
  }

  return { value: root, left: left, right: right}
}
