using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoteFrutas : MonoBehaviour
{
    private Animator objectAnim;
    public Transform player1;
    public Transform player2;
    public float interactionRadius = 3f;
    private bool isPlayer1Near = false;
    private bool isPlayer2Near = false;
    private bool isInteracting = false;
    private bool isPlayer1Interacting = false;
    private bool isPlayer2Interacting = false;
    public GameObject fruitSelectionMenu;
    public GameObject globoTextE;
    public GameObject globoTextShift;
    public TextMeshProUGUI usingText;

    private void Start()
    {
        objectAnim = GetComponent<Animator>();
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

        if (isPlayer1Near && !isInteracting && !isPlayer2Interacting)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Player 1 starts interacting.");
                PlayerPrefs.SetInt("CanMovePlayer1", 0);
                isInteracting = true;
                isPlayer1Interacting = true;
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().ShowMenu(true);
            }
        }

        if (isPlayer2Near && !isInteracting && !isPlayer1Interacting)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Debug.Log("Player 2 starts interacting.");
                PlayerPrefs.SetInt("CanMovePlayer2", 0);
                isInteracting = true;
                isPlayer2Interacting = true;
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().ShowMenu(false);
            }
        }

        if (isInteracting)
        {
            if (isPlayer1Interacting && Input.GetKeyDown(KeyCode.S))
            {
                Debug.Log("Player 1 selects fruit.");
                StartEatAnim();
                PlayerPrefs.SetInt("CanMovePlayer1", 1);
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().SelectFruit();
                fruitSelectionMenu.SetActive(false);
                isInteracting = false;
                isPlayer1Interacting = false;
                isPlayer2Interacting = false;
            }
            else if (isPlayer2Interacting && Input.GetKeyDown(KeyCode.DownArrow))
            {
                Debug.Log("Player 2 selects fruit.");
                StartEatAnim();
                PlayerPrefs.SetInt("CanMovePlayer2", 1);
                fruitSelectionMenu.GetComponent<FruitSelectionMenu>().SelectFruit();
                fruitSelectionMenu.SetActive(false);
                isInteracting = false;
                isPlayer1Interacting = false;
                isPlayer2Interacting = false;
            }
        }

        globoTextE.SetActive(isPlayer1Near && !isInteracting);
        globoTextShift.SetActive(isPlayer2Near && !isInteracting);

        if (isPlayer1Interacting)
        {
            usingText.text = "Usando: P1";
            Color colorP1;
            ColorUtility.TryParseHtmlString("#0FA8EF", out colorP1);
            usingText.color = colorP1;
        }
        else if (isPlayer2Interacting)
        {
            usingText.text = "Usando: P2";
            Color colorP2;
            ColorUtility.TryParseHtmlString("#FF453B", out colorP2);
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

        isPlayer1Near = distance1 <= interactionRadius;
        isPlayer2Near = distance2 <= interactionRadius;

        Debug.Log($"Player1 Near: {isPlayer1Near}, Distance: {distance1}");
        Debug.Log($"Player2 Near: {isPlayer2Near}, Distance: {distance2}");
    }

    public void StartEatAnim()
    {
        StartCoroutine(Eat());
    }

    IEnumerator Eat()
    {
        objectAnim.SetBool("Eat", true);
        yield return new WaitForSeconds(1.0f);
        objectAnim.SetBool("Eat", false);
    }
}

