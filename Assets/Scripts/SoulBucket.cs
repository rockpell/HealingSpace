using UnityEngine;
using System.Collections;

public class SoulBucket
{
    private SoulType soulType = SoulType.NONE;
    private int level = 0; // 0 ~ 20
    private int exp = 0;
    private int soulCount = 0; // 0 ~ 100
    private int maxSoulCount = 100;

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

    // modifiy velocity && frequency of soul bucket with lev && exp

    // visualize legi, soul, soul bucket
}
