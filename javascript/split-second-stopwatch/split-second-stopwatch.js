

export class SplitSecondStopwatch {
    
  constructor() {
    this._state = 'stopped'
    this.reset()
  }  

  get state() {
    return this._state
  }

  timeToString(seconds)
  {    
    let s = seconds % 60;
    let m = ((seconds - (seconds % 60)) / 60) % 60
    let h = (seconds - (m * 60) - s) / (60 * 60)
    return String(h).padStart(2, '0') + ':' + String(m).padStart(2, '0') + ':' + String(s).padStart(2, '0')
  }

  get currentLap() {
    return this.timeToString(this._lap)
  }

  get total() {
    return this.timeToString(this._previousLaps.reduce((accumulator, value) => accumulator + value, 0) + this._lap)
  }

  get previousLaps() {
    return this._previousLaps.map((seconds) => this.timeToString(seconds))
  }

  start() {
    if (this._state === 'running')
      throw new Error('cannot start an already running stopwatch')
    this._state = 'running'
  }

  stop() {
    if (this._state !== 'running')
      throw new Error('cannot stop a stopwatch that is not running')
    this._state = 'stopped'
  }

  lap() {
    if (this._state !== 'running')
      throw new Error('cannot lap a stopwatch that is not running')
    this._previousLaps.push(this._lap)
    this._lap = 0
  }

  reset() {
    if (this._state !== 'stopped')
      throw new Error('cannot reset a stopwatch that is not stopped')
    this._lap = 0
    this._previousLaps = []
    this._state = 'ready'
  }

  advanceTime(duration) {
    if (this._state === 'running')
    {      
      let parts = duration.split(':')
      this._lap += ((Number(parts[0]) * 3600) + (Number(parts[1]) * 60) + Number(parts[2]))
    }
  }
}
