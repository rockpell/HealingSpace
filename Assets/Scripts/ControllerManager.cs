using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController = null;
    [SerializeField] UIManager uiManager = null;
    private Character nowCharacter = null; // present selected character instance
    private Vector3 clickPoint = Vector3.zero;

    //private bool isDrag = false;
    //private bool isClick = false;

    void Start()
    {
        
    }

    void Update()
    {
        TouchEvent();
        if (Input.GetMouseButtonUp(0))
        {
            if (nowCharacter)
            {
                nowCharacter.IsDrag = false;
                nowCharacter = null;
            }
        }
    }

    public void TouchEvent()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D colider = Physics2D.OverlapPoint(touchPos);

        if (colider)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (colider.gameObject.GetComponent<Character>() &&
                    Vector3.Distance(clickPoint, Input.mousePosition) < 0.25f) // when push the character, cubes appear.
                {
                    nowCharacter.DisplaySoulCubes(!nowCharacter.IsClick);
                    uiManager.ToggleStatusBar(!nowCharacter.IsClick);
                    nowCharacter.IsClick = !nowCharacter.IsClick;
                    uiManager.RefreshStatusBar(nowCharacter);
                }
                if (colider.gameObject.GetComponent<SoulCube>()) // when push the cube, get stone.
                {
                    SoulCube cube = colider.gameObject.GetComponent<SoulCube>();
                    cube.RefineSoul();
                    cube.PickSoul();
                    cube.AutoPlay();
                }
            }
            if (Input.GetMouseButton(0))
            {
                if (nowCharacter && colider.gameObject.GetComponent<Character>())
                {
                    nowCharacter.IsDrag = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (colider.gameObject.GetComponent<Character>())
                {
                    clickPoint = Input.mousePosition;
                    nowCharacter = colider.gameObject.GetComponent<Character>();
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
