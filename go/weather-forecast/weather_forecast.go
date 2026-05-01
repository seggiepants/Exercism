/*
	Package weather contains a function to write out the weather forecast.

It also has variables to save the most recently used condition and location.
*/
package weather

var (
	// CurrentCondition is the current weather condition (cloudy, sunny, etc.).
	CurrentCondition string
	// CurrentLocation is the location we are forecasting the weather at.
	CurrentLocation string
)

/*
	Forecast will return a string with the current weather forecast

It will also save the passed in city and condition to CurrentLocation and
CurrentCondition variables.
*/
func Forecast(city, condition string) string {
	CurrentLocation, CurrentCondition = city, condition
	return CurrentLocation + " - current weather condition: " + CurrentCondition
}
