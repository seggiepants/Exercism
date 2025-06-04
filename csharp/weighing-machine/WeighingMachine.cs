using System.Dynamic;
using System.Globalization;

// I looked in the specification document. It never said the desired type for Weight, etc. I don't like not finding out
// that it should have been a double all-along after I already chose float.
// There should have been a discussion about number formatting too. I had to go down a rabbit hole for that.
class WeighingMachine
{
    // TODO: define the 'Precision' property
    public int Precision { get; }

    // TODO: define the 'Weight' property
    private double _weight;
    public double Weight
    {
        get
        {
            return _weight;
        }

        set
        {
            if (value < 0.0f)
                throw new ArgumentOutOfRangeException();
            _weight = value;
        }
    }

    // TODO: define the 'DisplayWeight' property
    public string DisplayWeight
    {
        get
        {
            NumberFormatInfo fmt = new()
            {
                NumberDecimalDigits = Precision
            };
            
            return $"{(_weight - TareAdjustment).ToString("N", fmt)} kg";
        }
    }

    // TODO: define the 'TareAdjustment' property
    public double TareAdjustment { get; set; }

    public WeighingMachine(int precision)
    {
        Precision = precision;
        Weight = 0.0d;
        TareAdjustment = 5.0d;
    }
}
