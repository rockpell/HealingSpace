using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccelerationController : MonoBehaviour
{
    [SerializeField] private Text acc = null;
    [SerializeField] private float loLim = 0.005f;
    [SerializeField] private float hiLim = 0.1f;
    [SerializeField] private int steps = 0;
    [SerializeField] private float fHight = 10.0f;
    [SerializeField] private float curAcc = 0f;
    [SerializeField] private float fLow = 0.1f;
    private float avgAcc = 0f;
    private float delta = 0f;
    private bool stateH = false;

    void Start()
    {
        avgAcc = Input.acceleration.magnitude;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        curAcc = Mathf.Lerp(curAcc, Input.acceleration.magnitude, Time.deltaTime * fHight);
        avgAcc = Mathf.Lerp(avgAcc, Input.acceleration.magnitude, Time.deltaTime * fLow);
        delta = curAcc - avgAcc;
        if (!stateH)
        {
            if (delta > hiLim)
            {
                stateH = true;
                steps++;
                acc.text = "steps: " + steps;
            }
        }
        else
        {
            if (delta < loLim)
            {
                stateH = false;
            }
        }
    }
}
