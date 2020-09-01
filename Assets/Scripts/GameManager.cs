using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    private int money = 0;

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
}
