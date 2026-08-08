<?php
// Binary Search Tree exercise.

declare(strict_types=1);

// Abstracts the root or node of a Binary Search Tree
class BinarySearchTree
{
    public ?BinarySearchTree $left;
    public ?BinarySearchTree $right;
    public int $data;

    // Constructor with optional data parameter. Sets data and puts left and right at null.
    // @param $data: Optional data parameter if not passed in is 0.
    public function __construct(int $data = 0) {
        $this->data = $data;
        $this->left = null;
        $this->right = null;
    }

    // Insert a value into the search tree.
    // @param $data: The data to insert.
    public function insert(int $data)
    {
        if ($data <= $this->data) {
            if ($this->left != null) {
                $this->left->insert($data);
            } else {
                $this->left = new BinarySearchTree($data);
            }
        } else if ($data > $this->data) {
            if ($this->right != null) {
                $this->right->insert($data);
            } else {
                $this->right = new BinarySearchTree($data);
            }
        }        
    }

    // Pretty print the data in the tree (was used for debugging)
    // @returns: Value in the tree in human readable format.
    public function ToString() : string {
        if ($this->left == null) {
            $strLeft = "null";
        } else {
            $strLeft = $this->left->toString();
        }

        if ($this->right == null) {
            $strRight = "null";
        } else {
            $strRight = $this->right->toString();
        }

        return "[" . strval($this->data) . ", Left: " . $strLeft . ", Right: " . $strRight . "]";
    }

    // Return an array of the data in the binary search tree in sorted order.
    // @returns: Data in the search tree as an array with values in sorted order.
    public function getSortedData(): array
    {
        $result = array();
        $result[] = $this->data;
        if ($this->left != null) {
            $result = array_merge($this->left->getSortedData(), $result);
        }
        if ($this->right != null) {
            $result = array_merge($result, $this->right->getSortedData());
        }
        return $result;
    }
}
