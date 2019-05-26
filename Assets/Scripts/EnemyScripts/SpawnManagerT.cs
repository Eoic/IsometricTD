using UnityEngine;

public class SpawnManagerT : MonoBehaviour
{
    public GameObject[] spawnPointObjects;
    private SpawnPointT[] spawnPoints;
    public int enemiesPerSpawn = 5;
    public float secondsBetweenSpawns = 1;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = new SpawnPointT[spawnPointObjects.Length];
        for (int i = 0; i < spawnPointObjects.Length; i++)
        {
            spawnPoints[i] = spawnPointObjects[i].GetComponent<SpawnPointT>();
        }
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float secondAcc = secondsBetweenSpawns;
            for (int j = 0; j < enemiesPerSpawn; j++)
            {
                spawnPoints[i].Invoke("SpawnEnemy", secondAcc);
                secondAcc += secondsBetweenSpawns;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
