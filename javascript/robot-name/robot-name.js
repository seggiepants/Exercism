// This is only a SKELETON file for the 'Robot Name' exercise. It's been
// provided as a convenience to get your started writing code faster.

let robotNames = new Set()
const ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
const DIGITS = "0123456789"

let randomName = () => {
    const ALPHA_CHARS = ALPHABET.length
    const DIGIT_CHARS = DIGITS.length
    let retry = true
    let name = ''
    while (retry)
    {
        name = ALPHABET[Math.floor(Math.random() * ALPHA_CHARS)] + 
            ALPHABET[Math.floor(Math.random() * ALPHA_CHARS)] +
            DIGITS[Math.floor(Math.random() * DIGIT_CHARS)] + 
            DIGITS[Math.floor(Math.random() * DIGIT_CHARS)] + 
            DIGITS[Math.floor(Math.random() * DIGIT_CHARS)]
        retry = robotNames.has(name)
    }
    robotNames.add(name)
    return name
}

export class Robot {
    constructor()
    {
        this._name = ""
        this.reset()
    }

    reset()
    {
        // I think you should release a name when reset
        // but the tests say otherwise.

        //if (this.name !== "")
        //    robotNames.delete(this.name)
        
        this._name = randomName()
    }

    get name()
    {
        return this._name
    }
}



Robot.releaseNames = () => {robotNames.clear()};
