using TMPro;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonKeyChanger : MonoBehaviour, IPointerClickHandler
{
    public string keyName;

    private bool keySetMode = false;
    private TextMeshProUGUI buttonText;
    private KeyCode key;

    private void Start()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        if (PlayerPrefs.GetString(keyName) != null)
            buttonText.text = PlayerPrefs.GetString(keyName); 
    }

    IEnumerator CatchKeyPress()
    {
        while (keySetMode)
        {
            foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    key = code;
                    buttonText.text = key.ToString();
                    PlayerPrefs.SetString(keyName, key.ToString());
                    ControlsMapper.Instance.MapKeys();
                    PlayerPrefs.Save();
                    keySetMode = false;
                }
            }

            yield return null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        keySetMode = true;
        buttonText.text = "Press key...";
        StartCoroutine(CatchKeyPress());
    }

    public void SetKey(string keyText)
    {
        buttonText.text = keyText;
    }
}
