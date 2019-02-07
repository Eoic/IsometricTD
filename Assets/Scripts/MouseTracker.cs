using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private RaycastHit hit;

    public static MouseTracker Instance { get; set; } = null;
    public RaycastHit Hit { get => hit; set => hit = value; }
    public Ray Ray { get; set; }
    public LayerMask raycastLayer;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    /// <summary>
    /// Performs raycasting on mouse cursor position
    /// </summary>
    /// <returns></returns>
    private bool PerformRaycast()
    {
        Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Ray, out hit, Mathf.Infinity, raycastLayer))
            return true;

        return false;
    }

    /// <summary>
    /// Returns position where cursor is pointing on object 
    /// with collider in world space
    /// </summary>
    /// <returns></returns>
    public Vector3 GetMousePosition()
    {
        if (PerformRaycast())
            return hit.point;

        return Vector3.negativeInfinity;
    }
}
