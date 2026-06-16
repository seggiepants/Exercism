// Platform as a Service I/O example -- flaky
package paasio

import (
	"io"
	"sync"
)

// Define readCounter and writeCounter types here.
type readCounter struct {
	readBytes int64
	readOps   int
	reader    io.Reader
	mutex     *sync.RWMutex
}

type writeCounter struct {
	writeBytes int64
	writeOps   int
	writer     io.Writer
	mutex      *sync.RWMutex
}

// For the return of the function NewReadWriteCounter, you must also define a type that satisfies the ReadWriteCounter interface.
type readWriteCounter struct {
	reader readCounter
	writer writeCounter
}

// Create a new WriteCounter struct.
// @param writer: The stream to use for writing
// @returns: pointer to a new WriteCounter struct
func NewWriteCounter(writer io.Writer) WriteCounter {
	counter := writeCounter{
		writer:     writer,
		writeBytes: 0,
		writeOps:   0,
		mutex:      new(sync.RWMutex),
	}
	return &counter
}

// Create a new ReadCounter struct.
// @param reader: The stream to use for reading
// @returns: pointer to a new ReadCounter struct
func NewReadCounter(reader io.Reader) ReadCounter {
	counter := readCounter{
		reader:    reader,
		readBytes: 0,
		readOps:   0,
		mutex:     new(sync.RWMutex),
	}
	return &counter
}

// Create a new ReadWriteCounter struct.
// @param readwriter: The stream to use for both reading and writing
// @returns: pointer to a new ReadWriteCounter struct
func NewReadWriteCounter(readwriter io.ReadWriter) ReadWriteCounter {
	counter := readWriteCounter{
		reader: readCounter{
			readBytes: 0,
			readOps:   0,
			reader:    readwriter,
			mutex:     new(sync.RWMutex),
		},
		writer: writeCounter{
			writeBytes: 0,
			writeOps:   0,
			writer:     readwriter,
			mutex:      new(sync.RWMutex),
		},
	}
	return &counter
}

// Read an array of bytes from the input stream
// @param p: Array of bytes to fill.
// @returns: bytes read (int64) and error/nil
func (rc *readCounter) Read(p []byte) (int, error) {
	rc.mutex.Lock()
	defer rc.mutex.Unlock()
	bytes, err := rc.reader.Read(p)
	if err != nil {
		return 0, err
	}
	rc.readOps++
	rc.readBytes += int64(bytes)
	return bytes, err
}

// Get the number of bytes read and read operations
// @returns: bytes read (int64) and read operations (int)
func (rc *readCounter) ReadCount() (int64, int) {
	rc.mutex.RLock()
	defer rc.mutex.RUnlock()
	return rc.readBytes, rc.readOps
}

// Write an array of bytes to the output stream
// @param p: Array of bytes to write.
// @returns: bytes written (int64) and error/nil
func (wc *writeCounter) Write(p []byte) (int, error) {
	wc.mutex.Lock()
	defer wc.mutex.Unlock()
	bytes, err := wc.writer.Write(p)
	if err != nil {
		return 0, err
	}
	wc.writeOps++
	wc.writeBytes += int64(bytes)
	return bytes, err
}

// Get the number of bytes written and write operations
// @returns: bytes written (int64) and write operations (int)
func (wc *writeCounter) WriteCount() (int64, int) {
	wc.mutex.RLock()
	defer wc.mutex.RUnlock()
	return wc.writeBytes, wc.writeOps
}

// Read an array of bytes from the input stream
// @param p: Array of bytes to fill.
// @returns: bytes read (int64) and error/nil
func (rw *readWriteCounter) Read(p []byte) (int, error) {
	return rw.reader.Read(p)
}

// Get the number of bytes read and read operations
// @returns: bytes read (int64) and read operations (int)
func (rw *readWriteCounter) ReadCount() (int64, int) {
	return rw.reader.ReadCount()
}

// Write an array of bytes to the output stream
// @param p: Array of bytes to write.
// @returns: bytes written (int64) and error/nil
func (rw *readWriteCounter) Write(p []byte) (int, error) {
	return rw.writer.Write(p)
}

// Get the number of bytes written and write operations
// @returns: bytes written (int64) and write operations (int)
func (rw *readWriteCounter) WriteCount() (int64, int) {
	return rw.writer.WriteCount()
}
