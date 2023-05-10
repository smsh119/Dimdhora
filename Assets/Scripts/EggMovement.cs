using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMovement : MonoBehaviour
{
    bool shouldMove = true;
    float eggSpeed = 0f;
    float maxEggSpeed = 13f;
    float eggSpeedVaryingFactor;
    private void Start()
    {
        eggSpeed = FindObjectOfType<GameManager>().GetEggSpeed();
        maxEggSpeed = FindObjectOfType<GameManager>().GetMaxEggSpeed();
        eggSpeedVaryingFactor = FindObjectOfType<GameManager>().GetEggSpeedVaryingFactor();
        if (gameObject.tag != "Egg") eggSpeed += 2f; // to make sure egg shit is faster than egg
        VaryEggSpeed();
    }

    void Update()
    {
        MoveEgg();
    }

    private void VaryEggSpeed()
    {
        if (eggSpeed+eggSpeedVaryingFactor > maxEggSpeed) eggSpeed = maxEggSpeed-eggSpeedVaryingFactor;  //just to make the egg speed less than the maxEggSpeed in the inspector
        float rand = UnityEngine.Random.Range(eggSpeed - eggSpeedVaryingFactor, eggSpeed + eggSpeedVaryingFactor);
        eggSpeed = rand;
        
    }

    private void MoveEgg()
    {
        if (shouldMove)
        {
            var pos = gameObject.transform.position;
            transform.Translate(Vector3.down * eggSpeed * Time.deltaTime);
        }
    }

    public void DestroyEgg(float delay)
    {
        shouldMove = false;
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("breakEgg");
        GetComponent<AudioSource>().Play();
        Invoke("DestroyOwn", delay);

    }

    public void DestroyOwn()
    {
        Destroy(gameObject);
    }


}
