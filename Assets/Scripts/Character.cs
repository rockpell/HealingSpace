using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private GameObject originCube = null;
    private string nickName = null;
    private int level = 0;
    private int exp = 0;
    private int amour = 0;
    private int hp = 100;
    private int darkSoul = 0;
    private int soulBuket = 1;
    private bool isArrival = true;

    private Vector3 targetPos = Vector3.zero;
    private Vector3 direction = Vector3.zero;
    private Vector3 default_direction = Vector3.zero;
    private float velocity = 0;
    private float default_velocity = 0.1f;
    private float accelaration = 0.1f;

    private List<SoulCube> soulCubeList = null;
    // need skill

    // Start is called before the first frame update
    void Start()
    {
        soulCubeList = new List<SoulCube>();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();
        createSoulCube();

        foreach (SoulCube cube in soulCubeList)
        {
            cube.PickSoul();
        }
        AutoPlay();

        default_direction.x = Random.Range(-1f, 1f);
        default_direction.y = Random.Range(-1f, 1f);
        default_direction.z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TouchEvent();
            if (isArrival)
            {
                targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = transform.position.z;
                isArrival = false;
            }
        }
        float distance = Vector3.Distance(targetPos, this.transform.position);
        if (distance < 1.5f)
            isArrival = true;

        if (!isArrival)
            MoveToTarget();
    }

    private void createSoulCube()
    {
        GameObject tempSoulCube = null;
        int index = -1;
        if (soulCubeList != null && soulCubeList.Count < 8)
        {
            index = soulCubeList.Count;
            tempSoulCube = Instantiate(originCube, GetPosition(index), Quaternion.identity, this.transform); // make object by prefab
            soulCubeList.Add(tempSoulCube.GetComponent<SoulCube>());
            tempSoulCube.GetComponent<SoulCube>().ActiveCube(false);
        }
    }
    
    private Vector3 GetPosition(int n)
    {
        Vector3 result = Vector3.zero;
        Vector3 thisPos= transform.position;
        float r = 1.0f;
        float pie = n * Mathf.PI / 4;
        result = new Vector3(thisPos.x + r * Mathf.Cos(pie), thisPos.y + r * Mathf.Sin(pie), thisPos.z);
        return result;
    }

    public void AutoPlay()
    {
        if (soulCubeList.Count > 0)
        {
            foreach (SoulCube cube in soulCubeList)
            {
                cube.AutoPlay();
                
            }
        }
    }

    public void TouchEvent()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D colider = Physics2D.OverlapPoint(touchPos);
        if (colider)
        {
            if (colider == this.GetComponent<Collider2D>()) // when push the character, cubes appear.
            {
                StartCoroutine(DisplaySoulCubes(soulCubeList));
            }
            if (colider.gameObject.GetComponent<SoulCube>()) // when push the cube, get stone.
            {
                SoulCube cube = colider.gameObject.GetComponent<SoulCube>();
                cube.RefineSoul();
                cube.PickSoul();
                cube.AutoPlay();
            }
        }

    }

    public IEnumerator DisplaySoulCubes(List<SoulCube> soulCubes)
    {
        foreach (SoulCube cube in soulCubes)
        {
            if (!(cube.IsActiveCube()))
                cube.ActiveCube(true);
            else
                cube.ActiveCube(false);
            yield return new WaitForSeconds(0.1f);
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

    public void DragToMove()
    {
    }

    public void MoveToTarget()
    {
        // Player의 위치와 이 객체의 위치를 빼고 단위 벡터화 한다.
        direction = (targetPos - transform.position).normalized;
        // 초가 아닌 한 프레임으로 가속도 계산하여 속도 증가
        velocity = (velocity + accelaration * Time.deltaTime);
        // 해당 방향으로 무빙
        this.transform.position = new Vector3(transform.position.x + (direction.x * velocity),
                                               transform.position.y + (direction.y * velocity),
                                                  transform.position.z);
    }
}
