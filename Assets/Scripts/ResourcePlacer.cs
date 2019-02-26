using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ResourcePlacer : MonoBehaviour
{
    public GameObject[] resources;

    [Header("Rotation")]
    public bool randomRotateX;
    public bool randomRotateY;
    public bool randomRotateZ;

    [Header("Scale")]
    public bool randomScale;
    [Range(1, 10)]  public int minScale = 1;
    [Range(1, 10)]  public int maxScale = 1;

    [Header("Size")] 
    [Min(1)]        public int mapWidth = 100;
    [Min(1)]        public int mapHeight = 100;

    private int[,] mapMatrix;

    void Awake()
    {
        mapMatrix = new int[mapWidth, mapHeight];
    }

    void Update()
    {
        
    }
}
