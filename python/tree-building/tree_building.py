"""
Rebuild a tree structure from a list of parent/child ids
"""

class Record:
    """
    Record class holds data that would have been retrieved from the data store with
    record and its parent relationship.
    """
    def __init__(self, record_id, parent_id):
        """
        Initialize the Record object with the given record and parent IDs
        :param record_id: id to set this record to.
        :param parent_id: id of this records parent (0 if root)
        """
        self.record_id = record_id
        self.parent_id = parent_id


class Node:
    """
    Tree Node class
    """
    def __init__(self, node_id):
        """
        Create a new tree node giving it the passed in id.
        Children will be initialized to an empty list.
        :param node_id: Value to set this node's node_id to
        """
        self.node_id = node_id
        self.children = []

def BuildTree(records):
    """
    Reconstruct a tree of nodes from the given records.
    :param records: A list of record objects to reconstruct the tree with.
    :returns: root of the reconstructed tree or None if there are no nodes.
    :raises: Value error if there are improper parent/child relationships, missing
    records/records not in sequential order or similar data that doesn't meet expectations.
    """
    record_ids = [record.record_id for record in records]
    if records:
        min_id = min(record_ids)
        max_id = max(record_ids)
        if max_id != len(records) - 1:
            raise ValueError('Record id is invalid or out of order.')
        if min_id != 0:
            raise ValueError('Record id is invalid or out of order.')
    trees = []
    for record in records:    
        if record.record_id == 0 and record.parent_id != 0:
            raise ValueError('Node parent_id should be smaller than its record_id.') 
        if record.record_id < record.parent_id:
            raise ValueError('Node parent_id should be smaller than its record_id.')
        if record.record_id == record.parent_id and record.record_id != 0:
            raise ValueError('Only root should have equal record and parent id.') 
        trees.append(Node(record.record_id))
    
    for tree in trees:
        child_ids = [record.record_id for record in records if record.parent_id == tree.node_id and record.record_id != tree.node_id]
        children = [node for node in trees if node.node_id in child_ids]
        children.sort(key=lambda x: x.node_id)
        tree.children.extend(children)

    roots = [tree for tree in trees if tree.node_id == 0] #trees[0]
    if len(roots) == 0:
        return None
    return roots[0]
