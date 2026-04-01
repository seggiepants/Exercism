//
// This is only a SKELETON file for the 'Prism' exercise. It's been provided as a
// convenience to get you started writing code faster.
//

export const findSequence = (start, prisms) => {
  let x = start.x
  let y = start.y
  let degrees = start.angle

  let intersections = []
  let done = false

  while(!done)
  {
      done = true
      let minDistance = Number.MAX_VALUE
      let hit = null
      let radians = DegreesToRadians(degrees);

      for(let prism of prisms)
      {
          let distance = Intersect(x, y, radians, prism.x, prism.y);
          if (distance < minDistance)
          {
              hit = prism;
              minDistance = distance;
              done = false
          }
      }
      if (!done && hit !== null)
      {
          x = hit.x
          y = hit.y
          degrees += hit.angle // Add the angle, don't replace.
          intersections.push(hit.id);
      }
  }

  return intersections
}

const DegreesToRadians = (degrees) => {
  return (degrees * Math.PI) / 180.0
}

const Intersect = (x0, y0, angle, x1, y1) => {
    const errorTerm = 0.01 // Very coarse but tests fail if any more precise.
    let dx = x1 - x0
    let dy = y1 - y0
    if (dx === 0.0 && dy === 0.0)
        return Number.MAX_VALUE

    let hypotenuse = Math.sqrt((dx * dx) + (dy * dy));
    let targetX = hypotenuse * Math.cos(angle) + x0;
    let targetY = hypotenuse * Math.sin(angle) + y0;

    return Math.abs(x1 - targetX) < errorTerm && Math.abs(y1 - targetY) < errorTerm ? hypotenuse : Number.MAX_VALUE
}
