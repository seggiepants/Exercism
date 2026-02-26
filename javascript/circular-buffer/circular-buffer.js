//
// This is only a SKELETON file for the 'Circular Buffer' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

class CircularBuffer {
  constructor(bufferSize) {
    this._buffer = new Array(bufferSize)
    this.clear()    
  }

  write(value) {
    if (this._cells_used >= this._buffer.length)
      throw new BufferFullError()

    this._buffer[this._write] = value
    this._cells_used++
    this._write = (this._write + 1) % this._buffer.length
  }

  read() {
    if (this._cells_used <= 0)
      throw new BufferEmptyError()
    let value = this._buffer[this._read]
    this._cells_used--
    this._read = ((this._read + 1) % this._buffer.length)    
    return value
  }

  forceWrite(value) {
    if (this._cells_used < this._buffer.length)
      this.write(value)
    else 
    {
      if (this._read === this._write)
          this._read = (this._write + 1) % this._buffer.length
      this._buffer[this._write] = value
      this._write = (this._write + 1) % this._buffer.length
    }
  }

  clear() {
    this._read = 0
    this._write = 0
    this._cells_used = 0
  }
}

export default CircularBuffer;

export class BufferFullError extends Error {  
}

export class BufferEmptyError extends Error {
}
