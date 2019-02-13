using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform worldCamera;

    void LateUpdate()
    {
        Vector3 minimapPosition = worldCamera.position;
        minimapPosition.y = transform.position.y;
        transform.position = minimapPosition;
    }
}
