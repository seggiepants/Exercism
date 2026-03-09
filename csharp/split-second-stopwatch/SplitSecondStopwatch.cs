public enum StopwatchState
{
    Ready,
    Running,
    Stopped
}

public class SplitSecondStopwatch(TimeProvider time)
{    
    // Need to save previous total when we stop 
    private TimeSpan _lapElapsed = new TimeSpan(0);
    // When did we start the current section of the lap from most recent start.
    private long _lapStart = 0L;
    // Keep track of how long the previous laps took.
    private List<TimeSpan> _laps = new List<TimeSpan>();
    // Mutable stopwatch state (would rather not add a setter if I can do this).
    private StopwatchState _state = StopwatchState.Ready;

    public StopwatchState State { get
        {
            return this._state;
        } 
    }

    public TimeSpan CurrentLap 
    { 
        get
        {             
            // previously stored time plus current timespan if running.
            return this._state != StopwatchState.Running ? this._lapElapsed : this._lapElapsed + time.GetElapsedTime(this._lapStart);
        } 
    }

    public TimeSpan Total 
    { 
        get
        {
            // previously stored time plus current timespan if running.
            TimeSpan current = this._state != StopwatchState.Running ? this._lapElapsed : this._lapElapsed + time.GetElapsedTime(this._lapStart);
            // Zero if no laps otherwise the sum of the lap timespans.
            TimeSpan previous = this._laps.Count == 0 ? new TimeSpan(0) : this._laps.Aggregate((TimeSpan a, TimeSpan b) => a + b);
            return previous + current;
        }
    }

    public IReadOnlyCollection<TimeSpan> PreviousLaps {
        get
        {
            return _laps;
        }
    }

    public void Start()
    {
        if (this._state == StopwatchState.Running)
            throw new System.InvalidOperationException("cannot start an already running stopwatch");
        this._state = StopwatchState.Running;
        this._lapStart = time.GetTimestamp();
    }

    public void Stop()
    {
        if (this._state != StopwatchState.Running)
            throw new InvalidOperationException("cannot stop a stopwatch that is not running");
        
        // Save the time already ran
        this._lapElapsed += time.GetElapsedTime(this._lapStart); 
        this._lapStart = 0L;       
        this._state = StopwatchState.Stopped;
    }

    public void Reset()
    {
        if (this._state != StopwatchState.Stopped)
            throw new InvalidOperationException("cannot reset a stopwatch that is not stopped");
        this._lapElapsed = new TimeSpan(0);
        this._lapStart = 0L;
        this._laps.Clear();
        this._state = StopwatchState.Ready;
    }

    public void Lap()
    {
        if (this._state != StopwatchState.Running)
            throw new System.InvalidOperationException("cannot lap a stopwatch that is not running");
        this._laps.Add(this._lapElapsed + time.GetElapsedTime(this._lapStart));
        this._lapElapsed = new TimeSpan(0);
        this._lapStart = time.GetTimestamp();
    }
}
