<?php

// Exercise: Circular Buffer

declare(strict_types=1);

// Implementation of the Circular Buffer
class CircularBuffer
{
    private array $buffer;
    private int $position_read;
    private int $count_elements;

    // Constructor. Setup the Circular Buffer instance.
    // @param int $size: How many items the circular buffer can hold.
    public function __construct(int $size) {
        $this->buffer = array_fill(0, $size, "");
        $this->position_read = 0;
        $this->count_elements = 0;
    }

    // Read the next item from the circular buffer.
    // @returns: The next item from the circular buffer.
    // @raises BufferEmptyError: When there are no items to read.
    public function read()
    {
        if ($this->count_elements <= 0) {
            throw new BufferEmptyError();
        }
        $element = $this->buffer[$this->position_read];
        $this->position_read = ($this->position_read + 1) % count($this->buffer);
        $this->count_elements--;
        return $element;
    }

    // Write an item to the next position in the circular buffer.
    // @param $item: The item to add to the buffer
    // @raises BufferFullError: When the buffer can not hold additional elements.
    public function write($item): void
    {
        if ($this->count_elements >= count($this->buffer)) {
            throw new BufferFullError();
        }
        $position = ($this->position_read + $this->count_elements) % count($this->buffer);
        $this->buffer[$position] = $item;
        $this->count_elements++;
    }

    // Write an item to the circular buffer even if it is full. The current read item will
    // be overwritten in that case.
    // @param $item: The item to force a write of.
    public function forceWrite($item): void 
    {
        if ($this->count_elements < count($this->buffer)) {
            $this->write($item);
            return;
        }
        $this->buffer[$this->position_read] = $item;
        $this->position_read = ($this->position_read + 1) % count($this->buffer);        
    }

    // Clear the circular buffer.
    public function clear() : void 
    {
        $this->position_read = 0;
        $this->count_elements = 0;
    }
}

// Buffer full error custom exception
class BufferFullError extends Exception
{
}

// Buffer empty error custom exception
class BufferEmptyError extends Exception
{
}
