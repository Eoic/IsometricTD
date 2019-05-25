using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerT : MonoBehaviour
{
    public GameObject[] targetPoints;
    public int speed;
    private int pointIterator = 0;
    private Rigidbody rb;
    private Animator animator;
    public float maxHealth = 100;
    public float currentHealth = 100;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pointIterator < targetPoints.Length)
            transform.LookAt(targetPoints[pointIterator].transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        animator.Play("Run");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            pointIterator++;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.transform.localScale = new Vector3(currentHealth/maxHealth, 0, 0);
        Debug.Log("DAMAGE TAKEN");
    }
}
