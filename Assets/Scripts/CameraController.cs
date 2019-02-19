using UnityEngine;

public static class Direction
{
    public static readonly Vector3 Up = new Vector3(1, 0, 1);
    public static readonly Vector3 Left = new Vector3(-1, 0, 1);
    public static readonly Vector3 Down = new Vector3(-1, 0, -1);
    public static readonly Vector3 Right = new Vector3(1, 0, -1);
}

public class CameraController : MonoBehaviour
{
    // Movement
    public int MovementSpeed = 30;

    // Rotation
    private Quaternion rotationTarget;
    public int RotationSpeed = 10;
    private int rotated = 0;

    // Zooming
    public float MinZoom = 2f;
    public float MaxZoom = 12f;
    public float ZoomSensitivity = 4f;
    public float ZoomSpeed = 20f;
    private float zoomSize;

    // Dragging
    private Vector3 origin;
    private Vector3 targetDirection;
    private Vector3 dragDelta;
        
    void Start()
    {
        zoomSize = Camera.main.orthographicSize;
        rotationTarget = transform.rotation;
    }

    void Update()
    {
        MoveCamera();
        RotateCamera();
        ZoomCamera();
        DragCamera();
    }

    void MoveCamera()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Direction.Up * MovementSpeed * Time.deltaTime, Space.Self);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(Direction.Down * MovementSpeed * Time.deltaTime, Space.Self);
        else if (Input.GetKey(KeyCode.A))
            transform.Translate(Direction.Left * (MovementSpeed / 2) * Time.deltaTime, Space.Self);
        else if (Input.GetKey(KeyCode.D))
            transform.Translate(Direction.Right * (MovementSpeed / 2) * Time.deltaTime, Space.Self);
    }

    void RotateCamera()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotated = (rotated + 90) % 360;
            rotationTarget = Quaternion.Euler(0, rotated, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotated = (rotated - 90) % 360;
            rotationTarget = Quaternion.Euler(0, rotated, 0);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, Time.deltaTime * RotationSpeed);
    }

    void ZoomCamera()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0.0f)
        {
            zoomSize += Input.GetAxisRaw("Mouse ScrollWheel") * ZoomSensitivity;
            zoomSize = Mathf.Clamp(zoomSize, MinZoom, MaxZoom);
        }
    }

    void DragCamera()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            origin = MouseTracker.Instance.GetMousePosition();
            origin.y = 0;
        }

        if (Input.GetKey(KeyCode.Mouse0) && !origin.Equals(Vector3.negativeInfinity))
        {
            targetDirection = MouseTracker.Instance.GetMousePosition();

            if (!targetDirection.Equals(Vector3.negativeInfinity))
            {
                targetDirection.y = 0;
                dragDelta = origin - targetDirection;
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            dragDelta = Vector3.zero;
    }

    void LateUpdate()
    {
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomSize, Time.deltaTime * ZoomSpeed);
        transform.position += dragDelta;
    }
}
