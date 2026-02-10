//
// This is only a SKELETON file for the 'Meetup' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

const weeks = ['first', 'second', 'third', 'fourth', 'last', 'teenth']
const days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']

export const meetup = (year, month, week, day) => {
  
  if (!weeks.includes(week))
    throw new Error(`Invalid day of week: ${week}`)
  if (!days.includes(day))
    throw new Error(`Invalid day of month: ${day}`)
    
  let dayIndex = days.indexOf(day)

  switch(week)
  {
    case 'first':
      return findDate(new Date(year, month - 1, 1), dayIndex)
    case 'second':
      return findDate(new Date(year, month - 1, 8), dayIndex)
    case 'third':
      return findDate(new Date(year, month - 1, 15), dayIndex)
    case 'fourth':
      return findDate(new Date(year, month - 1, 22), dayIndex)
    case 'last':
      return findDate(new Date(year, month, -6), dayIndex)
    case 'teenth':
      return findDate(new Date(year, month - 1, 13), dayIndex)
  }
  throw new Error('No matching date found')

};

function findDate(current, dayIndex, delta = 1)
{
  let count = 0
  while(count < 7)
  {
    if (current.getDay() === dayIndex)
      return current
    current.setDate(current.getDate() + delta)
    count++
  }
  throw new Error('No matching date found')
}