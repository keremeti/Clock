using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject hoursHand;
    [SerializeField] private GameObject minutesHand;
    [SerializeField] private GameObject secondsHand;

    [SerializeField] private HandsRotation hoursHandsRotation;
    [SerializeField] private HandsRotation minutesHandsRotation;
    [SerializeField] private HandsRotation secondsHandsRotation;

    [SerializeField] private HandsController hoursController;
    [SerializeField] private HandsController minutesController;
    [SerializeField] private HandsController secondsController;

    [SerializeField] private GameObject alarmPrefab;
    private Alarm alarm;

    [HideInInspector] public GameObject alarmCLock;
    public DateTime Time { get; private set; }
    public ClockState State { get; set; }

    [SerializeField] private Display displayClock;
    [SerializeField] private Button cancelButton;
    public Button CancelButton { get => cancelButton; }
    [SerializeField] private AlarmClockInputPanel inputFields;

    private DateTime beforeYieldTime = new();
    private DateTime afterYieldTime = new();

    private void Awake()
    {
        EventManager.OnTimeUpdated.AddListener(SynchronizeTime);
        EventManager.OnDeselectInputField.AddListener(CreateInputAlarm);
    }

    private void Start()
    {
        beforeYieldTime = DateTime.Now;
        cancelButton.onClick.AddListener(DestroyAlarm);
        State = new NormalClock(this);
        State.Start();
    }

    private void UpdateTime()
    {
        // вычесляем прошедшее время
        afterYieldTime = DateTime.Now;
        TimeSpan timeSpan = afterYieldTime - beforeYieldTime;
        if (timeSpan.TotalMilliseconds >= 1000)
        {
            Time = Time.AddMilliseconds(timeSpan.TotalMilliseconds);
            displayClock.SetTime(Time);
            beforeYieldTime = DateTime.Now;
        }
    }

    private void CreateInputAlarm()
    {
        int hours, minutes, seconds;
        inputFields.GetInputFieldTime(out hours, out minutes, out seconds);
        
        alarm = CreateAlarm(new(Time.Year, Time.Month, Time.Day,
            hours, minutes, seconds));
        alarm.StartAlarm(cancelButton);
    }

    public Alarm CreateAlarm(DateTime alarmTime)
    {
        if (alarmCLock)
        {
            Destroy(alarmCLock.gameObject);
            cancelButton.gameObject.SetActive(false);
        }
        // создание будильника из префаба и выставление времени на нем
        alarmCLock = Instantiate<GameObject>(alarmPrefab);
        alarm = alarmCLock.GetComponent<Alarm>();
        alarm.UpdateAlarm(alarmTime);
        alarm.clockTime = Time;
        return alarm;
    }

    private void LateUpdate()
    {
        State.Update();
        if (alarm is not null)
        {
            alarm.clockTime = Time;
        }

        UpdateTime();
    }

    // синхронизация времени с внешним источником
    private void SynchronizeTime(DateTime dateTime)
    {
        Time = dateTime;
    }

    // движение всех стрелок на заданное время
    public void MoveHands(DateTime time)
    {
        hoursHandsRotation.Move(time.Hour);
        minutesHandsRotation.Move(time.Minute);
        secondsHandsRotation.Move(time.Second);
    }

    // движение одной стрелки без участия времени с часов
    public void MoveHands(HandsRotation handsRotation)
    {
         handsRotation.Move();
    }

    public DateTime GetHandsTime()
    {
        DateTime handsTime = new(Time.Year, Time.Month, Time.Day,
            (int)hoursController.AlarmTime,
            (int)minutesController.AlarmTime,
            (int)secondsController.AlarmTime);
        return handsTime;
    }

    public void DestroyAlarm()
    {
        Destroy(alarmCLock.gameObject);
        cancelButton.gameObject.SetActive(false);
    }
}
