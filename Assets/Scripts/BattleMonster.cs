using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    private float speed = 0.1f;
    private Vector3 maxSize = new Vector3(2.5f, 2.5f, 0);
    private Vector3 minSize = new Vector3(0.5f, 0.5f, 0);
    private int count = 0;
    private BattleMode battleMode = BattleMode.NONE;

    void Start()
    {
        
    }

    void Update()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // because it is mousePos
        Collider2D colider = Physics2D.OverlapPoint(touchPos);

        if (colider && colider.gameObject.GetComponent<BattleMonster>())
        {
            BeatingEffect();
            Beating();
        }
    }
    // 한번 누를때 스케일 0.2 증가 뗄때 0.2 감소
    // 공격 당하면 스케일 0.05감소
    // 공격은 뗄떼 공격 당함
    private void Beating() // 터치 마구마구
    {
        if (Input.GetMouseButtonUp(0))
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0);
    }

    private void BeatingEffect()
    {
        if (Input.GetMouseButtonDown(0))
            this.transform.localScale += new Vector3(0.05f, 0.05f, 0);
        else if (Input.GetMouseButtonUp(0))
            this.transform.localScale -= new Vector3(0.05f, 0.05f, 0);
    }

    private void Rhythm() // 테두리가 커졌다 작아졌다
    {
        Vector3 size = teduri.transform.localScale;

        if (size.x < maxSize.x && size.y < maxSize.y)
        { 
            size += new Vector3(0.01f, 0.01f, 0) * Time.deltaTime * speed;
            teduri.transform.localScale = size;
        }
        else if (size.x > minSize.x && size.y > minSize.y)
        {
            size -= new Vector3(0.01f, 0.01f, 0) * Time.deltaTime * speed;
            teduri.transform.localScale = size;
        }
    }// need to add toggle flag

    private void AttackTiming()
    {
        Vector3 size = teduri.transform.localScale;
        float x = Mathf.Abs(size.x - this.transform.localScale.x);
        float y = Mathf.Abs(size.y - this.transform.localScale.y);

        if (x < 0.1f && y < 0.1f)
        {
            //damaged
            count++;
            if (count >= 2)
            {
                //change to slice mode
            }
        }

    }

    private void SliceAttack() // 최후의 한방!
    {

    }
}
