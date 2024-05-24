using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text textMeshScore;
    int Score;
    //string PlayerName;
    
    public GameObject Diamanco;
    public int coinValue = 50;

    [SerializeField] private AudioClip colect1;
    

    private void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 2)
        {
            gameObject.SetActive(false);
        }
    }

     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
       
        
        if (collision.gameObject.tag == "Player")
        {
            SoundManager.Instance.EjecutarSonido(colect1);
            
            GetScore();

            Destroy(Diamanco, .1f);

        }

    }

    public void GetScore()
    {
        
        int loadScore = PlayerPrefs.GetInt("Score");
        Score = loadScore;

        SafeData.sharedInstance.score += coinValue;
        Score = SafeData.sharedInstance.score;
        Debug.Log(Score);

        string textVar = Score.ToString();
        textMeshScore = FindAnyObjectByType<TMP_Text>();
        textMeshScore.text = "Score: " + textVar;

    }

}
