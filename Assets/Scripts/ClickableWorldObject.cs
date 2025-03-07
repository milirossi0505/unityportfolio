using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableWorldObject : MonoBehaviour
{
    public Transform targetCameraTr;
    private CameraCustomControl _cameraCustomControl;

    private void Start()
    {
        _cameraCustomControl = FindObjectOfType<CameraCustomControl>();
    }

    private void OnMouseDown()
    {
        CameraControl.CustomOrientation = true;
        _cameraCustomControl.OrientateCamera(targetCameraTr);
    }
}
