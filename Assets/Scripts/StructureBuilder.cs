using System.Collections.Generic;
using UnityEngine;

public class StructureBuilder : MonoBehaviour
{
    public static StructureBuilder instance = null;

    [SerializeField]
    private List<GameObject> buildings;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CreateStructure(string key, Vector3 position)
    {
        GameObject toPlace = buildings.Find(item => item.GetComponent<Building>().BuildingId == key);

        if (toPlace == null)
            return;

        position = new Vector3(position.x, position.y + (toPlace.transform.localScale.y / 2), position.z);
        toPlace.transform.position = position;
        Instantiate(toPlace, position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 point = MouseTracker.Instance.GetMousePosition();
            CreateStructure("H_01", point);
        }
    }
}
