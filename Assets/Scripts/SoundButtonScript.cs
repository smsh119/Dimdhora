using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{
    [SerializeField] GameObject soundButtonImage;
    [SerializeField] Sprite soundOnSprite;
    [SerializeField] Sprite soundOffSprite;
    [SerializeField] AudioSource dimDhoraAudio;

    void Start()
    {
        SetMasterVolume();
    }

    public void ChangeSoundState()
    {
        int chck = PlayerPrefs.GetInt("isSoundOn", 1);
        if (chck == 1)
        {
            PlayerPrefs.SetInt("isSoundOn", 0);
            AudioListener.volume = 0f;
            soundButtonImage.gameObject.GetComponent<Image>().sprite = soundOffSprite;
        }
        else
        {
            PlayerPrefs.SetInt("isSoundOn", 1);
            AudioListener.volume = 1f;
            soundButtonImage.gameObject.GetComponent<Image>().sprite = soundOnSprite;
            dimDhoraAudio.Play();
        }
    }

    private void SetMasterVolume()
    {
        int chck = PlayerPrefs.GetInt("isSoundOn", 1);  //to control master sound
        if (chck == 1)
        {
            soundButtonImage.gameObject.GetComponent<Image>().sprite = soundOnSprite;
            AudioListener.volume = 1f;
            dimDhoraAudio.Play();

        }
        else
        {
            soundButtonImage.gameObject.GetComponent<Image>().sprite = soundOffSprite;
            AudioListener.volume = 0f;
        }
    }

}
