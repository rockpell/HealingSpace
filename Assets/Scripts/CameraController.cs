using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxCameraX = 0;
    [SerializeField] private float minCameraX = 0;
    [SerializeField] private float maxCameraY = 0;
    [SerializeField] private float minCameraY = 0;

    private Vector3 startPos = Vector3.zero;
    private float speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraMoving();
    }

    public void CameraMoving()
    {
        if (Input.GetMouseButtonDown(0))
            startPos = Input.mousePosition;

        if (Input.GetMouseButton(0))
        {
            Vector3 nowPos = Camera.main.transform.position;
            Vector3 vector = new Vector3(nowPos.x - Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, nowPos.y - Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, nowPos.z);
            // Camera.main.transform.Translate(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0);
            vector.x = Mathf.Clamp(vector.x, minCameraX, maxCameraX);
            vector.y = Mathf.Clamp(vector.y, minCameraY, maxCameraY);
            Camera.main.transform.position = vector;
        }
    }
}
