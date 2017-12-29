using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_collision : MonoBehaviour {

    public AudioClip hit;
    private AudioSource source;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("house") || c.gameObject.CompareTag("terrain") || c.gameObject.CompareTag("Cars"))
        {
            source.PlayOneShot(hit, 1.0f);
        }
    }
}
