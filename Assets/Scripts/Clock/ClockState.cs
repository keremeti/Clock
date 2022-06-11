public abstract class ClockState
{
    protected Clock clock;

    public ClockState(Clock clock)
    {
        this.clock = clock;
    }

    public abstract void Start();

    public abstract void Update();
}
