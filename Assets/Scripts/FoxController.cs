using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxController : MonoBehaviour
{
    public float RunningSpeed = 2f;
    public float turningDirection = 1;
    public float TurningSpeed = 50f;
    public float TurnAroundSpeed = 100f;

    private bool move = true;
    private bool turnAround = false;
    private float secondsPassed = 0f;
    private float timeToTurnAround = 180f;

    readonly int walkHash = Animator.StringToHash("Walk");

    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        if (Input.GetKey(KeyCode.T))
        {
            move = true;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            move = false;
            animator.SetTrigger("Stop");
            animator.ResetTrigger(walkHash);
        }
    }


    private void Movement()
    {
        if (move)
        {
            animator.SetTrigger(walkHash);
            transform.Rotate(0, turningDirection * Time.deltaTime * TurningSpeed, 0);
            transform.Translate(Vector3.forward * RunningSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!turnAround)
            if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("collision");
                move = false;
                turnAround = true;
                animator.speed = 0;
            }
    }

}
