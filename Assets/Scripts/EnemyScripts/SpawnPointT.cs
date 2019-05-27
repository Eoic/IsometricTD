using UnityEngine;

public enum EnemyType
{
    Orc,
    Skeleton
}

public class SpawnPointT : MonoBehaviour, ISpawnPoint
{
    public GameObject targetsParentObj;
    public GameObject enemyOrc;
    public GameObject enemySkeltal;
    private GameObject[] targetArray;
    public EnemyType enemyType { get; set; }
    void Start()
    {
        targetArray = new GameObject[targetsParentObj.transform.childCount];
        for (int i = 0; i < targetsParentObj.transform.childCount; i++)
        {
            targetArray[i] = targetsParentObj.transform.GetChild(i).gameObject;
        }
    }


    //public void SpawnEnemy()
    //{
    //    var enemyObj = Instantiate(enemyOrc, transform.position, Quaternion.identity);
    //    var enm = enemyObj.GetComponent<EnemyControllerT>();
    //    enm.targetPoints = targetArray;
    //    StatisticsManager.instance.RegisterEnemySpawned();
    //}

    public void SpawnEnemy()
    {
        GameObject enemyObj = null;
        if(enemyType == EnemyType.Orc)
            enemyObj = Instantiate(enemyOrc, transform.position, Quaternion.identity);
        else if(enemyType == EnemyType.Skeleton)
            enemyObj = Instantiate(enemySkeltal, transform.position, Quaternion.identity);
        var enm = enemyObj.GetComponent<EnemyControllerT>();
        enm.targetPoints = targetArray;
        StatisticsManager.instance.RegisterEnemySpawned();
    }
}
