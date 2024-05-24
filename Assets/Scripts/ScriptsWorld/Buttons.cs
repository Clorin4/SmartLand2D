using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    private void Start()
    {
        PlayerPrefs.SetInt("GameMode", 0);
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void Jugar()
    {
        panel1.SetActive(false);
        panel2.SetActive(true);
    }

    public void Return()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
    }

    public void SinglePlayer()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        SceneManager.LoadScene(1);
    }
    public void MultiPlayer()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        SceneManager.LoadScene(1);
    }



    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit(); 
#endif
    }

}
