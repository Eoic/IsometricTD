using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDetector : MonoBehaviour
{
    public LayerMask resourceLayerMask;
        
    private void OnCollisionEnter(Collision collision)
    {
        // Resources are in range
        if(((1 << collision.gameObject.layer) & resourceLayerMask) == 0)
        {
            Debug.Log("Overlapping with trees");
        }
    }

    private void OnCollisionStay(Collision collision)
    {

    }


}
