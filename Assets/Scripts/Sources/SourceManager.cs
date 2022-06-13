using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Timers;

// управление запросами в различные источники
public class SourceManager : MonoBehaviour
{
    public List<DateTimeSource> dateTimeSources = new List<DateTimeSource>();
    private DateTimeSource selectedSource;
    private NetworkService network;
    private Timer timer;
    // время ожидания синхронизации
    private int waitingTime = 3600000;
    //CancellationTokenSource source = new CancellationTokenSource();

    private void Awake()
    {
        EventManager.OnDropdownItemSelected.AddListener(GetSourceByTitle);

        // добавление всех типов источников в список источников
        dateTimeSources.Add(new TimeApiSource());
        dateTimeSources.Add(new WorldTimeApiSource());
    }

    private void Start()
    {
        // синхронизация по таймеру
        timer = new Timer(waitingTime);
        timer.Elapsed += TimerElapse;
        timer.Start();
    }

    private void TimerElapse(object source, ElapsedEventArgs e)
    {
        StartCoroutine(GetTime((selectedSource.Url), OnCatchError, OnTimeLoaded));
    }

    // получить истояник по его title
    private void GetSourceByTitle(string title)
    {
        foreach (var dateTimeSource in dateTimeSources)
        {
            if(dateTimeSource.Title == title)
            {
                selectedSource = dateTimeSource;
            }
        }
        StartCoroutine(GetTime(selectedSource.Url, OnCatchError, OnTimeLoaded));
    }

    // получить время от источника
    private IEnumerator GetTime(
        string url,
        Action<string> errorCallback,
        Action<string> successCallback)
    {
        network = gameObject.AddComponent(typeof(NetworkService)) as NetworkService;
        yield return network.GetTimeJson(url, errorCallback, successCallback);
    }

    // обработать ошибку при получении времени
    private void OnCatchError(string error)
    {
        EventManager.SendTimeUpdated(DateTime.Now);
        Destroy(network);
    }

    // обработать полученое время
    private void OnTimeLoaded(string jsonString)
    {
        DateTime time = selectedSource.DeserializeResponse(jsonString);
        EventManager.SendTimeUpdated(time);
        Destroy(network);
    }

    private void OnDestroy()
    {
        timer.Dispose();
    }
}
