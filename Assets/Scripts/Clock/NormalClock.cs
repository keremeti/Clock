// обычное движение стрелок
public class NormalClock : ClockState
{
    public NormalClock(Clock clock) : base(clock) { }

    public override void Start()
    {
        EventManager.OnSelectHand.AddListener(SwitchToHandsAlarmState);
    }

    public override void Update()
    {
        clock.MoveHands(clock.Time);
    }

    private void SwitchToHandsAlarmState(HandsRotation handsRotation)
    {
        clock.state = new HandsAlarmClock(clock, handsRotation);
        clock.state.Start();
        EventManager.OnSelectHand.RemoveListener(SwitchToHandsAlarmState);
    }
}
