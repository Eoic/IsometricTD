using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCrate : MonoBehaviour
{
    private Rigidbody _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _rigidbody.AddRelativeForce(0, 0, -15000);
        }
    }
}
