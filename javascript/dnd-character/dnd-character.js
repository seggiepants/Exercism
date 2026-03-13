//
// This is only a SKELETON file for the 'D&D Character' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const abilityModifier = (value) => {
  if (value < 3)
    throw new Error('Ability scores must be at least 3')
  if (value > 18)
    throw new Error('Ability scores can be at most 18')

  return Math.floor((value - 10)/2)
};

export class Character {

  constructor()
  {
    this._strength = Character.rollAbility()
    this._dexterity = Character.rollAbility()
    this._constitution = Character.rollAbility()
    this._intelligence = Character.rollAbility()
    this._wisdom = Character.rollAbility()
    this._charisma = Character.rollAbility()
    this._hitpoints = 10 + abilityModifier(this._constitution)
    
  }
  static rollAbility() {
    let throws = [0, 0, 0, 0]
    throws.map((value, index) => throws[index] = Math.floor(Math.random() * 6) + 1)
    return throws.reduce((previous, current) => previous + current, 0) - Math.min(...throws)
  }

  get strength() {
    return this._strength
  }

  get dexterity() {
    return this._dexterity
  }

  get constitution() {
    return this._constitution
  }

  get intelligence() {
    return this._intelligence
  }

  get wisdom() {
    return this._wisdom
  }

  get charisma() {
    return this._charisma
  }

  get hitpoints() {
    return this._hitpoints
  }
}
