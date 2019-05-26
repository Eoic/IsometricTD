using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CreateLevelList : MonoBehaviour
{
    private TMP_Dropdown levelDropdown;

    void Start()
    {
        levelDropdown = GetComponent<TMP_Dropdown>();
        var list = new List<TMP_Dropdown.OptionData>();

        for (int i = 1; i <= 10; i++)
            list.Add(new TMP_Dropdown.OptionData("Level " + i));

        levelDropdown.options = list;
    }
}
