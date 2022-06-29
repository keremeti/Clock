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
        clock.State = new HandsAlarmClock(clock, handsRotation);
        clock.State.Start();
        EventManager.OnSelectHand.RemoveListener(SwitchToHandsAlarmState);
    }
}
