using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverSounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var parentClip = GetComponent<AudioSource>();
        for (int i = 0; i < transform.childCount; i++)
        {
            
            var riverTile = transform.GetChild(i);
            riverTile.gameObject.AddComponent<AudioSource>();
            var audioSource = riverTile.GetComponent<AudioSource>();
            audioSource.clip = parentClip.clip;
            audioSource.volume = parentClip.volume;
            audioSource.spatialBlend = parentClip.spatialBlend;
            audioSource.loop = parentClip.loop;
            audioSource.Play();
        }
        
    }

}
