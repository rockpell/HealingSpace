using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMonster : MonoBehaviour
{
    [SerializeField] private Transform teduri = null;
    [SerializeField] private Slider monsterHpBar = null;
    [SerializeField] private Slider characterHpBar = null;
    [SerializeField] private Button domang = null;
    [SerializeField] private GameObject item = null; // getting item when take the monster down.

    private float monsterHp = 100;
    private float characterHp = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
