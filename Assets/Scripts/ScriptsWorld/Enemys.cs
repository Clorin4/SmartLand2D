using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemys : MonoBehaviour
{
    int Scoree;
    public GameObject Enemy;
    public int enemyValue = 20;
    public TMP_Text textMeshScore;

    public CharacterHealt characterHealt;
    public int damageAmountt = 1;
    public Vector2 ola;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            GetScoreEnemys();
            //ANIMACION
            Destroy(Enemy, .1f);
        }

        if (collision.gameObject.tag == "Player")
        {
            characterHealt.TakeDamage(damageAmountt, ola);
            Debug.Log("Enemis teik damach AAAAA");
        }
    }

     
    public void GetScoreEnemys()
    {
        int loadScoree = PlayerPrefs.GetInt("Score");
        Scoree = loadScoree;

        SafeData.sharedInstance.score += enemyValue;
        Scoree = SafeData.sharedInstance.score;

        string textVar = Scoree.ToString();
        textMeshScore = FindAnyObjectByType<TMP_Text>();
        textMeshScore.text = "Score: " + textVar;
        Debug.Log(Scoree);
    }
}
