package ledger

import (
	"errors"
	"fmt"
	"sort"
	"strings"
	"time"
)

// Ledger Printer Refactor Log
// * Change output buffer variable from s to buffer and made it a string builder for performance
// * header by langage now in a switch statement
// * header just a plain string. Don't need dynamic padding since it is constant
// * header goes in localeInfo lookup
// * Throw an error if the entries to process is nil
// * Mindbending Garbage sort replaced with call to sort.SliceStable()
// * Channels get a named struct with reasonable variable names.
// * Forget that, Channels and their entry struct removed, this is dumb to queue them all back up again to maintain sort. Wasting time and memory
// * Use the copy function to copy entries to another array for sorting.
// * Number formatting is a function now that handles the fussy bits.
// * Parsing the entry's date is now in it's own function that returns the year, month and day as number like I want.
// * Locales have their own version of the number format function with most of the variables filled in for that locale
// * Locales have their own date format function too.
// * Got rid of redundant/duplicative locale specific code
// * Saw a community solution do better with the built in date parsing & formatting with the special zero date thing I forgot about. I swiped that code and deleted my custom stuff.

type Entry struct {
	Date        string // "Y-m-d"
	Description string
	Change      int // in cents
}

type LocalInfo struct {
	decimal           string
	thousands         string
	useParens         bool
	spaceBeforeSymbol bool
	dateFormat        string
	amountFormat      func(change int, currencySymbol string) string
	header            string
}

const errWriteStringBuilder string = "Error writing a line to the buffer."
const errNoEntries string = "Error, no entries to process."

func FormatAmount(change int, currencySymbol string, thousands string, decimalPoint string, useParens bool) string {
	isNegative := change < 0

	if change < 0 {
		change *= -1
	}
	cents := change % 100
	dollars := (change - cents) / 100
	var ret string
	// Batch up the numbers into thousands
	if dollars == 0 {
		ret = "0"
	}
	for dollars > 0 {
		batch := dollars % 1000
		dollars = (dollars - batch) / 1000
		if dollars != 0 {
			ret = fmt.Sprintf("%s%03d", thousands, batch) + ret
		} else {
			ret = fmt.Sprintf("%d", batch) + ret
		}
	}

	// Add the currency symbol and decimal point.
	if isNegative && !useParens {
		ret = "-" + ret
	}
	ret = currencySymbol + ret + fmt.Sprintf("%s%02d", decimalPoint, cents)

	if isNegative && useParens {
		ret = "(" + ret + ")"
	} else {
		ret += " "
	}
	return ret
}

func FormatAmount_en_US(change int, currencySymbol string) string {
	return FormatAmount(change, currencySymbol, ",", ".", true)
}

func FormatAmount_nl_NL(change int, currencySymbol string) string {
	return FormatAmount(change, currencySymbol+" ", ".", ",", false)
}

func FormatLedger(currency string, locale string, entries []Entry) (string, error) {
	CurrencyToSymbol := map[string]string{
		"EUR": "€",
		"USD": "$",
	}
	LocaleLookup := map[string]LocalInfo{
		"en-US": {
			amountFormat: FormatAmount_en_US,
			dateFormat:   "01/02/2006",
			header:       "Date       | Description               | Change       \n",
		},
		"nl-NL": {
			amountFormat: FormatAmount_nl_NL,
			dateFormat:   "02-01-2006",
			header:       "Datum      | Omschrijving              | Verandering  \n",
		},
	}

	if entries == nil {
		return "", errors.New(errNoEntries)
	}
	sortedEntries := make([]Entry, len(entries))
	copy(sortedEntries, entries)
	sort.SliceStable(sortedEntries, func(i, j int) bool {
		if sortedEntries[i].Date != sortedEntries[j].Date {
			return sortedEntries[i].Date < sortedEntries[j].Date
		}
		if sortedEntries[i].Description != sortedEntries[j].Description {
			return sortedEntries[i].Description < sortedEntries[j].Description
		}
		return sortedEntries[i].Change < sortedEntries[j].Change
	})

	var buffer strings.Builder
	localeInfo, ok := LocaleLookup[locale]
	if !ok {
		return "", errors.New("Unsupported locale")
	}
	buffer.WriteString(localeInfo.header)

	for _, entry := range sortedEntries {
		entryDate, err := time.Parse("2006-01-02", entry.Date)
		if err != nil {
			return "", errors.New("Invalid date")
		}

		date := entryDate.Format(localeInfo.dateFormat)

		description := entry.Description
		if len(description) > 25 {
			description = description[:22] + "..."
		}

		currencySymbol, ok := CurrencyToSymbol[currency]
		if !ok {
			return "", errors.New("Unsupported currency")
		}

		amount := localeInfo.amountFormat(entry.Change, currencySymbol)

		buffer.WriteString(fmt.Sprintf("%-10s | %-25s | %13s\n", date, description, amount))
	}

	return buffer.String(), nil
}
