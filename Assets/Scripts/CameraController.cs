using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private ControllerManager controllerManager = null;
    [SerializeField] private float maxCameraX = 0;
    [SerializeField] private float minCameraX = 0;
    [SerializeField] private float maxCameraY = 0;
    [SerializeField] private float minCameraY = 0;
    [SerializeField] private float seroEdgeSize = 40f;
    [SerializeField] private float garoEdgeSize = 15f;

    private Vector3 startPos = Vector3.zero;
    private float speed = 100f;
    private float scrollSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D colider = Physics2D.OverlapPoint(touchPos);

        if (!controllerManager.NowCharacter)
        {
            if (colider && colider.gameObject.name == "BG")
            {
                DragCamera();
            }
        }
        else if (controllerManager.NowCharacter.IsDrag)
        {
            ScrollMoving();
        }
    }

    public void ScrollMoving()
    {
        if (Input.mousePosition.x > Screen.width - garoEdgeSize)
        {
            MoveCamera(new Vector3(Time.deltaTime * -scrollSpeed, 0, 0));
        }
        if (Input.mousePosition.x < garoEdgeSize)
        {
            MoveCamera(new Vector3(Time.deltaTime * scrollSpeed, 0, 0));
        }
        if (Input.mousePosition.y > Screen.height - seroEdgeSize)
        {
            MoveCamera(new Vector3(0, Time.deltaTime * -scrollSpeed, 0));
        }
        if (Input.mousePosition.y < seroEdgeSize)
        {
            MoveCamera(new Vector3(0, Time.deltaTime * scrollSpeed, 0));
        }

    }

    public void DragCamera()
    {
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 moveVector = Vector3.zero;
            moveVector.x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed;
            moveVector.y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed;
            MoveCamera(moveVector);
        }
    }

    private void MoveCamera(Vector3 moveVector)
    {
        Vector3 nowPos = Camera.main.transform.position;
        Vector3 vector = new Vector3(nowPos.x - moveVector.x, nowPos.y - moveVector.y, nowPos.z);
        vector.x = Mathf.Clamp(vector.x, minCameraX, maxCameraX);
        vector.y = Mathf.Clamp(vector.y, minCameraY, maxCameraY);
        Camera.main.transform.position = vector;
    }

}
