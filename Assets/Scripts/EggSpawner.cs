using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public bool instantiate = true;

    [SerializeField] GameObject egg;
    [SerializeField] GameObject eggRotten;
    [SerializeField] GameObject[] chickens;
    [SerializeField] Animator[] chickenAnimators;

    float spawnDelay;
    
    void Start()
    {
        spawnDelay = FindObjectOfType<GameManager>().GetSpawnDelay();
        StartCoroutine(LayEgg());
    }
    IEnumerator LayEgg()
    {
        while(instantiate)
        {
            int rand = Random.Range(0, 5);
            spawnDelay = FindObjectOfType<GameManager>().GetSpawnDelay();
            yield return new WaitForSeconds(spawnDelay);
            chickenAnimators[rand].Play("ChickenAnim 0");
            InstantiateEgg(rand);
        }

    }

    private void InstantiateEgg(int rand)
    {
        int rnd = Random.Range(0, 10);
        if (rnd == 7)
        {
            Instantiate(eggRotten, chickens[rand].transform.position, Quaternion.identity); // eggrotten == chicken shit
        }
        else
        {
            Instantiate(egg, chickens[rand].transform.position, Quaternion.identity);
        }
    }
}
