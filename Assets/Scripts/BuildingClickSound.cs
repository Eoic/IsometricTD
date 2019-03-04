using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class BuildingClickSound : MonoBehaviour
{
    List<Transform> objects = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            objects.Add(transform.GetChild(i));
        }

        foreach (var item in objects)
        {
            var button = item.GetComponent<Button>();
            if(button != null)
                button.onClick.AddListener(()=> { AudioManager.instance.Play("BuildingClick"); });
        }
    }

}
