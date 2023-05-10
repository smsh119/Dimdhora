using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int life = 3;
    [SerializeField] int eggsCaught = 0;

    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI scoreText;

    //score Pop Related
    [SerializeField] GameObject scorePop;
    [SerializeField] Vector3 scorePopStartPos;

    //red Pannel Related
    [SerializeField] GameObject redPanel;
    [SerializeField] float redPanelShowTime = 1f;

    [SerializeField] bool stopDeathForDebug = false;
    private void Start()
    {
        lifeText.text = life.ToString();
        scoreText.text = "wWg ai‡Qb t " + eggsCaught.ToString();
    }

    public void EggsCaught()
    {
        eggsCaught++;
        PopScore(eggsCaught);
        //Debug.Log("Eggs Caught :" + eggsCaught);
        scoreText.text = "wWg ai‡Qb t "+ eggsCaught.ToString();
    }

    private void PopScore(int eggsCaught)
    {
        if(eggsCaught%10==0)
        {
            var obj = Instantiate(scorePop, scorePopStartPos, Quaternion.identity);
            obj.GetComponent<TextMesh>().text = eggsCaught.ToString();
            Destroy(obj, 2f);
        }
    }

    public void EggsDropped()
    {
        if (Debug.isDebugBuild && stopDeathForDebug == true) return;
        life--;
        if (life==0)
        {
            FindObjectOfType<PlayerMovement>().DestroyPlayer();
            FindObjectOfType<GameManager>().ShowMenu();
        }

        if (life >= 0)
        {
            StartCoroutine("ShowRedPanel");
            lifeText.text = life.ToString();
        }
    }

    IEnumerator ShowRedPanel()
    {
        redPanel.SetActive(true);
        yield return new WaitForSeconds(redPanelShowTime);
        redPanel.SetActive(false);
    }

    public int GetScore()
    {
        return eggsCaught;
    }
    
}
