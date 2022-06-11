using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    private Clock clock;
    private DateTime alarmTime;
    private TextMeshPro alarmClockTMP; // цифровое отображение будильника
    private AudioSource audioClip;
    private new ParticleSystem particleSystem;

    //public DateTime // взять время счасов

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        alarmClockTMP = GetComponent<TextMeshPro>();
        audioClip = GetComponent<AudioSource>();
    }

    // запуск будильника
    public void StartAlarm(Clock clock)
    {
        this.clock = clock;
        clock.cancelButton.gameObject.SetActive(true);
        StartCoroutine(StartAlarm());
    }

    // обновление времени на незаведенном будильнике
    public void UpdateAlarm(DateTime alarmTime)
    {
        this.alarmTime = alarmTime;
        alarmClockTMP.text = this.alarmTime.ToString("HH:mm:ss");
    }

    // запустить отсчет до звонка будильника
    private IEnumerator StartAlarm()
    {
        TimeSpan timeSpan = new();
        timeSpan = alarmTime - clock.Time;
        if (timeSpan.TotalMilliseconds < 0)
        {
            alarmTime = alarmTime.AddDays(1);
        }
        do
        {
            yield return new WaitForSeconds(1);
            timeSpan = alarmTime - clock.Time;
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
        clock.cancelButton.gameObject.SetActive(false);
    }
}
