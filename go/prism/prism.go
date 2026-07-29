package prism

import "math"

type Position struct {
	x     float64
	y     float64
	angle float64
}

type Prism struct {
	id    int
	x     float64
	y     float64
	angle float64
}

// @param start: Start location and angle of the laser.
// @param prisms: Slice of Prisms (position, angle, and id).
// @returns: Slice of integers prism ids in the order they are interacted with.
func FindSequence(start Position, prisms []Prism) []int {
	const minimal float64 = 0.01
	results := make([]int, 0)

	var currentPosition Position = Position{x: start.x, y: start.y, angle: start.angle}
	var dx float64
	var dy float64
	var found bool = true
	for found {
		found = false
		var minDist float64 = math.MaxFloat64
		var minIdx = len(prisms)
		for idx, prism := range prisms {
			// considered a hit if the angle between the two points is
			// equal to the angle of travel.
			dx = prism.x - currentPosition.x
			dy = prism.y - currentPosition.y
			var dist float64 = math.Sqrt(dx*dx + dy*dy)
			var angle float64 = NormalizeDegree((math.Atan2(dy, dx) * 180.0) / math.Pi)
			var delta float64 = math.Abs(NormalizeDegree(currentPosition.angle) - angle)
			// Adjust for near zero on the 360 side.
			if delta > (360.0 - minimal) {
				delta = 360.0 - delta
			}
			// Floating point is imprecise call it equal if less than 0.01 difference.
			if dist != 0.0 && delta < minimal {
				// Only keep the nearest intersection.
				if dist < minDist {
					minDist = dist
					minIdx = idx
					found = true
				}
			}
		}
		if found {
			// Move laser to hit location and deflect by angle
			currentPosition.x = prisms[minIdx].x
			currentPosition.y = prisms[minIdx].y
			currentPosition.angle = NormalizeDegree(currentPosition.angle + prisms[minIdx].angle)

			results = append(results, prisms[minIdx].id)
		}
	}

	return results
}

// Given a degree. Move it to [0, 360).
// @param degree: The angle to normalize.
// @returns: the normalized angle.
func NormalizeDegree(angle float64) float64 {
	for angle >= 360.0 {
		angle -= 360.0
	}
	for angle < 0.0 {
		angle += 360.0
	}
	return angle
}
