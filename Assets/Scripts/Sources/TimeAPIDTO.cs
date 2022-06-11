using System;

[Serializable]
public class TimeApiDto
{
    public int year;
    public int month;
    public int day;
    public int hour;
    public int minute;
    public int seconds;

    public DateTime ToDateTime()
    {
        return new DateTime(year, month, day, hour, minute, seconds);
    }
}
