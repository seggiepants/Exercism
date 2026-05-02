package birdwatcher

// TotalBirdCount return the total bird count by summing
// the individual day's counts.
func TotalBirdCount(birdsPerDay []int) int {
	total := 0
	for index := 0; index < len(birdsPerDay); index++ {
		total += birdsPerDay[index]
	}
	return total
}

// BirdsInWeek returns the total bird count by summing
// only the items belonging to the given week.
func BirdsInWeek(birdsPerDay []int, week int) int {
	total := 0
	for index := (week - 1) * 7; index < week*7; index++ {
		total += birdsPerDay[index]
	}
	return total
}

// FixBirdCountLog returns the bird counts after correcting
// the bird counts for alternate days.
func FixBirdCountLog(birdsPerDay []int) []int {
	for index := 0; index < len(birdsPerDay); index += 2 {
		birdsPerDay[index] += 1
	}
	return birdsPerDay
}
