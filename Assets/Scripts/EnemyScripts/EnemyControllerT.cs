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
        if(pointIterator < targetPoints.Length)
            transform.LookAt(targetPoints[pointIterator].transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target") && targetPoints.Contains(other.gameObject))//TODO: might be wrong
        {
            pointIterator++;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.transform.localScale = new Vector3((float)currentHealth/(float)maxHealth, 1, 1);
        Debug.Log("DAMAGE TAKEN");
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Die");
            speed = 0;
            Destroy(this.gameObject, 2);
        }
    }

}
