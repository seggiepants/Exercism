class BufferFullException(Exception):
    """Named exception for Buffer Full condition."""
    pass

class BufferEmptyException(Exception):
    """Named exception for Buffer Empty condition."""
    pass


class CircularBuffer(object):
    def __init__(self, capacity):
        """Initalize a buffer to the given capacity and 
        set it to the clear state.
        Parameters:
        * capacity: A positive integer value that says how many items the 
          buffer holds at once.
        """
        if capacity < 0 or capacity % 1 != 0:
            raise ValueError('Capactiy must be an integer value greater than zero.')
            
        self.capacity = capacity
        self.buffer = [None] * self.capacity
        self.clear()

    def read(self):
        """Read the oldest item from the buffer. And remove it from
        the queue.
        Returns:
        * Oldest item from the buffer.
        Errors:
        * Will raise a BufferEmptyException if the buffer doesn't have 
          any data.
        """
        if self.item_count <= 0:
            raise BufferEmptyException('Cannot read from empty buffer')
        
        result = self.buffer[(self.index - self.item_count) % self.capacity]
        self.item_count -= 1
        return result

    def write(self, data):
        """Write a new item to the buffer.
        Parameters:
        * data - The item to write to the buffer.
        Errors:
        * Will raise a BufferFullException if there is no free space to write
          to the buffer.
        """
        if self.item_count >= self.capacity:
            raise BufferFullException('Cannot write to full buffer, did you mean overwrite?')
        
        self.buffer[self.index] = data
        self.item_count += 1
        self.index = (self.index + 1) % self.capacity

    def overwrite(self, data):
        """Write a new item to the buffer. If the buffer is not full this is
        the same as write. However, if it is full, the oldest item will be
        overwritten with new data.
        Parameters:
        * data: The item to write/overwrite into the buffer.
        """
        if self.capacity != self.item_count:
            self.write(data)
        else:
            self.buffer[self.index] = data
            self.index = (self.index + 1) % self.capacity

    def clear(self):
        """Initialize the buffer to the default state. Doesn't actually
        erase any buffer contents"""        
        self.index = 0
        self.item_count = 0
        
