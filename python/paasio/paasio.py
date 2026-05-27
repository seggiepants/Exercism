""" 
Platform as a Service exercise
"""
import io

class MeteredFile(io.BufferedRandom):
    """
    Metered File 
    Implement using a subclassing model.
    """

    def __init__(self, *args, **kwargs):
        """
        Initialize the class variables to beginning state
        :param args: passed to base class
        :param kwargs: passed to base class
        """
        super().__init__(args, kwargs)
        self.bytes_read = 0
        self.bytes_write = 0
        self.ops_read = 0
        self.ops_write = 0

    def __enter__(self):
        """
        Enter a block where the object will be opened and used
        :returns: Reference to this object
        """
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):
        """
        Done using this object clean up and shut down.
        :param exc_type: Exception Type
        :param exc_val: Exception Value
        :param exc_tb: Exception Traceback
        :returns: value from calling the base class' __exit__"""
        return super().__exit__(exc_type, exc_val, exc_tb)

    def __iter__(self):
        """
        Prepare to iterate over this object
        """
        super().seek(0)
        return self

    def __next__(self):
        """
        Return the next line from the base file. 
        :raises: StopIteration exception when we reach end of file/receive empty string from readline call
        """
        line = super().readline()
        if not line:
            raise StopIteration()
        self.bytes_read += len(line)
        self.ops_read += 1
        return line

    def read(self, size=-1):
        """
        Read from the file 
        :param size: maximum size to read (the whole file is size = -1)
        :returns: The bytes read from the file.
        """
        ret = super().read(size)
        self.bytes_read += len(ret)
        self.ops_read += 1
        return ret

    @property
    def read_bytes(self):
        """
        How many bytes have been read
        :returns: Count of bytes read
        """
        return self.bytes_read

    @property
    def read_ops(self):
        """
        How many read operations
        :returns: How many read operations (not equal to read_bytes unless we only read one byte at a time)
        """
        return self.ops_read

    def write(self, b):
        """
        Write bytes to the file
        :param b: The bytes to write to the file.
        :returns: count of bytes written to the file.
        """
        ret = super().write(b)
        self.bytes_write += ret
        self.ops_write += 1
        return ret

    @property
    def write_bytes(self):
        """
        How many bytes have been written
        :returns: Count of bytes written to file
        """
        return self.bytes_write

    @property
    def write_ops(self):
        """
        How many write operations have been done
        :returns: How many write operations we have done (only equal to write_bytes if we only write one byte at a time)
        """
        return self.ops_write


class MeteredSocket:
    """
    Metered Socket
    Implement using a delegation model.
    """

    def __init__(self, socket):
        """
        Initialize the class saving the base socket object
        :param socket: Base socket object to delegate operations to.
        """
        self.socket = socket
        self.bytes_received = 0
        self.bytes_sent = 0
        self.ops_received = 0
        self.ops_sent = 0

    def __enter__(self):
        """
        Setup the class for use
        :returns: pointer to this object
        """
        return self

    def __exit__(self, exc_type, exc_val, exc_tb):        
        """
        Cleanup the class we are done using it
        :param exc_type: execution type
        :param exc_val: exception value
        :param exc_tb: exception trace back
        :return: status from socket__exit__()
        """
        return self.socket.__exit__(exc_type, exc_val, exc_tb)
        

    def recv(self, bufsize, flags=0):
        """
        Recive bytes from the socket
        :param bufsize: size of the buffer - maximum we can receive
        :param flags: recieve flags for the socket
        :returns: """
        ret = self.socket.recv(bufsize, flags)    
        self.bytes_received += len(ret)
        self.ops_received += 1
        return ret

    @property
    def recv_bytes(self):
        """
        How many bytes we have recieved so far
        :returns: number of bytes received
        """
        return self.bytes_received

    @property
    def recv_ops(self):
        """
        How many recive calls we have processed
        :returns: number of receive operations
        """
        return self.ops_received

    def send(self, data, flags=0):
        """
        Send data through the socket
        :param data: the data to send
        :param flags: flags for the socket
        :returns: data received from the socket
        """
        ret = self.socket.send(data, flags)
        self.bytes_sent += ret
        self.ops_sent += 1
        return ret

    @property
    def send_bytes(self):
        """
        How many bytes we have sent
        :returns: bytes sent
        """
        return self.bytes_sent

    @property
    def send_ops(self):
        """
        How many send operations we have processed
        :returns: read operations total
        """
        return self.ops_sent
