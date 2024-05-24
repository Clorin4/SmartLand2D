using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterHealt : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public GameObject PLAYER;

    public GameObject[] Hearts = new GameObject[10];
    public GameObject[] EmptyHearts = new GameObject[10];

    private PlayerController movimientojugador;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;

    [SerializeField] private AudioClip hurt;

    private void Awake()
    {
        //currentHealth = maxHealth;
        int loadHealth = PlayerPrefs.GetInt("Health");
        //currentHealth = loadHealth;
        currentHealth = SafeData.sharedInstance.health;
    }

    private void Start()
    {

        //currentHealth = maxHealth;
        //int loadHealth = PlayerPrefs.GetInt("Health");
        //currentHealth = loadHealth;
        //currentHealth = maxHealth;

        //Debug.Log("SISISISII " + currentHealth);
        
        movimientojugador = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        

        UIKokoros();
        if (currentHealth <= 0)
        {
            Debug.Log("AAAAAAAAAAAAAAA");
            //Animacion muerte
            currentHealth = maxHealth;
            SceneManager.LoadScene(1);
            
        }
    }

    public void TakeDamage(int damageAmount, Vector2 posicion)
    {
        SoundManager.Instance.EjecutarSonido(hurt);

        SafeData.sharedInstance.health -= damageAmount;
        currentHealth = SafeData.sharedInstance.health;
        Debug.Log("VIDA" + currentHealth);
        //currentHealth -= damageAmount;
        animator.SetTrigger("Golpe");
        StartCoroutine(LoseControl());
        movimientojugador.Rebote(posicion);
    }

    private IEnumerator LoseControl()
    {
        movimientojugador.canMove = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientojugador.canMove = true;
    }


    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        //cosas d vida
        
    }

    public void UIKokoros()
    {
        
        if (currentHealth == 9)
        {
            Hearts[9].SetActive(false);
            EmptyHearts[9].SetActive(true);
        }
        if (currentHealth == 8)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
        }
        if (currentHealth == 7)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
        }
        if (currentHealth == 6)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
        }
        if (currentHealth == 5)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
        }
        if (currentHealth == 4)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            Hearts[4].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
            EmptyHearts[4].SetActive(true);
        }
        if (currentHealth == 3)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            Hearts[4].SetActive(false);
            Hearts[3].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
            EmptyHearts[4].SetActive(true);
            EmptyHearts[3].SetActive(true);
        }
        if (currentHealth == 2)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            Hearts[4].SetActive(false);
            Hearts[3].SetActive(false);
            Hearts[2].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
            EmptyHearts[4].SetActive(true);
            EmptyHearts[3].SetActive(true);
            EmptyHearts[2].SetActive(true);
        }
        if (currentHealth == 1)
        {
            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            Hearts[4].SetActive(false);
            Hearts[3].SetActive(false);
            Hearts[2].SetActive(false);
            Hearts[1].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
            EmptyHearts[4].SetActive(true);
            EmptyHearts[3].SetActive(true);
            EmptyHearts[2].SetActive(true);
            EmptyHearts[1].SetActive(true);
        }
        if (currentHealth == 0)
        {

            Hearts[9].SetActive(false);
            Hearts[8].SetActive(false);
            Hearts[7].SetActive(false);
            Hearts[6].SetActive(false);
            Hearts[5].SetActive(false);
            Hearts[4].SetActive(false);
            Hearts[3].SetActive(false);
            Hearts[2].SetActive(false);
            Hearts[1].SetActive(false);
            Hearts[0].SetActive(false);
            EmptyHearts[9].SetActive(true);
            EmptyHearts[8].SetActive(true);
            EmptyHearts[7].SetActive(true);
            EmptyHearts[6].SetActive(true);
            EmptyHearts[5].SetActive(true);
            EmptyHearts[4].SetActive(true);
            EmptyHearts[3].SetActive(true);
            EmptyHearts[2].SetActive(true);
            EmptyHearts[1].SetActive(true);
            EmptyHearts[0].SetActive(true);
        }


    }

}
