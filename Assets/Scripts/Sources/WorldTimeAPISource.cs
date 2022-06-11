using System;
using UnityEngine;

public class WorldTimeApiSource : DateTimeSource
{
    private string url = "http://worldtimeapi.org/api/timezone/Asia/Krasnoyarsk";
    public override string Url { get => url; }

    private string title = "worldtimeapi.org";
    public override string Title { get => title; }

    public override DateTime DeserializeResponse(string jsonString)
    {
        WorldTimeApiDto DTO = JsonUtility.FromJson<WorldTimeApiDto>(jsonString);
        return DTO.ToDateTime();
    }
}
