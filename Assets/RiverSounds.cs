using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var riverTile = transform.GetChild(i);
            riverTile.gameObject.AddComponent<AudioSource>();
            var audioSource = riverTile.GetComponent<AudioSource>();
            audioSource.clip = GetComponent<AudioSource>().clip;
            audioSource.volume = GetComponent<AudioSource>().volume;
            audioSource.spatialBlend = 1;
            audioSource.loop = true;
            audioSource.Play();
        }
        
    }

}
