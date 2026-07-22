// Tournament exercise. More of an Extract/Transform/Load exercise really.
package tournament

import (
	"bufio"
	"errors"
	"fmt"
	"io"
	"maps"
	"slices"
	"strings"
)

// holds a single team's win/loss information
type ScoreTally struct {
	Team string // Team name
	MP   int    // Matches Played
	W    int    // Win
	D    int    // Draw
	L    int    // Lose
	P    int    // Points
}

// This will sort descending on score then ascending on name if a tie.
// @param first: ScoreTally for first record to compare
// @param second: ScoreTally for second record to compare
// @returns -1 if first > second, 1 if second > first and 0 if equal.
func TallyCompare(first *ScoreTally, second *ScoreTally) int {
	if first.P > second.P {
		return -1
	} else if first.P < second.P {
		return 1
	}
	// points are equal
	return strings.Compare(first.Team, second.Team)
}

// Read in score rows from the reader, and write out the result table to the writer.
// @param reader: io.Reader object to read data from
// @param writer: io.Writer to write data to
// @returns: nil on success or an error when there were problem(s)
func Tally(reader io.Reader, writer io.Writer) error {
	// The header is basically a constant.
	const HEADER string = "Team                           | MP |  W |  D |  L |  P\n"

	// Map team name to tally. Still need team name in tally when I turn it into a sorted slice for writing.
	scores := make(map[string]*ScoreTally, 0)

	// This lets me read strings instead of bytes.
	scanner := bufio.NewScanner(reader)
	for scanner.Scan() {
		// Was there a scan error?
		err := scanner.Err()
		if err != nil {
			return err
		}
		// Get the current line, trim it and skip it if blank or a comment.
		line := scanner.Text()
		line = strings.Trim(line, " \t\r\n")
		if len(line) == 0 || strings.HasPrefix(line, "# ") {
			// Ignore whitespace and comments
			continue
		}

		// If we don't have three items something is worng.
		cols := strings.Split(line, ";")
		if len(cols) != 3 {
			return errors.New("line should have three entries")
		}

		teamA := cols[0]
		teamB := cols[1]
		gameStatus := cols[2]

		// Add in new team tally's to the map if needed.
		_, exists := scores[teamA]
		if !exists {
			scores[teamA] = &ScoreTally{Team: teamA}
		}
		_, exists = scores[teamB]
		if !exists {
			scores[teamB] = &ScoreTally{Team: teamB}
		}
		// Both teams played a match
		scores[teamA].MP++
		scores[teamB].MP++

		switch gameStatus {
		case "win":
			scores[teamA].W++
			scores[teamA].P += 3

			scores[teamB].L++
		case "draw":
			scores[teamA].D++
			scores[teamA].P++

			scores[teamB].D++
			scores[teamB].P++
		case "loss":
			scores[teamA].L++

			scores[teamB].W++
			scores[teamB].P += 3
		default:
			// Error if we don't have win/loss/draw for the status.
			return errors.New("Game result is not win/loss/draw")
		}
	}
	// Done tallying just need the data in a slice now.
	rows := slices.Collect(maps.Values(scores))
	// so I can sort it.
	slices.SortFunc(rows, TallyCompare)

	// Writer lets me write strings instead of bytes.
	w := bufio.NewWriter(writer)
	w.WriteString(HEADER)
	// Write out the tally per row.
	for _, row := range rows {
		_, err := w.WriteString(fmt.Sprintf("%-30s | %2d | %2d | %2d | %2d | %2d\n", row.Team, row.MP, row.W, row.D, row.L, row.P))
		if err != nil {
			return err
		}
	}
	// Flush it or we don't get data returned. Throw an error if flush had an error.
	err := w.Flush()
	if err != nil {
		return err
	}

	// Happy path everything worked.
	return nil
}
