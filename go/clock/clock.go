package clock

import "fmt"

// Define the Clock type here.
type Clock struct {
	hours   int
	minutes int
}

func New(h, m int) Clock {
	return Clock{hours: h, minutes: m}.adjust()
}

func (c Clock) adjust() Clock {
	for c.minutes < 0 {
		c.minutes += 60
		c.hours--
	}
	for c.minutes >= 60 {
		c.minutes -= 60
		c.hours++
	}

	for c.hours < 0 {
		c.hours += 24
	}

	for c.hours >= 24 {
		c.hours -= 24
	}
	return c
}

func (c Clock) Add(m int) Clock {
	c.minutes += m
	return c.adjust()
}

func (c Clock) Subtract(m int) Clock {
	c.minutes -= m
	return c.adjust()
}

func (c Clock) String() string {
	return fmt.Sprintf("%02d:%02d", c.hours, c.minutes)
}
