using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastleBuilding : MonoBehaviour
{
    public RectTransform gameOverScreen;
    public TextMeshProUGUI wavesSurvived;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI structuresBuilt;

    public Image healthBar;
    public LayerMask enemyLayer;

    private int currentHealth = 3000;
    private readonly int maxHealth = 3000;

    public TextMeshProUGUI currentHealthText;
    public TextMeshProUGUI maxHealthText;

    private void Start()
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    public void TakeDamage(int amount)
    {
        StatisticsManager.instance.RegisterDamageTaken(amount);
        currentHealth -= amount;
        
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Time.timeScale = 0;
            wavesSurvived.text = (StatisticsManager.instance.WavesSurvived - 1).ToString();
            enemiesKilled.text = StatisticsManager.instance.EnemiesKilled.ToString();
            structuresBuilt.text = StatisticsManager.instance.StructuresBuilt.ToString();
            gameOverScreen.gameObject.SetActive(true);
        }
        else healthBar.fillAmount = (float)currentHealth / maxHealth;

        currentHealthText.text = currentHealth.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        var enemyRef = collider.gameObject;

        if (enemyRef != null)
        {
            if (enemyRef.GetComponent<EnemyController>() != null)
            {
                int damageToCastle = enemyRef.GetComponent<EnemyController>().currentHealth;
                TakeDamage(damageToCastle);
                Destroy(enemyRef);
                StatisticsManager.instance.RegisterEnemyKilled();
            }
        }
    }
}