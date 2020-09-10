using System;
public class Soul
{
    private int maxSoulCount = 100;
    private int frequency = 100;
    private int price = 100;

    public Soul(int maxSoulCount, int frequency, int price)
    {
        this.maxSoulCount = maxSoulCount;
        this.frequency = frequency;
        this.price = price;
    }

    public int MaxSoulCount
    {
        get { return maxSoulCount; }
    }

    public int Frequency
    {
        get { return frequency; }
    }

    public int Price
    {
        get { return price; }
    }
}
