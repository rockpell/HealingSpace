using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] CameraController cameraController = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetIsCharacter(bool value)
    {
        cameraController.IsCharacter = value;
    }
}
