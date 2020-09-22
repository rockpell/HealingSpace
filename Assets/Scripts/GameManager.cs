using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private Soul darkSoul;
    private Soul redSoul;
    private Soul blueSoul;
    private Soul whiteSoul;

    private int money = 0;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null) // only one GameManager
        {
            instance = this;
        }
        else // destroy to preventing from overlapping 
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        darkSoul = new Soul(100, 100);
        redSoul = new Soul(60, 20);
        blueSoul = new Soul(30, 5);
        whiteSoul = new Soul(10, 1);
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public int Money
    {
        get
        {
            return this.money;
        }
        set
        {
            this.money = value;
        }
    }

    public Soul GetSoul(SoulType type)
    {
        switch (type)
        {
            case SoulType.DARK:
                return darkSoul;
            case SoulType.RED:
                return redSoul;
            case SoulType.BLUE:
                return blueSoul;
            case SoulType.WHITE:
                return whiteSoul;
        }
        return null;
    }
}
