using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gamemaster : MonoBehaviour
{
    public TextMeshProUGUI gametimertext;
    public GameObject youwin;
    
    float gametimer = 300f;
    public GameObject gameverscreen;
    public void gameover()
    {
        Time.timeScale = 0;
        gameverscreen.SetActive(true);
    }
    public void youWin()
    {
        Time.timeScale = 0;
        youwin.SetActive(true);
    }
    public void play()
    {
        
        SceneManager.LoadScene(1);
        Debug.Log("play again");
        
    }
    private void Update()
    {
        gametimer -= Time.deltaTime;
        int seconds = (int)(gametimer % 60);
        int minutes = (int)(gametimer / 60) % 60;
        int hours = (int)(gametimer / 3600) % 24;
        string timerstring = string.Format("{0:00}:{1:00}", minutes, seconds);

        gametimertext.text = timerstring;

        if (gametimer < 0)
        {
            youWin();
        }

    }

}
