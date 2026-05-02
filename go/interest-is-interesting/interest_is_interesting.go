package interest

// InterestRate returns the interest rate for the provided balance.
func InterestRate(balance float64) float32 {
	if balance >= 5000.0 {
		return 2.475
	}
	if balance >= 1000.0 {
		return 1.621
	}
	if balance >= 0 {
		return 0.5
	}
	return 3.213 // punative interest for negative amounts.
}

// Interest calculates the interest for the provided balance.
func Interest(balance float64) float64 {
	multiplier := float64(InterestRate(balance)) / 100.0

	return balance * multiplier
}

// AnnualBalanceUpdate calculates the annual balance update, taking into account the interest rate.
func AnnualBalanceUpdate(balance float64) float64 {
	return balance + Interest(balance)
}

// YearsBeforeDesiredBalance calculates the minimum number of years required to reach the desired balance.
func YearsBeforeDesiredBalance(balance, targetBalance float64) int {
	currentBalance := balance
	var years int = 0

	for currentBalance = balance; currentBalance < targetBalance; years++ {
		currentBalance = AnnualBalanceUpdate(currentBalance)
	}

	return years
}
