using System;
public class Soul
{
    private int maxSoulCount = 100;
    private int frequency = 100;

    public Soul(int maxSoulCount, int frequency)
    {
        this.maxSoulCount = maxSoulCount;
        this.frequency = frequency;
    }

    public int MaxSoulCount
    {
        get { return maxSoulCount; }
    }

    public int Frequency
    {
        get { return frequency; }
        set { this.frequency = value; }
    }
}
