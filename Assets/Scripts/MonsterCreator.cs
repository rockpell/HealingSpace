using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] monsters = null;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    float time = 0;
    float maxTime = 1;
    bool isMonster = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            if (!isMonster)
                RandomRespawn();
            time -= maxTime;
        }
    }

    private void RandomRespawn()
    {
        int i = StoneFrequency();
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        Vector3 spawn = new Vector3(x, y, -1);
        Instantiate(monsters[i], spawn, Quaternion.identity);
        isMonster = true;
    }

    private int StoneFrequency()
    {
        int i = Random.Range(0, 100);
        if (i < 40)
            return 0;
        if (i < 70)
            return 1;
        if (i < 90)
            return 2;
        else
            return 3;
    }

    public bool IsMonster
    {
        get { return isMonster; }
        set { isMonster = value; }
    }
}
