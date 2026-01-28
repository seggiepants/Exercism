//
// This is only a SKELETON file for the 'Line Up' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const format = (customerName, ticketNumber) => {
  let postFix = "th"
  let tens = ticketNumber % 10
  let hundreds = ticketNumber % 100
  if (tens === 1 && hundreds !== 11)
    postFix = "st"
  else if (tens === 2 && hundreds !== 12)
    postFix = "nd"
  else if (tens === 3 && hundreds !== 13)
    postFix = "rd"

  return `${customerName}, you are the ${ticketNumber}${postFix} customer we serve today. Thank you!`
};
