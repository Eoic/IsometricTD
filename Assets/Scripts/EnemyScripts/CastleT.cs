using TMPro;
using UnityEngine;

public class CastleT : MonoBehaviour, IDamageable
{
    public int maxHealth = 1000;
    private int currentHealth;
    public GameObject healthBar;
    public RectTransform gameOverScreen;
    public TextMeshProUGUI wavesSurvived;
    public TextMeshProUGUI enemiesKilled;
    public TextMeshProUGUI structuresBuilt;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool theme3Playing = false;
    private bool theme5Playing = false;
    public void TakeDamage(int damage)
    {
        damage = currentHealth > damage ? damage : currentHealth;
        StatisticsManager.instance.RegisterDamageTaken(damage);
        currentHealth -= damage;
        if (currentHealth <= maxHealth * 0.7 && currentHealth > maxHealth * 0.4 && !theme3Playing)
        {
            theme3Playing = true;
            AudioManager.instance.Play("Theme3");
            AudioManager.instance.Stop("Theme1");
        }
        if (currentHealth <= maxHealth * 0.4 && !theme5Playing)
        {
            theme5Playing = true;
            AudioManager.instance.Play("Theme5");
            AudioManager.instance.Stop("Theme3");
        }
        healthBar.transform.localScale = new Vector3((float)currentHealth / (float)maxHealth, 1, 1);
        if (currentHealth <= 0)
        {
            Time.timeScale = 0;
            wavesSurvived.text = (StatisticsManager.instance.WavesSurvived - 1).ToString();
            enemiesKilled.text = StatisticsManager.instance.EnemiesKilled.ToString();
            structuresBuilt.text = StatisticsManager.instance.StructuresBuilt.ToString();
            gameOverScreen.gameObject.SetActive(true);
        }
    }
}
