using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleT : MonoBehaviour, IDamageable
{
    public int maxHealth = 1000;
    private int currentHealth;
    public GameObject healthBar;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;
        healthBar.transform.localScale = new Vector3((float)currentHealth / (float)maxHealth, 1, 1);
        //Debug.Log("DAMAGE TAKEN");
        if (currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
        }
    }
}
