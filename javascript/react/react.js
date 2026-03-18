//
// This is only a SKELETON file for the 'React' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

function removeItem(array, itemToRemove) {
    const index = array.indexOf(itemToRemove);

    if (index !== -1) {
        array = array.slice(0, index)
        .concat(array.slice(index + 1));
    }

	return array;
}

export class InputCell {
  constructor(value) {
    this.value = value
    this.subscribers = []
  }

  addCallback(cb)
  {
    if (!this.subscribers.includes(cb))
      this.subscribers.push(cb)
  }

  removeCallback(cb)
  {
    if (this.subscribers.includes(cb))
      this.subscribers = removeItem(this.subscribers, cb)
  }

  setValue(value) {
    let oldValue = this.value
    this.value = value
    if (oldValue !== value)
    {
      for(const subscriber of this.subscribers)
        subscriber.notify(this)
    }
  }

  notify()
  {
    // do nothing
  }
}

export class ComputeCell {
  constructor(inputCells, fn) {
    this.inputCells = inputCells
    this.subscribers = []
    this.fn = fn
    for(const cell of this.inputCells)
    {
      cell.addCallback(this)
    }
    this.oldValue = this.value
  }

  get value()
  {
    return this.fn(this.inputCells)
  }

  addCallback(cb) {
    if (!this.subscribers.includes(cb))
      this.subscribers.push(cb);
  }

  removeCallback(cb) {
    if (this.subscribers.includes(cb))
      this.subscribers = removeItem(this.subscribers, cb)
  }

  notify()
  {
    let newValue = this.value
    if (newValue !== this.oldValue)
    {
      this.oldValue = newValue
      for(const subscriber of this.subscribers)
        subscriber.notify(this)
    }
  }
}

export class CallbackCell {
  constructor(fn) {
    this.fn = fn
    this.values = []
    this.value = null
    this.subscribers = []
  }

  addCallback(cb) {
    if (!this.subscribers.includes(cb))
      this.subscribers.push(cb);
  }

  removeCallback(cb) {
    if (this.subscribers.includes(cb))
      this.subscribers = removeItem(this.subscribers, cb)
  }
  
  notify(cell)
  {
    this.value = this.fn(cell)
    this.values.push(this.value)
    for(const subscriber of this.subscribers)
      subscriber.notify(this)
  }
}
