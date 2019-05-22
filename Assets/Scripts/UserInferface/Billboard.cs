using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.back,
                         Camera.main.transform.rotation * Vector3.up);
    }
}