using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] stones = null;
    [SerializeField] private float xMin;
    [SerializeField] private float xMax;
    [SerializeField] private float yMin;
    [SerializeField] private float yMax;
    float time = 0;
    float maxTime = 1;


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
        Instantiate(stones[i], spawn, Quaternion.identity, this.transform.parent);
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
}
