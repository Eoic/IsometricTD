using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSwitch : MonoBehaviour
{
    private int focusIndex;
    private bool transitionComplete;
    public Vector3[] cameraFocusPoints;
    public GameObject worldCamera;

    void Start()
    {
        focusIndex = 0;
        transitionComplete = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            focusIndex = (focusIndex + 1) % cameraFocusPoints.Length;
            worldCamera.transform.position = cameraFocusPoints[focusIndex];
        }
    }
}
