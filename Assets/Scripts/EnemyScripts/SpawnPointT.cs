using UnityEngine;

public class SpawnPointT : MonoBehaviour, ISpawnPoint
{
    public GameObject targetsParentObj;
    public GameObject enemy;
    private GameObject[] targetArray;
    void Start()
    {
        targetArray = new GameObject[targetsParentObj.transform.childCount];
        for (int i = 0; i < targetsParentObj.transform.childCount; i++)
        {
            targetArray[i] = targetsParentObj.transform.GetChild(i).gameObject;
        }
    }


    public void SpawnEnemy()
    {
        var enemyObj = Instantiate(enemy, transform.position, Quaternion.identity);
        var enm = enemyObj.GetComponent<EnemyControllerT>();
        enm.targetPoints = targetArray;
        StatisticsManager.instance.RegisterEnemySpawned();
    }

}
