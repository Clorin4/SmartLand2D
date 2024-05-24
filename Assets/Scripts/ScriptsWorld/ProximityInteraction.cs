using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ProximityInteraction : MonoBehaviour
{
    public float interactionRadius = 3f; // Radio de interacción.
    public LayerMask interactionLayer; // Capa de objetos con los que puedes interactuar.

    private Transform player; // Referencia al transform del jugador.
    private bool isInRange = false; // Indica si el jugador está en rango de interacción.
    private bool isInteracting = false;
    private bool isOnClickedE = false;

    public PlayerController PC;

    public GameObject globoText1;
    public GameObject globoText2;
    public TextMeshProUGUI text;

    int gameIndex; 

    private void Start()
    {
        // Busca el objeto con la etiqueta "Player" y obtiene su Transform.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        globoText1.SetActive(false);
        globoText2.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Calcula la distancia entre este objeto y el jugador.
        float distance = Vector3.Distance(transform.position, player.position);

        // Comprueba si el jugador está dentro del radio de interacción.
        if (distance <= interactionRadius)
        {
            isInRange = true;

            if (!isOnClickedE)
            {
                globoText1.SetActive(true);
            }
            
            if (isInRange == true && Input.GetKeyDown(KeyCode.E))
            {
                globoText1.SetActive(false);
                isOnClickedE = true;
                Interact();
            }
            
        }
        else if (!isInRange == true && isInteracting == true)
        {
            isOnClickedE = false;
            globoText1.SetActive(false);
            globoText2.SetActive(false);
            text.gameObject.SetActive(false);
            isInteracting = false;
        }
        else
        {
            isOnClickedE = false;
            isInRange = false;
            globoText1.SetActive(false);
            globoText2.SetActive(false);
            text.gameObject.SetActive(false);
        }
    }



    private void OnDrawGizmosSelected()
    {
        // Dibuja un gizmo en el editor para visualizar el radio de interacción.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }

    private void Interact()
    {
        // Este método se llama cuando el jugador interactúa con el objeto o NPC.
        Debug.Log("E");
        

        //globoText1.SetActive(false);
        globoText2.SetActive(true);
        text.gameObject.SetActive(true);

        if (!isInteracting)
        {
            text.gameObject.SetActive(true);
            isInteracting = true;
        }
        else if(isInteracting && PC.O == 1)
        {
            text.gameObject.SetActive(false);
            isInteracting = false;

            gameIndex = 1;
            Debug.Log(gameIndex);

            PlayerPrefs.SetInt("gameIndex", gameIndex);
            PlayerPrefs.Save();

            SceneManager.LoadScene(2);
        }
        else if (isInteracting && PC.O == 2)
        {
            text.gameObject.SetActive(false);
            isInteracting = false;

            gameIndex = 2;
            Debug.Log(gameIndex);

            PlayerPrefs.SetInt("gameIndex", gameIndex);
            PlayerPrefs.Save();

            SceneManager.LoadScene(2);
        }
        else if (isInteracting && PC.O == 3)
        {
            text.gameObject.SetActive(false);
            isInteracting = false;

            gameIndex = 3;
            Debug.Log(gameIndex);

            PlayerPrefs.SetInt("gameIndex", gameIndex);
            PlayerPrefs.Save();

            SceneManager.LoadScene(2);
        }
        else if (isInteracting && PC.O == 4)
        {
            text.gameObject.SetActive(false);
            isInteracting = false;

            gameIndex = 4;
            Debug.Log(gameIndex);

            PlayerPrefs.SetInt("gameIndex", gameIndex);
            PlayerPrefs.Save();

            SceneManager.LoadScene(2);
        }
    }
}
