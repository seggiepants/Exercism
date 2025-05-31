static class SavingsAccount
{
    public static float InterestRate(decimal balance)
    {
        // I feel like I should be returning 0.02475f and so forth as that is an actual
        // percentage. 1% == 0.01, not 1. They should spell that out in the instructions better.
        if (balance >= 5000.0m)
            return 2.475f;
        else if (balance >= 1000.0m)
            return 1.621f;
        else if (balance >= 0.0m)
            return 0.5f;
        return 3.213f;
    }

    public static decimal Interest(decimal balance)
    {
        // Since I don't have an actual percentage I have to always 
        // divide by 100 on use.
        return balance * ((decimal)InterestRate(balance) / 100.0m);
    }

    public static decimal AnnualBalanceUpdate(decimal balance)
    {
        return balance + Interest(balance);
    }

    public static int YearsBeforeDesiredBalance(decimal balance, decimal targetBalance)
    {
        int years = 0;
        decimal total = balance;
        while ((targetBalance > 0.0m && total < targetBalance) || (targetBalance <= 0.0m && total > targetBalance))
        {
            total = AnnualBalanceUpdate(total);
            years++;
        }
        return years;
    }
}
