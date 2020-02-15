# Constants for object data type.
NODE, EDGE, ATTR = range(3)

class Node(object):
    """Node object. Models a node in our domain specific language."""
    def __init__(self, name, attrs):
        """Intialize the node object.
        Parameters:
        * name: The name of the node.
        * attrs: Dictionary of attributes for this node.
        """
        self.name = name
        self.attrs = attrs

    def __eq__(self, other):
        """Equality operator. 
        Parameters:
        * other: Object to compare this object against.
        Returns:
        * True if they equivalent, False if not close enough.
        """
        return self.name == other.name and self.attrs == other.attrs


class Edge(object):
    """Represents a edge between two nodes"""
    def __init__(self, src, dst, attrs):
        """Initalize the edge object
        Parameters:
        * src: The start node of the edge
        * dst: The end node of the edge.
        * attrs: Attributes for this edge, a dictionary of name, value pairs.
        """
        self.src = src
        self.dst = dst
        self.attrs = attrs

    def __eq__(self, other):
        """Equality operator.
        Parameters:
        * other: The object to compare against this object (self)
        Returns:
        * True if it is effectively the same, and false otherwise.
        """
        return (self.src == other.src and
                self.dst == other.dst and
                self.attrs == other.attrs)


class Graph(object):
    """The graph object represents a graphic and the commands
    used to generate it."""
    def __init__(self, data=None):
        """
        Initialize the Graph object. 
        Parameters:
        * data: None if not passed in which would give you an empty graphic.
        If passed in, it must be a list of tuples. Each tuple starts off with a
        data type which must be one of the constants NODE, EDGE, or ATTR. Followed
        by the parameters for that object. Node takes a name and attribute dictionary,
        while edges have a source, and destination that match node names as well as 
        a attribute dictionary. Finally attributes should have a name and value pair.
        """    
        # initalize our data storage to empty.
        self.nodes = []
        self.edges = []
        self.attrs = {}
        if data != None:
            # Create the correct object for each item in the list.
            for row in data:
                if type(row) != tuple:
                    raise TypeError('Invalid row')
                elif len(row) <= 1:
                    raise TypeError('Incomplete row detected')
                else:
                    data_type = row[0]
                    if data_type == NODE:
                        # 0 = data_type, 1 = name, 2 = attributes
                        if len(row) != 3:
                            raise ValueError('Incorrect number of parameters')
                        name = row[1]
                        attrs = row[2]
                        if attrs == None:
                            attrs = {}
                        elif type(attrs) != dict:
                            raise ValueError('Incorrect data type for attributes')
                        self.nodes.append(Node(name, attrs))
                    elif data_type == EDGE:
                        # 0 = data_type, 1 = source, 2 = destination, 3 = attributes
                        if len(row) != 4:
                            raise ValueError('Incorrect number of parameters')
                        src = row[1]
                        dest = row[2]
                        attrs = row[3]
                        if attrs == None:
                            attrs = {}
                        elif type(attrs) != dict:
                            raise ValueError('Incorrect data type for attributes')
                        self.edges.append(Edge(src, dest, attrs))
                    elif data_type == ATTR:
                        # 0 = data_type, 1 = key, 2 = value
                        if len(row) != 3:
                            raise ValueError('Incorrect number of parameters')

                        self.attrs[row[1]] = row[2]
                    else:
                        raise ValueError('Unexpected data type.')
