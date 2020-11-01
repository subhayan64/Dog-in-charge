using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{

    private AudioManager2 audiomanager;

    private void Start()
    {
        audiomanager = AudioManager2.instance;
        if (audiomanager == null)
        {
            Debug.LogError("No audio manager in the scene");
        }

        audiomanager.Play("theme");
    }
    public void play()
    {

        SceneManager.LoadScene(1);        

    }
}
