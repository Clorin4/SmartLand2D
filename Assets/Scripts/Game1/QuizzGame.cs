using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QuizzGame : MonoBehaviour
{
    #region  VARIABLEEEES

    public NewBehaviourScript questionData; // Referencia al nuevo script de dificultad

    public Transform[] spawnPoints;

    public GameObject[] P1Hearts = new GameObject[10];
    public GameObject[] P2Hearts = new GameObject[10];
    public int arrindex1 = 9;
    public int arrindex2 = 9;
    //public GameObject[] P1HalfHearts = new GameObject[10];
    //public GameObject[] P2HalfHearts = new GameObject[10];

    public SpriteRenderer sprite3Renderer;
    public SpriteRenderer sprite2Renderer;
    public SpriteRenderer sprite1Renderer;
    public SpriteRenderer spriteAdelanteRenderer;
    public SpriteRenderer spriteFinishRenderer;

    public GameObject apuntador1;
    public GameObject apuntador2;

    public GameObject Reloj;

    public GameObject panelQuestion; // El panel que contiene la pregunta y los botones
    public float panelScaleDuration = 1.0f;

    public Canvas canvasMaster;
    public Canvas canvasWinners;
    public GameObject panelP1Winner;
    public GameObject panelP2Winner;
    public GameObject panelEmpate;
    public GameObject canvasPausa;
    public Canvas howToPlay;

    public bool J1Responde;
    public bool J2Responde;
    public bool venganza;

    private bool J1Dañado;
    private bool J2Dañado;
    private bool dañoPaDos;
    //private bool halfHeart;

    private bool player1Pressed;
    private bool player2Pressed;
    private bool countDownStarted;
    private int secondCountDownStarted = 1;
    public float countdownTimer;

    public GameObject teclaD;
    public GameObject teclaK;

    //public QuestionManager questionManager;
    public Question currentQuestion;
    private List<Question> questions; // Lista de preguntas

    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    int correctButtonIndex = -1;

    public int player1Health = 100;
    public int player2Health = 100;


    #endregion
    private void Start()
    {
        howToPlay.gameObject.SetActive(true);
        TurnOffVariables();

        SaberDificultad();
    }

    public void ButtonStart()
    {
        howToPlay.gameObject.SetActive(false);
        canvasPausa.SetActive(true);
        StartCoroutine(Countdown());
    }

    public void TurnOffVariables()
    {
        Reloj.SetActive(false);
        panelQuestion.SetActive(false);
        canvasWinners.gameObject.SetActive(false);
        panelP1Winner.SetActive(false);
        panelP2Winner.SetActive(false);
        panelEmpate.SetActive(false);
        canvasPausa.SetActive(false);

        for (int i = 0; i < 10; i++)
        {
            P1Hearts[i].SetActive(true);
            P2Hearts[i].SetActive(true);
            
        }

        apuntador1.SetActive(false);
        apuntador2.SetActive(false);

        sprite3Renderer.gameObject.SetActive(false);
        sprite2Renderer.gameObject.SetActive(false);
        sprite1Renderer.gameObject.SetActive(false);
        spriteAdelanteRenderer.gameObject.SetActive(false);
        spriteFinishRenderer.gameObject.SetActive(false);

        J1Responde = false;
        J2Responde = false;
        venganza = false;

        J1Dañado = false;
        J2Dañado = false;
        dañoPaDos = false;
        //halfHeart = false;

        teclaD.SetActive(false);
        teclaK.SetActive(false);



    }

    public void SaberDificultad()
    {
        string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
        DifficultyLeveln selectedLevel = questionData.difficultyLevels.Find(level => level.name == selectedDifficulty);

        if (selectedLevel != null)
        {
            questions = new List<Question>();
            foreach (Materia materia in selectedLevel.materias)
            {
                questions.AddRange(materia.preguntas);
            }
        }
        else
        {
            Debug.LogError("Dificultad no encontrada: " + selectedDifficulty);
        }
    }

    public void GetRandomQuestion()
    {
        if (questions != null && questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questions.Count);
            currentQuestion = questions[randomIndex];
            StartCoroutine(ShowQuestionAndAnswers());
        }
        else
        {
            Debug.LogError("No hay preguntas disponibles.");
        }
    }


    IEnumerator Countdown()
    {
        //yield return new WaitForSeconds(.3f);

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
        yield return ScaleSpriteTo(spriteAdelanteRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tamaño específico
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

    IEnumerator Finish()
    {
        spriteFinishRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteFinishRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tamaño específico

    }


    public void StartGame()
    {
        StartCoroutine(ShowQuestionPanel());
        StartCoroutine(DetectKeyPress());
    }

    IEnumerator DetectKeyPress()
    {
        yield return new WaitForSeconds(4f);

        countDownStarted = false;
        float countdownTimer = 8f;
        while (countdownTimer > 0f && !countDownStarted)
        {
            //Reloj.SetActive(true);
            teclaD.SetActive(true);
            teclaK.SetActive(true);
            

            if (Input.GetKeyDown(KeyCode.D) && !player1Pressed)
            {
                player1Pressed = true;
                countDownStarted = true;
                
                // Realizar acciones para el jugador 1 cuando presiona la tecla D
            }

            if (Input.GetKeyDown(KeyCode.K) && !player2Pressed)
            {
                player2Pressed = true;
                countDownStarted = true;
                
                // Realizar acciones para el jugador 2 cuando presiona la tecla K
            }

            countdownTimer -= Time.deltaTime;
            yield return null;
        }

        teclaD.SetActive(false);
        teclaK.SetActive(false);
        

        DetermineWinner();

    } 

    void DetermineWinner() //definir banderas de jugadores
    {
        EnableArrows();

        if (player1Pressed && !player2Pressed)
        {
            J1Responde = true;
            secondCountDownStarted = 1;
            EnableAnswerButtons();
            Debug.Log("GANA EL 1");
            // Acciones si solo el jugador 1 presionó más rápido
        }
        else if (!player1Pressed && player2Pressed)
        {
            J2Responde = true;
            secondCountDownStarted = 1;
            EnableAnswerButtons();
            Debug.Log("GANA EL 2");
            // Acciones si solo el jugador 2 presionó más rápido
        }
        else if (player1Pressed && player2Pressed)
        {
            dañoPaDos = true;
            secondCountDownStarted = 3;
            player1Health -= 10;
            player2Health -= 10;
            Reloj.SetActive(false);
            Daños();
            Debug.Log("Ambos?");
            
            // Acciones si ambos jugadores presionaron, se puede considerar un empate
        }
        else
        {
            secondCountDownStarted = 0;
            Debug.Log("Ninguno");
            Reloj.SetActive(false);
            player1Health -= 10;
            player2Health -= 10;
            dañoPaDos = true;
            StartCoroutine(ChangeButtonColorBack());
        }
    }

    void EnableArrows()
    {
        if (player1Pressed)
        {
            apuntador1.SetActive(true);
            apuntador2.SetActive(false);
        }
        else if (player2Pressed)
        {
            apuntador2.SetActive(true);
            apuntador1.SetActive(false);
        }
        
    }

    IEnumerator ShowQuestionPanel()
    {
        // Mostrar el panel usando iTween (escala desde 0 a 1)
        panelQuestion.SetActive(true);
        iTween.ScaleFrom(panelQuestion, Vector3.zero, panelScaleDuration);

        // Deshabilitar los botones hasta que un jugador presione su tecla
        DisableAnswerButtons();

        // Esperar a que un jugador presione su tecla
        yield return ShowQuestionAndAnswers();
    }

    IEnumerator ShowQuestionAndAnswers()
    {
        if (PlayerPrefs.GetString("gameStyle") == "survival")
        {
            if (secondCountDownStarted == 1)
            {
                countdownTimer = 12f;
            }
            if (countdownTimer > 0f)
            {
                
                // Obtener una pregunta aleatoria de la lista
                int randomIndex = Random.Range(0, questions.Count);
                currentQuestion = questions[randomIndex];

                // Mostrar la pregunta en el TextMeshPro
                questionText.text = currentQuestion.questionText;

                List<string> answers = new List<string>(currentQuestion.options);
                List<string> displayedAnswers = new List<string>();

                // Añadir la respuesta correcta a las respuestas mostradas
                displayedAnswers.Add(answers[currentQuestion.correctAnswerIndex]);
                answers.RemoveAt(currentQuestion.correctAnswerIndex);

                // Mostrar las respuestas incorrectas en los botones restantes
                for (int i = 0; i < answerButtons.Length - 1; i++)
                {
                    int randomAnswerIndex = Random.Range(0, answers.Count);
                    displayedAnswers.Add(answers[randomAnswerIndex]);
                    answers.RemoveAt(randomAnswerIndex);
                }

                // Mezclar las respuestas mostradas
                displayedAnswers = ShuffleList(displayedAnswers);

                // Asignar las respuestas a los botones y añadir listeners
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    int buttonIndex = i; // Captura el índice del botón
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayedAnswers[i];
                    answerButtons[i].onClick.RemoveAllListeners();

                    if (displayedAnswers[i] == currentQuestion.options[currentQuestion.correctAnswerIndex])
                    {
                        correctButtonIndex = i; // Almacena el índice del botón con la respuesta correcta
                        answerButtons[i].onClick.AddListener(() => OnCorrectAnswerSelected());
                    }
                    else
                    {
                        answerButtons[i].onClick.AddListener(() => OnWrongAnswerSelected(buttonIndex));
                    }
                }

                
                 
            }

            while (countdownTimer > 0f && secondCountDownStarted != 0)
                {
                    countdownTimer -= Time.deltaTime;
                    //Debug.Log(countdownTimer);
                    //Debug.Log(secondCountDownStarted);
                    if (countdownTimer <= 8)
                    {
                        Reloj.SetActive(true);
                    }
                    yield return null;
                }

            if (countdownTimer <= 0f && secondCountDownStarted == 1)
            {
                
                countdownTimer += 8f;
                Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAFBYWUEFCIWE");

                if (J1Responde && !venganza)
                {
                    player1Pressed = false;
                    player2Pressed = true;
                    J1Responde = false;
                    venganza = true;

                    DetermineWinner();
                    Debug.Log("RESPONDE MAL EL 1");
                }
                else if (J2Responde && !venganza)
                {
                    player2Pressed = false;
                    player1Pressed = true;
                    J2Responde = false;
                    venganza = true;

                    DetermineWinner();
                    Debug.Log("RESPONDE MAL EL 2");
                }
                secondCountDownStarted = 3;

                while (countdownTimer > 0f && secondCountDownStarted != 0)
                {
                    countdownTimer -= Time.deltaTime;
                    //Debug.Log(countdownTimer);
                    Debug.Log(secondCountDownStarted);
                    if (countdownTimer <= 8)
                    {
                        Reloj.SetActive(true);
                    }
                    yield return null;
                }
            }

            if (countdownTimer <= 0 && secondCountDownStarted == 3)
            {
                Debug.Log("CEROOOOOOOOOOOOOO");
                player1Health -= 10;
                player2Health -= 10;
                dañoPaDos = true;
                Reloj.SetActive(false);
                StartCoroutine(ChangeButtonColorBack());
            }


        }
        else if (PlayerPrefs.GetString("gameStyle") == "xmateria")
        {
            // Lógica para el modo "xmateria"
        }
    }



    public List<string> ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }


    public void OnCorrectAnswerSelected()  // USAR BANDERA PARA DESHABILITAR
    {
        if (secondCountDownStarted != 0)
        {
            Reloj.SetActive(false);
            secondCountDownStarted = 0;
            ChangeButtonColor(true);

            if (J1Responde)
            {
                J2Dañado = true;
                player2Health -= 10;
                //DAÑO AL 2
                //Debug.Log("RESPONDE BIEN EL 1");
            }
            else if (J2Responde)
            {
                J1Dañado = true;
                player1Health -= 10;
                //DAÑO AL 1
                //Debug.Log("RESPONDE BIEN EL 2");
            }
            StartCoroutine(ChangeButtonColorBack());
        }
    }

    public void OnWrongAnswerSelected(int buttonIndex) //PASAR TURNOOOOOOOOOOOOOO
    {
        Reloj.SetActive(false);
        if (secondCountDownStarted == 1)
        {
            ChangeButtonColor(false, buttonIndex);
            countdownTimer = 8;
            

            if (J1Responde && !venganza)
            {
                player1Pressed = false;
                player2Pressed = true;
                J1Responde = false;
                venganza = true;

                DetermineWinner();
                //Debug.Log("RESPONDE MAL EL 1");
            }
            else if (J2Responde && !venganza)
            {
                player2Pressed = false;
                player1Pressed = true;
                J2Responde = false;
                venganza = true;

                DetermineWinner();
                //Debug.Log("RESPONDE MAL EL 2");
            }
            secondCountDownStarted = 3;
        }
        else if(secondCountDownStarted == 3)
        {
            secondCountDownStarted = 0;
            ChangeButtonColor(false, buttonIndex);

            
            if (venganza)
            {
                player1Health -= 10;
                player2Health -= 10;
                Reloj.SetActive(false);
                dañoPaDos = true;
                Debug.Log("DAÑO PA LOS DOS");
                StartCoroutine(ChangeButtonColorBack());

            }
            
        }
        
        

        
    }

    void ChangeButtonColor(bool correctAnswer, int buttonIndex = -1)
    {
        if (correctAnswer)
        {
            Color correctColor = Color.green;
            if (correctButtonIndex != -1)
            {
                Image img = answerButtons[correctButtonIndex].GetComponent<Image>();
                if (img != null)
                {
                    img.color = correctColor;
                }
            }
        }
        else
        {
            Color incorrectColor = Color.red;
            if (buttonIndex != -1)
            {
                Image img = answerButtons[buttonIndex].GetComponent<Image>();
                if (img != null)
                {
                    img.color = incorrectColor;
                }
            }
        }
    }

    IEnumerator ChangeButtonColorBack()
    {
        yield return new WaitForSeconds(1f); // Cambia esto al tiempo que desees mantener los colores

        // Restablece el color de todos los botones al color original
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image img = answerButtons[i].GetComponent<Image>();
            if (img != null)
            {
                img.color = Color.white; // Cambia esto al color original que desees para los botones
            }
        }
        Daños();
    }


    public void Daños() //AQUI VAN LAS ANIMACIONES DE LOS DAÑOS
    {
        PlayerAnimatorController[] playerControllers = FindObjectsOfType<PlayerAnimatorController>();

        if (player1Health > 0 && player2Health > 0)
        {
            

            foreach (var playerController in playerControllers)
            {
                if (dañoPaDos) // Verifica si dañoPaDos es verdadero para ejecutar la animación de daño en ambos jugadores
                {
                    playerController.StartDamageAnimation();
                    Debug.Log("ANIMACION DE DAÑO A JUGADOR");
                }
                else
                {
                    if (playerController.playerTag == "Player1" && J1Dañado)
                    {
                        playerController.StartDamageAnimation();
                        Debug.Log("ANIMACION DE DAÑO A JUGADOR 1");

                        // Encuentra al jugador 2 y comienza la animación de ataque
                        PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
                        foreach (var controller in controllers)
                        {
                            if (controller.playerTag == "Player2")
                            {
                                controller.StartAttackAnimation();
                                Debug.Log("ANIMACION DE ATAQUE A JUGADOR 2");
                                break;
                            }
                        }
                    }
                    else if (playerController.playerTag == "Player2" && J2Dañado)
                    {
                        playerController.StartDamageAnimation();
                        Debug.Log("ANIMACION DE DAÑO A JUGADOR 2");

                        // Encuentra al jugador 1 y comienza la animación de ataque
                        PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
                        foreach (var controller in controllers)
                        {
                            if (controller.playerTag == "Player1")
                            {
                                controller.StartAttackAnimation();
                                Debug.Log("ANIMACION DE ATAQUE A JUGADOR 1");
                                break;
                            }
                        }
                    }
                }
            }

            HeartsHUD();
            ReiniciarJuego();
        }

        else if (player1Health <= 0 && player2Health <= 0)
        {
            HeartsHUD();
            StartCoroutine(Finish());
            apuntador2.SetActive(false);
            apuntador1.SetActive(false);
            canvasWinners.gameObject.SetActive(true);
            panelEmpate.SetActive(true);

            PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
            foreach (var controller in controllers)
            {
                if (controller.playerTag == "Player1")
                {
                    controller.StartLoseAnimation();
                }
                if (controller.playerTag == "Player2")
                {
                    controller.StartLoseAnimation();
                }
            }

        }

        else
        {
            HeartsHUD();
            StartCoroutine(Finish());
            apuntador2.SetActive(false);
            apuntador1.SetActive(false);

            int indexJugador1 = PlayerPrefs.GetInt("Jugador1Index");
            int indexJugador2 = PlayerPrefs.GetInt("Jugador2Index");

            if (indexJugador1 >= 0 && indexJugador1 < GameManager.Instance.personajes.Count &&
                indexJugador2 >= 0 && indexJugador2 < GameManager.Instance.personajes.Count &&
                spawnPoints.Length >= 2)
            {
                if (player1Health <= 0) // Gana P2
                {
                    Debug.Log("GANA JUGADOR 2");
                    canvasWinners.gameObject.SetActive(true);
                    panelP2Winner.SetActive(true);

                    PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
                    foreach (var controller in controllers)
                    {
                        if (controller.playerTag == "Player2")
                        {
                            controller.StartVictoryAnimation();
                        }
                        else if (controller.playerTag == "Player1")
                        {
                            controller.StartLoseAnimation();
                        }
                    }
                }
                else if (player2Health <= 0) // Gana P1
                {
                    Debug.Log("GANA JUGADOR 1");
                    canvasWinners.gameObject.SetActive(true);
                    panelP1Winner.SetActive(true);
                    canvasMaster.gameObject.SetActive(false);

                    PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
                    foreach (var controller in controllers)
                    {
                        if (controller.playerTag == "Player1")
                        {
                            controller.StartVictoryAnimation();
                        }
                        else if (controller.playerTag == "Player2")
                        {
                            controller.StartLoseAnimation();
                        }
                    }
                }
                
            }
        }

    }

    void DisableAnswerButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = false;
        }
    }

    void EnableAnswerButtons()
    {
        foreach (Button button in answerButtons)
        {
            button.interactable = true;
        }
    }

    void ReiniciarJuego()
    {


        apuntador1.SetActive(false);
        apuntador2.SetActive(false);
        Reloj.SetActive(false);

        panelQuestion.SetActive(false);
        player1Pressed = false;
        player2Pressed = false;

        J1Responde = false;
        J2Responde = false;
        venganza = false;
        secondCountDownStarted = 1;

        J1Dañado = false;
        J2Dañado = false;
        dañoPaDos = false;

        Debug.Log("Vida del jugador 1: " + player1Health);
        Debug.Log("Vida del jugador 2: " + player2Health);

        // Llamar a la función que maneja el ciclo del juego desde el principio
        StartCoroutine(ShowNextQuestion());
    }

    IEnumerator ShowNextQuestion()
    {
        // Espera 2 segundos antes de mostrar la siguiente pregunta
        yield return new WaitForSeconds(2f);

        // Limpia los eventos de los botones
        ClearButtonEvents();

        // Muestra la siguiente pregunta
        yield return StartCoroutine(Countdown());
    }

    void ClearButtonEvents()
    {
        foreach (Button button in answerButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void HeartsHUD()
    {
        if (J1Dañado && !dañoPaDos)
        {
            int i = arrindex1;
            do
            {
                J1Dañado = false;
                P1Hearts[i].SetActive(false);
                arrindex1--;
            }
            while (J1Dañado);
        }
        else if (J2Dañado && !dañoPaDos)
        {
            int j = arrindex2;
            do
            {
                J2Dañado = false;
                P2Hearts[j].SetActive(false);
                arrindex2--;
            }
            while (J2Dañado);
        }
        else if (dañoPaDos)
        {
            int i = arrindex1;
            int j = arrindex2;
            do
            {


                P2Hearts[j].SetActive(false);

                arrindex2--;

                P1Hearts[i].SetActive(false);

                arrindex1--;

                dañoPaDos = false;
            }
            while (dañoPaDos);
        }



    }


    public void Salir()
    {
        SceneManager.LoadScene(1);
    }

}
