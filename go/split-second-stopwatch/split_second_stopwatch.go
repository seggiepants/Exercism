// Split Second Stopwatch - Simulate a stopwatch that strangely enough is only good to the second. Really a Finite State Machine exercise
package splitsecondstopwatch

import (
	"errors"
	"fmt"
)

const (
	START int = iota
	STOPPED
	RUNNING
	READY
	LAP
)

var StateToString map[int]string = map[int]string{
	START:   "start",
	STOPPED: "stopped",
	RUNNING: "running",
	READY:   "ready",
	LAP:     "lap",
}

type SplitSecondStopwatch struct {
	CurrentState   int
	CurrentSeconds int64
	Laps           []int64
}

// Start the stopwatch - must be in ready or stopped state.
// @raises: Error if not in ready or stopped state.
func (sss *SplitSecondStopwatch) Start() error {
	if sss.CurrentState != READY && sss.CurrentState != STOPPED {
		return errors.New("cannot start an already running stopwatch")
	}
	if sss.CurrentState != STOPPED {
		sss.CurrentState = STOPPED
		err := sss.Reset()
		if err != nil {
			return err
		}
	}
	sss.CurrentState = RUNNING
	return nil
}

// Stop a currently running stopwatch
// @raises: Error if not currently running.
func (sss *SplitSecondStopwatch) Stop() error {
	if sss.CurrentState != RUNNING {
		return errors.New("cannot stop a stopwatch that is not running")
	}
	sss.CurrentState = STOPPED
	return nil
}

// Reset a stopped stopwatch also clearing any laps
// @raises: Error if not in the stopped state.
func (sss *SplitSecondStopwatch) Reset() error {
	if sss.CurrentState != STOPPED {
		return errors.New("cannot reset a stopwatch that is not stopped")
	}
	sss.Laps = sss.Laps[:0]
	sss.CurrentSeconds = 0.0
	sss.CurrentState = READY
	return nil
}

func (sss *SplitSecondStopwatch) Lap() error {
	if sss.CurrentState != RUNNING {
		return errors.New("cannot lap a stopwatch that is not running")
	}
	sss.Laps = append(sss.Laps, sss.CurrentSeconds)
	sss.CurrentSeconds = 0.0
	return nil
}

func (sss *SplitSecondStopwatch) AdvanceTime(by string) {
	if sss.CurrentState == RUNNING {
		var hours, minutes, seconds int64
		fmt.Sscanf(by, "%02d:%02d:%02d", &hours, &minutes, &seconds)
		sss.CurrentSeconds += hours*60*60 + minutes*60 + seconds
	}
}

func (sss *SplitSecondStopwatch) State() string {
	value, ok := StateToString[sss.CurrentState]
	if ok {
		return value
	}
	return "Unknown"
}

func (sss *SplitSecondStopwatch) CurrentLap() string {
	return SecondsToString(sss.CurrentSeconds)
}

func (sss *SplitSecondStopwatch) Total() string {
	var totalSeconds int64 = 0.0
	for _, lapSeconds := range sss.Laps {
		totalSeconds += lapSeconds
	}
	totalSeconds += sss.CurrentSeconds
	return SecondsToString(totalSeconds)
}

func (sss *SplitSecondStopwatch) PreviousLaps() []string {
	results := make([]string, len(sss.Laps))
	for index, lapSeconds := range sss.Laps {
		results[index] = SecondsToString(lapSeconds)
	}
	return results
}

func NewSplitSecondStopwatch() *SplitSecondStopwatch {
	return &SplitSecondStopwatch{
		CurrentState:   READY,
		CurrentSeconds: 0.0,
		Laps:           make([]int64, 0),
	}
}

func SecondsToString(seconds int64) string {
	const HourInSeconds = 60.0 * 60.0
	const MinuteInSeconds = 60.0
	var intSeconds int64 = seconds
	var hours int64 = 0
	if intSeconds >= HourInSeconds {
		remainder := intSeconds % HourInSeconds
		hours = (intSeconds - remainder) / HourInSeconds
		intSeconds -= hours * HourInSeconds
	}

	minutes := 0
	if intSeconds >= MinuteInSeconds {
		remainder := intSeconds % MinuteInSeconds
		minutes = int(intSeconds-remainder) / MinuteInSeconds
		intSeconds = remainder
	}

	return fmt.Sprintf("%02d:%02d:%02d", hours, minutes, intSeconds)

}
