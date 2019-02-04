using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Moving
    public float CameraMovementSpeed = 20f;
    private Vector3 moveDirection = Vector3.zero;

    // Zooming
    private readonly float minZoom = 0.5f;
    private readonly float maxZoom = 12f;
    public float ZoomSensitivity = 4f;
    public float ZoomSpeed = 20f;
    private float zoomSize;

    void Start()
    {
        zoomSize = Camera.main.orthographicSize;
    }

    void Update()
    {
        // Camera movement
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(-1, 0, -1) * Time.deltaTime * CameraMovementSpeed;
        else if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(1, 0, 1) * Time.deltaTime * CameraMovementSpeed;
        else if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-1, 0, 1).normalized * Time.deltaTime * CameraMovementSpeed;
        else if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(1, 0, -1).normalized * Time.deltaTime * CameraMovementSpeed;

        // Calculate zoom step
        zoomSize += Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitivity;
        zoomSize = Mathf.Clamp(zoomSize, minZoom, maxZoom);
    }

    void LateUpdate()
    {
        // Smooth zooming
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomSize, Time.deltaTime * ZoomSpeed);
    }
}
