using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigMovementController : MonoBehaviour
{
    public float RunningSpeed = 2f;
    public float turningDirection = 1;
    public float TurningSpeed = 50f;
    public float TurnAroundSpeed = 100f;

    private bool move = true;
    private bool turnAround = false;
    private float secondsPassed = 0f;
    private float timeToTurnAround = 180f;

    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("Turn", 0, 3);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Movement();
        TurnAround();
    }


    private void Movement()
    {
        if (move)
        {
            transform.Rotate(0, turningDirection * Time.deltaTime * TurningSpeed, 0);
            transform.Translate(Vector3.forward * RunningSpeed * Time.deltaTime);
        }
        
    }

    private void TurnAround()
    {
        if (turnAround)
        {
            secondsPassed += Time.deltaTime;
            if (secondsPassed >= 180/TurnAroundSpeed)
            {
                turnAround = false;
                move = true;
                animator.speed = 1;
                secondsPassed = 0;
            }
            transform.Rotate(0, Time.deltaTime * TurnAroundSpeed, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!turnAround)
            if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
            {
                Debug.Log("collision");
                move = false;
                turnAround = true;
                animator.speed = 0;
            }
    }

    private void Turn()
    {
        turningDirection = Random.Range(-1, 2);
    }

}
