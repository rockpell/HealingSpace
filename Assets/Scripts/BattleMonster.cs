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
    private float speed = 1f;
    private Vector3 maxSize = new Vector3(2.5f, 2.5f, 0);
    private Vector3 minSize = new Vector3(0.5f, 0.5f, 0);
    private int count = 0;
    private BattleMode battleMode = BattleMode.NANTA;
    private bool upDown = false;
    private Vector2 downPos = Vector2.zero;
    private Vector2 upPos = Vector2.zero;
    private float sliceMin = 5000f;

    void Start()
    {
    }

    void Update()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // because it is mousePos
        Collider2D colider = Physics2D.OverlapPoint(touchPos);

        switch (battleMode)
        {
            case BattleMode.NANTA:
                if (colider && colider.gameObject.GetComponent<BattleMonster>())
                {
                    BeatingEffect();
                    Beating();
                    PouringEffect();
                }
                break;

            case BattleMode.RHYTHM:
                Rhythm();
                if (colider && colider.gameObject.GetComponent<BattleMonster>())
                {
                    AttackTiming();
                }
                break;

            case BattleMode.SLICE:
                SliceAttack();
                break;

            case BattleMode.END:
                break;
        }
        Debug.Log(battleMode);
    }
    // 한번 누를때 스케일 0.2 증가 뗄때 0.2 감소
    // 공격 당하면 스케일 0.05감소
    // 공격은 뗄떼 공격 당함
    private void Beating() // 터치 마구마구
    {
        if (Input.GetMouseButtonUp(0))
            this.transform.localScale -= new Vector3(0.1f, 0.1f, 0);
        // reduce hp

        if (this.transform.localScale.x < 1)
        {
            battleMode = BattleMode.RHYTHM;
            teduri.gameObject.SetActive(true);
        }
    }

    private void BeatingEffect()
    {
        if (Input.GetMouseButtonDown(0))
            this.transform.localScale += new Vector3(0.05f, 0.05f, 0);
        else if (Input.GetMouseButtonUp(0))
            this.transform.localScale -= new Vector3(0.05f, 0.05f, 0);
    }

    private void Rhythm() // size of teduri up and down
    {
        Vector3 size = teduri.transform.localScale;

        if (upDown && size.x < maxSize.x && size.y < maxSize.y)
        { 
            size += new Vector3(speed, speed, 0) * Time.deltaTime;
            teduri.transform.localScale = size;
            if (maxSize.x - size.x < 0.1f)
                upDown = !upDown;
        }
        else if (!upDown && size.x > minSize.x && size.y > minSize.y)
        {
            size -= new Vector3(speed, speed, 0) * Time.deltaTime;
            teduri.transform.localScale = size;
            if (size.x - minSize.x < 0.1f)
                upDown = !upDown;
        }
    }

    private void AttackTiming()
    {
        Vector3 size = teduri.transform.localScale;
        float x = Mathf.Abs(size.x - this.transform.localScale.x);
        float y = Mathf.Abs(size.y - this.transform.localScale.y);

        if (Input.GetMouseButtonUp(0))
        {
            if (x < 0.1f && y < 0.1f)
            {
                // damaged ( reduce moster hp )
                count++;
                if (count >= 2)
                {
                    battleMode = BattleMode.SLICE;
                    teduri.gameObject.SetActive(false);
                }
            }
            else
            {
                // character hp reduce
                if (count > 0)
                    count--;
            }
            Debug.Log(count);
        }
    }

    private void SliceAttack() // 최후의 한방!
    {
        //if (success)
        // battle end && scene change

        if (Input.GetMouseButtonDown(0))
            downPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
        if (Input.GetMouseButtonUp(0))
        {
            upPos = Camera.main.WorldToScreenPoint(Input.mousePosition);
            if (Vector2.Distance(downPos, upPos) > sliceMin)
            {
                battleMode = BattleMode.END;
            }
        }
    }

    private void EndMode()
    {
        // dialog console appear
        // dead monster fade out animation
        // kakao bank animation
        // show what player get
        // confirm button for change to space scene
    }

    private void PouringEffect()         // kakao bank animation
    {
        float randX = Random.Range(-0.5f, 0.5f);
        float randY = Random.Range(-0.5f, 0.5f);
        Vector3 offsetPos = new Vector3(randX, randY, -1);
        randX = Random.Range(-1f, 1f);
        randY = Random.Range(0.5f, 1.5f);
        Vector3 randomDirection = new Vector3(randX, randY, 0);
        GameObject stones = Instantiate(item, this.transform.position + offsetPos, Quaternion.identity, this.transform.parent);
        stones.GetComponent<Rigidbody2D>().AddForce(randomDirection * 150);
    }
}
