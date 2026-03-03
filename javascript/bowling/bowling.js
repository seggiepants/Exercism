export class Bowling {
  constructor()
  {
    this.frame = 1
    this.pins = 0
    this.ballRolls = 0
    this.remaining = 10
    this.rolls = []
  }

  roll(pins) {
    if (pins < 0)
      throw new Error('Negative roll is invalid')
    else if (pins > this.remaining)
      throw new Error('Pin count exceeds pins on the lane')

    if (this.frame > 10)
      throw new Error('Cannot roll after game is over')

    if (pins > this.remaining)
      throw new Error('Pin count exceeds pins on the lane')

    this.ballRolls++
    this.pins += pins 
    this.remaining -= pins

    if (this.frame === 10 && (this.pins === 10 || pins === 10))
      this.remaining = 10 // Reset if we threw a strike/spare since we don't advance the frame yet.

    if (this.frame <= 9 && (this.ballRolls === 2 || this.pins === 10))
    {
      this.nextFrame()
    }
    else if (this.frame === 10)
    {
      if (this.ballRolls === 3 || (this.ballRolls === 2 & this.pins < 10))
      {
        this.nextFrame()
      }      
    }
    this.rolls.push(pins)
  }

  nextFrame()
  {
    this.pins = 0;
    this.ballRolls = 0
    this.frame++
    this.remaining = 10
  }

  score() {
    let frame = 1
    let ballRolls = 0
    let pins = 0
    let points = 0
    
    if (this.frame < 11)      
      throw new Error('Score cannot be taken until the end of the game')

    for(let i = 0; i < this.rolls.length; i++)
    {
      if (frame > 10)
        break
      ballRolls++
      pins += this.rolls[i]

      if (ballRolls >= 2 || pins === 10)
      {
        let extra = 0
        if (pins === 10)
        {
          // Strike or spare
          if (ballRolls >= 1 && ballRolls <= 2)
          {
            // Strike or Spare gets one bonus
            if (i + 1 < this.rolls.length)
              extra += this.rolls[i + 1]
          }
          if (ballRolls === 1)
          {
            // Strike gets a second bonus
            if (i + 2 < this.rolls.length)
              extra += this.rolls[i + 2]
          }

        }
        points = points + pins + extra
        ballRolls = 0
        pins = 0
        frame++

      }
    }
    return points
  }
}
