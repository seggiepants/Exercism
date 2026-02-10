//
// This is only a SKELETON file for the 'Clock' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export class Clock {
  constructor(hours, minutes = 0) {
    this.hours = hours
    this.minutes = minutes
    this.adjustTime()
  }

  toString() {
    return String(this.hours).padStart(2, '0') + ':' + String(this.minutes).padStart(2, '0')
  }

  plus(minutes) {
    this.minutes += minutes
    this.adjustTime()
    return this
  }

  minus(minutes) {
    this.minutes -= minutes
    this.adjustTime()
    return this
  }

  adjustTime() 
  {
    while (this.minutes >= 60)
    {
      this.minutes -= 60
      this.hours += 1
    }
    
    while (this.minutes < 0)
    {
      this.minutes += 60
      this.hours -= 1
    }

    while (this.hours >= 24)
    {
      this.hours -= 24
    }

    while (this.hours < 0)
    {
      this.hours += 24
    }
  }

  equals(clock) {
    return this.hours === clock.hours && this.minutes === clock.minutes
  }
}
