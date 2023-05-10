using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] float destroyDelay = 1f; //delay after which egg will be destroyed


    private void OnTriggerEnter2D(Collider2D eggs)
    {
        if (eggs.tag == "Egg")
        {
            FindObjectOfType<ScoreManager>().EggsDropped();
            PlayParenaAudio();
        }
        eggs.GetComponent<EggMovement>().DestroyEgg(destroyDelay);

    }

    public void PlayParenaAudio()
    {
        if (FindObjectOfType<EggSpawner>().instantiate)
        {
            AudioSource source = GetComponent<AudioSource>();
            if(!source.isPlaying)
            {
                source.Play();
            }
        }
    }
}
