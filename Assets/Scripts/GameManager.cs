using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //floating menu related
    [SerializeField] float loadMenuDelay = 2f;
    [SerializeField] TextMeshProUGUI menuScore;
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject backGround;
    [SerializeField] GameObject ovinondonFX;
    [SerializeField] Vector3 ovinondonFXPos = new Vector3(0f,5f,-3f);

    //egg transition variables
    [SerializeField] float eggSpeed = 10f;
    [SerializeField] float eggSpeedVaryingFactor = 2f; //change of speed between eggs having same eggspeed
    [SerializeField] float transitionTimeFactor = 8f; //time after which dificulty changes
    [SerializeField] float eggSpeedChangingFactor = 3f; //speed of egg to increase after dificulty transition
    [SerializeField] float maximumEggSpeed = 13;
    float transitionTime;

    //Spawner transition variables
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] float transitionTimeSpawner = 10f;   //time after which dificulty changes
    [Tooltip("SpawnDelay to reduce after transitionTime")]
    [SerializeField] float spawnDelayTransitionFactor = 0.1f; //spawn delay will reduce after every transition time
    [SerializeField] float minimumSpawnDelay = 0.4f;
    float transitionTimeIncreaseFactor;

    //Start and finishing audio related;
    [SerializeField] AudioClip[] startAndFinishAudio;

    //debugging related
    [SerializeField] bool debugPrint = false;
    [SerializeField] bool clearHighScore = false;

    private void Start()
    {
        AudioListener.volume = (float)PlayerPrefs.GetInt("isSoundOn", 1);  //to control master sound
        GetComponent<AudioSource>().PlayOneShot(startAndFinishAudio[0]); //play khela shuru audio
        //egg speed related
        transitionTime = transitionTimeFactor;
        //spawner related
        transitionTimeIncreaseFactor = transitionTimeSpawner;  //it is just to control the spawn delay
        if (clearHighScore && Debug.isDebugBuild) PlayerPrefs.SetInt("HighScore", 0);
    }

    private void Update()
    {
        DifficultyTransition();
        if(Debug.isDebugBuild && debugPrint)
        {
            Debug.Log("spawnDelay = " + spawnDelay);
            Debug.Log("eggspeed = " + eggSpeed);
        }
    }

    //difficulty transition related methods
    private void DifficultyTransition()
    {
        float time = Time.timeSinceLevelLoad;
        if (time > transitionTime && eggSpeed <= maximumEggSpeed - eggSpeedChangingFactor)
        {
            transitionTime += transitionTimeFactor;
            eggSpeed += eggSpeedChangingFactor;
        }
        if (time > transitionTimeSpawner && spawnDelay >= minimumSpawnDelay)
        {
            spawnDelay -= spawnDelayTransitionFactor;
            transitionTimeSpawner += transitionTimeIncreaseFactor;
        }
    }

    public float GetSpawnDelay()
    {
        return spawnDelay;
    }

    public float GetEggSpeed()
    {
        return eggSpeed;
    }

    public float GetMaxEggSpeed()
    {
        return maximumEggSpeed;
    }

    public float GetEggSpeedVaryingFactor()
    {
        return eggSpeedVaryingFactor;
    }

    //floating menu loading methods
    public void ShowMenu()
    {
        FindObjectOfType<EggSpawner>().instantiate = false;
        GetComponent<AudioSource>().PlayOneShot(startAndFinishAudio[1]); //play khela sesh audio
        backGround.GetComponent<AudioSource>().enabled = false;
        LoadMenu();
    }

    private void LoadMenu()
    {
        int score = FindObjectOfType<ScoreManager>().GetScore();
        SetHighScore(score);
        StartCoroutine("Wait",score);
    }

    private void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetHighScore(int score)
    {
        float highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            var obj = Instantiate(ovinondonFX, ovinondonFXPos, Quaternion.identity);
            Destroy(obj, loadMenuDelay);
        }
    }

    IEnumerator Wait(int score)
    {
        yield return new WaitForSeconds(loadMenuDelay);
        menuCanvas.SetActive(true);
        menuScore.text = score.ToString();
    }


}
