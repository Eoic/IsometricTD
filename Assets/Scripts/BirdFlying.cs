using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlying : MonoBehaviour
{
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(2 * Vector3.forward * Time.deltaTime);
        transform.Rotate(0, 5 * Time.deltaTime, 0);
    }
}
