"""
Reactive System
"""

class InputCell:
    """
    Input cell in a reactive system
    """
    def __init__(self, initial_value):
        """
        Setup the input cell with an initial value
        :param initial_value: Value the input cell should start with
        """
        self._value = initial_value
        self.subscribers = set()
    
    def add_callback(self, callback):
        """
        Add a callback to the subscribers
        :param callback: Function to call when the value changes
        """
        self.subscribers.add(callback)
    
    def remove_callback(self, callback):
        """
        Remove a callback from the subscribers
        :param callback: The callback to remove if present
        """
        if callback in self.subscribers:
            self.subscribers.remove(callback)
    
    def get_value(self):
        """
        Return the cell's value
        :returns: the cell's value
        """
        return self._value

    def set_value(self, value):
        """
        Set the cell value to a new value
        Notifies all the callbacks.
        :param value: What to set the cell value to
        """
        old_value = self._value
        self._value = value
        if old_value != value:
            for subscriber in self.subscribers:
                subscriber(value)

    value = property(get_value, set_value)    

class ComputeCell:
    """
    A cell in the reactive system whoes value depends on the value of other cells
    """
    def __init__(self, inputs, compute_function):
        """
        Cell in the Reactive System that has values based on a computation of other values
        :param inputs: cells to use as inputs to the value computation
        :param compute_function: Function to call over the inputs to calculate the value
        """
        self.inputs = inputs
        self.subscribers = set()
        self.compute_function = compute_function
        for cell in inputs:
            cell.add_callback(self.notify)
        self.old_value = self.value

    def add_callback(self, callback):
        """
        Add a callback for this cell
        :param callback: function to call when the cell value changes.
        """
        self.subscribers.add(callback)
        
    def remove_callback(self, callback):
        """
        Remove a callback for this cell if present.
        :param callback: callback function to remove
        """
        if callback in self.subscribers:
            self.subscribers.remove(callback)

    def get_value(self):
        """
        Return the value of this cell
        :returns: the value of this cell.
        """
        return self.compute_function([cell.value for cell in self.inputs])
    
    value = property(get_value)
    
    def notify(self, value):
        """
        Notify subscriber cells when the value of the cell changes
        :param value: Not used - required by calling test cases
        """
        new_value = self.value
        if new_value != self.old_value:
            self.old_value = new_value 
            for subscriber in self.subscribers:
                subscriber(self.old_value)