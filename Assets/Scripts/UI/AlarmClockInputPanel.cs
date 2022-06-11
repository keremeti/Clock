using TMPro;
using UnityEngine;

public class AlarmClockInputPanel : MonoBehaviour
{
    [SerializeField] private GameObject hoursInputField;
    [SerializeField] private GameObject minutesInputField;
    [SerializeField] private GameObject secondsInputField;

    private AlarmInput hoursAlarm;
    private AlarmInput minutesAlarm;
    private AlarmInput secondsAlarm;

    private void Awake()
    {
        hoursAlarm = hoursInputField.GetComponent<AlarmInput>();
        minutesAlarm = minutesInputField.GetComponent<AlarmInput>();
        secondsAlarm = secondsInputField.GetComponent<AlarmInput>();
    }

    private void Start()
    {
        hoursAlarm.GetComponent<TMP_InputField>().onDeselect.AddListener(SendInput);
        minutesAlarm.GetComponent<TMP_InputField>().onDeselect.AddListener(SendInput);
        secondsAlarm.GetComponent<TMP_InputField>().onDeselect.AddListener(SendInput);
    }

    public void GetInputFieldTime(out int hours, out int minutes, out int seconds)
    {
        hours = hoursAlarm.Number;
        minutes = minutesAlarm.Number;
        seconds = secondsAlarm.Number;
    }

    public void SendInput(string _string)
    {
        EventManager.SendDeselectInputField();
    }
}
