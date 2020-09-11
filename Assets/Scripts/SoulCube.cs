using UnityEngine;
using System.Collections;

public class SoulCube
{
    private SoulType soulType = SoulType.NONE;
    private int level = 1; // 1 ~ 20
    private int exp = 0;
    private int soulCount = 0; // 0 ~ 100
    private int maxSoulCount = 100;

    private Soul darkSoul = null;
    private Soul redSoul = null;
    private Soul blueSoul = null;
    private Soul whiteSoul = null;

    public SoulCube()
    {
        darkSoul = new Soul(100, 100);
        redSoul = new Soul(60, 0);
        blueSoul = new Soul(30, 0);
        whiteSoul = new Soul(10, 0);
    }

    public int Level
    {
        get
        {
            return this.level;
        }
        set
        {
            this.level = value;
        }
    }

    public int Exp
    {
        get
        {
            return this.exp;
        }
        set
        {
            this.exp = value;
        }
    }

    public int SoulCount
    {
        get
        {
            return this.soulCount;
        }
        set
        {
            this.soulCount = value;
        }
    }

    public bool StorageSoul(SoulType type)
    {
        if (soulType == SoulType.NONE)
        {
            soulType = type;
            maxSoulCount = GameManager.Instance.GetSoul(type).MaxSoulCount;
        }
        if (soulType == type && soulCount < maxSoulCount)
        {
            this.soulCount += 1;
            return true;
        }
        return false;
    }

    public bool RefineSoul()
    {
        if (soulCount == 100)
        {
            soulCount = 0;
            soulType = SoulType.NONE;
            // create soul stone
            return true;
        }
        return false;
    }

    // modifiy speed && ratio of soul bucket with lev && exp

    public void LevelUp()
    {
        // level up
        // if level != 20 exp >= 100 : level++ && exp = 0;
        DetermineFrequency();
    }

    public void DetermineFrequency()
    {
        if (level == 5)
            redSoul.Frequency = 1;
        else if (level == 10)
            blueSoul.Frequency = 1;
        else if (level == 15)
            whiteSoul.Frequency = 1;

        if (redSoul.Frequency != 0)
            redSoul.Frequency += 1;
        if (blueSoul.Frequency != 0)
            blueSoul.Frequency += 1;
        if (whiteSoul.Frequency != 0)
            whiteSoul.Frequency += 1;
        darkSoul.Frequency = 100 - redSoul.Frequency - blueSoul.Frequency - whiteSoul.Frequency;
    }

    // visualize legi, soul, soul bucket
}
