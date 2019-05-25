using UnityEngine;

public class DeselectElements : MonoBehaviour
{
    private void OnMouseDown()
    {
        UIEvents.Instance.Deselect();
    }
}
