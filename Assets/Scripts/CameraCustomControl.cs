using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

public class CameraCustomControl : MonoBehaviour
{
    private Transform cameraTr;
    private Transform _currentTarget;

    private void Start()
    {
        cameraTr = Camera.main.transform;
    }

    public void OrientateCamera(Transform target)
    {
        _currentTarget = target;
        StartCoroutine(CrOrientate(1f, target));
    }

    public void OrientateCamera(float duration,Transform target)
    {
        _currentTarget = target;
        StartCoroutine(CrOrientate(duration, target));
    }

    public IEnumerator CrOrientate(float duration, Transform targetOrientation)
    {
        Quaternion startRot = cameraTr.rotation;
        Vector3 startPos = cameraTr.position;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            cameraTr.rotation = Quaternion.Lerp(startRot, targetOrientation.rotation, (i / duration));
            cameraTr.position = Vector3.Lerp(startPos, targetOrientation.position, (i / duration));
            yield return null;
        }
        cameraTr.rotation = targetOrientation.rotation;
        cameraTr.position = targetOrientation.position;
    }

}
