using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private RaycastHit hit;

    public static MouseTracker Instance { get; set; } = null;
    public RaycastHit Hit { get => hit; set => hit = value; }
    public Ray Ray { get; set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    /// <summary>
    /// Returns object's transform instance on which trigger button was clicked.
    /// </summary>
    /// <param name="trigger"> Defines which button causes raycasting on object.</param>
    /// <returns></returns>
    public Transform GetSelectedObject(KeyCode trigger)
    {
        if (Input.GetKeyDown(trigger))
        {
            if(PerformRaycast())
                return hit.transform;
        }

        return null;
    }
    
    // Raycast from mouse position
    private bool PerformRaycast()
    {
        Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(Ray, out hit))
            return true;

        return false;
    }
    
    public Vector3 GetMousePosition()
    {
        if (PerformRaycast())
            return hit.point;

        return Vector3.zero;
    }
}
