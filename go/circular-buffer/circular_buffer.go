// Circular Buffer.
package circularbuffer

import "errors"

// Implement a circular buffer of bytes supporting both overflow-checked writes
// and unconditional, possibly overwriting, writes.
//
// We chose the provided API so that Buffer implements io.ByteReader
// and io.ByteWriter and can be used (size permitting) as a drop in
// replacement for anything using that interface.

// Circular Buffer struct.
type Buffer struct {
	data     []byte
	write    int
	used     int
	capacity int
}

// Initialize a new circular buffer struct.
// @param size: How many bytes the buffer can hold.
// @returns: Pointer to new Circular buffer.
func NewBuffer(size int) *Buffer {
	return &Buffer{data: make([]byte, size),
		write:    0,
		used:     0,
		capacity: size,
	}
}

// Read a byte from the circular buffer.
// @returns: next byte from the buffer or error if no data
// @raises: Error if there are no bytes to read in the buffer.
func (b *Buffer) ReadByte() (byte, error) {
	if b.used == 0 {
		return 0, errors.New("No data in buffer.")
	}
	read := (b.write + b.capacity - b.used) % b.capacity
	value := b.data[read]
	b.used--
	return value, nil
}

// Write a byte to the circular buffer.
// @param c: The byte to write
// @returns: nil on success or error if full.
// @raises: Error if no more space in the buffer.
func (b *Buffer) WriteByte(c byte) error {
	if b.capacity == b.used {
		return errors.New("No space in buffer.")
	}
	b.data[b.write] = c
	b.write = (b.write + 1) % b.capacity
	b.used++
	return nil
}

// Write a byte to the buffer, overwriting a cell if full
// @param c: The byte to write
func (b *Buffer) Overwrite(c byte) {
	b.data[b.write] = c
	b.write = (b.write + 1) % b.capacity
	b.used++
	if b.used > b.capacity {
		b.used = b.capacity
	}
}

// Reset a circular buffer to a know empty start state
func (b *Buffer) Reset() {
	b.write = 0
	b.used = 0
}
