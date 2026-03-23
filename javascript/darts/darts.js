//
// This is only a SKELETON file for the 'Darts' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const score = (x, y) => {
  let distance_squared = x*x + y*y
  if (distance_squared > 100)
    return 0 // Out of bounds.
  else if (distance_squared > 25)
    return 1 // Outer
  else if (distance_squared > 1)
    return 5 // Middle
  return 10 // Bullseye
};
