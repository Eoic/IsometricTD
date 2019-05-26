using System.Linq;
using UnityEngine;

public class EnemyControllerT : MonoBehaviour, IDamageable
{
    public GameObject[] targetPoints;
    public int speed;
    private int pointIterator = 0;
    private Rigidbody rb;
    private Animator animator;
    public int maxHealth = 100;
    public int currentHealth = 100;
    public GameObject healthBar;
    public int damage = 40;
    private bool isAttacking = false;
    private IDamageable castle;
    private float secondsBetweenAttacks = 1f;
    private float secondCounter = 0;
    private bool isDead = false;
    
    [SerializeField]
    private int pointCount = 100;
    public int PointCount { get => pointCount; }


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        animator.Play("Run");
    }
    
    // Update is called once per frame
    void Update()
    {
        secondCounter += Time.deltaTime;
        if(pointIterator < targetPoints.Length)
            transform.LookAt(targetPoints[pointIterator].transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && targetPoints.Contains(other.gameObject))
        {
            pointIterator++;
        }

        if (other.CompareTag("Castle"))
        {
            castle = other.gameObject.GetComponent<IDamageable>();
            speed = 0;
            isAttacking = true;
            animator.SetBool("IsAttacking", true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            if (isAttacking)
            {
                if (secondCounter >= secondsBetweenAttacks)
                {
                    secondCounter = 0;
                    castle.TakeDamage(damage);
                }
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.transform.localScale = new Vector3((float)currentHealth/(float)maxHealth, 1, 1);
        //Debug.Log("DAMAGE TAKEN");

        if (currentHealth <= 0) //DETH
        {
            animator.SetTrigger("Die");
            speed = 0;
            Destroy(this.gameObject, 2);
            if (!isDead) {
                StatisticsManager.instance.RegisterEnemyKilled();
                isDead = true;
            }
        }
    }

}
