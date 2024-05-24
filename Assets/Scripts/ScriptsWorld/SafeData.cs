using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SafeData : MonoBehaviour
{
    public int score;
    
    public int currentScore;
    public int health;
    public int maxhealth = 10;

    public static SafeData sharedInstance;
    

    public TMP_Text textMeshScore;
    //public TMP_Text textMeshScore2;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            
            DontDestroyOnLoad(gameObject);
        }
            
        else
        {
            Destroy(gameObject);
        }

        
    }
    

    void Start()
    {
        

        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Health", health);




        int loadHealth = PlayerPrefs.GetInt("Health");
        health = loadHealth;



        string textVar = score.ToString();
        textMeshScore = FindAnyObjectByType<TMP_Text>();
        textMeshScore.text = "Score: " + textVar;
        Debug.Log(score);

        

    }

    private void Update()
    {
        
        if (health <= 0)
        {
            Debug.Log("PASANCOSAS");
            //Animacion muerte
            score = 0;
            health = maxhealth;
            SceneManager.LoadScene(1);

        }

    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.SetInt("Health", health);
        //Debug.Log("BAI PUNTOS " + score);
        //Debug.Log("TOMA MANGO " + health);
    }

}
