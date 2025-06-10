using System.Diagnostics.CodeAnalysis;

public struct CurrencyAmount
{
    const string ERR_CURRENCY_MISMATCH = "Currency amounts do not match.";
    private decimal amount;
    private string currency;

    public CurrencyAmount(decimal amount, string currency)
    {
        this.amount = amount;
        this.currency = currency;
    }

    // I don't like having waringing in my build so I added this function 
    // as requested by the compiler
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is null)
            return false;

        if (obj.GetType() != typeof(CurrencyAmount))
            return false;

        return ((CurrencyAmount)obj).amount == this.amount &&
                ((CurrencyAmount)obj).currency.Equals(this.currency);
    }

    // I don't like having waringing in my build so I added this function 
    // as requested by the compiler. I used HashCode.combine() this time.
    public override int GetHashCode()
    {
        return HashCode.Combine(this.amount, this.currency);
    }

    // TODO: implement equality operators
    public static bool operator ==(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);

        return amount1.amount == amount2.amount;
    }

    // Compiler demanded this, so did the test cases, but not the exercise description.
    public static bool operator !=(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);
        return amount1.amount != amount2.amount;
    }

    // TODO: implement comparison operators
    public static bool operator >(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);
        return amount1.amount > amount2.amount;
    }

    public static bool operator <(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);
        return amount1.amount < amount2.amount;
    }

    // TODO: implement arithmetic operators
    public static CurrencyAmount operator +(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);
        return new CurrencyAmount() { amount = amount1.amount + amount2.amount, currency = amount1.currency };
    }


    public static CurrencyAmount operator -(CurrencyAmount amount1, CurrencyAmount amount2)
    {
        if (!amount1.currency.Equals(amount2.currency))
            throw new ArgumentException(ERR_CURRENCY_MISMATCH);
        return new CurrencyAmount() { amount = amount1.amount - amount2.amount, currency = amount1.currency };
    }


    public static CurrencyAmount operator *(CurrencyAmount amount1, decimal amount2)
    {
        return new CurrencyAmount() { amount = amount1.amount * amount2, currency = amount1.currency };
    }

    public static CurrencyAmount operator *(decimal amount1, CurrencyAmount amount2)
    {
        return new CurrencyAmount() { amount = amount1 * amount2.amount, currency = amount2.currency };
    }

    public static CurrencyAmount operator /(CurrencyAmount amount1, decimal amount2)
    {
        return new CurrencyAmount() { amount = amount1.amount / amount2, currency = amount1.currency };
    }

    public static CurrencyAmount operator /(decimal amount1, CurrencyAmount amount2)
    {
        return new CurrencyAmount() { amount = amount1 / amount2.amount, currency = amount2.currency };
    }

    // TODO: implement type conversion operators
    public static explicit operator double(CurrencyAmount amount)
    {
        return (double)amount.amount;
    }

    public static implicit operator decimal(CurrencyAmount amount)
    {
        return amount.amount;
    }
}
