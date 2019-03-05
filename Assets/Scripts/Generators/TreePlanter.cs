using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TreePlanter : MonoBehaviour
{   
    public GameObject[] resources;
    
    [Header("Rotation")]
    public bool randomRotation;
    [Range(1, 360)] public int maxRotation = 1;

    [Header("Scale")]
    public bool randomScale;
    [Range(1, 10)]          public int maxScale = 1;

    [Header("Density")]
    public double densityTreshold = 0.01;
    
    [Header("Size")]
    [Min(1)]        public int mapWidth = 10;
    [Min(1)]        public int mapHeight = 10;

    private int[,] mapMatrix;
    private System.Random random;
    
    public void PlantTrees()
    {
        int generatedCount = 0;
        random = new System.Random(System.Guid.NewGuid().GetHashCode());
        mapMatrix = new int[mapWidth, mapHeight];

        DestroyPreviousGeneration();
        GeneratePlantPoints();

        for (int i = 0; i < mapMatrix.GetLength(0); i++)
        {
            for (int j = 0; j < mapMatrix.GetLength(1); j++)
            {
                if(mapMatrix[i, j] == 1)
                {
                    GameObject asset = PrefabUtility.InstantiatePrefab(PickRandomTree()) as GameObject;
                    asset.transform.position = new Vector3(i + transform.position.x, asset.transform.position.y, j + transform.position.z);
                    asset.transform.parent = transform;
                    ApplyRotation(asset);
                    ApplyScale(asset);
                    generatedCount++;
                }
            }
        }

        Debug.LogFormat("Generated {0} objects.", generatedCount);
    }
    
    public void GeneratePlantPoints()
    {
        if (densityTreshold > 1)
            densityTreshold = 1;
        else if (densityTreshold < 0)
            densityTreshold = 0;

        for(int i = 0; i < mapMatrix.GetLength(0); i++)
            for(int j = 0; j < mapMatrix.GetLength(1); j++)
                mapMatrix[i, j] = random.NextDouble() >= densityTreshold ? 0 : 1;
    }

    private void DestroyPreviousGeneration()
    {
        GameObject[] objectsToDelete = new GameObject[transform.childCount]; 
            
        for (int i = 0; i < transform.childCount; i++)
            objectsToDelete[i] = transform.GetChild(i).gameObject;

        foreach (GameObject item in objectsToDelete)
            DestroyImmediate(item);
    }

    private GameObject PickRandomTree() =>
        resources[random.Next(0, resources.Length)];

    void ApplyRotation(GameObject item)
    {
        if (!randomRotation)
            return;
        
        /*
        int rotationOffset = random.Next(maxRotation + 1);
        item.transform.localRotation *= Quaternion.AngleAxis(rotationOffset, Vector3.up);
        */
    }

    void ApplyScale(GameObject item)
    {
        if (!randomScale)
            return;

        int scale = random.Next(0, maxScale);
        item.transform.localScale += new Vector3(scale, scale, scale);
    }
}
