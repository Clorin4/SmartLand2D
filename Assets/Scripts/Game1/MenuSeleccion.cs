using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
//using System.Collections.Generic;

public class MenuSeleccion : MonoBehaviour
{
    public GameObject P2Items;
    public GameObject stylesSP;
    public GameObject stylesMP;

    public GameObject[] buttons;

    public GameObject canvasPersonajes;
    public GameObject canvasDificultad;
    public GameObject canvasGameModes;

    public RawImage introVideoRawImage;
    public VideoPlayer videoPlayer;
    public VideoClip[] videos; // Array para almacenar los videos de las combinaciones de personajes.

    private Dictionary<string, VideoClip> videoClipsDict = new Dictionary<string, VideoClip>();


    [SerializeField] private Image[] imagenes; // Arreglo de imágenes para los personajes de los jugadores.
    [SerializeField] private TextMeshProUGUI[] nombres; // Arreglo de TextMeshProUGUI para los nombres de los personajes.

    private GameManager gameManager;
    private int[] index; // Arreglo de índices de selección para los dos jugadores.

    public GameObject quitButton;
    bool ola = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt("GameMode") == 1)
        {
            P2Items.SetActive(false);
        }

        gameManager = GameManager.Instance;
        
        canvasPersonajes.SetActive(true);
        canvasDificultad.SetActive(false);
        canvasGameModes.SetActive(false);
        introVideoRawImage.gameObject.SetActive(false);

        index = new int[2]; // Inicializar arreglo de índices para los dos jugadores.
        index[0] = PlayerPrefs.GetInt("Jugador1Index"); // Obtener índice del jugador 1 desde PlayerPrefs.
        index[1] = PlayerPrefs.GetInt("Jugador2Index"); // Obtener índice del jugador 2 desde PlayerPrefs.

        // Verificar si los índices están dentro del rango de personajes disponibles.
        for (int i = 0; i < 2; i++)
        {
            if (index[i] > gameManager.personajes.Count - 1 || index[i] < 0)
            {
                index[i] = 0; // Si el índice está fuera de rango, establecer el primer personaje por defecto.
            }
            CambiarPantalla(i);
        }


        // Asegúrate de que los tamaños del array 'introVideo' y 'videos' coincidan
        if (introVideoRawImage == null || videoPlayer == null || videos.Length != 5) // Ajusta la longitud según sea necesario
        {
            Debug.LogError("RawImage o VideoPlayer no asignados, o la cantidad de VideoClips no coincide.");
            return;
        }

        // Llena el diccionario con las combinaciones de personajes y sus respectivos videos.
        videoClipsDict.Add("Comb0-0", videos[0]);
        videoClipsDict.Add("Comb0-1", videos[0]);
        videoClipsDict.Add("Comb0-2", videos[0]);
        videoClipsDict.Add("Comb1-0", videos[0]);
        videoClipsDict.Add("Comb1-1", videos[1]);
        videoClipsDict.Add("Comb1-2", videos[2]);
        videoClipsDict.Add("Comb2-0", videos[0]);
        videoClipsDict.Add("Comb2-1", videos[3]);
        videoClipsDict.Add("Comb2-2", videos[4]);

        // Agrega el resto de combinaciones
    }

    private void CambiarPantalla(int jugador)
    {
        PlayerPrefs.SetInt("Jugador" + (jugador + 1) + "Index", index[jugador]); // Guardar el índice del jugador.

        imagenes[jugador].sprite = gameManager.personajes[index[jugador]].imagen;
        nombres[jugador].text = gameManager.personajes[index[jugador]].nombre;
    }

    public void SigPersonaje(int jugador)
    {
        index[jugador] = (index[jugador] + 1) % gameManager.personajes.Count; // Cambiar al siguiente personaje circularmente.
        CambiarPantalla(jugador);
        PlayerPrefs.SetInt("Jugador" + (jugador + 1) + "Index", index[jugador]); // Guardar el índice del jugador.
    }

    public void AnteriorPersonaje(int jugador)
    {
        index[jugador] = (index[jugador] - 1 + gameManager.personajes.Count) % gameManager.personajes.Count; // Cambiar al anterior personaje circularmente.
        CambiarPantalla(jugador);
        PlayerPrefs.SetInt("Jugador" + (jugador + 1) + "Index", index[jugador]); // Guardar el índice del jugador.
    }

    public void ChangeNextCanvas()
    {

        if(!ola)
        {
            ola = true;
            canvasPersonajes.SetActive(false);
            canvasDificultad.SetActive(true);

            if(PlayerPrefs.GetInt("gameIndex") != 1)
            {
                buttons[0].SetActive(false);
                buttons[1].SetActive(true);
            }

        }
        else
        {
            ola = false;
            canvasPersonajes.SetActive(false);
            canvasDificultad.SetActive(false);
            canvasGameModes.SetActive(true);
            
            if (PlayerPrefs.GetInt("GameMode") == 2)
            {
                stylesMP.SetActive(true);
                stylesSP.SetActive(false);
            }
            else
            {
                stylesMP.SetActive(false);
                stylesSP.SetActive(true);
            }
        }

    }

    


    public void ChangeLastCanvas()
    {
        if (ola)
        {
            ola = false;
            canvasPersonajes.SetActive(true);
            canvasDificultad.SetActive(false);
        }
        else
        {
            ola = true;
            canvasPersonajes.SetActive(false);
            canvasDificultad.SetActive(true);
            canvasGameModes.SetActive(false);

            if (PlayerPrefs.GetInt("GameMode") == 2)
            {
                stylesMP.SetActive(true);
                stylesSP.SetActive(false);
            }
            else
            {
                stylesMP.SetActive(false);
                stylesSP.SetActive(true);
            }
        }

        
    }

    public void SelectDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("SelectedDifficulty", difficulty);


        Debug.Log(difficulty);
    }

    public void SelectGameStyle(string gameStyle)
    {
        PlayerPrefs.SetString("gameStyle", gameStyle);

        Debug.Log(gameStyle);
    }


    public string DetermineCombination()
    {
        int indexJugador1 = PlayerPrefs.GetInt("Jugador1Index");
        int indexJugador2 = PlayerPrefs.GetInt("Jugador2Index");

        string combinationIdentifier = "Comb" + indexJugador1 + "-" + indexJugador2;
        return combinationIdentifier;
    }

    public void StartGame()
    {
        quitButton.SetActive(false);
        canvasPersonajes.SetActive(false);
        canvasDificultad.SetActive(false);
        canvasGameModes.SetActive(false);
        introVideoRawImage.gameObject.SetActive(true);
        StartCoroutine(PlayIntroAndStartGame());
    }

    IEnumerator PlayIntroAndStartGame()
    {
        // Obtén la combinación de personajes.
        string combination = DetermineCombination();

        // Obtén el video correspondiente a la combinación.
        VideoClip video = null;
        if (videoClipsDict.ContainsKey(combination))
        {
            video = videoClipsDict[combination];
        }
        else
        {
            Debug.LogWarning("No se encontró un video para la combinación de personajes.");
            yield break;
        }

        // Prepara el VideoPlayer para reproducir el video correspondiente.
        videoPlayer.clip = video;
        videoPlayer.Prepare();

        // Espera hasta que el VideoPlayer esté preparado.
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        // Muestra el RawImage con el video del VideoPlayer.
        introVideoRawImage.texture = videoPlayer.texture;
        videoPlayer.Play();

        // Espera hasta que termine de reproducirse el video.
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Después de la presentación, desactiva el RawImage y carga la siguiente escena.
        introVideoRawImage.texture = null;
        canvasPersonajes.SetActive(false);
        canvasDificultad.SetActive(true);

        LoadGameScene();
    }

    void LoadGameScene()
    {
        int gameChoice = PlayerPrefs.GetInt("gameIndex");
        int gameMode = PlayerPrefs.GetInt("GameMode");

        // Redirigir al juego específico según la elección del jugador
        switch (gameChoice)
        {
            case 1:
                if(gameMode == 1)
                {
                    SceneManager.LoadScene(7);
                }
                else
                    SceneManager.LoadScene(3);
                break;
            case 2:
                SceneManager.LoadScene(4);
                break;
            case 3:
                SceneManager.LoadScene(5);
                break;
            case 4:
                SceneManager.LoadScene(6);
                break;
            // Otros casos según sea necesario para más minijuegos
            default:
                Debug.LogWarning("??????????");
                break;
        }
    }

    public void Salir()
    {
        SceneManager.LoadScene(1);
    }
}
