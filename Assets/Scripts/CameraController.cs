using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Moving (Keys)
    public float CameraMovementSpeed = 20f;
    private Vector3 moveDirection = Vector3.zero;

    // Moving (Mouse dragging)
    private Vector3 dragPoint;
    public float dragSpeed = 10f;

    // Zooming
    [SerializeField] private float minZoom = 2f;
    [SerializeField] private float maxZoom = 12f;
    public float ZoomSensitivity = 4f;
    public float ZoomSpeed = 20f;
    private float zoomSize;

    // Rotating
    public int RotationSpeed = 10;
    private Quaternion rotateTo;
    private int rotated = 45;
    private readonly Vector3[] directions = new Vector3[4];
    private int[] directionIndex = { 0, 1, 2, 3 };

    void Start()
    {
        zoomSize = Camera.main.orthographicSize;
        rotateTo = transform.rotation;

        directions[0] = new Vector3(-1, 0, -1);
        directions[1] = new Vector3(1, 0, 1);
        directions[2] = new Vector3(-1, 0, 1);
        directions[3] = new Vector3(1, 0, -1);
    }

    void Update()
    {
        RotateCamera();
        MoveCamera();

        // Calculate zoom step
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            zoomSize += Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitivity;
            zoomSize = Mathf.Clamp(zoomSize, minZoom, maxZoom);
        }
    }

    void LateUpdate()
    {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomSize, Time.deltaTime * ZoomSpeed);
    }

    void RotateCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotated = (rotated + 90) % 360;
            rotateTo = Quaternion.Euler(30, rotated, 0);
            UpdateDirections();
        }
        else if(Input.GetKeyDown(KeyCode.E))
        {
            rotated = (rotated - 90) % 360;
            rotateTo = Quaternion.Euler(30, rotated, 0);
            UpdateDirections();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotateTo, Time.deltaTime * RotationSpeed);
    }

    void MoveCamera()
    {
        if (Input.GetKey(KeyCode.S))
            transform.Translate(directions[directionIndex[0]] * Time.deltaTime * CameraMovementSpeed, Space.World);
        else if (Input.GetKey(KeyCode.W))
            transform.Translate(directions[directionIndex[1]] * Time.deltaTime * CameraMovementSpeed, Space.World);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(directions[directionIndex[2]].normalized * Time.deltaTime * CameraMovementSpeed, Space.World);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(directions[directionIndex[3]].normalized * Time.deltaTime * CameraMovementSpeed, Space.World);
    }

    void UpdateDirections()
    {
        if (rotated == 45 || rotated == -315)
        {
            directionIndex[0] = 0;
            directionIndex[1] = 1;
            directionIndex[2] = 2;
            directionIndex[3] = 3;
        } else if(rotated == 135 || rotated == -225)
        {
            directionIndex[0] = 2;
            directionIndex[1] = 3;
            directionIndex[2] = 1;
            directionIndex[3] = 0;
        } else if (rotated == 225 || rotated == -135)
        {
            directionIndex[0] = 1;  
            directionIndex[1] = 0;
            directionIndex[2] = 3;
            directionIndex[3] = 2; 
        } else if(rotated == -45 || rotated == 315)
        {
            directionIndex[0] = 3;
            directionIndex[1] = 2; 
            directionIndex[2] = 0;
            directionIndex[3] = 1; 
        }
    }
}
