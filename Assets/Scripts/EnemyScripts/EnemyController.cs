using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum Actions
{
    IsIdle = 0,
    IsWalking = 1,
    IsAttacking = 2,
    IsTakingDamage = 3,
    IsDying = 4
}

public class EnemyController : MonoBehaviour, IDamageable
{
    private Animator animator;

    public Image healthBar;

    public float speed;
    public int fullHealth;
    public int currentHealth;

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
                //animator.SetBool("IsTakingDamage", true);
                StartCoroutine(TakeDamage());
                break;
            case Actions.IsAttacking:
                animator.SetBool("IsAttacking", true);
                break;
            case Actions.IsWalking:
                animator.SetBool("IsWalking", true);
                break;
            case Actions.IsDying:
                if (!pathFinished)
                {
                    animator.SetBool("IsDying", true);
                    animator.SetBool("IsWalking", false);
                    pathFinished = true;
                    StatisticsManager.instance.RegisterEnemyKilled();
                    StartCoroutine(Die());
                }
                break;
        }

        if (!pathFinished)
        {
            // Target was reached or there is no more points to go to.
            if (Vector3.Distance(transform.position, targetPointer) < targetOffset)
            {
                // Set new target
                if (targetNodes != null)
                {
                    if (targetIndex < targetNodes.Length - 1)
                        targetPointer = targetNodes[++targetIndex].position;
                    else
                    {
                        state = Actions.IsIdle;
                        pathFinished = true;
                    }
                }
            }

            float step = speed * Time.deltaTime;
            var positionTarget = new Vector3(targetPointer.x, transform.position.y, targetPointer.z);

            if (!(positionTarget - transform.position).Equals(Vector3.zero))
            {
                Quaternion rotationTarget = Quaternion.LookRotation(positionTarget - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, step);
            }

            transform.position = Vector3.MoveTowards(transform.position, positionTarget, step);
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            healthBar.fillAmount = 0;
            state = Actions.IsDying;
        }
        else
        {
            UpdateHealth();
            state = Actions.IsTakingDamage;
        }
    }

    void UpdateHealth()
    {
        float fill = (float)currentHealth / fullHealth;
        healthBar.fillAmount = fill;
    }

    IEnumerator TakeDamage()
    {
        yield return new WaitForSeconds(0.8f);
        //animator.SetBool("IsTakingDamage", false);
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
