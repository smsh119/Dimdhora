using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StartMenu : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI  highScoreText;
    void Start()
    {
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    
}
