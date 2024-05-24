using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RunningGame : MonoBehaviour
{
    public SpriteRenderer sprite3Renderer;
    public SpriteRenderer sprite2Renderer;
    public SpriteRenderer sprite1Renderer;
    public SpriteRenderer spriteAdelanteRenderer;
    public SpriteRenderer spriteFinishRenderer;

    private Coroutine countdownCoroutine; 
    private Coroutine showPhraseCoroutine; 
    private Coroutine playerTurnTimerCoroutine;

    public GameObject howToPlay;
    public GameObject canvasMain;

    public GameObject WinnerP1;
    public GameObject WinnerP2;
    public GameObject panelFrases;
    public TextMeshProUGUI textPanel; // frase
    public TMP_InputField inputField;
    public Button submitButton;
    private string currentRandomPhrase;

    public PhraseManager phraseManager;
    PhraseData selectedPhraseData;
    float phraseTime;
    string[] phrases;

    private bool isTimerRunning = false;
    private float elapsedTime = 0f;

    private bool avanzamo = false;
    private bool avanzamo2 = false;
    private bool isPlayer1Turn = true; // Variable para controlar los turnos
    private int currentPlayer = 1; // Variable para identificar el jugador actual

    int i = -10;
    int j = -10;

    public GameObject apuntadorP1;
    public GameObject apuntadorP2;
    public TextMeshProUGUI turnoText;

    int zonaP1;
    int zonaP2;
    bool alguienGano = false;
    public GameObject[] P1ScoreUI;
    public GameObject[] P2ScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        submitButton.onClick.AddListener(SubmitAnswerWithoutParameter);
        TurnOffVariables();
        SaberDificultad();

        for (int i = 1; i <= 5; i++)
        {
            P1ScoreUI[i].SetActive(false);
            P2ScoreUI[i].SetActive(false);
        }
    }

    public void ButtonStartGame()
    {
        canvasMain.SetActive(true);
        howToPlay.SetActive(false);
        StartCoroutine(Countdown());
    }

    private void SubmitAnswerWithoutParameter()
    {
        SubmitAnswer(currentRandomPhrase); // Llama a SubmitAnswer con el parámetro almacenado
    }


    public void TurnOffVariables()
    {
        canvasMain.SetActive(false);
        howToPlay.SetActive(true);
        WinnerP1.SetActive(false);
        WinnerP2.SetActive(false);
        isPlayer1Turn = true;
        apuntadorP1.SetActive(true);
        apuntadorP2.SetActive(false);
        panelFrases.SetActive(false);
        sprite3Renderer.gameObject.SetActive(false);
        sprite2Renderer.gameObject.SetActive(false);
        sprite1Renderer.gameObject.SetActive(false);
        spriteAdelanteRenderer.gameObject.SetActive(false);
        spriteFinishRenderer.gameObject.SetActive(false);
    }

    public void SaberDificultad()
    {
        DifficultyLevel selectedDifficulty;

        string selectedDifficultyy = PlayerPrefs.GetString("SelectedDifficulty");
        switch (selectedDifficultyy)
        {
            case "dif1":
                selectedDifficulty = DifficultyLevel.Easy;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            case "dif2":
                selectedDifficulty = DifficultyLevel.Normal;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            case "dif3":
                selectedDifficulty = DifficultyLevel.Hard;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            case "dif4":
                selectedDifficulty = DifficultyLevel.Insane;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            case "dif5":
                selectedDifficulty = DifficultyLevel.Demon;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            case "dif6":
                selectedDifficulty = DifficultyLevel.SuperDemon;
                selectedPhraseData = phraseManager.GetPhraseDataByDifficulty(selectedDifficulty);
                break;

            default:
                // Manejar una dificultad inesperada
                break;
        }

        // Obtener datos de frases y tiempos para un nivel de dificultad específico (por ejemplo, "Easy")



        if (selectedPhraseData != null)
        {
            phraseTime = selectedPhraseData.phraseTime;
            phrases = selectedPhraseData.phrases;
        }
        else
        {
            Debug.LogWarning("No se encontraron datos para la dificultad seleccionada.");
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f); 
        

        sprite3Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite3Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite3Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite2Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite2Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite2Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite1Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite1Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tamaño específico
        sprite1Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        spriteAdelanteRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteAdelanteRenderer, Vector3.zero, Vector3.one * .5f, .9f); // Escalar de 0 a un tamaño específico
        spriteAdelanteRenderer.gameObject.SetActive(false);

        // Lógica para iniciar el juego después de la cuenta regresiva
        StartGame();
    }

    IEnumerator ScaleSpriteTo(SpriteRenderer spriteRenderer, Vector3 startScale, Vector3 endScale, float duration)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            spriteRenderer.transform.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        spriteRenderer.transform.localScale = endScale;
    }

    public void StartGame()
    {
        StartCoroutine(ShowRandomPhrase());
        SelectInputField();
    }

    IEnumerator ShowRandomPhrase()
    {
        panelFrases.SetActive(true);
        panelFrases.transform.localScale = Vector3.one; // Establecer el tamaño inicial

        iTween.ScaleFrom(panelFrases, Vector3.zero, 1f);

        inputField.gameObject.SetActive(true);

        if (selectedPhraseData != null)
        {
            currentRandomPhrase = phrases[Random.Range(0, phrases.Length)];
            textPanel.text = currentRandomPhrase;

            isTimerRunning = true;
            elapsedTime = 0f;

            while (elapsedTime < phraseTime)
            {
                if (!isTimerRunning) break;

                elapsedTime += Time.deltaTime;
                //Debug.Log(elapsedTime);
                yield return null;
            }
            isTimerRunning = false;

            if (isTimerRunning)
            {
                textPanel.text = "";
                inputField.onEndEdit.AddListener(delegate { SubmitAnswer(currentRandomPhrase); });
            }
            else
            {
                if (!alguienGano)
                {
                    
                    Debug.Log("Tiempo agotado para el Jugador " + currentPlayer);
                    EndCurrentTurn();
                }
                
            }
        }
        else
        {
            Debug.LogWarning("No se encontraron datos para la dificultad seleccionada.");
        }
    }

    void EndCurrentTurn()
    {
        SelectInputField();
        inputField.text = "";

        if (isPlayer1Turn)
        {
            isPlayer1Turn = false;
            currentPlayer = 2;
            apuntadorP1.SetActive(false);
            apuntadorP2.SetActive(true);
        }
        else
        {
            isPlayer1Turn = true;
            currentPlayer = 1;
            apuntadorP1.SetActive(true);
            apuntadorP2.SetActive(false);
        }

        Debug.Log("Turno del Jugador " + currentPlayer);
        ReiniciarJuego();
    }

    private void SubmitAnswer(string randomPhrase)
    {
        SelectInputField();
        randomPhrase = textPanel.text;
        string playerTypedPhrase = inputField.text;

        if (playerTypedPhrase == randomPhrase)
        {
            if (isPlayer1Turn)
            {
                inputField.text = "";
                inputField.DeactivateInputField();
                avanzamo = true;
                Debug.Log("¡Correcto! Jugador " + currentPlayer);

            }
            else
            {
                inputField.text = "";
                inputField.DeactivateInputField();
                avanzamo2 = true;
                Debug.Log("¡Correcto! Jugador " + currentPlayer);
            }
            
        }
        else
        {
            if (!alguienGano && inputField.text != "")
            {
                inputField.text = "";
                inputField.DeactivateInputField();
                Debug.Log("¡Incorrecto! Jugador " + currentPlayer);
                EndCurrentTurn();
            }

        }
    }
    private void Update()
    {
        RunAnimations();

        if (isPlayer1Turn)
        {
            turnoText.text = "Turno:  jugador 1";
            Color colorP1;
            ColorUtility.TryParseHtmlString("#0FA8EF", out colorP1); // Azul en hexadecimal
            turnoText.color = colorP1;
        }
        else
        {
            turnoText.text = "Turno:  jugador 2";
            Color colorP1;
            ColorUtility.TryParseHtmlString("#FF453B", out colorP1); // Azul en hexadecimal
            turnoText.color = colorP1;
        }
    }

    private void RunAnimations()
    {
        PlayerAnimatorController[] playerControllers = FindObjectsOfType<PlayerAnimatorController>();

        foreach (var playerController in playerControllers)
        {
            if (zonaP1 < 5 && zonaP2 < 5)
            {
                
                if (playerController.playerTag == "Player1" && avanzamo)
                {
                    zonaP1++;
                    Debug.Log("PLAYER 1 " + zonaP1);
                    avanzamo = false;
                    playerController.StartRunningAnimation();
                    StartCoroutine(AdvancePlayer1Grid());

                }

                else if (playerController.playerTag == "Player2" && avanzamo2)
                {
                    zonaP2++;
                    Debug.Log("PLAYER 2 " + zonaP2);
                    avanzamo2 = false;
                    playerController.StartRunningAnimation();
                    StartCoroutine(AdvancePlayer2Grid());
                }
                ScoreUI();
            }
            else
            {
                StartCoroutine(Finish());
                alguienGano = true;

                if (playerController.playerTag == "Player2" && zonaP1 == 5)
                {
                    playerController.StartLoseAnimation();
                    WinnerP1.SetActive(true);
                }
                else if (playerController.playerTag == "Player1" && zonaP1 == 5)
                {
                    playerController.StartVictoryAnimation();
                }

                else if (playerController.playerTag == "Player1" && zonaP2 == 5)
                {
                    playerController.StartLoseAnimation();
                    WinnerP2.SetActive(true);
                }
                else if (playerController.playerTag == "Player2" && zonaP2 == 5)
                {
                    playerController.StartVictoryAnimation();
                }
                ScoreUI();
            }
        }

    }

    public void ScoreUI()
    {
        int indexP1 = zonaP1;
        int indexP2 = zonaP2;

        if (indexP1 != 0)
        {
            P1ScoreUI[indexP1].SetActive(true);
            P1ScoreUI[indexP1 - 1].SetActive(false);
        }
        if (indexP2 != 0)
        {
            P2ScoreUI[indexP2].SetActive(true);
            P2ScoreUI[indexP2 - 1].SetActive(false);
        }
    }

    IEnumerator Finish()
    {
        spriteFinishRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteFinishRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tamaño específico

    }

    IEnumerator AdvancePlayer1Grid()
    {
        //float velocidadMovimiento = 1.0f; // Modifica este valor según la velocidad deseada
        int distanciaEntreMovimientos = 10; // Distancia a moverse entre cada avance
        float tiempoDeMovimiento = 1.0f; // Tiempo que tarda el movimiento

        GameObject gridObject = GameObject.Find("GridP1");

        if (gridObject != null)
        {
            Vector3 destinoPos = new Vector3(i, 0.0f, 0.0f); // Posición de destino

            float tiempoTranscurrido = 0.0f;
            Vector3 posiciónInicial = gridObject.transform.position;

            while (tiempoTranscurrido < tiempoDeMovimiento)
            {
                tiempoTranscurrido += Time.deltaTime;
                float t = tiempoTranscurrido / tiempoDeMovimiento;

                gridObject.transform.position = Vector3.Lerp(posiciónInicial, destinoPos, t);

                yield return null;
            }

            gridObject.transform.position = destinoPos; // Asegura que termine en la posición exacta
            i -= distanciaEntreMovimientos; // Actualiza la posición del grid
            Debug.Log(i);

        }

        if (!alguienGano)
        {
            EndCurrentTurn();
            
        }


    }

    void SelectInputField()
    {
        // Activa y selecciona el InputField
        inputField.ActivateInputField();
        inputField.Select();
    }

    IEnumerator AdvancePlayer2Grid()
    {
        int distanciaEntreMovimientos = 10; // Distancia a moverse entre cada avance
        float tiempoDeMovimiento = 1.0f; // Tiempo que tarda el movimiento

        GameObject gridObject = GameObject.Find("GridP2");

        if (gridObject != null)
        {
            Vector3 destinoPos = new Vector3(j, 0.0f, 0.0f); // Posición de destino

            float tiempoTranscurrido = 0.0f;
            Vector3 posiciónInicial = gridObject.transform.position;

            while (tiempoTranscurrido < tiempoDeMovimiento)
            {
                tiempoTranscurrido += Time.deltaTime;
                float t = tiempoTranscurrido / tiempoDeMovimiento;

                gridObject.transform.position = Vector3.Lerp(posiciónInicial, destinoPos, t);

                yield return null;
            }

            gridObject.transform.position = destinoPos; // Asegura que termine en la posición exacta
            j -= distanciaEntreMovimientos; // Actualiza la posición del grid
            Debug.Log(j);

        }

        if (!alguienGano)
        {
            EndCurrentTurn();

        }
    }


    private void ReiniciarJuego()
    {
        
        // Detener las corrutinas activas si es que están ejecutándose
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        if (showPhraseCoroutine != null)
        {
            StopCoroutine(showPhraseCoroutine);
        }

        if (playerTurnTimerCoroutine != null)
        {
            StopCoroutine(playerTurnTimerCoroutine);
        }
        StopCoroutine(AdvancePlayer1Grid());

        //avanzamo = false;
        panelFrases.SetActive(false);
        showPhraseCoroutine = StartCoroutine(ShowRandomPhrase()); // Iniciar la siguiente frase directamente
    }


    public void Salir()
    {
        SceneManager.LoadScene(1);
    }

}
