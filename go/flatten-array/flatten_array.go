// Flatten array exercise.
package flattenarray

import (
	"reflect"
)

// Flatten a nested array.
// The any/reflection stuff is what made it hard. Still didn't
// think I handled everything.
// @param nested: value or array that may have arrays inside it.
// @returns: Array with values at a single level.
func Flatten(nested any) []any {
	var ret []any = []any{}
	var value reflect.Value = reflect.ValueOf(nested)

	if value.Kind() == reflect.Invalid {
		return ret
	}

	if value.Kind() != reflect.Slice && value.Kind() != reflect.Array {
		ret = append(ret, nested)
		return ret
	}

	for i := 0; i < value.Len(); i++ {
		var itemValue reflect.Value = value.Index(i)
		if !itemValue.IsNil() {
			switch {
			case itemValue.Kind() == reflect.Bool:
				ret = append(ret, itemValue.Bool())
			case itemValue.Kind() == reflect.Interface:
				ret = append(ret, Flatten(itemValue.Interface())...)
			case itemValue.Kind() == reflect.Int ||
				itemValue.Kind() == reflect.Int16 ||
				itemValue.Kind() == reflect.Int32 ||
				itemValue.Kind() == reflect.Int64 ||
				itemValue.Kind() == reflect.Int8 ||
				itemValue.Kind() == reflect.Uint8 ||
				itemValue.Kind() == reflect.Uint16 ||
				itemValue.Kind() == reflect.Uint32 ||
				itemValue.Kind() == reflect.Uint64:
				ret = append(ret, itemValue.Int())
			case itemValue.Kind() == reflect.Float32 ||
				itemValue.Kind() == reflect.Float64:
				ret = append(ret, itemValue.Float())
			case itemValue.Kind() == reflect.String:
				ret = append(ret, itemValue.String)
			case itemValue.Kind() == reflect.Slice ||
				itemValue.Kind() == reflect.Array:
				itemArray := itemValue.Slice(0, itemValue.Len())
				ret = append(ret, Flatten(itemArray)...)
			}
		}
	}
	return ret
}
