public class Reactor
{
    public InputCell CreateInputCell(int value)
    {
        return new InputCell(value);
    }

    public ComputeCell CreateComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        return new ComputeCell(producers, compute);
    }
}

public abstract class Cell
{
    public virtual int Value { get; set; }
    public EventHandler<int>? Changed;
}

public class InputCell : Cell
{
    private int _value;
    
    public InputCell(int value)
    {
        _value = value;
    }
    protected virtual void OnChanged(int arg)
    {  
        if (_value != arg)
        {
            _value = arg;
            Changed?.Invoke(this, _value);
        }
    }

    public override int Value { 
        get
        {            
            return _value;
        } 
        
        set
        {           
            OnChanged(value);
        } 
    }

}

public class ComputeCell : Cell
{
    Cell[] producers;
    Func<int[], int> compute;

    private int GetValue()
    {
        return compute((from key in producers
                            select key.Value).ToArray<int>());
    }

    int _oldValue = 0;

    private void UpdateValue(object? sender,int arg)
    {
        int newValue = GetValue();
        if (newValue != _oldValue)
        {
            _oldValue = newValue;
            Changed?.Invoke(this, newValue);
        }
        return;
    }

    public ComputeCell(IEnumerable<Cell> producers, Func<int[], int> compute)
    {
        this.compute = compute;
        this.producers = producers.ToArray<Cell>();
        _oldValue = GetValue();
        foreach(Cell producer in producers)
        {
            producer.Changed += UpdateValue;

        }
    }

    public override int Value
    {
        get
        {
            return GetValue();
        }

        set
        {
            throw new InvalidOperationException();
        }
    }    
}