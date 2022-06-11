using System;

// абстрактный класс для источников
public abstract class DateTimeSource
{
    public abstract string Title
    {
        get;
    }

    public abstract string Url
    {
        get;
    }

    public abstract DateTime DeserializeResponse(string jsonString);
}
