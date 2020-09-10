using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private GameObject confirmMenu;
    [SerializeField] private Button apply;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowWindow(bool value)
    {
        confirmMenu.SetActive(value);
    }

    public void PushBuy()
    {
        // buy items
        // apply butten must be activated
        apply.gameObject.SetActive(true);
        // when push the buy button, that item increases one by one.
    }

    public void PushApply()
    {
        // need to think what we need
        // when the number of items is zero, it has to be unactivated
        // when push the apply button, that item decreases on by one.
    }

    public void PushCancel()
    {
        confirmMenu.SetActive(false);
    }
}
