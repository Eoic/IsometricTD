using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions
{
    IsIdle = 0,
    IsWalking = 1,
    IsAttacking = 2,
    IsTakingDamage = 3,
    IsDying = 4
}

public class EnemyController : MonoBehaviour
{
    private Animator animator;

    public float speed;
    public int health;

    public Actions state = Actions.IsIdle;
    private Transform[] targetNodes;
    private Vector3 targetPointer;
    private readonly float targetOffset = 0.8f;
    private int targetIndex = 0;
    private bool pathFinished = false;

    private void Start() => animator = GetComponent<Animator>();

    void Update()
    {
        switch (state)
        {
            case Actions.IsIdle:
                animator.SetBool("IsWalking", false);
                break;
            case Actions.IsTakingDamage:
                animator.SetBool("IsTakingDamage", true);
                break;
            case Actions.IsAttacking:
                animator.SetBool("IsAttacking", true);
                break;
            case Actions.IsWalking:
                animator.SetBool("IsWalking", true);
                break;
            case Actions.IsDying:
                animator.SetBool("IsDying", true);

                //Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 2f);
                // Dying animation finished playing. Bye.
                //if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0))

                break;
        }

        if (!pathFinished)
        {
            // Target was reached or there is no more points to go to.
            if (Vector3.Distance(transform.position, targetPointer) < targetOffset)
            {
                // Set new target
                if (targetIndex < targetNodes.Length - 1)
                    targetPointer = targetNodes[++targetIndex].position;
                else
                {
                    state = Actions.IsIdle;
                    pathFinished = true;
                }
            }

            float step = speed * Time.deltaTime;
            var positionTarget = new Vector3(targetPointer.x, transform.position.y, targetPointer.z);
            var rotationTarget = Quaternion.LookRotation(positionTarget - transform.position);
            transform.position = Vector3.MoveTowards(transform.position, positionTarget, step);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, step);
        }
    }

    /// <summary>
    /// Sets path on how enemy should be moving.
    /// </summary>
    /// <param name="targetNodes"></param>
    public void SetPath(Transform[] targetNodes)
    {
        this.targetNodes = targetNodes;

        if (this.targetNodes.Length > 0)
            targetPointer = this.targetNodes[targetIndex].position;
        else targetPointer = transform.position;

        state = Actions.IsWalking;
    }
}
