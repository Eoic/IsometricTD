using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementForTesting : MonoBehaviour
{
    public float WalkingSpeed = 30;
    public float TurningSpeed = 90;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(WalkingSpeed * Vector3.forward * Input.GetAxis("Vertical") * Time.deltaTime);
        transform.Rotate(0, TurningSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, 0);
        animator.Play("Run");
    }
}
