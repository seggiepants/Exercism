// Exercise: Binary Search Tree
class BinarySearchTree {
  // Put your code here
  String data;
  late BinarySearchTree? left;
  late BinarySearchTree? right;
  late BinarySearchTree root;

  // Create a new Binary Search Tree.
  // @param String data: The value for this node of the search tree.
  // @param BinarySearchTree? root: Optional root of the binary search tree. This object if not passed in.
  BinarySearchTree(this.data, {BinarySearchTree? root}) {
    this.left = null;
    this.right = null;
    this.root = root ?? this;
  }

  // Insert a value into the binary search tree. Equal value will be counted as less than current.
  // @param String value: The value to insert.
  // @returns BinarySearchTree: Reference to the BinarySearchTree for the inserted node.
  BinarySearchTree insert(String value) {
    int compare = this.data.compareTo(value);

    if (compare < 0)
    {
      if (this.right != null) {
        return this.right!.insert(value);
      } else {
        this.right = BinarySearchTree(value, root: this.root);
        return this.right!;
      }
    } else {
      // (compare >= 0) 
      if (this.left != null) {
        return this.left!.insert(value);
      } else {
        this.left = BinarySearchTree(value, root: this.root);
        return this.left!;
      }
    }
  }

  // Get the strings in the search tree in sorted order.
  // @returns: Iterable<String>: List<String> with the sorted strings.
  Iterable<String> get sortedData {
    List<String> result = [];
    if (this.left != null) {
      result.addAll(this.left!.sortedData);
    }

    result.add(this.data);

    if (this.right != null) {
      result.addAll(this.right!.sortedData);
    }
    return result;
  }

}
