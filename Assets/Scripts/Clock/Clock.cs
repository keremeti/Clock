using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private GameObject hoursHand;
    [SerializeField] private GameObject minutesHand;
    [SerializeField] private GameObject secondsHand;

    private HandsRotation hoursHandsRotation;
    private HandsRotation minutesHandsRotation;
    private HandsRotation secondsHandsRotation;

    private HandsController hoursController;
    private HandsController minutesController;
    private HandsController secondsController;

    [SerializeField] private GameObject alarmPrefab;
    private Alarm alarm;

    [HideInInspector] public GameObject alarmCLock;
    private DateTime time = new();
    public DateTime Time => time;
    public ClockState state;

    public Display displayClock;
    public Button cancelButton;
    public AlarmClockInputPanel inputFields;

    private void Awake()
    {
        hoursHandsRotation = hoursHand.GetComponent<HandsRotation>();
        minutesHandsRotation = minutesHand.GetComponent<HandsRotation>();
        secondsHandsRotation = secondsHand.GetComponent<HandsRotation>();
        hoursController = hoursHand.GetComponent<HandsController>();
        minutesController = minutesHand.GetComponent<HandsController>();
        secondsController = secondsHand.GetComponent<HandsController>();

        EventManager.OnTimeUpdated.AddListener(SynchronizeTime);
        EventManager.OnDeselectInputField.AddListener(CreateInputAlarm);
    }

    private void Start()
    {
        StartCoroutine(StartClock());
        cancelButton.onClick.AddListener(DestroyAlarm);
        state = new NormalClock(this);
        state.Start();
    }

    // сами часы
    private IEnumerator StartClock()
    {
        DateTime beforeYieldTime = new();
        DateTime afterYieldTime = new();
        TimeSpan timeSpan = new();
        while (true)
        {
            beforeYieldTime = DateTime.Now;
            yield return new WaitForSeconds(1);
            afterYieldTime = DateTime.Now;
            // так как WaitForSeconds(1) не гарантирует абсолюбной точности,
            // то мы вычесляем прошедшее время
            timeSpan = afterYieldTime - beforeYieldTime;
            time = time.AddMilliseconds(timeSpan.TotalMilliseconds);
            displayClock.SetTime(time);
        }
    }

    private void CreateInputAlarm()
    {
        int hours, minutes, seconds;
        inputFields.GetInputFieldTime(out hours, out minutes, out seconds);
        
        alarm = CreateAlarm(new(time.Year, time.Month, time.Day,
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
        alarm.clockTime = time;
        return alarm;
    }

    private void LateUpdate()
    {
        state.Update();
        if (alarm is not null)
        {
            alarm.clockTime = time;
        }
    }

    // синхронизация времени с внешним источником
    private void SynchronizeTime(DateTime dateTime)
    {
        time = dateTime;
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
        DateTime handsTime = new(time.Year, time.Month, time.Day,
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
