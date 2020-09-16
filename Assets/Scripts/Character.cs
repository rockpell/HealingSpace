using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject originCube = null;
    [SerializeField] private Transform[] positions = null;
    private string nickName = null;
    private int level = 0;
    private int exp = 0;
    private int amour = 0;
    private int hp = 100;
    private int darkSoul = 0;
    private int soulBuket = 1;
    //private SoulCube soulCube = null;
    private List<SoulCube> soulCubeList = null;
    // need skill

    // Start is called before the first frame update
    void Start()
    {
        soulCubeList = new List<SoulCube>();
        createSoulCube();
        createSoulCube();
        foreach (SoulCube cube in soulCubeList)
        {
            cube.PickSoul();
        }
        AutoPlay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void createSoulCube()
    {
        GameObject tempSoulCube = null;
        int index = -1;
        if (soulCubeList != null)
        {
            index = soulCubeList.Count;
            tempSoulCube = Instantiate(originCube, positions[index].position, Quaternion.identity);
            soulCubeList.Add(tempSoulCube.GetComponent<SoulCube>());
        }
    }

    public void AutoPlay()
    {
        if (soulCubeList.Count > 0)
        {
            foreach (SoulCube cube in soulCubeList)
            {
                StartCoroutine(cube.LoopCreateSoul());
            }
        }
    }

    public string NickName {
        get {
            return this.nickName;
        }
        set {
            this.nickName = value;
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

    public int Level {
        get {
            return this.level;
        }
        set {
            this.level = value;
        }
    }

    public int Amour {
        get {
            return this.amour;
        }

        set {
            this.amour = value;
        }
    }

    public int Hp {
        get {
            return this.hp;
        }
        set {
            this.hp = value;
        }
    }

    public int DarkSoul {
        get {
            return this.darkSoul;
        }
        set {
            this.darkSoul = value;
        }
    }

    public int SoulBuket {
        get {
            return this.soulBuket;
        }
        set {
            this.soulBuket = value;
        }
    }
}
