using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeapon : MonoBehaviour
{
    private float speed = 3f;
    private Vector3 target;

    private void Start() =>
        target = transform.position;

    /*
    void Update()
    {
        var targetRotation = Quaternion.LookRotation(target - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }
    */

    public void SetTarget(Vector3 target) =>
        this.target = new Vector3(target.x, target.y, transform.position.z);
}
