package sqlbuilder

import "strings"

// ColumnValue represents a single database column and its corresponding value
// to be used in SQL INSERT statements.
type ColumnValue struct {
	Column string
	Value  string
}

// BuildSelectQuery generates a valid SQL SELECT query for the specified table and columns.
// If the columns slice is empty or nil, it defaults to the wildcard "*".
func BuildSelectQuery(table string, columns []string) string {
	var builder strings.Builder

	builder.WriteString("SELECT ")
	if len(columns) == 0 {
		builder.WriteString("*")
	} else {
		for index, value := range columns {
			if index >= 1 {
				builder.WriteString(", ")
			}
			builder.WriteString(value)
		}
	}

	builder.WriteString(" FROM ")
	builder.WriteString(table)
	builder.WriteRune(';')
	return builder.String()
}

// BuildBatchInserts generates a slice of individual SQL INSERT statements for the provided rows.
// Empty rows are skipped, and values must be enclosed in single quotes.
func BuildBatchInserts(table string, rows [][]ColumnValue) []string {
	var builder strings.Builder
	var columns strings.Builder
	var values strings.Builder
	var results []string = make([]string, 0)

	for _, row := range rows {
		builder.Reset()
		columns.Reset()
		values.Reset()
		empty := true
		for _, value := range row {
			if len(value.Column) == 0 || len(value.Value) == 0 {
				continue
			}
			if !empty {
				columns.WriteString(", ")
				values.WriteString(", ")
			}
			columns.WriteString(value.Column)
			values.WriteRune('\'')
			values.WriteString(value.Value)
			values.WriteRune('\'')
			empty = false
		}
		if !empty {
			builder.WriteString("INSERT INTO ")
			builder.WriteString(table)
			builder.WriteString(" (")
			builder.WriteString(columns.String())
			builder.WriteString(") VALUES (")
			builder.WriteString(values.String())
			builder.WriteString(");")
			results = append(results, builder.String())
		}
	}

	return results
}
