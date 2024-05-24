using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoteFrutas : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float interactionRadius = 3f;
    private bool isPlayer1Near = false; // Indica si el jugador 1 está cerca
    private bool isPlayer2Near = false; // Indica si el jugador 2 está cerca
    private bool isInteracting = false;
    private bool isPlayer1Interacting = false; // Indica si el jugador 1 está interactuando
    private bool isPlayer2Interacting = false; // Indica si el jugador 2 está interactuando
    public GameObject fruitSelectionMenu;
    public GameObject globoTextE;
    public GameObject globoTextShift;
    public TextMeshProUGUI usingText; // TextMeshPro para mostrar el mensaje de quién está usando el menú

    private void Start()
    {
        PlayerPrefs.SetInt("CanMovePlayer1", 1);
        PlayerPrefs.SetInt("CanMovePlayer2", 1);
        StartCoroutine(BuscarPlayers());
    }

    IEnumerator BuscarPlayers()
    {
        yield return new WaitForSeconds(.001f);
        player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
    }

    void Update()
    {
        FuncBuscarPlayers();

        // Si el jugador 1 está cerca y no está interactuando ni el jugador 2, y no hay nadie interactuando
        if (isPlayer1Near && !isInteracting && !isPlayer2Interacting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                PlayerPrefs.SetInt("CanMovePlayer1", 0);
                isInteracting = true;
                isPlayer1Interacting = true;
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().ShowMenu(true);
            }
        }

        // Si el jugador 2 está cerca y no está interactuando ni el jugador 1, y no hay nadie interactuando
        if (isPlayer2Near && !isInteracting && !isPlayer1Interacting)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                PlayerPrefs.SetInt("CanMovePlayer2", 0);
                isInteracting = true;
                isPlayer2Interacting = true;
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().ShowMenu(false);
            }
        }

        // Si hay un jugador interactuando y presiona la tecla para seleccionar la fruta
        if (isInteracting)
        {
            if (isPlayer1Interacting && Input.GetKeyDown(KeyCode.S))
            {
                PlayerPrefs.SetInt("CanMovePlayer1", 1);
                //PlayerPrefs.SetInt("CanMovePlayer2", 1);
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().SelectFruit();
                fruitSelectionMenu.SetActive(false); // Desactiva el menú de selección de frutas
                isInteracting = false;
                isPlayer1Interacting = false;
                isPlayer2Interacting = false;
            }
            else if (isPlayer2Interacting && Input.GetKeyDown(KeyCode.DownArrow))
            {
                PlayerPrefs.SetInt("CanMovePlayer2", 1);
                //PlayerPrefs.SetInt("CanMovePlayer2", 1);
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().SelectFruit();
                fruitSelectionMenu.SetActive(false); // Desactiva el menú de selección de frutas
                isInteracting = false;
                isPlayer1Interacting = false;
                isPlayer2Interacting = false;

            }
        }

        // Gestión de los globos de texto
        globoTextE.SetActive(isPlayer1Near && !isInteracting);
        globoTextShift.SetActive(isPlayer2Near && !isInteracting);

        if (isPlayer1Interacting)
        {
            usingText.text = "Usando: P1";
            Color colorP1;
            ColorUtility.TryParseHtmlString("#0FA8EF", out colorP1); // Azul en hexadecimal
            usingText.color = colorP1;
        }
        else if (isPlayer2Interacting)
        {
            usingText.text = "Usando: P2";
            Color colorP2;
            ColorUtility.TryParseHtmlString("#FF453B", out colorP2); // Rojo en hexadecimal
            usingText.color = colorP2;
        }
        else
        {
            usingText.text = "";
        }
    }

    public void FuncBuscarPlayers()
    {
        float distance1 = Vector3.Distance(transform.position, player1.position);
        float distance2 = Vector3.Distance(transform.position, player2.position);

        // Comprueba si el jugador 1 está dentro del radio de interacción.
        isPlayer1Near = distance1 <= interactionRadius;

        // Comprueba si el jugador 2 está dentro del radio de interacción.
        isPlayer2Near = distance2 <= interactionRadius;
    }
}
