// Determine the date when a gigasecond has passed from a given date.
package gigasecond

// import path for the time package from the standard library
import "time"

// For a given time return that time with a billion seconds added to it.
// @param t: The time to add to.
// @returns: Time when a gigasecond interval has passed.
func AddGigasecond(t time.Time) time.Time {
	const seconds = 1_000_000_000
	var duration time.Duration = time.Second * seconds
	return t.Add(duration)
}
