using System;

[Serializable]
public class WorldTimeApiDto
{
    public string datetime;

    private char[] delimiterChars = { '-', 'T', ':', '.', '+'};
    private int[] numbers;

    public DateTime ToDateTime()
    {
        numbers = Array.ConvertAll(datetime.Split(delimiterChars),
            int.Parse);
        return new DateTime(numbers[0], numbers[1], numbers[2], numbers[3],
            numbers[4], numbers[5]);
    }
}
