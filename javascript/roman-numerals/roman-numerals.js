//
// This is only a SKELETON file for the 'Roman Numerals' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const OneToTen = (num) => {
  switch(num)
  {
    case 1:
    case 2:
    case 3:
      return "I".repeat(num)
    case 4:
      return "IV"
    case 5:
      return 'V'
    case 6:
    case 7:
    case 8:
      return "V" + "I".repeat(num % 5)
    case 9:
      return "IX"
    case 10:
      return "X"
  }
  return ""
}

const OneToFifty = (num) => {
    let ones = num % 10
    let tens = (num - ones) / 10
    switch(tens)
    {
      case 0:
        return OneToTen(ones)
      case 1:
      case 2:
      case 3:
        return 'X'.repeat(tens) + OneToTen(ones)
      case 4:
        return "XL" + OneToTen(ones)
      case 5:
        return "L"
    }
    return ""
}

const OneToFiveHundred = (num) => {
  const lookup = ["", "L", "C", "CL", "CC", "CCL", "CCC", "CCCL", "CD", "CDL", "D"]
  const fifties = (num - (num % 50)) / 50
  switch (fifties)
  {
    case 0:
      return OneToFifty(num)
    case 1:
      if (num >= 90)
        return "XC" + OneToFifty(num - 90)
      else
        return OneToFifty(num - (num % 50)) + OneToFifty(num - 50)
    default:
      return lookup[fifties]+ OneToFifty(num - (50 * fifties))
  }
}

const OneToOneThousand = (num) => {
  const remainder = num % 100
  const hundreds = (num - remainder) / 100
  switch (hundreds)
  {
    case 0:
    case 1:
    case 2:
    case 3:
    case 4:
      return OneToFiveHundred(num)
    case 5:
    case 6:
    case 7:
    case 8:
      return "D" + "C".repeat(hundreds - 5) + OneToFiveHundred(num - (hundreds * 100))
    case 9:
      return "CM" + OneToFiveHundred(num - 900)
    case 10:
      return "M"
  }
  return ""
}

const OneToThreeThousandNineHundredNinteyNine = (num) => 
{
  const remainder = num % 1000
  const thousands = (num - remainder) / 1000
  switch (thousands)
  {
    case 0:
      return OneToOneThousand(num)
    case 1:
    case 2:
    case 3:
      return "M".repeat(thousands) + OneToOneThousand(remainder)
  }
  return ""
}


export const toRoman = (num) => {
  if (num > 0 && num <= 3999) 
  {
    return OneToThreeThousandNineHundredNinteyNine(num)    
  }
  throw new Error(`${num} is out of Range, only 1-3999 accepted.`)
};
