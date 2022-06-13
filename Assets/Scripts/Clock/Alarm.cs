using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alarm : MonoBehaviour
{
    private DateTime alarmTime;
    public DateTime clockTime;
    private TextMeshPro alarmClockTMP; // цифровое отображение будильника
    private AudioSource audioClip;
    private ParticleSystem particleSystem;
    private Button cancelButton;

    //public DateTime // взять время счасов

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        alarmClockTMP = GetComponent<TextMeshPro>();
        audioClip = GetComponent<AudioSource>();
    }

    // запуск будильника
    public void StartAlarm(Button button)
    {
        cancelButton = button;
        cancelButton.gameObject.SetActive(true);
        StartCoroutine(AlarmClock());
    }

    // обновление времени на незаведенном будильнике
    public void UpdateAlarm(DateTime alarmTime)
    {
        this.alarmTime = alarmTime;
        alarmClockTMP.text = this.alarmTime.ToString("HH:mm:ss");
    }

    // запустить отсчет до звонка будильника
    private IEnumerator AlarmClock()
    {
        TimeSpan timeSpan = new();
        timeSpan = alarmTime - clockTime;
        if (timeSpan.TotalMilliseconds < 0)
        {
            alarmTime = alarmTime.AddDays(1);
        }
        do
        {
            yield return new WaitForSeconds(1);
            timeSpan = alarmTime - clockTime;
        } while (timeSpan.TotalMilliseconds > 0);
        audioClip.Play();
        StartCoroutine(NotifyAlarm());
    }

    // оповестить, когда сработает будильник
    IEnumerator NotifyAlarm()
    {
        particleSystem.Play();
        while(Input.touchCount < 1)
        {
            Handheld.Vibrate();
            yield return new WaitForSeconds(1);
        }
        Destroy(this.gameObject);
        cancelButton.gameObject.SetActive(false);
    }
}
