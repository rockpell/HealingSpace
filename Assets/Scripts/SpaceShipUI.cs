using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipUI : MonoBehaviour
{

    [SerializeField] private Transform cameraObject = null;
    [SerializeField] private Transform endPos = null;
    [SerializeField] private Transform storePos = null;

    private Vector3 clickPositon = Vector3.zero;
    private Vector3 upPosition = Vector3.zero;

    [SerializeField] private float threshold = 300f;
    private float deltaValue = 0f;
    [SerializeField] private float speed = 2.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickPositon = Input.mousePosition;
            // Debug.Log("left click");
        }
        if (Input.GetMouseButton(0))
        {
            // Debug.Log("left pressing");
        }
        if (Input.GetMouseButtonUp(0))
        {
            upPosition = Input.mousePosition;
            deltaValue = (upPosition.y - clickPositon.y);
            // Debug.Log("delta value: " + (deltaValue));

            if (deltaValue > threshold)
            {
                if (storePos != null)
                    StartCoroutine(MoveCamera(storePos.position, "Store"));
            }
            else if (deltaValue < -threshold)
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Store")
                    StartCoroutine(MoveCamera(endPos.position, "SpaceShip"));
                else
                    StartCoroutine(MoveCamera(endPos.position, "Space"));
            }
        }
    }

    private IEnumerator MoveCamera(Vector3 targetPos, string sceneName)
    {
        Vector3 target = targetPos;

        target.z = -10;
        while (Mathf.Abs(cameraObject.position.y - targetPos.y) > 0.01f)
        {
            cameraObject.position = Vector3.Lerp(cameraObject.position, target, Time.deltaTime * speed);
            yield return new WaitForSeconds(0.05f);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
