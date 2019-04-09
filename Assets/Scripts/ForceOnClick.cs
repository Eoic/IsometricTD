using UnityEngine;
using UnityEngine.EventSystems;

public class ForceOnClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.one * 80, ForceMode.Acceleration);
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}