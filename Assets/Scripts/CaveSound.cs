using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSound : MonoBehaviour
{
    AudioSource source;
    public bool playMonsterSound = true;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("script started");
        source = GetComponent<AudioSource>();
        InvokeRepeating("Playmonstersound", 3f, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playMonsterSound)
            CancelInvoke();
    }

    void Playmonstersound()
    {
        source.Play();
    }
}
