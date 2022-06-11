using System;

// состояние часов при прикосновении к любой стрелке
// для создания будильника
public class HandsAlarmClock : ClockState
{
    private HandsRotation selectingHand;
    private DateTime alarmTime;
    private Alarm alarm;

    public HandsAlarmClock(Clock clock, HandsRotation handsRotation) : base(clock)
    {
        selectingHand = handsRotation;
    }

    public override void Start()
    {
        alarm = clock.CreateAlarm(alarmTime);
        EventManager.OnDeselectHand.AddListener(StartAlarm);
    }

    // движение конкретной стрелки и обновление времени будильника
    public override void Update()
    {
        clock.MoveHands(selectingHand);
        alarmTime = clock.GetHandsTime();
        alarm.UpdateAlarm(alarmTime);
    }

    // запуск будильника и изменения состояния часов
    private void StartAlarm()
    {
        alarm.StartAlarm(clock);
        EventManager.OnDeselectHand.RemoveListener(StartAlarm);
        clock.state = new NormalClock(clock);
    }
}
