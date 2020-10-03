using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController = null;
    [SerializeField] UIManager uiManager = null;
    private Character nowCharacter = null; // present selected character instance
    private Character preCharacter = null;
    private Vector3 clickPoint = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        TouchEvent();
    }

    public void TouchEvent()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D colider = Physics2D.OverlapPoint(touchPos);

        if (colider)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (nowCharacter)
                {
                    nowCharacter.IsDrag = false;
                }
                if (preCharacter && preCharacter != nowCharacter) // off the previous character when click the new character (방금전에 클릭한 캐릭터가 있으면서 현재 선택한 캐릭터가 다를 경우 전의 캐릭터를 꺼준다)
                {
                    preCharacter.DisplaySoulCubes(false);
                    preCharacter.IsClick = false;
                    uiManager.RefreshStatusBar(nowCharacter);
                }
                if (colider.gameObject.GetComponent<Character>() &&
                    Vector3.Distance(clickPoint, Input.mousePosition) < 0.25f) // when push the character, cubes appear.
                {
                    nowCharacter.DisplaySoulCubes(!nowCharacter.IsClick);
                    uiManager.ToggleStatusBar(!nowCharacter.IsClick);
                    nowCharacter.IsClick = !nowCharacter.IsClick;
                    uiManager.RefreshStatusBar(nowCharacter);
                }
                else if (colider.gameObject.GetComponent<SoulCube>()) // when push the cube, get stone.
                {
                    SoulCube cube = colider.gameObject.GetComponent<SoulCube>();
                    cube.RefineSoul();
                    cube.PickSoul();
                    cube.AutoPlay();
                }
                else
                {
                    if (nowCharacter)
                    {
                        nowCharacter.DisplaySoulCubes(false);
                        nowCharacter.IsClick = false;
                    }
                    uiManager.ToggleStatusBar(false);
                    nowCharacter = null;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (colider.gameObject.GetComponent<Character>())
                {
                    clickPoint = Input.mousePosition;
                    preCharacter = nowCharacter;
                    nowCharacter = colider.gameObject.GetComponent<Character>();
                    nowCharacter.IsDrag = true;
                }
            }
        }
    }

    public Character NowCharacter
    {
        get { return nowCharacter; }
        set { nowCharacter = value; }
    }
}
