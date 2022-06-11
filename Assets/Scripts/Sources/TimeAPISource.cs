using System;
using UnityEngine;

public class TimeApiSource : DateTimeSource
{
    private string url = "https://www.timeapi.io/api/Time/current/zone?timeZone=Asia/Krasnoyarsk";
    public override string Url { get => url; }

    private string title = "timeapi.io";
    public override string Title { get => title; }

    public override DateTime DeserializeResponse(string jsonString)
    {
        TimeApiDto dto = JsonUtility.FromJson<TimeApiDto>(jsonString);
        return dto.ToDateTime();
    }
}
