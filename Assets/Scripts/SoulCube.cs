﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulCube : MonoBehaviour
{
    [SerializeField] private Sprite[] sprite = null;
    private SoulType soulType = SoulType.NONE;
    private int level = 1; // 1 ~ 20
    private int soulCount = 0; // 0 ~ 100
    private int maxSoulCount = 5;

    private Soul darkSoul = null;
    private Soul redSoul = null;
    private Soul blueSoul = null;
    private Soul whiteSoul = null;

    private bool isAuto = false;

    private static int[] needDarkStone = { 1, 5, 10, 15, 20, 30, 40, 50, 60, 90, 120, 150, 180, 210, 250, 300, 350, 400, 500};
    private static int[] needRedStone = { 5, 10, 15, 20, 25, 30, 40, 50, 60, 70, 80, 100, 120, 150, 200 };
    private static int[] needBlueStone = { 3, 6, 9, 12, 15, 30, 45, 60, 90, 150 };
    private static int[] needWhiteStone = { 1, 3, 5, 10, 15 };

    private static float[] speedDarkSoul = { 6, 5.7f, 5.4f, 5.1f, 4.8f, 4.5f, 4.2f, 3.9f, 3.6f, 3.3f, 3, 2.7f, 2.4f, 2.1f, 1.8f, 1.5f, 1.2f, 0.9f, 0.6f, 0.3f };
    private static float[] speedRedSoul = { 10, 9.5f, 9, 8.5f, 8, 7.5f, 7, 6.5f, 6, 5.5f, 5, 4.5f, 4, 3.5f, 3, 1 };
    private static float[] speedBlueSoul = { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 6 };
    private static float[] speedWhiteSoul = { 60, 55, 50, 45, 40, 30 };

    private static float[] ratioDarkSoul = { 100, 100, 100, 100, 99, 98, 97, 96, 95, 93, 91, 89, 87, 85, 82, 79, 76, 73, 70, 70 };
    private static float[] ratioRedSoul = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 15 };
    private static float[] ratioBlueSoul = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 10 };
    private static float[] ratioWhiteSoul = { 1, 2, 3, 4, 5, 5 };

    private Dictionary<SoulType, int> levUpStoneCounter = null;

    void Start()
    {
        darkSoul = new Soul(100, 100);
        redSoul = new Soul(60, 0);
        blueSoul = new Soul(30, 0);
        whiteSoul = new Soul(10, 0);
        levUpStoneCounter = new Dictionary<SoulType, int>();
        InitLevUpStoneCounter();
    }

    private void Update()
    {
        
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

    private void InitLevUpStoneCounter()
    {
        levUpStoneCounter.Clear();
        levUpStoneCounter.Add(SoulType.DARK, 0);
        levUpStoneCounter.Add(SoulType.RED, 0);
        levUpStoneCounter.Add(SoulType.BLUE, 0);
        levUpStoneCounter.Add(SoulType.WHITE, 0);
    }

    public bool StorageSoul()
    {
        if (soulCount < maxSoulCount)
        {
            this.soulCount += 1;
            return true;
        }
        return false;
    }

    public bool RefineSoul()
    {
        if (soulCount >= maxSoulCount)
        {
            soulCount = 0;
            levUpStoneCounter[soulType] += 1;
            GameManager.Instance.StoneCounter[soulType] += 1;
            soulType = SoulType.NONE;
            isAuto = false;
            LevelUp();
            return true;
        }
        return false;
    }

    // modifiy speed && ratio of soul bucket with lev && exp
    public void PickSoul()
    {
        int randValue = Random.Range(0, 100);
        int l = level - 1;

        if (randValue <= ratioDarkSoul[l])
        {
            soulType = SoulType.DARK;
            GetComponent<SpriteRenderer>().sprite = sprite[0];
        }
        else if (level >= 5 && randValue <= ratioDarkSoul[l] + ratioRedSoul[l - 4])
        {
            soulType = SoulType.RED;
            GetComponent<SpriteRenderer>().sprite = sprite[1];
        }
        else if (level >= 10 && randValue <= ratioDarkSoul[l] + ratioRedSoul[l - 4] + ratioBlueSoul[l - 9])
        {
            soulType = SoulType.BLUE;
            GetComponent<SpriteRenderer>().sprite = sprite[2];
        }
        else if (level >= 15 && randValue <= ratioDarkSoul[l] + ratioRedSoul[l - 4] + ratioBlueSoul[l - 9] + ratioWhiteSoul[l - 14])
        {
            soulType = SoulType.WHITE;
            GetComponent<SpriteRenderer>().sprite = sprite[3];
        }
        maxSoulCount = GameManager.Instance.GetSoul(soulType).MaxSoulCount;
    }

    public void AutoPlay()
    {
        if (!isAuto)
        {
            isAuto = true;
            StartCoroutine(LoopCreateSoul());
        }
    }

    private IEnumerator LoopCreateSoul()
    {
        float speed = GetSpeed(soulType, level) / 600f;

        while (soulCount < maxSoulCount)
        {
            yield return new WaitForSeconds(speed);
            StorageSoul();
        }
    }

    private float GetSpeed(SoulType soulType, int level)
    {
        if (soulType == SoulType.DARK)
            return speedDarkSoul[level - 1];
        if (soulType == SoulType.RED)
            return speedRedSoul[level - 5];
        if (soulType == SoulType.BLUE)
            return speedBlueSoul[level - 10];
        if (soulType == SoulType.WHITE)
            return speedWhiteSoul[level - 15];
        return -1;
    }


    public bool IsStoneComplete()
    {
        if (needDarkStone[level - 1] != levUpStoneCounter[SoulType.DARK])
            return false;
        if (level >= 5 && needRedStone[level - 5] != levUpStoneCounter[SoulType.RED])
            return false;
        if (level >= 10 && needBlueStone[level - 10] != levUpStoneCounter[SoulType.BLUE])
            return false;
        if (level >= 15 && needWhiteStone[level - 15] != levUpStoneCounter[SoulType.WHITE])
            return false;
        return true;
    }

    public void LevelUp()
    {
        if (level != 20 && IsStoneComplete())
        {
            Debug.Log("Level UP!!!!!!!!!!!!!!!!!!!!!!");
            level++;
            InitLevUpStoneCounter();
            DetermineFrequency();
        }
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

    public void ActiveCube(bool value)
    {
        GetComponent<SpriteRenderer>().enabled = value;
        GetComponent<BoxCollider2D>().enabled = value;
    }

    public bool IsActiveCube()
    {
        return GetComponent<SpriteRenderer>().enabled && GetComponent<BoxCollider2D>().enabled;
    }
    // visualize legi, soul, soul bucket
}
