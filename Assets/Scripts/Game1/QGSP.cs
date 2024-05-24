using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class QGSP : MonoBehaviour
{

    #region  VARIABLEEEES

    public NewBehaviourScript questionData; // Referencia al nuevo script de dificultad



    public Transform[] spawnPoints;

    public GameObject[] P1Hearts = new GameObject[5];
    
    public int arrindex1 = 4;
    
    

    public SpriteRenderer sprite3Renderer;
    public SpriteRenderer sprite2Renderer;
    public SpriteRenderer sprite1Renderer;
    public SpriteRenderer spriteAdelanteRenderer;
    public SpriteRenderer spriteFinishRenderer;

    public GameObject apuntador1;
    private int correctAnsCount;
    public TextMeshProUGUI txtCAC;

    public GameObject Reloj;

    public GameObject panelQuestion; // El panel que contiene la pregunta y los botones
    public float panelScaleDuration = 1.0f;

    public Canvas canvasMaster;
    public Canvas canvasWinners;
    public GameObject panelP1Winner;
    

    public Canvas howToPlay;

    public bool J1Responde;
    private bool J1Da�ado;
    private bool disableButtons = false;

    //private bool countDownStarted;
    private bool secondCountDownStarted;

    

    //public QuestionManager questionManager;
    public Question currentQuestion;
    private List<Question> questions; // Lista de preguntas
    private int currentMateriaIndex = 0; // �ndice de la materia actual
    public int questionsPerMateria = 1; // Cantidad de preguntas por materia
    private List<Question> currentMateriaQuestions; // Preguntas de la materia actual
    private int currentQuestionIndex = 0; // �ndice de la pregunta actual
    private List<Materia> materiasList; // Lista de materias


    private List<string> displayedAnswers;
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    //int correctButtonIndex = -1;

    public int player1Health = 50;
    


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
        StartCoroutine(Countdown());
    }

    public void TurnOffVariables()
    {
        Reloj.SetActive(false);
        panelQuestion.SetActive(false);
        canvasWinners.gameObject.SetActive(false);
        panelP1Winner.SetActive(false);
        

        for (int i = 0; i < 5; i++)
        {
            P1Hearts[i].SetActive(true);
            
        }

        apuntador1.SetActive(false);
        

        sprite3Renderer.gameObject.SetActive(false);
        sprite2Renderer.gameObject.SetActive(false);
        sprite1Renderer.gameObject.SetActive(false);
        spriteAdelanteRenderer.gameObject.SetActive(false);
        spriteFinishRenderer.gameObject.SetActive(false);

        J1Responde = false;
        J1Da�ado = false;

        correctAnsCount = 0;
        txtCAC.text = "Respuestas correctas: " + correctAnsCount;

    }

    public void SaberDificultad()
    {
        string selectedDifficulty = PlayerPrefs.GetString("SelectedDifficulty");
        DifficultyLeveln selectedLevel = questionData.difficultyLevels.Find(level => level.name == selectedDifficulty);

        if (selectedLevel != null)
        {
            questions = new List<Question>();
            materiasList = selectedLevel.materias;

            if (PlayerPrefs.GetString("gameStyle") == "fases" && materiasList.Count > 0)
            {
                currentMateriaIndex = 0;
                currentMateriaQuestions = new List<Question>(materiasList[currentMateriaIndex].preguntas);
                currentMateriaQuestions = ShuffleList(currentMateriaQuestions);
            }
            else
            {
                foreach (Materia materia in materiasList)
                {
                    questions.AddRange(materia.preguntas);
                }
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
        yield return ScaleSpriteTo(sprite3Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tama�o espec�fico
        sprite3Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite2Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite2Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tama�o espec�fico
        sprite2Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        sprite1Renderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(sprite1Renderer, Vector3.zero, Vector3.one * 1f, .9f); // Escalar de 0 a un tama�o espec�fico
        sprite1Renderer.gameObject.SetActive(false);

        yield return new WaitForSeconds(.1f);

        spriteAdelanteRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteAdelanteRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tama�o espec�fico
        spriteAdelanteRenderer.gameObject.SetActive(false);

        // L�gica para iniciar el juego despu�s de la cuenta regresiva
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
        StartCoroutine(ShowQuestionPanel());
        
        StartCoroutine(DetectKeyPress());
    }

    IEnumerator DetectKeyPress()
    {
        yield return new WaitForSeconds(4f);

        Reloj.SetActive(true);
        J1Responde = true;
        EnableAnswerButtons();
    }

    IEnumerator Finish()
    {
        spriteFinishRenderer.gameObject.SetActive(true);
        yield return ScaleSpriteTo(spriteFinishRenderer, Vector3.zero, Vector3.one * .7f, .9f); // Escalar de 0 a un tama�o espec�fico
        
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
            secondCountDownStarted = false;
            float countdownTimer = 12f;

            if (countdownTimer > 0f && !secondCountDownStarted)
            {
                // Obtener una pregunta aleatoria de la lista
                int randomIndex = Random.Range(0, questions.Count);
                currentQuestion = questions[randomIndex];

                // Mostrar la pregunta en el TextMeshPro
                questionText.text = currentQuestion.questionText;

                List<string> answers = new List<string>(currentQuestion.options);
                List<string> displayedAnswers = new List<string>();

                // A�adir la respuesta correcta a las respuestas mostradas
                string correctAnswer = answers[currentQuestion.correctAnswerIndex];
                displayedAnswers.Add(correctAnswer);
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

                // Asignar las respuestas a los botones y a�adir listeners
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayedAnswers[i];
                    answerButtons[i].onClick.RemoveAllListeners();

                    // Verificar si la respuesta del bot�n es la respuesta correcta
                    if (displayedAnswers[i] == correctAnswer)
                    {
                        answerButtons[i].onClick.AddListener(() => OnCorrectAnswerSelected());
                    }
                    else
                    {
                        answerButtons[i].onClick.AddListener(() => OnWrongAnswerSelected());
                    }
                }

                while (countdownTimer > 0f && !secondCountDownStarted)
                {
                    countdownTimer -= Time.deltaTime;
                    yield return null;
                }

                if (countdownTimer <= 0f)
                {
                    player1Health -= 5;
                    Reloj.SetActive(false);
                    Da�os();
                }
            }
        }
        else if (PlayerPrefs.GetString("gameStyle") == "fases")
        {
            
                if (currentMateriaQuestions == null || currentMateriaQuestions.Count == 0)
                {
                    Debug.LogError("No hay preguntas disponibles para la materia actual.");
                    yield break;
                }

            if (currentQuestionIndex < questionsPerMateria && currentQuestionIndex < currentMateriaQuestions.Count)
            {
                // Obtener una pregunta aleatoria de la lista actual
                int randomIndex = Random.Range(0, currentMateriaQuestions.Count);
                currentQuestion = currentMateriaQuestions[randomIndex];
                currentMateriaQuestions.RemoveAt(randomIndex); // Eliminar la pregunta seleccionada para evitar repetici�n
                currentQuestionIndex++;

                // Mostrar la pregunta en el TextMeshPro
                questionText.text = currentQuestion.questionText;

                List<string> answers = new List<string>(currentQuestion.options);
                List<string> displayedAnswers = new List<string>();

                // A�adir la respuesta correcta a las respuestas mostradas
                string correctAnswer = answers[currentQuestion.correctAnswerIndex];
                displayedAnswers.Add(correctAnswer);
                answers.RemoveAt(currentQuestion.correctAnswerIndex);

                // Mostrar las respuestas incorrectas en los botones restantes
                for (int j = 0; j < answerButtons.Length - 1; j++)
                {
                    int randomAnswerIndex = Random.Range(0, answers.Count);
                    displayedAnswers.Add(answers[randomAnswerIndex]);
                    answers.RemoveAt(randomAnswerIndex);
                }

                // Mezclar las respuestas mostradas
                displayedAnswers = ShuffleList(displayedAnswers);

                // Asignar las respuestas a los botones y a�adir listeners
                for (int i = 0; i < answerButtons.Length; i++)
                {
                    int index = i; // Se necesita una variable local para mantener el valor correcto de 'i' en el cierre
                    answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = displayedAnswers[i];
                    answerButtons[i].onClick.RemoveAllListeners(); // Limpiar listeners previos
                                                                   // Verificar si la respuesta del bot�n es la respuesta correcta
                    if (displayedAnswers[index] == correctAnswer)
                    {
                        answerButtons[i].onClick.AddListener(() => OnCorrectAnswerSelected());
                    }
                    else
                    {
                        answerButtons[i].onClick.AddListener(() => OnWrongAnswerSelected());
                    }
                }
            }
            else
            {
                // L�gica para cuando se terminan las preguntas de la materia actual
                // Avanzar a la siguiente materia o finalizar el modo "fases"
                currentMateriaIndex++;
                Debug.Log(currentMateriaIndex);
                if (currentMateriaIndex < materiasList.Count)
                {
                    // Reiniciar las preguntas de la nueva materia
                    currentMateriaQuestions = new List<Question>(materiasList[currentMateriaIndex].preguntas);
                    currentQuestionIndex = 0;
                    StartCoroutine(ShowQuestionAndAnswers());
                }
                else
                {
                    Debug.Log("Todas las materias han sido completadas.");
                    panelQuestion.SetActive(false);
                    Reloj.SetActive(false);
                    //StartCoroutine(Finish());
                    // L�gica para finalizar el modo "fases"
                }
            }
        }
    }



    public List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    public void OnCorrectAnswerSelected()
    {
        if (!disableButtons)
        {
            disableButtons = true;
            Reloj.SetActive(false);
            secondCountDownStarted = true;
            ChangeButtonColor(true);

            if (J1Responde)
            {
                correctAnsCount += 1;
                txtCAC.text = "Respuestas correctas: " + correctAnsCount;
                Debug.Log("RESPONDE BIEN EL 1");
            }
        }
    }

    public void OnWrongAnswerSelected() 
    {
        if (!disableButtons)
        {
            disableButtons = true;
            secondCountDownStarted = true;
            ChangeButtonColor(false);
            Reloj.SetActive(false);

            if (J1Responde)
            {
                J1Responde = false;
                player1Health -= 10;
                J1Da�ado = true;

                Debug.Log("RESPONDE MALL EL 1");
            }
        }
    }

    void ChangeButtonColor(bool correctAnswer)
    {
        
        Color color = correctAnswer ? Color.green : Color.red;

        // Accede a la variable miembro para verificar las respuestas mostradas
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Image img = answerButtons[i].GetComponent<Image>();
            if (img != null)
            {
                // Cambia el color del bot�n seg�n si es la respuesta correcta o incorrecta
                if (correctAnswer && answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text == currentQuestion.options[currentQuestion.correctAnswerIndex])
                {
                    img.color = Color.green; // Cambia el color del bot�n con la respuesta correcta a verde
                }
                else if (!correctAnswer && answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text != currentQuestion.options[currentQuestion.correctAnswerIndex])
                {
                    img.color = Color.red; // Cambia el color del bot�n con respuestas incorrectas a rojo
                }
            }
        }

        // Restablecer el color de los botones despu�s de un tiempo determinado
        StartCoroutine(ChangeButtonColorBack());
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
        Da�os(); // Llama a la funci�n Da�os despu�s de restablecer los colores
    }




    public void Da�os() //AQUI VAN LAS ANIMACIONES DE LOS DA�OS
    {
        PlayerAnimatorController[] playerControllers = FindObjectsOfType<PlayerAnimatorController>();

        if (player1Health > 0)
        {
            foreach (var playerController in playerControllers)
            {    
                if (playerController.playerTag == "Player1" && !J1Da�ado)
                {
                    playerController.StartAttackAnimation();
                    Debug.Log("ANIMACION DE DA�O A JUGADOR 2");

                        
                }
                else if (playerController.playerTag == "Player1" && J1Da�ado)
                {
                    playerController.StartDamageAnimation();
                    Debug.Log("ANIMACION DE DA�O A JUGADOR 1");

                    
                }
            }

            HeartsHUD();
            ReiniciarJuego();
        }

        

        else
        {
            apuntador1.SetActive(false);

            if (player1Health <= 0) // PIERDE P1
            {
                HeartsHUD();
                Debug.Log("GANA JUGADOR 2");
                canvasWinners.gameObject.SetActive(true);
                panelQuestion.SetActive(false);
                StartCoroutine(Finish());

                PlayerAnimatorController[] controllers = FindObjectsOfType<PlayerAnimatorController>();
                foreach (var controller in controllers)
                {

                    if (controller.playerTag == "Player1")
                    {
                            controller.StartLoseAnimation();
                    }
                }
            }
                /*else if (player2Health <= 0) // Gana P1  NO BORRAR, MODIFICAR
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
                }*/

            
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
        disableButtons = false;   
        Reloj.SetActive(false);

        panelQuestion.SetActive(false);
        
        

        J1Responde = false;
        

        J1Da�ado = false;
        

        Debug.Log("Vida del jugador 1: " + player1Health);
        

        // Llamar a la funci�n que maneja el ciclo del juego desde el principio
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
        
            if (J1Da�ado)
            {
                int i = arrindex1;
                do
                {
                    J1Da�ado = false;
                    P1Hearts[i].SetActive(false);
                    arrindex1--;
                }
                while (J1Da�ado);
            }
          

    }


    public void Salir()
    {
        SceneManager.LoadScene(1);
    }
}
